﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.AggregateRoots.Accounts;
using Domain.Portfolio.AggregateRoots.Asset;
using Domain.Portfolio.AggregateRoots.Liability;
using Domain.Portfolio.Base;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Entities.CostRecord;
using Domain.Portfolio.Entities.CreationModels;
using Domain.Portfolio.Entities.CreationModels.Cost;
using Domain.Portfolio.Entities.CreationModels.Income;
using Domain.Portfolio.Entities.CreationModels.Transaction;
using Domain.Portfolio.Entities.IncomeRecord;
using Domain.Portfolio.Entities.Transactions;
using Domain.Portfolio.Interfaces;
using Domain.Portfolio.Values;
using Domain.Portfolio.Values.ManagedInvestmentValues;
using Domain.Portfolio.Values.Ratios;
using Edis.Db;
using Edis.Db.Assets;
using Edis.Db.Enums;
using Edis.Db.ExpenseRecords;
using Edis.Db.IncomeRecords;
using Edis.Db.Liabilities;
using Edis.Db.Transactions;
using Shared;
using SqlRepository.Extensions;
using Adviser = Domain.Portfolio.AggregateRoots.Adviser;
using BondTransaction = Edis.Db.Transactions.BondTransaction;
using Client = Domain.Portfolio.AggregateRoots.Client;
using ClientGroup = Domain.Portfolio.AggregateRoots.ClientGroup;
using Equity = Edis.Db.Assets.Equity;
using EquityTransaction = Edis.Db.Transactions.EquityTransaction;
using InsuranceTransaction = Domain.Portfolio.Entities.Transactions.InsuranceTransaction;
using MarginLendingTransactionCreation = Domain.Portfolio.Entities.CreationModels.Transaction.MarginLendingTransactionCreation;
using PropertyTransaction = Edis.Db.Transactions.PropertyTransaction;
using RepaymentCreation = Domain.Portfolio.Entities.CreationModels.RepaymentCreation;
using AssetPrice = Edis.Db.Assets.AssetPrice;
using RebalanceModel = Domain.Portfolio.Rebalance.RebalanceModel;
using TemplateDetailsItemParameter = Domain.Portfolio.Rebalance.TemplateDetailsItemParameter;
using RiskProfile = Domain.Portfolio.EdisDatabase.RiskProfile;
using CashAccount = Edis.Db.Assets.CashAccount;
using Domain.Portfolio.Correspondence;
using System.Reflection;
using System.ComponentModel;
using Domain.Portfolio.CorporateActions;
using Domain.Portfolio.TransactionModels;
using Edis.Db.CorperateActions;
using Domain.Portfolio.DataFeed;

namespace SqlRepository
{
    public class EdisRepository : IRepository, IDisposable
    {
        private const double RetirementAge = 70;
        private const int MaxNumberOfRetries = 10;
        private const int MemberNumberDigits = 8;
        private readonly EdisContext _db = new EdisContext();
        private readonly Random _rdm = new Random();
        public void Dispose()
        {
            _db.Dispose();
        }

        public void CreateTestSectors()
        {
            _db.Sectors.Add(new Sector
            {
                Id = Guid.NewGuid().ToString(),
                SectorName = "Test Sector"
            });

            _db.Sectors.Add(new Sector
            {
                Id = Guid.NewGuid().ToString(),
                SectorName = "Test Sector1"
            });

            _db.Sectors.Add(new Sector
            {
                Id = Guid.NewGuid().ToString(),
                SectorName = "Test Sector2"
            });

            _db.Sectors.Add(new Sector
            {
                Id = Guid.NewGuid().ToString(),
                SectorName = "Test Sector3"
            });

            _db.Sectors.Add(new Sector
            {
                Id = Guid.NewGuid().ToString(),
                SectorName = "Test Sector4"
            });

            _db.SaveChanges();
        }



        public void AdviserMakeEquityTransactions(EquityTransactionModel model) {

            //to do front end will pass in a what ever the number is 
            //then we get this client's account number then make an equity transaction
            var account = _db.Accounts.Where(a => a.AccountId == model.Account.id).FirstOrDefault();
            var accountToMakeTrans = GetTransactionAccountByAccountId(account);
            var equity = getEquityByTicker(model.Ticker);

            accountToMakeTrans.MakeTransactionSync(new EquityTransactionCreation() {
                FeesRecords = new List<TransactionFeeRecordCreation>() {
                     new TransactionFeeRecordCreation()
                    {
                        Amount = Convert.ToDouble( model.TransactionFee),
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                },
                EquityType = equity.EquityType,
                //FeesRecords = new List<TransactionFeeRecordCreation>(),
                Name = equity.Name,
                NumberOfUnits = model.NumberOfUnits,
                Price = model.Price,
                Sector = equity.Sector,
                Ticker = equity.Ticker,
                TransactionDate = model.TransactionDate,
                LoanAmount = model.LoanAmount
            });

            MakeCashTransactions(account.AccountNumber, - (model.NumberOfUnits * model.Price - model.LoanAmount));
            _db.SaveChanges();     
        }


        private AccountBase GetTransactionAccountByAccountId(Account account) {

            var checkAccount = CheckAccountIsAGroupAccountOrAClientAccount(account);
            AccountBase accountToMakeTrans = null;
            if (checkAccount == AccountCatergories.ClientAccount)
            {
                accountToMakeTrans = GetGroupAccountById(account.AccountId);
            }

            if (checkAccount == AccountCatergories.GroupAccount)
            {
                accountToMakeTrans = GetClientAccountById(account.AccountId);
            }
            return accountToMakeTrans;
        }

        public void AdviserMakeBondsTransactions(EquityTransactionModel model)
        {
            var account = _db.Accounts.Where(a => a.AccountId == model.Account.id).FirstOrDefault();
            var accountToMakeTrans = GetTransactionAccountByAccountId(account);
            var bond = GetBondByTicker(model.Ticker);
            accountToMakeTrans.MakeTransactionSync(new BondTransactionCreation() {
                BondName = bond.Name,
                Ticker = bond.Ticker,
                Frequency = bond.Frequency,
                BondType = bond.BondType,
                Issuer = bond.Issuer,
                NumberOfUnits = model.NumberOfUnits,
                TransactionDate = model.TransactionDate,
                UnitPrice = model.Price,
                TransactionFeeRecords = new List<TransactionFeeRecordCreation>() {
                     new TransactionFeeRecordCreation()
                    {
                        Amount = Convert.ToDouble( model.TransactionFee),
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                },
            });
            MakeCashTransactions(account.AccountNumber, -(model.NumberOfUnits * model.Price - model.LoanAmount));
            _db.SaveChanges();
        }

        private Bond GetBondByTicker(string Ticker) {
           return _db.Bonds.Where(b => b.Ticker == Ticker).FirstOrDefault();
        }

        public void insertData3()
        {
            var account = GetClientAccountSync("04263398", DateTime.Now);
            account.MakeTransactionSync(new InsuranceTransactionCreation()
            {
                AmountInsured = 20000,//
                EntitiesInsured = "123",//
                ExpiryDate = DateTime.Now.AddDays(30),//
                GrantedOn = DateTime.Now,//
                InsuranceType = InsuranceType.AssetInsurance,//
                NameOfPolicy = PolicyType.Car.ToString(),//
                PolicyType = PolicyType.Car,//
                Premium = 28000,//
                IsAcquire = true,
                Issuer = "Steven",//
                PolicyAddress = "517 flinders lane, melbourne, vic",//
                PolicyNumber = "0193"//
            });
            account.MakeTransactionSync(new InsuranceTransactionCreation()
            {
                AmountInsured = 23000,
                EntitiesInsured = "123",
                ExpiryDate = DateTime.Now.AddDays(30),
                GrantedOn = DateTime.Now,
                InsuranceType = InsuranceType.PersoanlInsurance,
                NameOfPolicy = PolicyType.Accident.ToString(),
                PolicyType = PolicyType.Accident,
                Premium = 54000,
                IsAcquire = true,
                Issuer = "Steven",
                PolicyAddress = "517 flinders lane, melbourne, vic",
                PolicyNumber = "0193"
            });
            account.MakeTransactionSync(new InsuranceTransactionCreation()
            {
                AmountInsured = 10000,
                EntitiesInsured = "123",
                ExpiryDate = DateTime.Now.AddDays(30),
                GrantedOn = DateTime.Now,
                InsuranceType = InsuranceType.AssetInsurance,
                NameOfPolicy = PolicyType.Boat.ToString(),
                PolicyType = PolicyType.Boat,
                Premium = 18000,
                IsAcquire = true,
                Issuer = "Steven",
                PolicyAddress = "517 flinders lane, melbourne, vic",
                PolicyNumber = "0193"
            });
            account.MakeTransactionSync(new InsuranceTransactionCreation()
            {
                AmountInsured = 80000,
                EntitiesInsured = "123",
                ExpiryDate = DateTime.Now.AddDays(30),
                GrantedOn = DateTime.Now,
                InsuranceType = InsuranceType.AssetInsurance,
                NameOfPolicy = PolicyType.Building.ToString(),
                PolicyType = PolicyType.Building,
                Premium = 98000,
                IsAcquire = true,
                Issuer = "Steven",
                PolicyAddress = "517 flinders lane, melbourne, vic",
                PolicyNumber = "0193"
            });
            account.MakeTransactionSync(new InsuranceTransactionCreation()
            {
                AmountInsured = 10000,
                EntitiesInsured = "123",
                ExpiryDate = DateTime.Now.AddDays(30),
                GrantedOn = DateTime.Now,
                InsuranceType = InsuranceType.MiscellaneousInsurance,
                NameOfPolicy = PolicyType.RentalIncome.ToString(),
                PolicyType = PolicyType.RentalIncome,
                Premium = 10099,
                IsAcquire = true,
                Issuer = "Steven",
                PolicyAddress = "517 flinders lane, melbourne, vic",
                PolicyNumber = "0193"
            });

            account.MakeTransactionSync(new PropertyTransactionCreation()
            {
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 888,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                },
                FullAddress = "292 Waverley Road, Malvern East, 3145",
                IsBuy = true,
                Price = 980000,
                PropertyType = "Office",
                TransactionDate = new DateTime(2015, 1, 1, 12, 32, 30),
            });
            account.MakeTransactionSync(new PropertyTransactionCreation()
            {
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 777,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                },
                FullAddress = "5 inga street, oakleigh east, 3166",
                IsBuy = true,
                Price = 580000,
                PropertyType = "Home",
                TransactionDate = new DateTime(2015, 12, 1, 12, 32, 30),
            });
            //...................
            account.MakeTransactionSync(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.InternationalEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>(),
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 320,
                Sector = "Test Sector1",
                Ticker = "ONT",
                TransactionDate = new DateTime(2015, 6, 1, 12, 32, 30),
            });

            account.MakeTransactionSync(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.InternationalEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>(),
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 120,
                Sector = "Test Sector2",
                Ticker = "ONT",
                TransactionDate = new DateTime(2015, 2, 1, 12, 32, 30),
            });

            account.MakeTransactionSync(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.ManagedInvestments,
                FeesRecords = new List<TransactionFeeRecordCreation>(),
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 220,
                Sector = "Test Sector3",
                Ticker = "ONT",
                TransactionDate = new DateTime(2015, 3, 1, 12, 32, 30),
            });


            account.MakeTransactionSync(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.ManagedInvestments,
                FeesRecords = new List<TransactionFeeRecordCreation>(),
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 320,
                Sector = "Test Sector4",
                Ticker = "AYC",
                TransactionDate = new DateTime(2015, 3, 1, 12, 32, 30),
            });

            account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            {
                Amount = 150000,
                AnnualInterestSoFar = 0,
                Bsb = "123456",
                CashAccountName = "Account name",
                CashAccountNumber = "Number",
                CashAccountType = CashAccountType.TermDeposit,
                CurrencyType = CurrencyType.AustralianDollar,
                Frequency = Frequency.Annually,
                InterestRate = 0.9,
                MaturityDate = DateTime.Now.AddYears(1),
                TermsInMonths = 20,
                TransactionDate = new DateTime(2015, 11, 1, 12, 32, 30),
                TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 8909,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                }

            });



            account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            {
                Amount = 150000,
                AnnualInterestSoFar = 0,
                Bsb = "123456",
                CashAccountName = "Account name",
                CashAccountNumber = "Number",
                CashAccountType = CashAccountType.TermDeposit,
                CurrencyType = CurrencyType.AustralianDollar,
                Frequency = Frequency.Annually,
                InterestRate = 0.9,
                MaturityDate = DateTime.Now.AddYears(1),
                TermsInMonths = 20,
                TransactionDate = new DateTime(2015, 6, 1, 12, 32, 30),
                TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 1231,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                }

            });

            account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            {
                Amount = 150000,
                AnnualInterestSoFar = 0,
                Bsb = "123456",
                CashAccountName = "Account name",
                CashAccountNumber = "Number",
                CashAccountType = CashAccountType.TermDeposit,
                CurrencyType = CurrencyType.AustralianDollar,
                Frequency = Frequency.Annually,
                InterestRate = 0.9,
                MaturityDate = DateTime.Now.AddYears(1),
                TermsInMonths = 20,
                TransactionDate = new DateTime(2015, 5, 1, 12, 32, 30),
                TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 5675,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                }

            });

            account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            {
                Amount = 150000,
                AnnualInterestSoFar = 0,
                Bsb = "123456",
                CashAccountName = "Account name",
                CashAccountNumber = "Number",
                CashAccountType = CashAccountType.TermDeposit,
                CurrencyType = CurrencyType.AustralianDollar,
                Frequency = Frequency.Annually,
                InterestRate = 0.9,
                MaturityDate = DateTime.Now.AddYears(1),
                TermsInMonths = 20,
                TransactionDate = new DateTime(2015, 4, 1, 12, 32, 30),
                TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 4564,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                }

            });

            account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            {
                Amount = 150000,
                AnnualInterestSoFar = 0,
                Bsb = "123456",
                CashAccountName = "Account name",
                CashAccountNumber = "Number",
                CashAccountType = CashAccountType.TermDeposit,
                CurrencyType = CurrencyType.AustralianDollar,
                Frequency = Frequency.Annually,
                InterestRate = 0.9,
                MaturityDate = DateTime.Now.AddYears(1),
                TermsInMonths = 20,
                TransactionDate = new DateTime(2015, 3, 1, 12, 32, 30),
                TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 3453,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                }

            });

            account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            {
                Amount = 150000,
                AnnualInterestSoFar = 0,
                Bsb = "123456",
                CashAccountName = "Account name",
                CashAccountNumber = "Number",
                CashAccountType = CashAccountType.TermDeposit,
                CurrencyType = CurrencyType.AustralianDollar,
                Frequency = Frequency.Annually,
                InterestRate = 0.9,
                MaturityDate = DateTime.Now.AddYears(1),
                TermsInMonths = 20,
                TransactionDate = new DateTime(2015, 2, 1, 12, 32, 30),
                TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 2342,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                }

            });

            account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            {
                Amount = 150000,
                AnnualInterestSoFar = 0,
                Bsb = "123456",
                CashAccountName = "Account name",
                CashAccountNumber = "Number",
                CashAccountType = CashAccountType.TermDeposit,
                CurrencyType = CurrencyType.AustralianDollar,
                Frequency = Frequency.Annually,
                InterestRate = 0.9,
                MaturityDate = DateTime.Now.AddYears(1),
                TermsInMonths = 20,
                TransactionDate = new DateTime(2015, 1, 1, 12, 32, 30),
                TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 2312,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                }

            });
        }

        public void insertData2()
        {

            var account = GetClientGroupAccountSync("65593420", DateTime.Now);

            account.RecordIncomeSync(new DividendPaymentCreation()
            {
                Amount = 2000,
                Franking = 100,
                PaymentOn = DateTime.Now,
                Ticker = ((AustralianEquity)account.GetAssetsSync()[0]).Ticker
            });
            account.RecordIncomeSync(new InterestPaymentCreation()
            {
                Amount = 6000,
                PaymentOn = DateTime.Now,
                CashAccountId = "b54c7d6a-b9d8-4528-aef1-8a90f01165d3"
            });
            account.RecordIncomeSync(new InterestPaymentCreation()
            {
                Amount = 1000,
                PaymentOn = DateTime.Now,
                CashAccountId = "b54c7d6a-b9d8-4528-aef1-8a90f01165d3"
            });
            account.RecordIncomeSync(new RentalPaymentCreation()
            {
                Amount = 6000,
                PaymentOn = DateTime.Now,
                PropertyId = "0fd8e41f-d599-4a7a-848a-fbf4531e60fd"
            });
            account.RecordIncomeSync(new RentalPaymentCreation()
            {
                Amount = 2000,
                PaymentOn = DateTime.Now,
                PropertyId = "0fd8e41f-d599-4a7a-848a-fbf4531e60fd"
            });
            account.MakeTransactionSync(new HomeLoanTransactionCreation()
            {
                ExpiryDate = DateTime.Now.AddDays(30),
                GrantedOn = DateTime.Now,
                Institution = "Monash",
                LoanAmount = 3000,
                LoanRate = 0.5,
                LoanRepaymentType = LoanRepaymentType.DirectDebt,
                PropertyId = "0fd8e41f-d599-4a7a-848a-fbf4531e60fd",
                TypeOfMortgageRates = TypeOfMortgageRates.Combination,
                IsAcquire = true
            });
            account.MakeTransactionSync(new HomeLoanTransactionCreation()
            {
                ExpiryDate = DateTime.Now.AddDays(60),
                GrantedOn = DateTime.Now,
                Institution = "Grand Canal",
                LoanAmount = 5000,
                LoanRate = 0.3,
                LoanRepaymentType = LoanRepaymentType.NotSpecified,
                PropertyId = "0fd8e41f-d599-4a7a-848a-fbf4531e60fd",
                TypeOfMortgageRates = TypeOfMortgageRates.Fixed,
                IsAcquire = true
            });
            account.RecordIncomeSync(new InterestPaymentCreation()
            {
                Amount = 4000,
                CashAccountId = "b54c7d6a-b9d8-4528-aef1-8a90f01165d3",
                PaymentOn = DateTime.Now.AddDays(-20)
            });
            account.RecordIncomeSync(new InterestPaymentCreation()
            {
                Amount = 1000,
                CashAccountId = "b54c7d6a-b9d8-4528-aef1-8a90f01165d3",
                PaymentOn = DateTime.Now.AddDays(-10)
            });

        }

        public void insertData1()
        {
            var account = GetClientGroupAccountSync("65593420", DateTime.Now);

            account.RecordIncomeSync(new DividendPaymentCreation()
            {
                Amount = 2000,
                Franking = 100,
                PaymentOn = DateTime.Now,
                Ticker = ((AustralianEquity)account.GetAssets()[0]).Ticker
            });


        }


        public void insertTestingData()
        {
            var account = GetClientGroupAccountSync("65593420", DateTime.Now);

            //account.MakeTransactionSync(new PropertyTransactionCreation()
            //{
            //    FeesRecords = new List<TransactionFeeRecordCreation>()
            //    {
            //        new TransactionFeeRecordCreation()
            //        {
            //            Amount = 888,
            //            TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
            //        }
            //    },
            //    FullAddress = "292 Waverley Road, Malvern East, 3145",
            //    IsBuy = true,
            //    Price = 980000,
            //    PropertyType = "Office",
            //    TransactionDate = new DateTime(2015, 1, 1, 12, 32, 30),
            //});
            //account.MakeTransactionSync(new PropertyTransactionCreation()
            //{
            //    FeesRecords = new List<TransactionFeeRecordCreation>()
            //    {
            //        new TransactionFeeRecordCreation()
            //        {
            //            Amount = 777,
            //            TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
            //        }
            //    },
            //    FullAddress = "5 inga street, oakleigh east, 3166",
            //    IsBuy = true,
            //    Price = 580000,
            //    PropertyType = "Home",
            //    TransactionDate = new DateTime(2015, 12, 1, 12, 32, 30),
            //});

            //account.MakeTransactionSync(new EquityTransactionCreation()
            //{
            //    EquityType = EquityTypes.InternationalEquity,
            //    FeesRecords = new List<TransactionFeeRecordCreation>(),
            //    Name = "Test Stock",
            //    NumberOfUnits = 100,
            //    Price = 320,
            //    Sector = "Test Sector1",
            //    Ticker = "Test Ticker 01",
            //    TransactionDate = new DateTime(2015, 6, 1, 12, 32, 30),
            //});

            //account.MakeTransactionSync(new EquityTransactionCreation()
            //{
            //    EquityType = EquityTypes.InternationalEquity,
            //    FeesRecords = new List<TransactionFeeRecordCreation>(),
            //    Name = "Test Stock",
            //    NumberOfUnits = 100,
            //    Price = 120,
            //    Sector = "Test Sector2",
            //    Ticker = "Test Ticker 03",
            //    TransactionDate = new DateTime(2015, 2, 1, 12, 32, 30),
            //});

            //account.MakeTransactionSync(new EquityTransactionCreation()
            //{
            //    EquityType = EquityTypes.ManagedInvestments,
            //    FeesRecords = new List<TransactionFeeRecordCreation>(),
            //    Name = "Test Stock",
            //    NumberOfUnits = 100,
            //    Price = 220,
            //    Sector = "Test Sector3",
            //    Ticker = "Test Ticker 02",
            //    TransactionDate = new DateTime(2015, 3, 1, 12, 32, 30),
            //});


            //account.MakeTransactionSync(new EquityTransactionCreation()
            //{
            //    EquityType = EquityTypes.ManagedInvestments,
            //    FeesRecords = new List<TransactionFeeRecordCreation>(),
            //    Name = "Test Stock",
            //    NumberOfUnits = 100,
            //    Price = 320,
            //    Sector = "Test Sector4",
            //    Ticker = "Test Ticker 04",
            //    TransactionDate = new DateTime(2015, 3, 1, 12, 32, 30),
            //});

            account.MakeTransactionSync(new BondTransactionCreation()
            {
                BondName = "Test Bond3",
                BondType = "Bond Type 3",
                Frequency = Frequency.Annually,
                Issuer = "Issuer",
                NumberOfUnits = 1000,
                Ticker = "Ticker 2",
                TransactionDate = new DateTime(2015, 11, 1, 12, 32, 30),
                UnitPrice = 17.99,
                TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 100,
                        TransactionExpenseType = TransactionExpenseType.Brokerage
                    }
                }
            });

            account.MakeTransactionSync(new BondTransactionCreation()
            {
                BondName = "Test Bond4",
                BondType = "Bond Type 4",
                Frequency = Frequency.Annually,
                Issuer = "Issuer",
                NumberOfUnits = 1000,
                Ticker = "Ticker 5",
                TransactionDate = new DateTime(2015, 2, 1, 12, 32, 30),
                UnitPrice = 47.99,
                TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 300,
                        TransactionExpenseType = TransactionExpenseType.EntryFee
                    }
                }
            });


            //account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            //{
            //    Amount = 150000,
            //    AnnualInterestSoFar = 0,
            //    Bsb = "123456",
            //    CashAccountName = "Account name",
            //    CashAccountNumber = "Number",
            //    CashAccountType = CashAccountType.TermDeposit,
            //    CurrencyType = CurrencyType.AustralianDollar,
            //    Frequency = Frequency.Annually,
            //    InterestRate = 0.9,
            //    MaturityDate = DateTime.Now.AddYears(1),
            //    TermsInMonths = 20,
            //    TransactionDate = new DateTime(2015, 11, 1, 12, 32, 30),
            //    TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
            //    {
            //        new TransactionFeeRecordCreation()
            //        {
            //            Amount = 8909,
            //            TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
            //        }
            //    }

            //});



            //account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            //{
            //    Amount = 150000,
            //    AnnualInterestSoFar = 0,
            //    Bsb = "123456",
            //    CashAccountName = "Account name",
            //    CashAccountNumber = "Number",
            //    CashAccountType = CashAccountType.TermDeposit,
            //    CurrencyType = CurrencyType.AustralianDollar,
            //    Frequency = Frequency.Annually,
            //    InterestRate = 0.9,
            //    MaturityDate = DateTime.Now.AddYears(1),
            //    TermsInMonths = 20,
            //    TransactionDate = new DateTime(2015, 6, 1, 12, 32, 30),
            //    TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
            //    {
            //        new TransactionFeeRecordCreation()
            //        {
            //            Amount = 1231,
            //            TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
            //        }
            //    }

            //});

            //account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            //{
            //    Amount = 150000,
            //    AnnualInterestSoFar = 0,
            //    Bsb = "123456",
            //    CashAccountName = "Account name",
            //    CashAccountNumber = "Number",
            //    CashAccountType = CashAccountType.TermDeposit,
            //    CurrencyType = CurrencyType.AustralianDollar,
            //    Frequency = Frequency.Annually,
            //    InterestRate = 0.9,
            //    MaturityDate = DateTime.Now.AddYears(1),
            //    TermsInMonths = 20,
            //    TransactionDate = new DateTime(2015, 5, 1, 12, 32, 30),
            //    TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
            //    {
            //        new TransactionFeeRecordCreation()
            //        {
            //            Amount = 5675,
            //            TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
            //        }
            //    }

            //});

            //account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            //{
            //    Amount = 150000,
            //    AnnualInterestSoFar = 0,
            //    Bsb = "123456",
            //    CashAccountName = "Account name",
            //    CashAccountNumber = "Number",
            //    CashAccountType = CashAccountType.TermDeposit,
            //    CurrencyType = CurrencyType.AustralianDollar,
            //    Frequency = Frequency.Annually,
            //    InterestRate = 0.9,
            //    MaturityDate = DateTime.Now.AddYears(1),
            //    TermsInMonths = 20,
            //    TransactionDate = new DateTime(2015, 4, 1, 12, 32, 30),
            //    TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
            //    {
            //        new TransactionFeeRecordCreation()
            //        {
            //            Amount = 4564,
            //            TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
            //        }
            //    }

            //});

            //account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            //{
            //    Amount = 150000,
            //    AnnualInterestSoFar = 0,
            //    Bsb = "123456",
            //    CashAccountName = "Account name",
            //    CashAccountNumber = "Number",
            //    CashAccountType = CashAccountType.TermDeposit,
            //    CurrencyType = CurrencyType.AustralianDollar,
            //    Frequency = Frequency.Annually,
            //    InterestRate = 0.9,
            //    MaturityDate = DateTime.Now.AddYears(1),
            //    TermsInMonths = 20,
            //    TransactionDate = new DateTime(2015, 3, 1, 12, 32, 30),
            //    TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
            //    {
            //        new TransactionFeeRecordCreation()
            //        {
            //            Amount = 3453,
            //            TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
            //        }
            //    }

            //});

            //account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            //{
            //    Amount = 150000,
            //    AnnualInterestSoFar = 0,
            //    Bsb = "123456",
            //    CashAccountName = "Account name",
            //    CashAccountNumber = "Number",
            //    CashAccountType = CashAccountType.TermDeposit,
            //    CurrencyType = CurrencyType.AustralianDollar,
            //    Frequency = Frequency.Annually,
            //    InterestRate = 0.9,
            //    MaturityDate = DateTime.Now.AddYears(1),
            //    TermsInMonths = 20,
            //    TransactionDate = new DateTime(2015, 2, 1, 12, 32, 30),
            //    TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
            //    {
            //        new TransactionFeeRecordCreation()
            //        {
            //            Amount = 2342,
            //            TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
            //        }
            //    }

            //});

            //account.MakeTransactionSync(new CashAccountTransactionAccountCreation()
            //{
            //    Amount = 150000,
            //    AnnualInterestSoFar = 0,
            //    Bsb = "123456",
            //    CashAccountName = "Account name",
            //    CashAccountNumber = "Number",
            //    CashAccountType = CashAccountType.TermDeposit,
            //    CurrencyType = CurrencyType.AustralianDollar,
            //    Frequency = Frequency.Annually,
            //    InterestRate = 0.9,
            //    MaturityDate = DateTime.Now.AddYears(1),
            //    TermsInMonths = 20,
            //    TransactionDate = new DateTime(2015, 1, 1, 12, 32, 30),
            //    TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
            //    {
            //        new TransactionFeeRecordCreation()
            //        {
            //            Amount = 2312,
            //            TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
            //        }
            //    }

            //});


        }

        public void CreateNewTransactionsForSectors()
        {
            var groupAccount = GetClientGroupAccountSync("65593420", DateTime.Now);

            groupAccount.MakeTransactionSync(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 589,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                },
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 20,
                Sector = "Test Sector",
                Ticker = "Test Ticker 01",
                TransactionDate = new DateTime(2015, 9, 1, 12, 32, 30),
            });


            groupAccount.MakeTransactionSync(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 987,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                },
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 20,
                Sector = "Test Sector1",
                Ticker = "Test Ticker 01",
                TransactionDate = new DateTime(2015, 2, 1, 12, 32, 30),
            });


            groupAccount.MakeTransactionSync(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 345,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                },
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 20,
                Sector = "Test Sector2",
                Ticker = "Test Ticker 01",
                TransactionDate = DateTime.Now,
            });


            groupAccount.MakeTransactionSync(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 785,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                },
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 20,
                Sector = "Test Sector3",
                Ticker = "Test Ticker 01",
                TransactionDate = DateTime.Now,
            });


            groupAccount.MakeTransactionSync(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 124,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                },
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 20,
                Sector = "Test Sector4",
                Ticker = "Test Ticker 01",
                TransactionDate = DateTime.Now,
            });


        }

        public void CreateCashFlowInfoForAssets()
        {
            var groupAccount = GetClientGroupAccountSync("65593420", DateTime.Now);

            groupAccount.MakeTransactionSync(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 1000,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                },
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 20,
                Sector = "Test Sector",
                Ticker = "Test Ticker 01",
                TransactionDate = DateTime.Now,
            });



        }

        public void CreateStockTransaction()
        {
            var groupAccount = GetClientGroupAccountSync("65593420", DateTime.Now);

            groupAccount.MakeTransaction(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>(),
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 20,
                Sector = "Test Sector",
                Ticker = "Test Ticker 01",
                TransactionDate = DateTime.Now,
            }).Wait();



        }
        public void InsertRandomDataIntoAssets()
        {

            List<AssetPrice> priceList = new List<AssetPrice>();
            for (int i = 0; i < 5; i++)
            {
                AssetPrice assetPrice = new AssetPrice
                {
                    Id = MemberNumberGenerator(8),
                    Price = Convert.ToDouble(MemberNumberGenerator(7)) / 100,
                    CreatedOn = DateTime.Now,
                    CorrespondingAssetKey = MemberNumberGenerator(8)
                };
                int rdmIntForPrice = _rdm.Next(6);
                switch (rdmIntForPrice)
                {
                    case 0: assetPrice.AssetType = AssetTypes.AustralianEquity; break;
                    case 1: assetPrice.AssetType = AssetTypes.CashAndTermDeposit; break;
                    case 2: assetPrice.AssetType = AssetTypes.DirectAndListedProperty; break;
                    case 3: assetPrice.AssetType = AssetTypes.FixedIncomeInvestments; break;
                    case 4: assetPrice.AssetType = AssetTypes.InternationalEquity; break;
                    case 5: assetPrice.AssetType = AssetTypes.ManagedInvestments; break;
                }
                priceList.Add(assetPrice);

            }


            // CashAccount------------------------------------------
            CashAccount cashAccount = new CashAccount
            {
                Id = MemberNumberGenerator(8),
                Bsb = MemberNumberGenerator(6),
                AccountName = "Name : " + MemberNumberGenerator(5),
                AccountNumber = MemberNumberGenerator(5),
                MaturityDate = DateTime.Now,
                TermsInMonths = _rdm.Next(4),
                InterestRate = Convert.ToDouble(MemberNumberGenerator(3)) / 100,
                AnnualInterest = Convert.ToDouble(MemberNumberGenerator(4)) / 100,
                FaceValue = Convert.ToDouble(MemberNumberGenerator(6)) / 100,
                CurrencyType = CurrencyType.AustralianDollar
            };

            int rdmIntForCash = _rdm.Next(6);
            switch (rdmIntForCash)
            {
                case 0: cashAccount.Frequency = Shared.Frequency.Annually; break;
                case 1: cashAccount.Frequency = Shared.Frequency.Daily; break;
                case 2: cashAccount.Frequency = Shared.Frequency.Monthly; break;
                case 3: cashAccount.Frequency = Shared.Frequency.Quarterly; break;
                case 4: cashAccount.Frequency = Shared.Frequency.Semiannually; break;
                case 5: cashAccount.Frequency = Shared.Frequency.Weekly; break;
            }

            rdmIntForCash = _rdm.Next(5);
            switch (rdmIntForCash)
            {
                case 0: cashAccount.CashAccountType = CashAccountType.CashAtCall; break;
                case 1: cashAccount.CashAccountType = CashAccountType.CashManagementAccount; break;
                case 2: cashAccount.CashAccountType = CashAccountType.ForeignCurrencyAccount; break;
                case 3: cashAccount.CashAccountType = CashAccountType.OnlineSavingsAccount; break;
                case 4: cashAccount.CashAccountType = CashAccountType.TermDeposit; break;
            }


            List<CashTransaction> cashTransactionList = new List<CashTransaction>();
            for (int i = 0; i < 5; i++)
            {
                CashTransaction cashTransaction = new CashTransaction
                {
                    Id = MemberNumberGenerator(8),
                    CreatedOn = DateTime.Now,
                    CashAccount = cashAccount,
                    Amount = Convert.ToDouble(MemberNumberGenerator(6)) / 100,
                    TransactionDate = DateTime.Now
                };
                cashTransactionList.Add(cashTransaction);
            }

            List<Interest> interestList = new List<Interest>();
            for (int i = 0; i < 5; i++)
            {
                Interest interest = new Interest
                {
                    Id = MemberNumberGenerator(8),
                    CreatedOn = DateTime.Now,
                    CashAccount = cashAccount,
                    Amount = Convert.ToDouble(MemberNumberGenerator(6)) / 100,
                    PaymentOn = DateTime.Now,
                };
                interestList.Add(interest);
            }
            //CashAccount -----------------------------------------------------------------------------


            //Equity -------------------------------------------------------------------------


            Equity equity = new Equity
            {
                AssetId = MemberNumberGenerator(8),
                Ticker = MemberNumberGenerator(6),
                Name = "Name : " + MemberNumberGenerator(5),
                Sector = MemberNumberGenerator(5),
                Prices = priceList
            };

            int rdmIntForEquity = _rdm.Next(3);
            switch (rdmIntForEquity)
            {
                case 0: equity.EquityType = Shared.EquityTypes.AustralianEquity; break;
                case 1: equity.EquityType = Shared.EquityTypes.InternationalEquity; break;
                case 2: equity.EquityType = Shared.EquityTypes.ManagedInvestments; break;
            }


            List<Dividend> dividendList = new List<Dividend>();
            for (int i = 0; i < 5; i++)
            {
                Dividend divident = new Dividend
                {
                    Id = MemberNumberGenerator(8),
                    PaymentOn = DateTime.Now,
                    Amount = Convert.ToDouble(MemberNumberGenerator(6)) / 100,
                    FrankingCredit = Convert.ToDouble(MemberNumberGenerator(6)) / 100,
                    Equity = equity,
                    CreatedOn = DateTime.Now,
                };
                dividendList.Add(divident);
            }


            List<EquityTransaction> equityTransactionList = new List<EquityTransaction>();
            for (int i = 0; i < 5; i++)
            {
                EquityTransaction equityTransaction = new EquityTransaction
                {
                    Id = MemberNumberGenerator(8),
                    NumberOfUnits = _rdm.Next(100),
                    UnitPriceAtPurchase = Convert.ToDouble(MemberNumberGenerator(6)) / 100,
                    Equity = equity,
                    CreatedOn = DateTime.Now,
                    TransactionDate = DateTime.Now,
                };
                equityTransactionList.Add(equityTransaction);
            }

            List<ResearchValue> researchValueList = new List<ResearchValue>();
            for (int i = 0; i < 5; i++)
            {
                ResearchValue researchValue = new ResearchValue
                {
                    Id = MemberNumberGenerator(8),
                    Key = MemberNumberGenerator(5),
                    Value = Convert.ToDouble(MemberNumberGenerator(5)) / 100,
                    Issuer = MemberNumberGenerator(5),
                    CreatedOn = DateTime.Now
                };
                researchValueList.Add(researchValue);
            }

            //Equity -------------------------------------------------------------------------

            //Bond -------------------------------------------------------------------------

            Bond bond = new Bond
            {
                BondId = MemberNumberGenerator(8),
                Ticker = MemberNumberGenerator(8),
                Name = "Name : " + MemberNumberGenerator(5),
                BondType = "Type : " + MemberNumberGenerator(5),
                Issuer = MemberNumberGenerator(5),
                Prices = priceList,
                ResearchValues = researchValueList
            };

            int rdmIntForBond = _rdm.Next(6);
            switch (rdmIntForBond)
            {
                case 0: bond.Frequency = Shared.Frequency.Annually; break;
                case 1: bond.Frequency = Shared.Frequency.Daily; break;
                case 2: bond.Frequency = Shared.Frequency.Monthly; break;
                case 3: bond.Frequency = Shared.Frequency.Quarterly; break;
                case 4: bond.Frequency = Shared.Frequency.Semiannually; break;
                case 5: bond.Frequency = Shared.Frequency.Weekly; break;
            }



            CouponPayment couponPayment = new CouponPayment
            {
                Id = MemberNumberGenerator(8),
                Amount = Convert.ToDouble(MemberNumberGenerator(8)) / 100,
                Bond = bond,
                CreatedOn = DateTime.Now,
                PaymentOn = DateTime.Now
            };

            BondTransaction bondTransaction = new BondTransaction
            {
                Id = MemberNumberGenerator(8),
                CreatedOn = DateTime.Now,
                NumberOfUnits = _rdm.Next(100),
                UnitPriceAtPurchase = Convert.ToDouble(MemberNumberGenerator(6)) / 100,
                Bond = bond,
                TransactionDate = DateTime.Now
            };

            //Bond -------------------------------------------------------------------------


            //Property -------------------------------------------------------------------------





            Property property = new Property
            {
                PropertyId = MemberNumberGenerator(8),
                FullAddress = "517 Flinder lane Melbourne Vic Australia",
                PropertyType = "office",
                Prices = priceList,

            };

            List<Rental> rentalList = new List<Rental>();
            for (int i = 0; i < 5; i++)
            {
                Rental rental = new Rental
                {
                    Id = MemberNumberGenerator(8),
                    PaymentOn = DateTime.Now,
                    Amount = Convert.ToDouble(MemberNumberGenerator(10)) / 100,
                    PropertyAddress = property,
                    CreatedOn = DateTime.Now,
                };
                rentalList.Add(rental);
            }

            List<PropertyTransaction> propertyTransactionList = new List<PropertyTransaction>();
            for (int i = 0; i < 5; i++)
            {
                PropertyTransaction propertyTransaction = new PropertyTransaction
                {
                    Id = MemberNumberGenerator(8),
                    Price = Convert.ToDouble(MemberNumberGenerator(10)) / 100,
                    PropertyAddress = property,
                    CreatedOn = DateTime.Now,
                    IsBuy = _rdm.Next(2) == 0 ? true : false,
                    TransactionDate = DateTime.Now,
                };
                propertyTransactionList.Add(propertyTransaction);
            }
            _db.CashTransactions.AddRange(cashTransactionList);
            _db.Interests.AddRange(interestList);
            _db.Dividends.AddRange(dividendList);
            _db.EquityTransactions.AddRange(equityTransactionList);
            _db.CouponPayments.Add(couponPayment);
            _db.BondTransactions.Add(bondTransaction);
            _db.Rentals.AddRange(rentalList);
            _db.PropertyTransactions.AddRange(propertyTransactionList);

            _db.Bonds.Add(bond);
            _db.CashAccounts.Add(cashAccount);
            _db.Equities.Add(equity);
            _db.Properties.Add(property);
            _db.SaveChanges();
        }


        /// <summary>
        ///     Get all equity sale/buy activities
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="ticker"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        /// 
        public async Task<List<ActivityBase>> GetEquityActivitiesForAccount(string accountId, string ticker, DateTime to)
        {
            //Account lookup ignores date range
            var account = await _db.Accounts.Where(a => a.AccountId == accountId)
                .Include(a => a.EquityTransactions)
                .Include(a => a.EquityTransactions.Select(t => t.Equity))
                .Include(a => a.EquityPayments)
                .Include(a => a.EquityPayments.Select(eq => eq.Equity))
                .SingleOrDefaultAsync();
            if (account == null)
            {
                ProfileCannotBefound(accountId, DateTime.Now, "Account");
            }
            var result = new List<ActivityBase>();
            var asset = _db.Equities.FirstOrDefault(e => e.Ticker == ticker);
            CollectEquityTransactions(to, account, result, asset.AssetId);
            CollectEquityDividends(to, account, result);
            return result;
        }

        public List<ActivityBase>  GetEquityActivitiesForAccountSync(string accountId, string ticker, DateTime to)       //added
        {
            //Account lookup ignores date range
            var account = _db.Accounts.Where(a => a.AccountId == accountId)
                .Include(a => a.EquityTransactions)
                .Include(a => a.EquityTransactions.Select(t => t.Equity))
                .Include(a => a.EquityPayments)
                .Include(a => a.EquityPayments.Select(eq => eq.Equity))
                .SingleOrDefault();
            if (account == null)
            {
                ProfileCannotBefound(accountId, DateTime.Now, "Account");
            }
            var result = new List<ActivityBase>();
            var asset = _db.Equities.FirstOrDefault(e => e.Ticker == ticker);
            CollectEquityTransactions(to, account, result, asset.AssetId);
            CollectEquityDividends(to, account, result);
            return result;
        }


        public async Task<List<ActivityBase>> GetPropertyActivitiesForAccount(string accountId, string placeId,
            DateTime to)
        {
            //Account lookup ignores date range
            var account = await _db.Accounts.Where(a => a.AccountId == accountId)
                .Include(a => a.PropertyTransactions)
                .Include(a => a.PropertyTransactions.Select(t => t.PropertyAddress))
                .Include(a => a.DirectPropertyPayments)
                .Include(a => a.DirectPropertyPayments.Select(p => p.PropertyAddress))
                .SingleOrDefaultAsync();

            if (account == null)
            {
                ProfileCannotBefound(accountId, DateTime.Now, "Account");
            }

            var result = new List<ActivityBase>();
            CollectPropertyTransactions(to, account, result);
            CollectPropertyRentals(to, account, result);
            return result;
        }

        public List<ActivityBase> GetPropertyActivitiesForAccountSync(string accountId, string placeId,             //added
            DateTime to)
        {
            //Account lookup ignores date range
            var account = _db.Accounts.Where(a => a.AccountId == accountId)
                .Include(a => a.PropertyTransactions)
                .Include(a => a.PropertyTransactions.Select(t => t.PropertyAddress))
                .Include(a => a.DirectPropertyPayments)
                .Include(a => a.DirectPropertyPayments.Select(p => p.PropertyAddress))
                .SingleOrDefault();

            if (account == null)
            {
                ProfileCannotBefound(accountId, DateTime.Now, "Account");
            }

            var result = new List<ActivityBase>();
            CollectPropertyTransactions(to, account, result);
            CollectPropertyRentals(to, account, result);
            return result;
        }

        public async Task<List<ActivityBase>> GetFixedIncomeActivitiesForAccount(string accountId, string ticker,
            DateTime toDate)
        {
            //Account lookup ignores date range
            var account = await _db.Accounts.Where(a => a.AccountId == accountId)
                .Include(a => a.BondTransactions)
                .Include(a => a.BondTransactions.Select(t => t.Bond))
                .Include(a => a.FixedIncomePayments)
                .Include(a => a.FixedIncomePayments.Select(p => p.Bond))
                .SingleOrDefaultAsync();

            if (account == null)
            {
                ProfileCannotBefound(accountId, DateTime.Now, "Account");
            }
            var result = new List<ActivityBase>();
            CollectBondTransactions(toDate, account, result);
            CollectCouponPayments(toDate, account, result);
            return result;
        }


        public List<ActivityBase> GetFixedIncomeActivitiesForAccountSync(string accountId, string ticker,               //added
            DateTime toDate)
        {
            //Account lookup ignores date range
            var account = _db.Accounts.Where(a => a.AccountId == accountId)
                .Include(a => a.BondTransactions)
                .Include(a => a.BondTransactions.Select(t => t.Bond))
                .Include(a => a.FixedIncomePayments)
                .Include(a => a.FixedIncomePayments.Select(p => p.Bond))
                .SingleOrDefault();

            if (account == null)
            {
                ProfileCannotBefound(accountId, DateTime.Now, "Account");
            }
            var result = new List<ActivityBase>();
            CollectBondTransactions(toDate, account, result);
            CollectCouponPayments(toDate, account, result);
            return result;
        }
        public async Task<List<ActivityBase>> GetCashActivitiesForAccount(string accountId, string cashAccountNumber,
            DateTime toDate)
        {
            //Account lookup ignores date range
            var account = await _db.Accounts.Where(a => a.AccountId == accountId)
                .Include(a => a.CashTransactions)
                .Include(a => a.CashTransactions.Select(t => t.CashAccount))
                .Include(a => a.CashAndTermDepositPayments)
                .Include(a => a.CashAndTermDepositPayments.Select(c => c.CashAccount))
                .SingleOrDefaultAsync();

            if (account == null)
            {
                ProfileCannotBefound(accountId, DateTime.Now, "Account");
            }

            var result = new List<ActivityBase>();
            CollectCashTransactions(toDate, account, result);
            CollectCashAccountInterests(toDate, account, result);
            return result;
        }

        public List<ActivityBase> GetCashActivitiesForAccountSync(string accountId, string cashAccountNumber,           //added
            DateTime toDate)
        {
            //Account lookup ignores date range
            var account = _db.Accounts.Where(a => a.AccountId == accountId)
                .Include(a => a.CashTransactions)
                .Include(a => a.CashTransactions.Select(t => t.CashAccount))
                .Include(a => a.CashAndTermDepositPayments)
                .Include(a => a.CashAndTermDepositPayments.Select(c => c.CashAccount))
                .SingleOrDefault();

            if (account == null)
            {
                ProfileCannotBefound(accountId, DateTime.Now, "Account");
            }

            var result = new List<ActivityBase>();
            CollectCashTransactions(toDate, account, result);
            CollectCashAccountInterests(toDate, account, result);
            return result;
        }


        public async Task<List<string>> GetAllSectors()
        {
            return await _db.Sectors.Select(s => s.SectorName).ToListAsync();
        }

        public List<string> GetAllSectorsSync()             //added
        {
            return _db.Sectors.Select(s => s.SectorName).Distinct().ToList();
        }

        public async Task<List<string>> GetAllBondTypes()
        {
            return await _db.BondTypes.Select(b => b.TypeName).ToListAsync();
        }

        public List<string> GetAllBondTypesSync()       //added
        {
            return _db.BondTypes.Select(b => b.TypeName).ToList();
        }
        public async Task<List<string>> GetAllAustralianStates()
        {
            return await _db.AustralianStates.Select(a => a.State).ToListAsync();
        }

        public List<string> GetAllAustralianStatesSync()        //added
        {
            return _db.AustralianStates.Select(a => a.State).ToList();
        }
        public async Task<List<string>> GetAllPropertyTypes()
        {
            return await _db.PropertyTypes.Select(p => p.TypeName).ToListAsync();
        }

        public List<string> GetAllPropertyTypesSync()       //added
        {
            return _db.PropertyTypes.Select(p => p.TypeName).ToList();
        }
        public Task<List<CashAccountType>> GetAllCashAccountTypes()
        {
            return Task.Run(() => new List<CashAccountType>
            {
                CashAccountType.CashAtCall,
                CashAccountType.CashManagementAccount,
                CashAccountType.ForeignCurrencyAccount,
                CashAccountType.OnlineSavingsAccount,
                CashAccountType.TermDeposit
            });
        }

        public List<CashAccountType> GetAllCashAccountTypesSync()           //added
        {
            return new List<CashAccountType>
            {
                CashAccountType.CashAtCall,
                CashAccountType.CashManagementAccount,
                CashAccountType.ForeignCurrencyAccount,
                CashAccountType.OnlineSavingsAccount,
                CashAccountType.TermDeposit
            };
        }

        public async Task<TAggregateRoot> Get<TAggregateRoot>(string id, DateTime todate)
            where TAggregateRoot : AggregateRootBase, new()
        {
            if (typeof(TAggregateRoot) == typeof(Client))
            {
                return await GetClientProfile(id, todate) as TAggregateRoot;
            }
            if (typeof(TAggregateRoot) == typeof(Adviser))
            {
                return await GetAdviserProfile(id, todate) as TAggregateRoot;
            }
            if (typeof(TAggregateRoot) == typeof(ClientGroup))
            {
                return await GetClientGroupProfile(id, todate) as TAggregateRoot;
            }
            throw new NotSupportedException("Only support get client, adviser, clientgroup");
        }


        public TAggregateRoot GetSync<TAggregateRoot>(string id, DateTime todate)       //added
            where TAggregateRoot : AggregateRootBase, new()
        {
            if (typeof(TAggregateRoot) == typeof(Client))
            {
                return GetClientProfile(id, todate) as TAggregateRoot;
            }
            if (typeof(TAggregateRoot) == typeof(Adviser))
            {
                return GetAdviserProfile(id, todate) as TAggregateRoot;
            }
            if (typeof(TAggregateRoot) == typeof(ClientGroup))
            {
                return GetClientGroupProfile(id, todate) as TAggregateRoot;
            }
            throw new NotSupportedException("Only support get client, adviser, clientgroup");
        }

        public async Task<Client> GetClient(string clientNumber, DateTime todate)
        {
            var client =
                _db.Clients.Local.SingleOrDefault(
                    c => c.ClientNumber == clientNumber && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate) ??
                await
                    _db.Clients.Where(
                        c => c.ClientNumber == clientNumber && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate)
                        .FirstOrDefaultAsync();
            if (client == null)
            {
                ProfileCannotBefound(clientNumber, todate, "client");
            }
            return await GetClientProfile(client.ClientId, todate);
        }

        public Client GetClientSync(string clientNumber, DateTime todate)
        {
            var client =
                _db.Clients.Local.SingleOrDefault(
                    c => c.ClientNumber == clientNumber && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate) ??
                    _db.Clients.Where(
                        c => c.ClientNumber == clientNumber && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate)
                        .FirstOrDefault();
            if (client == null)
            {
                ProfileCannotBefound(clientNumber, todate, "client");
            }
            return GetClientProfileSync(client.ClientId, todate);
        }


        public async Task<ClientGroup> GetClientGroup(string clientGroupNumber, DateTime todate)
        {
            var clientGroup =
                _db.ClientGroups.Local
                    .FirstOrDefault(
                        c => c.ClientGroupId == clientGroupNumber && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate) ??
                await
                    _db.ClientGroups.Where(
                        c => c.GroupNumber == clientGroupNumber && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate)
                        .FirstOrDefaultAsync();
            if (clientGroup == null)
            {
                ProfileCannotBefound(clientGroupNumber, todate, "client group");
            }
            return await GetClientGroupProfile(clientGroup.ClientGroupId, todate);
        }


        public ClientGroup GetClientGroupSync(string clientGroupNumber, DateTime todate)            //added ------------------------------------clientGroupId
        {
            var clientGroup =
                _db.ClientGroups.Local
                    .FirstOrDefault(
                        c => c.ClientGroupId == clientGroupNumber && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate) ??
                    _db.ClientGroups.Where(
                        c => c.GroupNumber == clientGroupNumber && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate)
                        .FirstOrDefault();
            if (clientGroup == null)
            {
                ProfileCannotBefound(clientGroupNumber, todate, "client group");
            }
            return GetClientGroupProfileSync(clientGroup.ClientGroupId, todate);
        }

        public Adviser GetAdviserSync(string adviserNumber, DateTime todate)
        {
            var dbAdviser =
                _db.Advisers.Local.SingleOrDefault(
                    a => a.AdviserNumber == adviserNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= todate) ??
                    _db.Advisers.Where(
                        a => a.AdviserNumber == adviserNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= todate)
                        .SingleOrDefault();
            if (dbAdviser == null)
                ProfileCannotBefound(adviserNumber, todate, "adviser");
            return GetAdviserProfileSync(dbAdviser.AdviserId, todate);
        }


        public async Task<Adviser> GetAdviser(string adviserNumber, DateTime todate)
        {
            var dbAdviser =
                _db.Advisers.Local.SingleOrDefault(
                    a => a.AdviserNumber == adviserNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= todate) ??
                   await _db.Advisers.Where(
                        a => a.AdviserNumber == adviserNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= todate)
                        .SingleOrDefaultAsync();
            if (dbAdviser == null)
                ProfileCannotBefound(adviserNumber, todate, "adviser");
            return await GetAdviserProfile(dbAdviser.AdviserId, todate);
        }


        public async Task<ClientAccount> GetClientAccount(string accountNumber, DateTime todate)
        {
            var account = _db.Accounts.Local.SingleOrDefault(
                a => a.AccountNumber == accountNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= todate) ??
                          await _db.Accounts.SingleOrDefaultAsync(
                              a =>
                                  a.AccountNumber == accountNumber && a.CreatedOn.HasValue &&
                                  a.CreatedOn.Value <= todate);
            if (account != null)
            {
                return await GenerateClientAccount(account.AccountId, todate);
            }

            throw new Exception("Cannot find client account " + accountNumber);
        }


        public ClientAccount GetClientAccountSync(string accountNumber, DateTime todate)            //added
        {
            var account = _db.Accounts.Local.SingleOrDefault(
                a => a.AccountNumber == accountNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= todate) ??
                          _db.Accounts.SingleOrDefault(
                              a =>
                                  a.AccountNumber == accountNumber && a.CreatedOn.HasValue &&
                                  a.CreatedOn.Value <= todate);
            if (account != null)
            {
                return GenerateClientAccountSync(account.AccountId, todate);
            }

            throw new Exception("Cannot find client account " + accountNumber);
        }

        public async Task<GroupAccount> GetClientGroupAccount(string accountNumber, DateTime todate)
        {
            var dbGroupAccount =
                _db.Accounts.Local
                    .FirstOrDefault(
                        a => a.AccountNumber == accountNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= todate) ??
                await _db.Accounts.Where(
                    a => a.AccountNumber == accountNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= todate)
                    .FirstOrDefaultAsync();
            if (dbGroupAccount == null)
            {
                ProfileCannotBefound(accountNumber, todate, "group account");
            }
            return await GenerateClientGroupAccount(dbGroupAccount.AccountId);
        }

        public GroupAccount GetClientGroupAccountSync(string accountNumber, DateTime todate)
        {
            var dbGroupAccount =
                _db.Accounts.Local
                    .FirstOrDefault(
                        a => a.AccountNumber == accountNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= todate) ??
                _db.Accounts.Where(
                    a => a.AccountNumber == accountNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= todate)
                    .FirstOrDefault();
            if (dbGroupAccount == null)
            {
                ProfileCannotBefound(accountNumber, todate, "group account");
            }
            return GenerateClientGroupAccountSync(dbGroupAccount.AccountId);
        }


        public async Task<Adviser> CreateAdviser(Adviser newAdviser)
        {

            //if (_db.Advisers.Local.SingleOrDefault(a => a.AdviserNumber == adviserNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= DateTime.Now) != null)


            _db.Advisers.Add(new Edis.Db.Adviser
            {
                CreatedOn = DateTime.Now,
                AdviserNumber = newAdviser.AdviserNumber,
                AdviserId = Guid.NewGuid().ToString(),
                FirstName = newAdviser.FirstName,
                ConsultancyExpenses = new List<ConsultancyExpense>(),
                LastName = newAdviser.LastName,
                TransactionExpenses = new List<TransactionExpense>(),

                ABNACN = newAdviser.ABNACN,
                MiddleName = newAdviser.MiddleName,
                CompanyName = newAdviser.CompanyName,
                Country = newAdviser.Country,
                AddressLn1 = newAdviser.AddressLn1,
                AddressLn2 = newAdviser.AddressLn2,
                AddressLn3 = newAdviser.AddressLn3,
                CurrentTitle = newAdviser.CurrentTitle,
                ExperienceStartDate = newAdviser.ExperienceStartDate,
                Fax = newAdviser.Fax,
                Gender = newAdviser.Gender,
                LastUpdate = DateTime.Now,
                Lat = newAdviser.Lat,
                Lng = newAdviser.Lng,
                Mobile = newAdviser.Mobile,
                Phone = newAdviser.Phone,
                PostCode = newAdviser.PostCode,
                State = newAdviser.State,
                Suburb = newAdviser.Suburb,
                Title = newAdviser.Title,
                VerifiedId = newAdviser.VerifiedId,

                IndustryExperienceStartDate = newAdviser.IndustryExperienceStartDate,
                BusinessPhone = newAdviser.BusinessPhone,
                BusinessMobile = newAdviser.BusinessMobile,
                BusinessFax = newAdviser.BusinessFax,

                DAddressLine1 = newAdviser.DAddressLine1,
                DAddressLine2 = newAdviser.DAddressLine2,
                DAddressLine3 = newAdviser.DAddressLine3,
                DPostcode = newAdviser.DPostcode,
                DState = newAdviser.DState,
                DSuburb = newAdviser.DSuburb,
                DCountry = newAdviser.DCountry,
                Asfl = newAdviser.Asfl,
                AuthorizedRepresentativeNumber = newAdviser.AuthorizedRepresentativeNumber,
                DealerGroupName = newAdviser.DealerGroupName,
                DealerGroupHasDerivativesLicense = newAdviser.DealerGroupHasDerivativesLicense ? true : false,
                IsAuthorizedRepresentative = newAdviser.IsAuthorizedRepresentative ? true : false,

                TotalAssetUnderManagement = newAdviser.TotalAssetUnderManagement,
                TotalInvestmentUndermanagement = newAdviser.TotalInvestmentUndermanagement,
                TotalDirectAustralianEquitiesUnderManagement = newAdviser.TotalDirectAustralianEquitiesUnderManagement,
                TotalDirectInterantionalEquitiesUnderManagement = newAdviser.TotalDirectInterantionalEquitiesUnderManagement,
                TotalDirectFixedInterestUnderManagement = newAdviser.TotalDirectFixedInterestUnderManagement,
                TotalDirectLendingBookInterestUnderManagement = newAdviser.TotalDirectLendingBookInterestUnderManagement,
                ApproximateNumberOfClients = newAdviser.ApproximateNumberOfClients,

                ProfessiontypeId = newAdviser.ProfessiontypeId,
                RemunerationMethodSpecified = newAdviser.RemunerationMethodSpecified,
                RemunerationMethod = newAdviser.RemunerationMethod,
                NumberOfClientsId = newAdviser.NumberOfClientsId,
                AnnualIncomeLevelId = newAdviser.AnnualIncomeLevelId,
                InvestibleAssetLevel = newAdviser.InvestibleAssetLevel,
                TotalAssetLevel = newAdviser.TotalAssetLevel,

                Institution = newAdviser.Institution,
                CourseTitle = newAdviser.CourseTitle,
                CourseStatus = newAdviser.CourseStatus,
                EducationLevelId = newAdviser.EducationLevelId,
            });

            //await _db.SaveChangesAsync();


            _db.SaveChanges();

            //todo date time now is not playing nicely with async, and 20 seconds have been added to avoid possible conflicts
            return GetAdviserSync(newAdviser.AdviserNumber, DateTime.Now.AddSeconds(20));
        }


        public Adviser CreateAdviserSync(Adviser newAdviser)
        {

            //if (_db.Advisers.Local.SingleOrDefault(a => a.AdviserNumber == adviserNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= DateTime.Now) != null)


            _db.Advisers.Add(new Edis.Db.Adviser
            {
                CreatedOn = DateTime.Now,
                AdviserNumber = newAdviser.AdviserNumber,
                AdviserId = Guid.NewGuid().ToString(),
                FirstName = newAdviser.FirstName,
                ConsultancyExpenses = new List<ConsultancyExpense>(),
                LastName = newAdviser.LastName,
                TransactionExpenses = new List<TransactionExpense>(),

                ABNACN = newAdviser.ABNACN,
                MiddleName = newAdviser.MiddleName,
                CompanyName = newAdviser.CompanyName,
                Country = newAdviser.Country,
                AddressLn1 = newAdviser.AddressLn1,
                AddressLn2 = newAdviser.AddressLn2,
                AddressLn3 = newAdviser.AddressLn3,
                CurrentTitle = newAdviser.CurrentTitle,
                ExperienceStartDate = newAdviser.ExperienceStartDate,
                Fax = newAdviser.Fax,
                Gender = newAdviser.Gender,
                LastUpdate = DateTime.Now,
                Lat = newAdviser.Lat,
                Lng = newAdviser.Lng,
                Mobile = newAdviser.Mobile,
                Phone = newAdviser.Phone,
                PostCode = newAdviser.PostCode,
                State = newAdviser.State,
                Suburb = newAdviser.Suburb,
                Title = newAdviser.Title,
                VerifiedId = newAdviser.VerifiedId,

                IndustryExperienceStartDate = newAdviser.IndustryExperienceStartDate,
                BusinessPhone = newAdviser.BusinessPhone,
                BusinessMobile = newAdviser.BusinessMobile,
                BusinessFax = newAdviser.BusinessFax,

                DAddressLine1 = newAdviser.DAddressLine1,
                DAddressLine2 = newAdviser.DAddressLine2,
                DAddressLine3 = newAdviser.DAddressLine3,
                DPostcode = newAdviser.DPostcode,
                DState = newAdviser.DState,
                DSuburb = newAdviser.DSuburb,
                DCountry = newAdviser.DCountry,
                Asfl = newAdviser.Asfl,
                AuthorizedRepresentativeNumber = newAdviser.AuthorizedRepresentativeNumber,
                DealerGroupName = newAdviser.DealerGroupName,
                DealerGroupHasDerivativesLicense = newAdviser.DealerGroupHasDerivativesLicense ? true : false,
                IsAuthorizedRepresentative = newAdviser.IsAuthorizedRepresentative ? true : false,

                TotalAssetUnderManagement = newAdviser.TotalAssetUnderManagement,
                TotalInvestmentUndermanagement = newAdviser.TotalInvestmentUndermanagement,
                TotalDirectAustralianEquitiesUnderManagement = newAdviser.TotalDirectAustralianEquitiesUnderManagement,
                TotalDirectInterantionalEquitiesUnderManagement = newAdviser.TotalDirectInterantionalEquitiesUnderManagement,
                TotalDirectFixedInterestUnderManagement = newAdviser.TotalDirectFixedInterestUnderManagement,
                TotalDirectLendingBookInterestUnderManagement = newAdviser.TotalDirectLendingBookInterestUnderManagement,
                ApproximateNumberOfClients = newAdviser.ApproximateNumberOfClients,

                ProfessiontypeId = newAdviser.ProfessiontypeId,
                RemunerationMethodSpecified = newAdviser.RemunerationMethodSpecified,
                RemunerationMethod = newAdviser.RemunerationMethod,
                NumberOfClientsId = newAdviser.NumberOfClientsId,
                AnnualIncomeLevelId = newAdviser.AnnualIncomeLevelId,
                InvestibleAssetLevel = newAdviser.InvestibleAssetLevel,
                TotalAssetLevel = newAdviser.TotalAssetLevel,

                Institution = newAdviser.Institution,
                CourseTitle = newAdviser.CourseTitle,
                CourseStatus = newAdviser.CourseStatus,
                EducationLevelId = newAdviser.EducationLevelId,
                CAFDescription = newAdviser.CAFDescription,
                CAFId = newAdviser.CAFId,
                CAFSelected = newAdviser.CAFSelected,
                GroupName = newAdviser.GroupName,
                Image = newAdviser.Image,
                ImageMimeType = newAdviser.ImageMimeType,
                NewsLetterSelected = newAdviser.NewsLetterSelected,
                NewsLetterServiceId = newAdviser.NewsLetterServiceId,
                NewsLetterServiceName = newAdviser.NewsLetterServiceName,
                Providing = newAdviser.Providing,
                RoleAndServicesSummary = newAdviser.RoleAndServicesSummary,
                ServiceId = newAdviser.ServiceId,
                ServiceName = newAdviser.ServiceName,
                TotalAssetLevelId = newAdviser.TotalAssetLevelId
            });

            //await _db.SaveChangesAsync();


            _db.SaveChanges();

            //todo date time now is not playing nicely with async, and 20 seconds have been added to avoid possible conflicts
            return GetAdviserSync(newAdviser.AdviserNumber, DateTime.Now.AddSeconds(20));
        }

        public Adviser UpdateAdviser(Adviser adviser)
        {

            Edis.Db.Adviser currentAdviser = _db.Advisers.SingleOrDefault(a => a.AdviserNumber == adviser.AdviserNumber);

            currentAdviser.FirstName = adviser.FirstName;
            currentAdviser.ABNACN = adviser.ABNACN;
            currentAdviser.AddressLn1 = adviser.AddressLn1;
            currentAdviser.AddressLn2 = adviser.AddressLn2;
            currentAdviser.AddressLn3 = adviser.AddressLn3;
            currentAdviser.AnnualIncomeLevelId = adviser.AnnualIncomeLevelId;
            currentAdviser.ApproximateNumberOfClients = adviser.ApproximateNumberOfClients;
            currentAdviser.Asfl = adviser.Asfl;
            currentAdviser.AuthorizedRepresentativeNumber = adviser.AuthorizedRepresentativeNumber;
            currentAdviser.BusinessFax = adviser.BusinessFax;
            currentAdviser.BusinessMobile = adviser.BusinessMobile;
            currentAdviser.BusinessPhone = adviser.BusinessPhone;
            currentAdviser.CAFDescription = adviser.CAFDescription;
            currentAdviser.CAFId = adviser.CAFId;
            currentAdviser.CAFSelected = adviser.CAFSelected;
            currentAdviser.CompanyName = adviser.CompanyName;
            currentAdviser.Country = adviser.Country;
            currentAdviser.CourseStatus = adviser.CourseStatus;
            currentAdviser.CourseTitle = adviser.CourseTitle;
            currentAdviser.CreatedOn = adviser.CreatedOn;
            currentAdviser.CurrentTitle = adviser.CurrentTitle;
            currentAdviser.DAddressLine1 = adviser.DAddressLine1;
            currentAdviser.DAddressLine2 = adviser.DAddressLine2;
            currentAdviser.DAddressLine3 = adviser.DAddressLine3;
            currentAdviser.DCountry = adviser.DCountry;
            currentAdviser.DealerGroupHasDerivativesLicense = adviser.DealerGroupHasDerivativesLicense;
            currentAdviser.DealerGroupName = adviser.DealerGroupName;
            currentAdviser.DPostcode = adviser.DPostcode;
            currentAdviser.DState = adviser.DState;
            currentAdviser.DSuburb = adviser.DSuburb;
            currentAdviser.EducationLevelId = adviser.EducationLevelId;
            currentAdviser.ExperienceStartDate = adviser.ExperienceStartDate;
            currentAdviser.Fax = adviser.Fax;
            currentAdviser.FirstName = adviser.FirstName;
            currentAdviser.Gender = adviser.Gender;
            currentAdviser.GroupName = adviser.GroupName;
            currentAdviser.IndustryExperienceStartDate = adviser.IndustryExperienceStartDate;
            currentAdviser.Institution = adviser.Institution;
            currentAdviser.InvestibleAssetLevel = adviser.InvestibleAssetLevel;
            currentAdviser.IsAuthorizedRepresentative = adviser.IsAuthorizedRepresentative;
            currentAdviser.LastName = adviser.LastName;
            currentAdviser.LastUpdate = adviser.LastUpdate;
            currentAdviser.Lat = adviser.Lat;
            currentAdviser.Lng = adviser.Lng;
            currentAdviser.MiddleName = adviser.MiddleName;
            currentAdviser.Mobile = adviser.Mobile;
            currentAdviser.NewsLetterSelected = adviser.NewsLetterSelected;
            currentAdviser.NewsLetterServiceId = adviser.NewsLetterServiceId;
            currentAdviser.NewsLetterServiceName = adviser.NewsLetterServiceName;
            currentAdviser.NumberOfClientsId = adviser.NumberOfClientsId;
            currentAdviser.Phone = adviser.Phone;
            currentAdviser.PostCode = adviser.PostCode;
            currentAdviser.ProfessiontypeId = adviser.ProfessiontypeId;
            currentAdviser.Providing = adviser.Providing;
            currentAdviser.RemunerationMethod = adviser.RemunerationMethod;
            currentAdviser.RemunerationMethodSpecified = adviser.RemunerationMethodSpecified;
            currentAdviser.RoleAndServicesSummary = adviser.RoleAndServicesSummary;
            currentAdviser.ServiceId = adviser.ServiceId;
            currentAdviser.ServiceName = adviser.ServiceName;
            currentAdviser.State = adviser.State;
            currentAdviser.Suburb = adviser.Suburb;
            currentAdviser.Title = adviser.Title;
            currentAdviser.TotalAssetLevel = adviser.TotalAssetLevel;
            currentAdviser.TotalAssetLevelId = adviser.TotalAssetLevelId;
            currentAdviser.TotalAssetUnderManagement = adviser.TotalAssetUnderManagement;
            currentAdviser.TotalDirectAustralianEquitiesUnderManagement = adviser.TotalDirectAustralianEquitiesUnderManagement;
            currentAdviser.TotalDirectFixedInterestUnderManagement = adviser.TotalDirectFixedInterestUnderManagement;
            currentAdviser.TotalDirectInterantionalEquitiesUnderManagement = adviser.TotalDirectInterantionalEquitiesUnderManagement;
            currentAdviser.TotalDirectLendingBookInterestUnderManagement = adviser.TotalDirectLendingBookInterestUnderManagement;
            currentAdviser.TotalInvestmentUndermanagement = adviser.TotalInvestmentUndermanagement;
            currentAdviser.VerifiedId = adviser.VerifiedId;

            _db.SaveChanges();

            return GetAdviserSync(currentAdviser.AdviserNumber, DateTime.Now.AddSeconds(20));
        }

        public Adviser UpdateAdviserImage(Adviser adviser)
        {

            Edis.Db.Adviser currentAdviser = _db.Advisers.SingleOrDefault(a => a.AdviserNumber == adviser.AdviserNumber);

            currentAdviser.Image = adviser.Image;
            currentAdviser.ImageMimeType = adviser.ImageMimeType;

            _db.SaveChanges();

            return GetAdviserSync(currentAdviser.AdviserNumber, DateTime.Now.AddSeconds(20));
        }

        public async Task CreateNewClient(ClientRegistration client)
        {
            var group = await _db.ClientGroups.SingleOrDefaultAsync(g => g.ClientGroupId == client.GroupNumber);
            //var clientNumber = GenerateUniqueClientNumber();

            //todo validate success association of relationships
            _db.Clients.Add(new Edis.Db.Client
            {
                Accounts = new List<Account>(),
                ClientId = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                ClientGroupId = group.ClientGroupId,
                ClientType = client.ClientType,
                ClientGroup = group,
                ClientNumber = client.ClientNumber,             //clientNumber => client.ClientNumber

                //Person
                Dob = client.Dob,
                FirstName = client.FirstName,
                MiddleName = client.MiddleName,
                LastName = client.LastName,
                Address = client.Address,
                Email = client.Email,
                Phone = client.Phone,

                //Entity
                ABN = client.ABN,
                ACN = client.ACN,
                EntityName = client.EntityName,
                EntityType = client.EntityType
            });
            await _db.SaveChangesAsync();
        }


        public void CreateNewClientSync(ClientRegistration client)
        {
            var group = _db.ClientGroups.SingleOrDefaultAsync(g => g.ClientGroupId == client.GroupNumber).Result;
            //var clientNumber = GenerateUniqueClientNumber();

            //todo validate success association of relationships
            _db.Clients.Add(new Edis.Db.Client
            {
                Accounts = new List<Account>(),
                ClientId = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                ClientGroupId = group.ClientGroupId,
                ClientType = client.ClientType,
                ClientGroup = group,
                ClientNumber = client.ClientNumber,             //clientNumber => client.ClientNumber

                //Person
                Dob = client.Dob,
                FirstName = client.FirstName,
                MiddleName = client.MiddleName,
                LastName = client.LastName,
                Address = client.Address,
                Email = client.Email,
                Phone = client.Phone,

                //Entity
                ABN = client.ABN,
                ACN = client.ACN,
                EntityName = client.EntityName,
                EntityType = client.EntityType
            });
            _db.SaveChanges();
        }

        public void UpdateClientSync(ClientRegistration client)
        {
            Edis.Db.Client currentClient = _db.Clients.SingleOrDefault(c => c.ClientNumber == client.ClientNumber);

            currentClient.ClientGroup = currentClient.ClientGroup;
            currentClient.FirstName = client.FirstName;
            currentClient.LastName = client.LastName;
            currentClient.Gender = client.Gender;
            currentClient.ABN = client.ABN;
            currentClient.ACN = client.ACN;
            currentClient.EntityName = client.EntityName;
            currentClient.EntityType = client.EntityType;
            currentClient.MiddleName = client.MiddleName;
            currentClient.Dob = client.Dob;
            currentClient.Mobile = client.Mobile;
            currentClient.Phone = client.Phone;
            currentClient.Fax = client.Fax;
            currentClient.Address = client.Address;
            //_db.Clients.Attach(currentClient);
            //_db.Clients
            //var entry = _db.Entry(currentClient);
            //entry.Property(e => e.Email).IsModified = true;
            _db.SaveChanges();
        }

        public void CreateRiskProfileForClient(RiskProfile riskProfile) {
            _db.RiskProfiles.Add(new Edis.Db.RiskProfile {
                RiskProfileID = Guid.NewGuid().ToString(),
                CapitalLossAttitude = riskProfile.CapitalLossAttitude,
                ClientID = riskProfile.ClientID,
                Comments = riskProfile.Comments,
                DateCreated = riskProfile.DateCreated,
                DateModified = riskProfile.DateModified,
                IncomeSource = riskProfile.IncomeSource,
                InvestmentKnowledge = riskProfile.InvestmentKnowledge,
                InvestmentObjective1 = riskProfile.InvestmentObjective1,
                InvestmentObjective2 = riskProfile.InvestmentObjective2,
                InvestmentObjective3 = riskProfile.InvestmentObjective3,
                InvestmentProfile = riskProfile.InvestmentProfile,
                InvestmentTimeHorizon = riskProfile.InvestmentTimeHorizon,
                LongTermGoal1 = riskProfile.LongTermGoal1,
                LongTermGoal2 = riskProfile.LongTermGoal2,
                LongTermGoal3 = riskProfile.LongTermGoal3,
                MedTermGoal1 = riskProfile.MedTermGoal1,
                MedTermGoal2 = riskProfile.MedTermGoal2,
                MedTermGoal3 = riskProfile.MedTermGoal3,
                RetirementAge = riskProfile.RetirementAge,
                RetirementIncome = riskProfile.RetirementIncome,
                RiskAttitude = riskProfile.RiskAttitude,
                ShortTermAssetPercent = riskProfile.ShortTermAssetPercent,
                ShortTermEquityPercent = riskProfile.ShortTermEquityPercent,
                ShortTermGoal1 = riskProfile.ShortTermGoal1,
                ShortTermGoal2 = riskProfile.ShortTermGoal2,
                ShortTermGoal3 = riskProfile.ShortTermGoal3,
                ShortTermTrading = riskProfile.ShortTermTrading,
                riskLevel = riskProfile.riskLevel
            });
            _db.SaveChanges();
        }

        public RiskProfile getRiskProfileForClient(string clientID) {
            var riskProfile = _db.RiskProfiles.SingleOrDefault(r => r.ClientID == clientID);

            if(riskProfile == null){
                return null;
            }
            return new RiskProfile { 
                ClientID = riskProfile.ClientID,
                CapitalLossAttitude = riskProfile.CapitalLossAttitude,
                RetirementAge = riskProfile.RetirementAge,
                riskLevel = riskProfile.riskLevel,
                RiskAttitude = riskProfile.RiskAttitude,
                ShortTermAssetPercent = riskProfile.ShortTermAssetPercent,
                Comments = riskProfile.Comments,
                DateCreated = riskProfile.DateCreated,
                DateModified = riskProfile.DateModified,
                IncomeSource = riskProfile.IncomeSource,
                InvestmentKnowledge = riskProfile.InvestmentKnowledge,
                InvestmentObjective1 = riskProfile.InvestmentObjective1,
                InvestmentObjective2 = riskProfile.InvestmentObjective2,
                InvestmentObjective3 = riskProfile.InvestmentObjective3,
                InvestmentProfile = riskProfile.InvestmentProfile,
                InvestmentTimeHorizon = riskProfile.InvestmentTimeHorizon,
                LongTermGoal1 = riskProfile.LongTermGoal1,
                LongTermGoal2 = riskProfile.LongTermGoal2,
                LongTermGoal3 = riskProfile.LongTermGoal3,
                MedTermGoal1 = riskProfile.MedTermGoal1,
                MedTermGoal2 = riskProfile.MedTermGoal2,
                MedTermGoal3 = riskProfile.MedTermGoal3,
                RetirementIncome = riskProfile.RetirementIncome,
                RiskProfileID = riskProfile.RiskProfileID,
                ShortTermEquityPercent = riskProfile.ShortTermEquityPercent,
                ShortTermGoal1 = riskProfile.ShortTermGoal1,
                ShortTermGoal2 = riskProfile.ShortTermGoal2,
                ShortTermGoal3 = riskProfile.ShortTermGoal3,
                ShortTermTrading = riskProfile.ShortTermTrading
            };
        }

        public void UpdateRiskProfile(RiskProfile riskProfile) {
            Edis.Db.RiskProfile existingProfile = _db.RiskProfiles.SingleOrDefault(r => r.ClientID == riskProfile.ClientID);
            existingProfile.CapitalLossAttitude = riskProfile.CapitalLossAttitude;
            existingProfile.RetirementAge = riskProfile.RetirementAge;
            existingProfile.riskLevel = riskProfile.riskLevel;
            existingProfile.RiskAttitude = riskProfile.RiskAttitude;
            existingProfile.ShortTermAssetPercent = riskProfile.ShortTermAssetPercent;
            existingProfile.Comments = riskProfile.Comments;
            existingProfile.DateCreated = riskProfile.DateCreated;
            existingProfile.DateModified = riskProfile.DateModified;
            existingProfile.IncomeSource = riskProfile.IncomeSource;
            existingProfile.InvestmentKnowledge = riskProfile.InvestmentKnowledge;
            existingProfile.InvestmentObjective1 = riskProfile.InvestmentObjective1;
            existingProfile.InvestmentObjective2 = riskProfile.InvestmentObjective2;
            existingProfile.InvestmentObjective3 = riskProfile.InvestmentObjective3;
            existingProfile.InvestmentProfile = riskProfile.InvestmentProfile;
            existingProfile.InvestmentTimeHorizon = riskProfile.InvestmentTimeHorizon;
            existingProfile.LongTermGoal1 = riskProfile.LongTermGoal1;
            existingProfile.LongTermGoal2 = riskProfile.LongTermGoal2;
            existingProfile.LongTermGoal3 = riskProfile.LongTermGoal3;
            existingProfile.MedTermGoal1 = riskProfile.MedTermGoal1;
            existingProfile.MedTermGoal2 = riskProfile.MedTermGoal2;
            existingProfile.MedTermGoal3 = riskProfile.MedTermGoal3;
            existingProfile.RetirementIncome = riskProfile.RetirementIncome;
            existingProfile.ShortTermEquityPercent = riskProfile.ShortTermEquityPercent;
            existingProfile.ShortTermGoal1 = riskProfile.ShortTermGoal1;
            existingProfile.ShortTermGoal2 = riskProfile.ShortTermGoal2;
            existingProfile.ShortTermGoal3 = riskProfile.ShortTermGoal3;
            existingProfile.ShortTermTrading = riskProfile.ShortTermTrading;

            _db.SaveChanges();
        }

        public async Task CreateNewClientGroup(ClientGroupRegistration clientGroup)
        {
            //var adviser = await _db.Advisers.Where(ad => ad.AdviserNumber == clientGroup.AdviserNumber).FirstOrDefaultAsync();
            var adviser = _db.Advisers.Where(ad => ad.AdviserNumber == clientGroup.AdviserNumber).FirstOrDefault();

            var group = new Edis.Db.ClientGroup
            {
                ClientGroupId = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                GroupNumber = GenerateUniqueClientGroupNumber(),
                GroupAmount = clientGroup.GroupAmount,
                GroupName = clientGroup.GroupName,
                GroupAccounts = new List<Account>()
            };
            if (adviser != null)
            {
                group.Adviser = adviser;
            }
            var client = new Edis.Db.Client
            {
                Accounts = new List<Account>(),
                CreatedOn = DateTime.Now,
                FirstName = clientGroup.client.FirstName,
                LastName = clientGroup.client.LastName,
                Address = clientGroup.client.Address,
                Dob = clientGroup.client.Dob,
                ClientType = clientGroup.client.ClientType,
                ClientNumber = clientGroup.client.ClientNumber,                           //GenerateUniqueClientNumber() => clientGroup.client.ClientNumber
                ClientId = Guid.NewGuid().ToString(),
                Phone = clientGroup.client.Phone,
                Email = clientGroup.client.Email,
                ClientGroupId = group.ClientGroupId,
                ClientGroup = group
            };
            group.MainClientId = client.ClientId;
            _db.Clients.Add(client);
            //group.MainClient = client;
            _db.ClientGroups.Add(group);
            await _db.SaveChangesAsync();
            //_db.SaveChanges();
        }

        public void CreateNewClientGroupSync(ClientGroupRegistration clientGroup)
        {
            //var adviser = await _db.Advisers.Where(ad => ad.AdviserNumber == clientGroup.AdviserNumber).FirstOrDefaultAsync();
            var adviser = _db.Advisers.Where(ad => ad.AdviserNumber == clientGroup.AdviserNumber).FirstOrDefault();

            var group = new Edis.Db.ClientGroup
            {
                ClientGroupId = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                GroupNumber = GenerateUniqueClientGroupNumber(),
                GroupAmount = clientGroup.GroupAmount,
                GroupName = clientGroup.GroupName,
                GroupAccounts = new List<Account>()
            };
            if (adviser != null)
            {
                group.Adviser = adviser;
            }
            var client = new Edis.Db.Client
            {
                Accounts = new List<Account>(),
                CreatedOn = DateTime.Now,
                FirstName = clientGroup.client.FirstName,
                MiddleName = clientGroup.client.MiddleName,
                LastName = clientGroup.client.LastName,
                Address = clientGroup.client.Address,
                Dob = clientGroup.client.Dob,
                ClientType = clientGroup.client.ClientType,
                ClientNumber = clientGroup.client.ClientNumber,                           //GenerateUniqueClientNumber() => clientGroup.client.ClientNumber
                ClientId = Guid.NewGuid().ToString(),
                Phone = clientGroup.client.Phone,
                Email = clientGroup.client.Email,
                ClientGroupId = group.ClientGroupId,
                ClientGroup = group
            };

            _db.CashAccounts.Add(new CashAccount {
                Id = Guid.NewGuid().ToString(),
                AccountNumber = group.GroupNumber,
                AccountName = group.GroupName,
                CashTransactions = new List<CashTransaction>(),
                FaceValue = Double.Parse(group.GroupAmount),
                CurrencyType = CurrencyType.AustralianDollar,
                CashAccountType = CashAccountType.CashManagementAccount,
                Frequency = Frequency.Annually
            });

            group.MainClientId = client.ClientId;
            _db.Clients.Add(client);
            //group.MainClient = client;
            _db.ClientGroups.Add(group);
            _db.SaveChanges();
            //_db.SaveChanges();
        }
        public async Task RecordTransaction(TransactionCreationBase transaction)
        {
            var adviser = await _db.Advisers.Where(ad => _db.ClientGroups.Where(g => g.GroupAccounts
                .Any(a => a.AccountNumber == transaction.AccountNumber
                          || _db.Clients.Any(c => c.ClientGroupId == g.ClientGroupId &&
                                                 c.Accounts.Any(acc => acc.AccountNumber == transaction.AccountNumber))))
                .Any(c => c.Adviser.AdviserId == ad.AdviserId))
                .FirstOrDefaultAsync();
            var account = await _db.Accounts.Where(a => a.AccountNumber == transaction.AccountNumber)
                .Include(a => a.BondTransactions)
                .Include(a => a.EquityTransactions)
                .Include(a => a.CashTransactions)
                .Include(a => a.PropertyTransactions)
                .Include(a => a.Insurances)
                .Include(a => a.MortgageHomeLoans.Select(m => m.CorrespondingProperty))
                .Include(a => a.MarginLendings)
                .SingleOrDefaultAsync();

            if (account.EquityTransactions == null)
            {
                account.EquityTransactions = new List<EquityTransaction>();
            }
            if (account.BondTransactions == null)
            {
                account.BondTransactions = new List<BondTransaction>();
            }
            if (account.CashTransactions == null)
            {
                account.CashTransactions = new List<CashTransaction>();
            }

            if (account.PropertyTransactions == null)
            {
                account.PropertyTransactions = new List<PropertyTransaction>();
            }

            if (account.MortgageHomeLoans == null)
            {
                account.MortgageHomeLoans = new List<MortgageHomeLoanTransaction>();
            }
            if (account.MarginLendings == null)
            {
                account.MortgageHomeLoans = new List<MortgageHomeLoanTransaction>();
            }
            if (account.Insurances == null)
            {
                account.Insurances = new List<Edis.Db.Liabilities.InsuranceTransaction>();
            }


            if (transaction is BondTransactionCreation)
            {
                await RecordBondTransaction(transaction, account, adviser);
                await _db.SaveChangesAsync();
            }

            else if (transaction is CashAccountTransactionAccountCreation)
            {
                await RecordCashAccountTransaction(transaction, adviser, account);
                await _db.SaveChangesAsync();
            }
            else if (transaction is EquityTransactionCreation)
            {
                await RecordEquityTransaction(transaction, account, adviser);
                await _db.SaveChangesAsync();
            }

            else if (transaction is PropertyTransactionCreation)
            {
                await RecordPropertyTransaction(transaction, account, adviser);
                await _db.SaveChangesAsync();
            }
            else if (transaction is MarginLendingTransactionCreation)
            {
                RecordMarginLendingTransaction(transaction, account);
                await _db.SaveChangesAsync();
            }
            else if (transaction is HomeLoanTransactionCreation)
            {
                await RecordHomeLoanTransaction(transaction, account);
                await _db.SaveChangesAsync();
            }
            else if (transaction is Domain.Portfolio.Entities.CreationModels.Transaction.InsuranceTransactionCreation)
            {
                RecordInsuranceTransaction(transaction, account);
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new NotSupportedException(
                    "Unknown transaction type");
            }
        }


        public void RecordTransactionSync(TransactionCreationBase transaction)            //added
        {
            var adviser = _db.Advisers.Where(ad => _db.ClientGroups.Where(g => g.GroupAccounts
                .Any(a => a.AccountNumber == transaction.AccountNumber)
                          || (_db.Clients.Any(c => c.ClientGroupId == g.ClientGroupId &&
                                                 c.Accounts.Any(acc => acc.AccountNumber == transaction.AccountNumber))))
                .Any(c => c.Adviser.AdviserId == ad.AdviserId))
                .FirstOrDefault();
            var account = _db.Accounts.Where(a => a.AccountNumber == transaction.AccountNumber)
                .Include(a => a.BondTransactions)
                .Include(a => a.EquityTransactions)
                .Include(a => a.CashTransactions)
                .Include(a => a.PropertyTransactions)
                .Include(a => a.Insurances)
                .Include(a => a.MortgageHomeLoans.Select(m => m.CorrespondingProperty))
                .Include(a => a.MarginLendings)
                .SingleOrDefault();

            if (account.EquityTransactions == null)
            {
                account.EquityTransactions = new List<EquityTransaction>();
            }
            if (account.BondTransactions == null)
            {
                account.BondTransactions = new List<BondTransaction>();
            }
            if (account.CashTransactions == null)
            {
                account.CashTransactions = new List<CashTransaction>();
            }

            if (account.PropertyTransactions == null)
            {
                account.PropertyTransactions = new List<PropertyTransaction>();
            }

            if (account.MortgageHomeLoans == null)
            {
                account.MortgageHomeLoans = new List<MortgageHomeLoanTransaction>();
            }
            if (account.MarginLendings == null)
            {
                account.MortgageHomeLoans = new List<MortgageHomeLoanTransaction>();
            }
            if (account.Insurances == null)
            {
                account.Insurances = new List<Edis.Db.Liabilities.InsuranceTransaction>();
            }


            if (transaction is BondTransactionCreation)
            {
                RecordBondTransactionSync(transaction, account, adviser);
                _db.SaveChanges();
            }

            else if (transaction is CashAccountTransactionAccountCreation)
            {
                RecordCashAccountTransactionSync(transaction, adviser, account);
                _db.SaveChanges();
            }
            else if (transaction is EquityTransactionCreation)
            {
                RecordEquityTransactionSync(transaction, account, adviser);
                _db.SaveChanges();
            }

            else if (transaction is PropertyTransactionCreation)
            {
                RecordPropertyTransactionSync(transaction, account, adviser);
                _db.SaveChanges();
            }
            else if (transaction is MarginLendingTransactionCreation)
            {
                RecordMarginLendingTransaction(transaction, account);
                _db.SaveChanges();
            }
            else if (transaction is HomeLoanTransactionCreation)
            {
                RecordHomeLoanTransactionSync(transaction, account);
                _db.SaveChanges();
            }
            else if (transaction is Domain.Portfolio.Entities.CreationModels.Transaction.InsuranceTransactionCreation)
            {
                RecordInsuranceTransaction(transaction, account);
                _db.SaveChanges();
            }
            else
            {
                throw new NotSupportedException(
                    "Unknown transaction type");
            }
        }


        private void RecordInsuranceTransaction(TransactionCreationBase transaction, Account account)
        {
            var insurance = (Domain.Portfolio.Entities.CreationModels.Transaction.InsuranceTransactionCreation)transaction;
            _db.InsuranceTransactions.Add(new Edis.Db.Liabilities.InsuranceTransaction()
            {
                Id = Guid.NewGuid().ToString(),
                AccountId = account.AccountId,
                CreatedOn = DateTime.Now,
                Account = account,
                ExpiryDate = insurance.ExpiryDate,
                IsAcquire = insurance.IsAcquire,
                PolicyNumber = insurance.PolicyNumber,
                NameOfPolicy = insurance.NameOfPolicy,
                PolicyType = insurance.PolicyType,
                AmountInsured = insurance.AmountInsured,
                EntitiesInsured = insurance.EntitiesInsured,
                InsuranceType = insurance.InsuranceType,
                Issuer = insurance.Issuer,
                PolicyAddress = insurance.PolicyAddress,
                Premium = insurance.Premium,
                PurchasedOn = insurance.GrantedOn
            });
        }

        private async Task RecordHomeLoanTransaction(TransactionCreationBase transaction, Account account)
        {
            var homeLoan = (HomeLoanTransactionCreation)transaction;
            MortgageHomeLoanTransaction tHomeloan = new MortgageHomeLoanTransaction()
            {
                AccountId = account.AccountId,
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                LoanExpiryDate = homeLoan.ExpiryDate,
                LoanRepaymentType = homeLoan.LoanRepaymentType,
                IsAcquire = homeLoan.IsAcquire,
                LoanAmount = homeLoan.LoanAmount,
                Account = account,
                TypeOfMortgageRates = homeLoan.TypeOfMortgageRates,
                InterestRate = new List<LiabilityRate>()
                {
                    new LiabilityRate()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = DateTime.Now,
                        EffectiveFrom = homeLoan.GrantedOn,
                        Rate = homeLoan.LoanRate
                    }
                },
                LoanAquiredOn = homeLoan.GrantedOn,
                Institution = homeLoan.Institution,
                CorrespondingProperty = _db.Properties.Local.SingleOrDefault(p => p.PropertyId == homeLoan.PropertyId) ??
                                        await
                                            _db.Properties.Where(p => p.PropertyId == homeLoan.PropertyId).FirstOrDefaultAsync(),
                PropertyId = homeLoan.PropertyId
            };
            _db.MortgageHomeLoanTransactions.Add(tHomeloan);
        }


        private void RecordHomeLoanTransactionSync(TransactionCreationBase transaction, Account account)          //added
        {
            var homeLoan = (HomeLoanTransactionCreation)transaction;
            MortgageHomeLoanTransaction tHomeloan = new MortgageHomeLoanTransaction()
            {
                AccountId = account.AccountId,
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                LoanExpiryDate = homeLoan.ExpiryDate,
                LoanRepaymentType = homeLoan.LoanRepaymentType,
                IsAcquire = homeLoan.IsAcquire,
                LoanAmount = homeLoan.LoanAmount,
                Account = account,
                TypeOfMortgageRates = homeLoan.TypeOfMortgageRates,
                InterestRate = new List<LiabilityRate>()
                {
                    new LiabilityRate()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = DateTime.Now,
                        EffectiveFrom = homeLoan.GrantedOn,
                        Rate = homeLoan.LoanRate
                    }
                },
                LoanAquiredOn = homeLoan.GrantedOn,
                Institution = homeLoan.Institution,
                CorrespondingProperty = _db.Properties.Local.SingleOrDefault(p => p.PropertyId == homeLoan.PropertyId) ??
                                            _db.Properties.Where(p => p.PropertyId == homeLoan.PropertyId).FirstOrDefault(),
                PropertyId = homeLoan.PropertyId
            };
            _db.MortgageHomeLoanTransactions.Add(tHomeloan);
        }

        private void RecordMarginLendingTransaction(TransactionCreationBase transaction, Account account)
        {
            var marginLending = (MarginLendingTransactionCreation)transaction;
            var marginLendingTransaction = new Edis.Db.Liabilities.MarginLendingTransaction()
            {
                AccountId = account.AccountId,
                ExpiryDate = marginLending.ExpiryDate,
                LoanAmount = marginLending.LoanAmount,
                Account = account,
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                GrantedOn = marginLending.GrantedOn,
                IsAcquire = marginLending.IsAcquire,
                AssetId = marginLending.AssetId,
                AssetTypes = marginLending.AssetTypes,
                Ratio = marginLending.Ratio,
                EquityTransactionId = marginLending.EquityTransactionId,

                LiabilityRates = new List<LiabilityRate>()
                {
                    new LiabilityRate()
                    {
                        EffectiveFrom = marginLending.GrantedOn,
                        Rate = marginLending.InterestRate,
                        CreatedOn = DateTime.Now,
                        Id = Guid.NewGuid().ToString()
                    }
                },
                //LoanValueRatios = new List<LoanValueRatio>()
            };
            //foreach (var marginLendingLoanValueRatio in marginLending.Ratios)
            //{
            //    marginLendingTransaction.LoanValueRatios.Add(new LoanValueRatio()
            //    {
            //        Ticker = marginLendingLoanValueRatio.AssetId,
            //        Id = Guid.NewGuid().ToString(),
            //        AssetTypes = marginLendingLoanValueRatio.AssetTypes,
            //        MaxRatio = marginLendingLoanValueRatio.Ratio,
            //        CreatedOn = DateTime.Now,
            //        ActiveDate = marginLendingLoanValueRatio.EffectiveFrom
            //    });
            //}
            _db.MarginLendingTransactions.Add(marginLendingTransaction);
        }

        public async Task<ClientAccount> CreateNewClientAccount(string clientNumber, string notes, AccountType accountType)
        {
            var accountNumber = "";

            var client = await _db.Clients.FirstOrDefaultAsync(c => c.ClientNumber == clientNumber);
            if (client == null)
            {
                ProfileCannotBefound(clientNumber, DateTime.Now, "Client");
            }
            accountNumber = GenerateUniqueAccountNumber();
            client.Accounts.Add(new Account
            {
                CreatedOn = DateTime.Now,
                AccountNumber = accountNumber,
                AccountId = Guid.NewGuid().ToString(),
                EquityTransactions = new List<EquityTransaction>(),
                BondTransactions = new List<BondTransaction>(),
                EquityPayments = new List<Dividend>(),
                CashTransactions = new List<CashTransaction>(),
                PropertyTransactions = new List<PropertyTransaction>(),
                FixedIncomePayments = new List<CouponPayment>(),
                CashAndTermDepositPayments = new List<Interest>(),
                DirectPropertyPayments = new List<Rental>(),
                AccountType = accountType,
                AccountInfo = notes
            });
            await _db.SaveChangesAsync();

            return await GetClientAccount(accountNumber, DateTime.Now);
        }


        public ClientAccount CreateNewClientAccountSync(string clientNumber, string notes, AccountType accountType, string marginLenderId)     //added
        {
            var accountNumber = "";

            var client = _db.Clients.FirstOrDefault(c => c.ClientId == clientNumber);
            if (client == null)
            {
                ProfileCannotBefound(clientNumber, DateTime.Now, "Client");
            }
            accountNumber = GenerateUniqueAccountNumber();
            client.Accounts.Add(new Account
            {
                CreatedOn = DateTime.Now,
                AccountNumber = accountNumber,
                EquityTransactions = new List<EquityTransaction>(),
                AccountId = Guid.NewGuid().ToString(),
                BondTransactions = new List<BondTransaction>(),
                EquityPayments = new List<Dividend>(),
                CashTransactions = new List<CashTransaction>(),
                PropertyTransactions = new List<PropertyTransaction>(),
                FixedIncomePayments = new List<CouponPayment>(),
                CashAndTermDepositPayments = new List<Interest>(),
                DirectPropertyPayments = new List<Rental>(),
                AccountType = accountType,
                AccountInfo = notes,
                MarginLenderId = marginLenderId
            });
            _db.SaveChanges();

            return GetClientAccountSync(accountNumber, DateTime.Now);
        }


        public async Task<GroupAccount> CreateNewClientGroupAccount(string clientGroupNumber, string notes, AccountType accountType)
        {
            var groupNumber = "";

            var group = await _db.ClientGroups.FirstOrDefaultAsync(c => c.ClientGroupId == clientGroupNumber);
            if (group == null)
            {
                ProfileCannotBefound(clientGroupNumber, DateTime.Now, "Client Group");
            }
            groupNumber = GenerateUniqueAccountNumber();
            group.GroupAccounts.Add(new Account
            {
                CreatedOn = DateTime.Now,
                AccountNumber = groupNumber,
                EquityTransactions = new List<EquityTransaction>(),
                AccountId = Guid.NewGuid().ToString(),
                BondTransactions = new List<BondTransaction>(),
                EquityPayments = new List<Dividend>(),
                CashTransactions = new List<CashTransaction>(),
                PropertyTransactions = new List<PropertyTransaction>(),
                FixedIncomePayments = new List<CouponPayment>(),
                CashAndTermDepositPayments = new List<Interest>(),
                DirectPropertyPayments = new List<Rental>(),
                AccountType = accountType,
                AccountInfo = notes
            });
            await _db.SaveChangesAsync();

            return await GetClientGroupAccount(groupNumber, DateTime.Now);
        }

        public GroupAccount CreateNewClientGroupAccountSync(string clientGroupNumber, string notes, AccountType accountType, string marginLenderId)        //added
        {
            var groupNumber = "";

            var group = _db.ClientGroups.FirstOrDefault(c => c.ClientGroupId == clientGroupNumber);
            if (group == null)
            {
                ProfileCannotBefound(clientGroupNumber, DateTime.Now, "Client Group");
            }
            groupNumber = GenerateUniqueAccountNumber();
            group.GroupAccounts.Add(new Account
            {
                CreatedOn = DateTime.Now,
                AccountNumber = groupNumber,
                EquityTransactions = new List<EquityTransaction>(),
                AccountId = Guid.NewGuid().ToString(),
                BondTransactions = new List<BondTransaction>(),
                EquityPayments = new List<Dividend>(),
                CashTransactions = new List<CashTransaction>(),
                PropertyTransactions = new List<PropertyTransaction>(),
                FixedIncomePayments = new List<CouponPayment>(),
                CashAndTermDepositPayments = new List<Interest>(),
                DirectPropertyPayments = new List<Rental>(),
                AccountType = accountType,
                AccountInfo = notes,
                MarginLenderId = marginLenderId
            });
            _db.SaveChanges();

            return GetClientGroupAccountSync(groupNumber, DateTime.Now);
        }

        public async Task RecordConsultancyFee(ConsultancyFeeRecordCreation fee)
        {
            var account = await _db.Accounts.Where(ac => ac.AccountNumber == fee.AccountNumber).SingleOrDefaultAsync();
            var adviser =
                await _db.Advisers.Where(ad => ad.AdviserNumber == fee.AdviserNumber).SingleOrDefaultAsync();
            if (account == null)
            {
                ProfileCannotBefound(fee.AccountNumber, DateTime.Now, "Account");
            }
            if (adviser == null)
            {
                ProfileCannotBefound(fee.AdviserNumber, DateTime.Now, "Adviser");
            }

            var expense = new ConsultancyExpense
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                Account = account,
                Amount = fee.Amount,
                AdviserId = adviser.AdviserId,
                AccountId = account.AccountId,
                IncurredOn = fee.IncurredOn,
                ConsultancyExpenseType = fee.ConsultancyExpenseType,
                Notes = fee.Notes
            };
            _db.ConsultancyExpenses.Add(expense);
            await _db.SaveChangesAsync();
        }

        public void RecordConsultancyFeeSync(ConsultancyFeeRecordCreation fee)
        {
            var account = _db.Accounts.Where(ac => ac.AccountNumber == fee.AccountNumber).SingleOrDefault();
            var adviser = _db.Advisers.Where(ad => ad.AdviserNumber == fee.AdviserNumber).SingleOrDefault();
            if (account == null)
            {
                ProfileCannotBefound(fee.AccountNumber, DateTime.Now, "Account");
            }
            if (adviser == null)
            {
                ProfileCannotBefound(fee.AdviserNumber, DateTime.Now, "Adviser");
            }

            var expense = new ConsultancyExpense
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                Account = account,
                Amount = fee.Amount,
                AdviserId = adviser.AdviserId,
                AccountId = account.AccountId,
                IncurredOn = fee.IncurredOn,
                ConsultancyExpenseType = fee.ConsultancyExpenseType,
                Notes = fee.Notes
            };
            _db.ConsultancyExpenses.Add(expense);
            _db.SaveChanges();
        }

        public async Task RecordIncome(IncomeCreationBase income)
        {
            var account = await _db.Accounts.Where(ac => ac.AccountNumber == income.AccountNumber)
                .Include(a => a.FixedIncomePayments)
                .Include(a => a.CashAndTermDepositPayments)
                .Include(a => a.DirectPropertyPayments)
                .Include(a => a.EquityPayments)
                .SingleOrDefaultAsync();
            if (account == null)
            {
                throw new Exception("Cannot find account " + income.AccountNumber + " and cannot record income");
            }
            if (income is CouponPaymentCreation)
            {
                var record = (CouponPaymentCreation)income;
                var couponPayment = new CouponPayment
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.Now,
                    Amount = record.Amount,
                    Bond = await _db.Bonds.Where(b => b.Ticker == record.Ticker).SingleOrDefaultAsync(),
                    PaymentOn = record.PaymentOn
                };
                _db.CouponPayments.Add(couponPayment);
                account.FixedIncomePayments.Add(couponPayment);
                await _db.SaveChangesAsync();
            }
            else if (income is DividendPaymentCreation)
            {
                var record = (DividendPaymentCreation)income;
                var dividend = new Dividend
                {
                    PaymentOn = record.PaymentOn,
                    Id = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.Now,
                    Amount = record.Amount,
                    FrankingCredit = record.Franking,
                    Equity = await _db.Equities.Where(e => e.Ticker == record.Ticker).FirstOrDefaultAsync()
                };
                _db.Dividends.Add(dividend);
                account.EquityPayments.Add(dividend);
                await _db.SaveChangesAsync();
            }
            else if (income is InterestPaymentCreation)
            {
                var record = (InterestPaymentCreation)income;
                var interest = new Interest
                {
                    Amount = record.Amount,
                    Id = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.Now,
                    CashAccount =
                        await _db.CashAccounts.Where(c => c.Id == record.CashAccountId).FirstOrDefaultAsync(),
                    PaymentOn = record.PaymentOn
                };
                _db.Interests.Add(interest);
                account.CashAndTermDepositPayments.Add(interest);
                await _db.SaveChangesAsync();
            }
            else if (income is RentalPaymentCreation)
            {
                var record = (RentalPaymentCreation)income;
                var rental = new Rental
                {
                    CreatedOn = DateTime.Now,
                    PaymentOn = record.PaymentOn,
                    Id = Guid.NewGuid().ToString(),
                    Amount = record.Amount,
                    PropertyAddress =
                        await _db.Properties.Where(p => p.PropertyId == record.PropertyId).FirstOrDefaultAsync()
                };
                _db.Rentals.Add(rental);
                account.DirectPropertyPayments.Add(rental);
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new NotSupportedException(
                    "Income recording currently only support rental, dividend, interest and coupon");
            }
        }

        public void RecordIncomeSync(IncomeCreationBase income)                   //added
        {
            var account = _db.Accounts.Where(ac => ac.AccountNumber == income.AccountNumber)
                .Include(a => a.FixedIncomePayments)
                .Include(a => a.CashAndTermDepositPayments)
                .Include(a => a.DirectPropertyPayments)
                .Include(a => a.EquityPayments)
                .SingleOrDefault();
            if (account == null)
            {
                throw new Exception("Cannot find account " + income.AccountNumber + " and cannot record income");
            }
            if (income is CouponPaymentCreation)
            {
                var record = (CouponPaymentCreation)income;
                var couponPayment = new CouponPayment
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.Now,
                    Amount = record.Amount,
                    Bond = _db.Bonds.Where(b => b.Ticker == record.Ticker).SingleOrDefault(),
                    PaymentOn = record.PaymentOn
                };
                _db.CouponPayments.Add(couponPayment);
                account.FixedIncomePayments.Add(couponPayment);
                _db.SaveChanges();
            }
            else if (income is DividendPaymentCreation)
            {
                var record = (DividendPaymentCreation)income;
                var dividend = new Dividend
                {
                    PaymentOn = record.PaymentOn,
                    Id = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.Now,
                    Amount = record.Amount,
                    FrankingCredit = record.Franking,
                    Equity = _db.Equities.Where(e => e.Ticker == record.Ticker).FirstOrDefault()
                };
                _db.Dividends.Add(dividend);
                account.EquityPayments.Add(dividend);
                _db.SaveChanges();
            }
            else if (income is InterestPaymentCreation)
            {
                var record = (InterestPaymentCreation)income;
                var interest = new Interest
                {
                    Amount = record.Amount,
                    Id = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.Now,
                    CashAccount =
                        _db.CashAccounts.Where(c => c.Id == record.CashAccountId).FirstOrDefault(),
                    PaymentOn = record.PaymentOn
                };
                _db.Interests.Add(interest);
                account.CashAndTermDepositPayments.Add(interest);
                _db.SaveChanges();
            }
            else if (income is RentalPaymentCreation)
            {
                var record = (RentalPaymentCreation)income;
                var rental = new Rental
                {
                    CreatedOn = DateTime.Now,
                    PaymentOn = record.PaymentOn,
                    Id = Guid.NewGuid().ToString(),
                    Amount = record.Amount,
                    PropertyAddress =
                        _db.Properties.Where(p => p.PropertyId == record.PropertyId).FirstOrDefault()
                };
                _db.Rentals.Add(rental);
                account.DirectPropertyPayments.Add(rental);
                _db.SaveChanges();
            }
            else
            {
                throw new NotSupportedException(
                    "Income recording currently only support rental, dividend, interest and coupon");
            }
        }


        public Adviser GetAdviserForClient(string clientUserId)
        {
            var client = _db.Clients.FirstOrDefault(c => c.ClientNumber == clientUserId);
            var clientGroup = _db.ClientGroups.FirstOrDefault(g => g.ClientGroupId == client.ClientGroupId);
            var adviser = _db.Advisers.FirstOrDefault(a => a.AdviserId == clientGroup.Adviser.AdviserId);
            return new Adviser(this)
            {
                Id = clientGroup.Adviser.AdviserId,
                FirstName = adviser.FirstName,
                LastName = adviser.LastName,
                AdviserNumber = clientGroup.Adviser.AdviserNumber
            };
        }


        public List<ClientGroup> GetAllClientGroupsForAdviserSync(string adviserNumber, DateTime todate)
        {
            var groups =
                _db.ClientGroups.Local.Any(
                    g => g.Adviser.AdviserNumber == adviserNumber && g.CreatedOn.HasValue && g.CreatedOn.Value <= todate)
                    ? _db.ClientGroups.Local.Where(
                        g =>
                            g.Adviser.AdviserNumber == adviserNumber && g.CreatedOn.HasValue &&
                            g.CreatedOn.Value <= todate).ToList()
                    : _db.ClientGroups
                        .Where(
                            g =>
                                g.Adviser.AdviserNumber == adviserNumber && g.CreatedOn.HasValue &&
                                g.CreatedOn.Value <= todate).ToList();

            var result = new List<ClientGroup>();
            foreach (var clientGroup in groups)
            {
                result.Add(GetClientGroupProfileSync(clientGroup.ClientGroupId, todate));
            }
            return result;
        }


        public async Task<List<ClientGroup>> GetAllClientGroupsForAdviser(string adviserNumber, DateTime todate)
        {
            var groups =
                _db.ClientGroups.Local.Any(
                    g => g.Adviser.AdviserNumber == adviserNumber && g.CreatedOn.HasValue && g.CreatedOn.Value <= todate)
                    ? _db.ClientGroups.Local.Where(
                        g =>
                            g.Adviser.AdviserNumber == adviserNumber && g.CreatedOn.HasValue &&
                            g.CreatedOn.Value <= todate).ToList()
                    : await _db.ClientGroups
                        .Where(
                            g =>
                                g.Adviser.AdviserNumber == adviserNumber && g.CreatedOn.HasValue &&
                                g.CreatedOn.Value <= todate).ToListAsync();

            var result = new List<ClientGroup>();
            foreach (var clientGroup in groups)
            {
                result.Add(await GetClientGroupProfile(clientGroup.ClientGroupId, todate));
            }
            return result;
        }

        public List<ClientAccount> getAllClientAccountsForAdviser(string adviserNumber, DateTime toDate)
        {                        //added

            Adviser adviser = GetAdviserSync(adviserNumber, toDate);
            List<ClientGroup> clientGroups = GetAllClientGroupsForAdviserSync(adviserNumber, toDate);

            List<Client> clients = new List<Client>();
            foreach (var clientGroup in clientGroups)
            {
                clients.AddRange(clientGroup.GetClientsSync());
            }

            List<ClientAccount> clientAccounts = new List<ClientAccount>();
            foreach (var client in clients)
            {
                clientAccounts.AddRange(client.GetAccountsSync());
            }
            return clientAccounts;
        }

        public List<GroupAccount> getAllClientGroupAccountsForAdviser(string adviserNumber, DateTime toDate)
        {                        //added

            Adviser adviser = GetAdviserSync(adviserNumber, toDate);
            List<ClientGroup> clientGroups = GetAllClientGroupsForAdviserSync(adviserNumber, toDate);


            List<GroupAccount> groupAccounts = new List<GroupAccount>();
            foreach (var group in clientGroups)
            {
                groupAccounts.AddRange(group.GetAccountsSync());
            }
            return groupAccounts;
        }

        public async Task<List<ClientAccount>> GetAccountsForClient(string clientNumber, DateTime toDate, AccountType accountType)
        {
            var client =
                await
                    _db.Clients.Where(
                        c => c.ClientNumber == clientNumber && c.CreatedOn.HasValue && c.CreatedOn.Value <= toDate)
                        .Include(c => c.Accounts)
                        .SingleOrDefaultAsync();
            if (client == null)
            {
                ProfileCannotBefound(clientNumber, toDate, "Client");
            }
            var result = new List<ClientAccount>();
            foreach (var account in client.Accounts.Where(acc => acc.AccountType == accountType))
            {
                result.Add(await GetClientAccount(account.AccountNumber, toDate));
            }
            return result;
        }

        public List<ClientAccount> GetAccountsForClientSync(string clientNumber, DateTime toDate, AccountType accountType)      //added
        {
            var client =
                    _db.Clients.Where(
                        c => c.ClientNumber == clientNumber && c.CreatedOn.HasValue && c.CreatedOn.Value <= toDate)
                        .Include(c => c.Accounts)
                        .SingleOrDefault();
            if (client == null)
            {
                ProfileCannotBefound(clientNumber, toDate, "Client");
            }
            var result = new List<ClientAccount>();
            //foreach (var account in client.Accounts.Where(acc => acc.AccountType == accountType))                                 //..........................................Account Type changed
            //{
            //    result.Add(GetClientAccountSync(account.AccountNumber, toDate));
            //}
            foreach (var account in client.Accounts) {
                result.Add(GetClientAccountSync(account.AccountNumber, toDate));
            }

            return result;
        }

        public List<ClientAccount> GetAccountsForClientSync(string clientNumber, DateTime toDate)      //added
        {
            var client =
                    _db.Clients.Where(
                        c => c.ClientNumber == clientNumber && c.CreatedOn.HasValue && c.CreatedOn.Value <= toDate)
                        .Include(c => c.Accounts)
                        .SingleOrDefault();
            if (client == null)
            {
                ProfileCannotBefound(clientNumber, toDate, "Client");
            }
            var result = new List<ClientAccount>();
            foreach (var account in client.Accounts)
            {
                result.Add(GetClientAccountSync(account.AccountNumber, toDate));
            }
            return result;
        }

        public async Task<List<GroupAccount>> GetAccountsForClientGroup(string clientGroupNumber, DateTime toDate, AccountType accountType)
        {
            var group = _db.ClientGroups.Local
                        .FirstOrDefault(g => g.GroupNumber == clientGroupNumber && g.CreatedOn.HasValue && g.CreatedOn.Value <= toDate) ??
                await
                    _db.ClientGroups.Where(
                        g =>
                            g.GroupNumber == clientGroupNumber && g.CreatedOn.HasValue && g.CreatedOn.Value <= toDate)
                        .Include(c => c.GroupAccounts)
                        .FirstOrDefaultAsync();
            var result = new List<GroupAccount>();
            foreach (var groupAccount in group.GroupAccounts.Where(acc => acc.AccountType == accountType))
            {
                result.Add(await GetClientGroupAccount(groupAccount.AccountNumber, toDate));
            }
            return result;
        }

        public ClientGroup getClientGroupByGroupId(string groupId)
        {
            var group = _db.ClientGroups.FirstOrDefault(g => g.ClientGroupId == groupId);
            ClientGroup clientGroup = new ClientGroup(this)
            {
                Id = group.ClientGroupId,
                ClientGroupNumber = group.GroupNumber,
                CreatedOn = group.CreatedOn,
                GroupAmount = group.GroupAmount,
                GroupName = group.GroupName,
                MainClientId = group.MainClientId
            };
            return clientGroup;
        }

        public List<GroupAccount> GetAccountsForClientGroupSync(string clientGroupNumber, DateTime toDate, AccountType accountType)         //added
        {
            var group = _db.ClientGroups.Local
                        .FirstOrDefault(g => g.GroupNumber == clientGroupNumber && g.CreatedOn.HasValue && g.CreatedOn.Value <= toDate) ??
                    _db.ClientGroups.Where(
                        g =>
                            g.GroupNumber == clientGroupNumber && g.CreatedOn.HasValue && g.CreatedOn.Value <= toDate)
                        .Include(c => c.GroupAccounts)
                        .FirstOrDefault();
            var result = new List<GroupAccount>();
            //foreach (var groupAccount in group.GroupAccounts.Where(acc => acc.AccountType == accountType))
            //{
            //    result.Add(GetClientGroupAccountSync(groupAccount.AccountNumber, toDate));
            //}
            foreach (var account in group.GroupAccounts)
            {
                result.Add(GetClientGroupAccountSync(account.AccountNumber, toDate));
            }


            return result;
        }


        public List<GroupAccount> GetAccountsForClientGroupSync(string clientGroupNumber, DateTime toDate)         //added
        {
            var group = _db.ClientGroups.Local
                        .FirstOrDefault(g => g.GroupNumber == clientGroupNumber && g.CreatedOn.HasValue && g.CreatedOn.Value <= toDate) ??
                    _db.ClientGroups.Where(
                        g =>
                            g.GroupNumber == clientGroupNumber && g.CreatedOn.HasValue && g.CreatedOn.Value <= toDate)
                        .Include(c => c.GroupAccounts)
                        .FirstOrDefault();
            var result = new List<GroupAccount>();
            foreach (var groupAccount in group.GroupAccounts)
            {
                result.Add(GetClientGroupAccountSync(groupAccount.AccountNumber, toDate));
            }
             return result;
        }

        public List<GroupAccount> GetAccountsForClientGroupByIdSync(string clientGroupId, DateTime toDate)         //added
        {
            var group = _db.ClientGroups.Local
                        .FirstOrDefault(g => g.ClientGroupId == clientGroupId && g.CreatedOn.HasValue && g.CreatedOn.Value <= toDate) ??
                    _db.ClientGroups.Where(
                        g =>
                            g.ClientGroupId == clientGroupId && g.CreatedOn.HasValue && g.CreatedOn.Value <= toDate)
                        .Include(c => c.GroupAccounts)
                        .FirstOrDefault();
            var result = new List<GroupAccount>();
            foreach (var groupAccount in group.GroupAccounts)
            {
                result.Add(GetClientGroupAccountSync(groupAccount.AccountNumber, toDate));
               // result.Add(GetClientGroupAccountSync());
            }
            return result;
        }


        public async Task<List<Client>> GetClientsForGroup(string clientGroupNumber, DateTime toDate)
        {
            var group =
                await _db.ClientGroups.Where(
                    g => g.GroupNumber == clientGroupNumber && g.CreatedOn.HasValue && g.CreatedOn.Value <= toDate)
                    .SingleOrDefaultAsync();
            if (group == null)
            {
                ProfileCannotBefound(clientGroupNumber, toDate, "Client Group");
            }
            var result = new List<Client>();
            foreach (var client in _db.Clients.Where(c => c.ClientGroupId == group.ClientGroupId))
            {
                result.Add(await GetClientProfile(client.ClientId, toDate));
            }
            return result;
        }
        public List<Client> GetClientsForGroupSync(string clientGroupNumber, DateTime toDate)
        {
            var group =
                 _db.ClientGroups.Where(
                    g => g.GroupNumber == clientGroupNumber && g.CreatedOn.HasValue && g.CreatedOn.Value <= toDate)
                    .SingleOrDefault();
            if (group == null)
            {
                ProfileCannotBefound(clientGroupNumber, toDate, "Client Group");
            }
            var result = new List<Client>();
            foreach (var client in _db.Clients.Where(c => c.ClientGroupId == group.ClientGroupId))
            {
                result.Add(GetClientProfileSync(client.ClientId, toDate));
            }
            return result;
        }

        public async Task<List<AssetBase>> GetAssetsForAccount(string accountNumber, DateTime beforeDate)
        {
            var dbAccount =
                _db.Accounts.Local
                        .FirstOrDefault(a => a.AccountNumber == accountNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= beforeDate) ??
                await
                    _db.Accounts.Where(
                        a => a.AccountNumber == accountNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= beforeDate)
                        .Include(a => a.BondTransactions.Select(b => b.Bond.Prices))
                        .Include(a => a.CashTransactions.Select(c => c.CashAccount))
                        .Include(a => a.EquityTransactions.Select(e => e.Equity.Prices))
                        .Include(a => a.BondTransactions.Select(b => b.Bond.CouponPayments))
                        .Include(a => a.PropertyTransactions.Select(p => p.PropertyAddress.Prices))
                        .FirstOrDefaultAsync();

            var result = new List<AssetBase>();

            if (dbAccount.EquityTransactions.Any())
            {
                result.AddRange(await GenerateAustralianEquityForAccount(dbAccount.AccountId, beforeDate, dbAccount));
                result.AddRange(await GenerateInternationalEquityForAccount(dbAccount.AccountId, beforeDate, dbAccount));
                result.AddRange(await GenerateManagedFundForAccount(dbAccount.AccountId, beforeDate, dbAccount));
            }


            if (dbAccount.CashTransactions.Any())
            {
                result.AddRange(GenerateCashAssetForAccount(dbAccount.AccountId, beforeDate, dbAccount));
            }

            if (dbAccount.BondTransactions.Any())
            {
                result.AddRange(await GenerateFixedIncomeForAccount(dbAccount.AccountId, beforeDate, dbAccount));
            }

            if (dbAccount.PropertyTransactions.Any())
            {
                result.AddRange(await GenerateDirectPropertyForAccount(dbAccount.AccountId, beforeDate, dbAccount));
            }

            return result;
        }

        public List<AssetBase> GetAssetsForAccountSync(string accountNumber, DateTime beforeDate)           //added
        {
            var dbAccount =
                _db.Accounts.Local
                        .FirstOrDefault(a => a.AccountNumber == accountNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= beforeDate) ??
                    _db.Accounts.Where(
                        a => a.AccountNumber == accountNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= beforeDate)
                        .Include(a => a.BondTransactions.Select(b => b.Bond.Prices))
                        .Include(a => a.CashTransactions.Select(c => c.CashAccount))
                        .Include(a => a.EquityTransactions.Select(e => e.Equity.Prices))
                        .Include(a => a.BondTransactions.Select(b => b.Bond.CouponPayments))
                        .Include(a => a.PropertyTransactions.Select(p => p.PropertyAddress.Prices))
                        .FirstOrDefault();

            var result = new List<AssetBase>();

            if (dbAccount.EquityTransactions.Any())
            {
                result.AddRange(GenerateAustralianEquityForAccountSync(dbAccount.AccountId, beforeDate, dbAccount));
                result.AddRange(GenerateInternationalEquityForAccountSync(dbAccount.AccountId, beforeDate, dbAccount));
                result.AddRange(GenerateManagedFundForAccountSync(dbAccount.AccountId, beforeDate, dbAccount));
            }


            if (dbAccount.CashTransactions.Any())
            {
                result.AddRange(GenerateCashAssetForAccount(dbAccount.AccountId, beforeDate, dbAccount));
            }

            if (dbAccount.BondTransactions.Any())
            {
                result.AddRange(GenerateFixedIncomeForAccountSync(dbAccount.AccountId, beforeDate, dbAccount));
            }

            if (dbAccount.PropertyTransactions.Any())
            {
                result.AddRange(GenerateDirectPropertyForAccountSync(dbAccount.AccountId, beforeDate, dbAccount));
            }

            return result;
        }

        public List<AssetBase> GetEquityAssetsForAccount(string accountNumber, DateTime beforeDate) {
            var dbAccount =
                           _db.Accounts.Local
                                   .FirstOrDefault(a => a.AccountNumber == accountNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= beforeDate) ??
                               _db.Accounts.Where(
                                   a => a.AccountNumber == accountNumber && a.CreatedOn.HasValue && a.CreatedOn.Value <= beforeDate)
                                   .Include(a => a.EquityTransactions.Select(e => e.Equity.Prices))
                                   .FirstOrDefault();
            var result = new List<AssetBase>();

            if (dbAccount.EquityTransactions.Any()) {
                result.AddRange(GenerateAustralianEquityForAccountSync(dbAccount.AccountId, beforeDate, dbAccount));
                result.AddRange(GenerateInternationalEquityForAccountSync(dbAccount.AccountId, beforeDate, dbAccount));
                result.AddRange(GenerateManagedFundForAccountSync(dbAccount.AccountId, beforeDate, dbAccount));
            }
            return result;
        }

        public async Task FeedResearchValueForBond(string key, double value, string ticker, string issuer)
        {

            var bond = _db.Bonds.Local.SingleOrDefault(eq => eq.Ticker == ticker) ??
                         await
                             _db.Bonds.Where(eq => eq.Ticker == ticker)
                                 .Include(eq => eq.ResearchValues)
                                 .FirstOrDefaultAsync();
            if (bond == null)
            {
                throw new Exception("Cannot find bond with ticker " + ticker);
            }

            if (bond.ResearchValues == null)
            {
                bond.ResearchValues = new List<ResearchValue>();
            }

            bond.ResearchValues.Add(new ResearchValue
            {
                Value = value,
                Id = Guid.NewGuid().ToString(),
                Key = key,
                CreatedOn = DateTime.Now,
                Issuer = issuer
            });
            await _db.SaveChangesAsync();
        }


        public void FeedResearchValueForBondSync(string key, double value, string ticker, string issuer)
        {

            var bond = _db.Bonds.Local.SingleOrDefault(eq => eq.Ticker == ticker) ??
                             _db.Bonds.Where(eq => eq.Ticker == ticker)
                                 .Include(eq => eq.ResearchValues)
                                 .FirstOrDefault();
            if (bond == null)
            {
                throw new Exception("Cannot find bond with ticker " + ticker);
            }

            if (bond.ResearchValues == null)
            {
                bond.ResearchValues = new List<ResearchValue>();
            }

            bond.ResearchValues.Add(new ResearchValue
            {
                Value = value,
                Id = Guid.NewGuid().ToString(),
                Key = key,
                CreatedOn = DateTime.Now,
                Issuer = issuer
            });
            _db.SaveChanges();
        }


        public async Task<double?> GetResearchValueForBond(string key, string ticker)
        {
            var bond =
                _db.Bonds.Local.SingleOrDefault(
                    eq => eq.Ticker == ticker) ??
                await
                    _db.Bonds.Where(eq => eq.Ticker == ticker)
                        .Include(eq => eq.ResearchValues)
                        .FirstOrDefaultAsync();
            if (bond == null)
            {
                throw new Exception("Cannot find equity with ticker " + ticker +
                                    " or no research value for this equity of key " + key +
                                    " is yet available from database");
            }
            return bond.ResearchValues == null || !bond.ResearchValues.Any() ? (double?)null :
                bond.ResearchValues.Where(v => v.Key == key)
                    .OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefault()
                    .Value;
        }


        public double? GetResearchValueForBondSync(string key, string ticker)
        {
            var bond =
                _db.Bonds.Local.SingleOrDefault(
                    eq => eq.Ticker == ticker) ??
                    _db.Bonds.Where(eq => eq.Ticker == ticker)
                        .Include(eq => eq.ResearchValues)
                        .FirstOrDefault();
            if (bond == null)
            {
                throw new Exception("Cannot find equity with ticker " + ticker +
                                    " or no research value for this equity of key " + key +
                                    " is yet available from database");
            }
            return bond.ResearchValues == null || !bond.ResearchValues.Any() ? (double?)null :
                bond.ResearchValues.Where(v => v.Key == key)
                    .OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefault()
                    .Value;
        }

        public async Task FeedResearchValueForEquity(string key, double value, string ticker, string issuer)
        {
            var equity = _db.Equities.Local.SingleOrDefault(eq => eq.Ticker == ticker) ??
                         await
                             _db.Equities.Where(eq => eq.Ticker == ticker)
                                 .Include(eq => eq.ResearchValues)
                                 .FirstOrDefaultAsync();
            if (equity == null)
            {
                throw new Exception("Cannot find equity with ticker " + ticker);
            }

            if (equity.ResearchValues == null)
            {
                equity.ResearchValues = new List<ResearchValue>();
            }

            equity.ResearchValues.Add(new ResearchValue
            {
                Value = value,
                Id = Guid.NewGuid().ToString(),
                Key = key,
                CreatedOn = DateTime.Now,
                Issuer = issuer
            });
            await _db.SaveChangesAsync();
        }


        public void FeedResearchValueForEquitySync(string key, double value, string ticker, string issuer)
        {
            var equity = _db.Equities.Local.SingleOrDefault(eq => eq.Ticker == ticker) ??
                             _db.Equities.Where(eq => eq.Ticker == ticker)
                                 .Include(eq => eq.ResearchValues)
                                 .FirstOrDefault();
            if (equity == null)
            {
                throw new Exception("Cannot find equity with ticker " + ticker);
            }

            if (equity.ResearchValues == null)
            {
                equity.ResearchValues = new List<ResearchValue>();
            }

            equity.ResearchValues.Add(new ResearchValue
            {
                Value = value,
                Id = Guid.NewGuid().ToString(),
                Key = key,
                CreatedOn = DateTime.Now,
                Issuer = issuer
            });
            _db.SaveChanges();
        }

        public List<Domain.Portfolio.AggregateRoots.Asset.Equity> GetAllEquities()
        {
            List<Domain.Portfolio.AggregateRoots.Asset.Equity> equities = new List<Domain.Portfolio.AggregateRoots.Asset.Equity>();

            foreach (var equity in _db.Equities.ToList())
            {
                Domain.Portfolio.AggregateRoots.Asset.Equity subEquity = null;
                switch (equity.EquityType)
                {
                    case EquityTypes.AustralianEquity:
                        subEquity = new AustralianEquity(this);
                        break;
                    case EquityTypes.InternationalEquity:
                        subEquity = new InternationalEquity(this);
                        break;
                    case EquityTypes.ManagedInvestments:
                        subEquity = new ManagedInvestment(this);
                        break;
                }
                subEquity.Id = equity.AssetId;
                subEquity.Ticker = equity.Ticker;
                subEquity.Name = equity.Name;
                subEquity.Sector = equity.Sector;
                subEquity.EquityType = equity.EquityType;
                
                equities.Add(subEquity);
            }
            return equities;
        }

        public async Task<double?> GetResearchValueForEquity(string key, string ticker)
        {
            var equity =
                _db.Equities.Local.SingleOrDefault(
                    eq => eq.Ticker == ticker) ??
                await
                    _db.Equities.Where(eq => eq.Ticker == ticker)
                        .Include(eq => eq.ResearchValues)
                        .FirstOrDefaultAsync();
            if (equity == null)
            {
                throw new Exception("Cannot find equity with ticker " + ticker +
                                    " or no research value for this equity of key " + key +
                                    " is yet available from database");
            }
            return equity.ResearchValues == null || !equity.ResearchValues.Any() ? (double?)null :
                equity.ResearchValues.Where(v => v.Key == key)
                    .OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefault()
                    .Value;
        }

        public double? GetResearchValueForEquitySync(string key, string ticker)             //added
        {
            var equity =
                _db.Equities.Local.SingleOrDefault(
                    eq => eq.Ticker == ticker) ??
                    _db.Equities.Where(eq => eq.Ticker == ticker)
                        .Include(eq => eq.ResearchValues)
                        .FirstOrDefault();
            if (equity == null)
            {
                throw new Exception("Cannot find equity with ticker " + ticker +
                                    " or no research value for this equity of key " + key +
                                    " is yet available from database");
            }

            var researchValue = equity.ResearchValues.Where(v => v.Key == key);

            return equity.ResearchValues == null || !equity.ResearchValues.Any() || researchValue.Count() == 0 ? (double?)null :
                researchValue.OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefault()
                    .Value;
                    
        }


        public string GetStringResearchValueForEquitySync(string key, string ticker)             //added
        {
            var equity =
                _db.Equities.Local.SingleOrDefault(
                    eq => eq.Ticker == ticker) ??
                    _db.Equities.Where(eq => eq.Ticker == ticker)
                        .Include(eq => eq.ResearchValues)
                        .FirstOrDefault();
            if (equity == null)
            {
                throw new Exception("Cannot find equity with ticker " + ticker +
                                    " or no research value for this equity of key " + key +
                                    " is yet available from database");
            }
            var researchValue = equity.ResearchValues.Where(v => v.Key == key);

            return equity.ResearchValues == null || !equity.ResearchValues.Any() || researchValue.Count() == 0 ? (string)null :
                researchValue.OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefault()
                    .StringValue;            // StringValue => Value.ToString()
        }

        public double? GetMaxRatio(string ticker, string lenderId) {
            MarginLender lender = _db.MarginLenders.Where(m => m.LenderId == lenderId).Include(m => m.Ratios).FirstOrDefault();

            if (lender != null) {
                var ratio = lender.Ratios.Where(r => r.Ticker == ticker).ToList();
                if (ratio.Count != 0) {
                    return ratio.OrderByDescending(r => r.CreatedOn).FirstOrDefault().MaxRatio;
                }
            }
            return null;
        }


        public List<MarginLenderPasser> GetAllMarginLenders() {
            var lenders = _db.MarginLenders.ToList();
            List<MarginLenderPasser> results = new List<MarginLenderPasser>();

            lenders.ForEach(l => {
                results.Add(new MarginLenderPasser {
                    LenderId = l.LenderId,
                    LenderName = l.LenderName
                });
            });
            return results;
        }

        //public double? GetResearchValue(string key, string ) {
        //    var researchValues = _db.ResearchValues.Where(r => r.Key == key).ToList();

        //    if (researchValues.Count == 0 || researchValues == null) {
        //        return null;
        //    } else {
        //        return researchValues.OrderByDescending(r => r.CreatedOn).SingleOrDefault().Value;
        //    }
        //}



        public async Task FeedResearchValueForProperty(string key, double value, string propertyId, string issuer)
        {
            var property = _db.Properties.Local.SingleOrDefault(p => p.PropertyId == propertyId) ??
                           await
                               _db.Properties.Where(p => p.PropertyId == propertyId)
                                   .Include(p => p.ResearchValues)
                                   .FirstOrDefaultAsync();
            if (property == null)
            {
                throw new Exception("Cannot find property with property id " + propertyId);
            }

            if (property.ResearchValues == null)
            {
                property.ResearchValues = new List<ResearchValue>();
            }

            property.ResearchValues.Add(new ResearchValue
            {
                Value = value,
                Id = Guid.NewGuid().ToString(),
                Key = key,
                CreatedOn = DateTime.Now,
                Issuer = issuer
            });
            await _db.SaveChangesAsync();
        }


        public void FeedResearchValueForPropertySync(string key, double value, string propertyId, string issuer)
        {
            var property = _db.Properties.Local.SingleOrDefault(p => p.PropertyId == propertyId) ??
                               _db.Properties.Where(p => p.PropertyId == propertyId)
                                   .Include(p => p.ResearchValues)
                                   .FirstOrDefault();
            if (property == null)
            {
                throw new Exception("Cannot find property with property id " + propertyId);
            }

            if (property.ResearchValues == null)
            {
                property.ResearchValues = new List<ResearchValue>();
            }

            property.ResearchValues.Add(new ResearchValue
            {
                Value = value,
                Id = Guid.NewGuid().ToString(),
                Key = key,
                CreatedOn = DateTime.Now,
                Issuer = issuer
            });
            _db.SaveChanges();
        }

        public async Task<double?> GetResearchValueForProperty(string key, string propertyId)
        {
            var property =
                _db.Properties.Local.SingleOrDefault(p => p.PropertyId == propertyId) ??
                await
                    _db.Properties.Where(p => p.PropertyId == propertyId)
                        .Include(p => p.ResearchValues)
                        .FirstOrDefaultAsync();
            if (property == null)
            {
                throw new Exception("Cannot find property with id " + propertyId +
                                    " or no research value for this equity of key " + key +
                                    " is yet available from database");
            }

            return property.ResearchValues == null || !property.ResearchValues.Any() ? (double?)null :
                property.ResearchValues.Where(v => v.Key == key)
                    .OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefault()
                    .Value;
        }


        public double? GetResearchValueForPropertySync(string key, string propertyId)
        {
            var property =
                _db.Properties.Local.SingleOrDefault(p => p.PropertyId == propertyId) ??
                    _db.Properties.Where(p => p.PropertyId == propertyId)
                        .Include(p => p.ResearchValues)
                        .FirstOrDefault();
            if (property == null)
            {
                throw new Exception("Cannot find property with id " + propertyId +
                                    " or no research value for this equity of key " + key +
                                    " is yet available from database");
            }

            return property.ResearchValues == null || !property.ResearchValues.Any() ? (double?)null :
                property.ResearchValues.Where(v => v.Key == key)
                    .OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefault()
                    .Value;
        }

        public async Task<string> GetLatestIssuerForEquityResearchValue(string key, string ticker)
        {
            var equity =
                _db.Equities.Local.SingleOrDefault(
                    eq => eq.Ticker == ticker && eq.ResearchValues != null) ??
                await
                    _db.Equities.Where(eq => eq.Ticker == ticker)
                        .Include(eq => eq.ResearchValues)
                        .FirstOrDefaultAsync();
            if (equity == null)
            {
                throw new Exception("Cannot find equity with ticker " + ticker +
                                    " or no research value for this equity of key " + key +
                                    " is yet available from database");
            }
            return equity.ResearchValues == null || !equity.ResearchValues.Any() ? "" :
                equity.ResearchValues.Where(v => v.Key == key)
                    .OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefault()
                    .Issuer;
        }


        public string GetLatestIssuerForEquityResearchValueSync(string key, string ticker)
        {
            var equity =
                _db.Equities.Local.SingleOrDefault(
                    eq => eq.Ticker == ticker && eq.ResearchValues != null) ??
                    _db.Equities.Where(eq => eq.Ticker == ticker)
                        .Include(eq => eq.ResearchValues)
                        .FirstOrDefault();
            if (equity == null)
            {
                throw new Exception("Cannot find equity with ticker " + ticker +
                                    " or no research value for this equity of key " + key +
                                    " is yet available from database");
            }
            return equity.ResearchValues == null || !equity.ResearchValues.Any() ? "" :
                equity.ResearchValues.Where(v => v.Key == key)
                    .OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefault()
                    .Issuer;
        }




        public async Task<string> GetLatestIssuerForBondResearchValue(string key, string ticker)
        {
            var bond =
                _db.Bonds.Local.SingleOrDefault(
                    eq => eq.Ticker == ticker && eq.ResearchValues != null) ??
                await
                    _db.Bonds.Where(eq => eq.Ticker == ticker)
                        .Include(eq => eq.ResearchValues)
                        .FirstOrDefaultAsync();
            if (bond == null)
            {
                throw new Exception("Cannot find bond with ticker " + ticker +
                                    " or no research value for this equity of key " + key +
                                    " is yet available from database");
            }
            return bond.ResearchValues == null || !bond.ResearchValues.Any() ? "" :
                bond.ResearchValues.Where(v => v.Key == key)
                    .OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefault()
                    .Issuer;
        }


        public string GetLatestIssuerForBondResearchValueSync(string key, string ticker)
        {
            var bond =
                _db.Bonds.Local.SingleOrDefault(
                    eq => eq.Ticker == ticker && eq.ResearchValues != null) ??
                    _db.Bonds.Where(eq => eq.Ticker == ticker)
                        .Include(eq => eq.ResearchValues)
                        .FirstOrDefault();
            if (bond == null)
            {
                throw new Exception("Cannot find bond with ticker " + ticker +
                                    " or no research value for this equity of key " + key +
                                    " is yet available from database");
            }
            return bond.ResearchValues == null || !bond.ResearchValues.Any() ? "" :
                bond.ResearchValues.Where(v => v.Key == key)
                    .OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefault()
                    .Issuer;
        }


        public async Task<Ratios> GetAsx200AverageRatios(EquityTypes type)
        {

            var ratios = new Ratios()
            {
                PriceEarningRatio = 0,
                CurrentRatio = 0,
                Frank = 0,
                FiveYearTrackingErrorRatio = 0,
                ReturnOnEquity = 0,
                ReturnOnAsset = 0,
                QuickRatio = 0,
                GlobalCategory = 0,
                Beta = 0,
                Capitalisation = 0,
                FiveYearSkewnessRatio = 0,
                DividendYield = 0,
                DebtEquityRatio = 0,
                PayoutRatio = 0,
                EarningsStability = 0,
                FiveYearInformation = 0,
                ThreeYearReturn = 0,
                InterestCover = 0,
                FiveYearSharpRatio = 0,
                FiveYearReturn = 0,
                FundSize = 0,
                BetaFiveYears = 0,
                FiveYearStandardDeviation = 0,
                OneYearReturn = 0,
                FiveYearAlphaRatio = 0,
                EpsGrowth = 0
            };

            var assets = await _db.IndexedEquities.Where(eq => eq.EquityType == type
            && ((eq.AsxIndexTypes & ASXIndexTypes.Asx200) == ASXIndexTypes.Asx200))
            .GroupBy(ind => ind.Ticker)
                .Select(ind => ind.OrderByDescending(r => r.CreatedOn).FirstOrDefault()).ToListAsync();
            foreach (var indexedEquity in assets)
            {
                ratios.Frank +=
                    (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.Frank), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.Frank), indexedEquity.Ticker)) ?? 0;
                ratios.Beta += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.Beta), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.Beta), indexedEquity.Ticker)) ?? 0;
                ratios.BetaFiveYears += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.BetaFiveYears), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.BetaFiveYears), indexedEquity.Ticker)) ?? 0;
                ratios.Capitalisation += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.Capitalisation), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.Capitalisation), indexedEquity.Ticker)) ?? 0;
                ratios.CurrentRatio += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.CurrentRatio), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.CurrentRatio), indexedEquity.Ticker)) ?? 0;
                ratios.DebtEquityRatio += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.DebtEquityRatio), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.DebtEquityRatio), indexedEquity.Ticker)) ?? 0;
                ratios.DividendYield += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.DividendYield), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.DividendYield), indexedEquity.Ticker)) ?? 0;
                ratios.EarningsStability += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.EarningsStability), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.EarningsStability), indexedEquity.Ticker)) ?? 0;
                ratios.EpsGrowth += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.Frank), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.Frank), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearAlphaRatio += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearAlphaRatio), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.FiveYearAlphaRatio), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearInformation += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearInformation), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.FiveYearInformation), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearReturn += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearReturn), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.FiveYearReturn), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearSkewnessRatio += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearSkewnessRatio), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.FiveYearSkewnessRatio), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearSharpRatio += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearSharpRatio), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.FiveYearSharpRatio), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearStandardDeviation += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearStandardDeviation), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.FiveYearStandardDeviation), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearTrackingErrorRatio += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearTrackingErrorRatio), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.FiveYearTrackingErrorRatio), indexedEquity.Ticker)) ?? 0;
                ratios.FundSize += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FundSize), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.FundSize), indexedEquity.Ticker)) ?? 0;
                ratios.GlobalCategory += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.GlobalCategory), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.GlobalCategory), indexedEquity.Ticker)) ?? 0;
                ratios.InterestCover += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.InterestCover), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.InterestCover), indexedEquity.Ticker)) ?? 0;
                ratios.OneYearReturn += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.OneYearReturn), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.OneYearReturn), indexedEquity.Ticker)) ?? 0;
                ratios.PayoutRatio += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.PayoutRatio), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.PayoutRatio), indexedEquity.Ticker)) ?? 0;
                ratios.PriceEarningRatio += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.PriceEarningRatio), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.PriceEarningRatio), indexedEquity.Ticker)) ?? 0;
                ratios.QuickRatio += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.QuickRatio), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.QuickRatio), indexedEquity.Ticker)) ?? 0;
                ratios.ReturnOnEquity += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.ReturnOnEquity), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.ReturnOnEquity), indexedEquity.Ticker)) ?? 0;
                ratios.ReturnOnAsset += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.ReturnOnAsset), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.ReturnOnAsset), indexedEquity.Ticker)) ?? 0;
                ratios.ThreeYearReturn += (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.ThreeYearReturn), indexedEquity.Ticker)) ??
                    (await GetResearchValueForBond(Nameof<Ratios>.Property(r => r.ThreeYearReturn), indexedEquity.Ticker)) ?? 0;
            }
            return ratios;
        }



        public Ratios GetAsx200AverageRatiosSync(EquityTypes type)
        {

            var ratios = new Ratios()
            {
                PriceEarningRatio = 0,
                CurrentRatio = 0,
                Frank = 0,
                FiveYearTrackingErrorRatio = 0,
                ReturnOnEquity = 0,
                ReturnOnAsset = 0,
                QuickRatio = 0,
                GlobalCategory = 0,
                Beta = 0,
                Capitalisation = 0,
                FiveYearSkewnessRatio = 0,
                DividendYield = 0,
                DebtEquityRatio = 0,
                PayoutRatio = 0,
                EarningsStability = 0,
                FiveYearInformation = 0,
                ThreeYearReturn = 0,
                InterestCover = 0,
                FiveYearSharpRatio = 0,
                FiveYearReturn = 0,
                FundSize = 0,
                BetaFiveYears = 0,
                FiveYearStandardDeviation = 0,
                OneYearReturn = 0,
                FiveYearAlphaRatio = 0,
                EpsGrowth = 0
            };

            var assets = _db.IndexedEquities.Where(eq => eq.EquityType == type
            && ((eq.AsxIndexTypes & ASXIndexTypes.Asx200) == ASXIndexTypes.Asx200))
            .GroupBy(ind => ind.Ticker)
                .Select(ind => ind.OrderByDescending(r => r.CreatedOn).FirstOrDefault()).ToList();
            foreach (var indexedEquity in assets)
            {
                ratios.Frank +=
                    (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.Frank), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.Frank), indexedEquity.Ticker)) ?? 0;
                ratios.Beta += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.Beta), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.Beta), indexedEquity.Ticker)) ?? 0;
                ratios.BetaFiveYears += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.BetaFiveYears), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.BetaFiveYears), indexedEquity.Ticker)) ?? 0;
                ratios.Capitalisation += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.Capitalisation), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.Capitalisation), indexedEquity.Ticker)) ?? 0;
                ratios.CurrentRatio += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.CurrentRatio), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.CurrentRatio), indexedEquity.Ticker)) ?? 0;
                ratios.DebtEquityRatio += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.DebtEquityRatio), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.DebtEquityRatio), indexedEquity.Ticker)) ?? 0;
                ratios.DividendYield += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.DividendYield), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.DividendYield), indexedEquity.Ticker)) ?? 0;
                ratios.EarningsStability += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.EarningsStability), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.EarningsStability), indexedEquity.Ticker)) ?? 0;
                ratios.EpsGrowth += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.Frank), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.Frank), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearAlphaRatio += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearAlphaRatio), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.FiveYearAlphaRatio), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearInformation += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearInformation), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.FiveYearInformation), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearReturn += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearReturn), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.FiveYearReturn), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearSkewnessRatio += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearSkewnessRatio), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.FiveYearSkewnessRatio), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearSharpRatio += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearSharpRatio), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.FiveYearSharpRatio), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearStandardDeviation += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearStandardDeviation), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.FiveYearStandardDeviation), indexedEquity.Ticker)) ?? 0;
                ratios.FiveYearTrackingErrorRatio += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearTrackingErrorRatio), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.FiveYearTrackingErrorRatio), indexedEquity.Ticker)) ?? 0;
                ratios.FundSize += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FundSize), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.FundSize), indexedEquity.Ticker)) ?? 0;
                ratios.GlobalCategory += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.GlobalCategory), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.GlobalCategory), indexedEquity.Ticker)) ?? 0;
                ratios.InterestCover += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.InterestCover), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.InterestCover), indexedEquity.Ticker)) ?? 0;
                ratios.OneYearReturn += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.OneYearReturn), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.OneYearReturn), indexedEquity.Ticker)) ?? 0;
                ratios.PayoutRatio += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.PayoutRatio), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.PayoutRatio), indexedEquity.Ticker)) ?? 0;
                ratios.PriceEarningRatio += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.PriceEarningRatio), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.PriceEarningRatio), indexedEquity.Ticker)) ?? 0;
                ratios.QuickRatio += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.QuickRatio), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.QuickRatio), indexedEquity.Ticker)) ?? 0;
                ratios.ReturnOnEquity += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.ReturnOnEquity), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.ReturnOnEquity), indexedEquity.Ticker)) ?? 0;
                ratios.ReturnOnAsset += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.ReturnOnAsset), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.ReturnOnAsset), indexedEquity.Ticker)) ?? 0;
                ratios.ThreeYearReturn += (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.ThreeYearReturn), indexedEquity.Ticker)) ??
                    (GetResearchValueForBondSync(Nameof<Ratios>.Property(r => r.ThreeYearReturn), indexedEquity.Ticker)) ?? 0;
            }
            return ratios;
        }



        public async Task<List<LiabilityBase>> GetLiabilitiesForAccount(string accountNumber, DateTime beforeDate)
        {

            List<LiabilityBase> result = new List<LiabilityBase>();
            var account = _db.Accounts.Local
                .FirstOrDefault(
                    acc =>
                        acc.AccountNumber == accountNumber && acc.CreatedOn.HasValue &&
                        acc.CreatedOn.Value <= beforeDate) ??
                          await
                              _db.Accounts.Where(
                                  acc =>
                                      acc.AccountNumber == accountNumber && acc.CreatedOn.HasValue &&
                                      acc.CreatedOn.Value <= beforeDate)
                                  .Include(ac => ac.MarginLendings)
                                  .Include(ac => ac.MarginLendings.Select(m => m.LiabilityRates))
                                  .Include(ac => ac.MortgageHomeLoans)
                                  .Include(ac => ac.MortgageHomeLoans.Select(m => m.InterestRate))
                                  .Include(ac => ac.MortgageHomeLoans.Select(m => m.CorrespondingProperty))
                                  .Include(ac => ac.MortgageHomeLoans.Select(m => m.CorrespondingProperty.Prices))
                                  .Include(ac => ac.MortgageHomeLoans.Select(m => m.CorrespondingProperty.PropertyTransactions))

                                  .Include(ac => ac.Insurances)
                                  .Include(ac => ac.RepaymentRecords)
                                  .FirstOrDefaultAsync();
            if (account == null)
            {
                ProfileCannotBefound(accountNumber, beforeDate, "Account");
            }


            if (account.MarginLendings.Any())
            {
                await GenerateMarginLendingForAccount(account, result, beforeDate);
            }
            if (account.MortgageHomeLoans.Any())
            {
                await GenerateMortgageAndHomeLoanForAccount(beforeDate, account, result);
            }

            if (account.Insurances.Any())
            {
                await GenerateInsuranceForAccount(result, account, beforeDate);
            }
            return result;
        }



        public List<LiabilityBase> GetLiabilitiesForAccountSync(string accountNumber, DateTime beforeDate)
        {

            List<LiabilityBase> result = new List<LiabilityBase>();
            var account = _db.Accounts.Local
                .FirstOrDefault(
                    acc =>
                        acc.AccountNumber == accountNumber && acc.CreatedOn.HasValue &&
                        acc.CreatedOn.Value <= beforeDate) ??
                              _db.Accounts.Where(
                                  acc =>
                                      acc.AccountNumber == accountNumber && acc.CreatedOn.HasValue &&
                                      acc.CreatedOn.Value <= beforeDate)
                                  .Include(ac => ac.MarginLendings)
                                  .Include(ac => ac.MarginLendings.Select(m => m.LiabilityRates))
                                  .Include(ac => ac.MortgageHomeLoans)
                                  .Include(ac => ac.MortgageHomeLoans.Select(m => m.InterestRate))
                                  .Include(ac => ac.MortgageHomeLoans.Select(m => m.CorrespondingProperty))
                                  .Include(ac => ac.MortgageHomeLoans.Select(m => m.CorrespondingProperty.Prices))
                                  .Include(ac => ac.MortgageHomeLoans.Select(m => m.CorrespondingProperty.PropertyTransactions))

                                  .Include(ac => ac.Insurances)
                                  .Include(ac => ac.RepaymentRecords)
                                  .FirstOrDefault();
            if (account == null)
            {
                ProfileCannotBefound(accountNumber, beforeDate, "Account");
            }


            if (account.MarginLendings.Any())
            {
                GenerateMarginLendingForAccountSync(account, result, beforeDate);
            }
            if (account.MortgageHomeLoans.Any())
            {
                GenerateMortgageAndHomeLoanForAccountSync(beforeDate, account, result);
            }

            if (account.Insurances.Any())
            {
                GenerateInsuranceForAccountSync(result, account, beforeDate);
            }
            return result;
        }

        public async Task<List<ActivityBase>> GetInsuranceActivities(string insuranceId, DateTime beforeDate)
        {

            #region retrievals
            var insurance_old = _db.InsuranceTransactions.Local.FirstOrDefault(ins => ins.Id == insuranceId && ins.PurchasedOn.HasValue && ins.PurchasedOn.Value <= beforeDate) ??
                await _db.InsuranceTransactions.Where(ins => ins.Id == insuranceId && ins.PurchasedOn.HasValue && ins.PurchasedOn.Value <= beforeDate).FirstOrDefaultAsync();
            if (insurance_old == null)
            {
                ProfileCannotBefound(insuranceId, beforeDate, "Insurance");
            }
            var key = insurance_old.PolicyNumber + "*" + insurance_old.NameOfPolicy;

            var allInsurance =
                _db.InsuranceTransactions.Local.Where(
                    ins =>
                        ins.PolicyNumber == insurance_old.PolicyNumber &&
                        ins.NameOfPolicy == insurance_old.NameOfPolicy && ins.PurchasedOn.HasValue && ins.PurchasedOn.Value <= beforeDate).ToList();
            allInsurance.AddRange(await
                    _db.InsuranceTransactions.Where(
                        ins =>
                            ins.PolicyNumber == insurance_old.PolicyNumber &&
                            ins.NameOfPolicy == insurance_old.NameOfPolicy && ins.PurchasedOn.HasValue && ins.PurchasedOn.Value <= beforeDate).ToListAsync());
            var orderedInsurance = allInsurance.OrderBy(ins => ins.PurchasedOn).ToList();
            var allRepayments =
                _db.RepaymentRecords.Local.Where(
                    r =>
                        r.CorrespondingLiabilityGroupingKey == key && r.PaymentOn.HasValue &&
                        r.PaymentOn.Value <= beforeDate).ToList();
            allRepayments.AddRange(await _db.RepaymentRecords.Where(r => r.CorrespondingLiabilityGroupingKey == key
            && r.PaymentOn.HasValue
            && r.PaymentOn.Value <= beforeDate).ToListAsync());
            List<ActivityBase> result = new List<ActivityBase>();
            #endregion
            #region  grant loan activity and adjustments
            for (int i = 0; i < orderedInsurance.Count; i++)
            {
                var insurance = orderedInsurance[i];
                FinancialActivity activity = new FinancialActivity()
                {
                    Id = insurance.Id,
                    Expenses = new List<FinancialActivityCostRecord>(),
                    ActivityDate = insurance.PurchasedOn.Value,
                    Incomes = new List<IncomeRecordBase>(),
                    Transactions = new List<TransactionBase>()
                };
                var relevantExpenses =
                    _db.TransactionExpenses.Local.Where(ex => ex.CorrespondingTransactionId == insurance.Id).ToList();
                relevantExpenses.AddRange(await
                    _db.TransactionExpenses.Where(ex => ex.CorrespondingTransactionId == insurance.Id).ToListAsync());
                var transaction = new InsuranceTransaction()
                {
                    Id = insurance.Id,
                    NumberOfUnits = i == 0 ? 1 : -1,//will assume loan removal until we know for sure that this loan has been adjusted with new amount
                    AmountPerUnit = i == 0 ? insurance.AmountInsured.GetValueOrDefault() : orderedInsurance[i - 1].AmountInsured.GetValueOrDefault(),
                    LiabilityTransactionType = i == 0 ? LiabilityTransactionType.Acquire :
                    i == (allInsurance.Count - 1) && insurance.IsAcquire.HasValue && !insurance.IsAcquire.Value ? LiabilityTransactionType.Payout :
                    LiabilityTransactionType.Adjustment,
                    TransactionTime = insurance.PurchasedOn.Value
                };

                activity.Transactions.Add(transaction);


                if (i != 0 && i != (allInsurance.Count - 1) && insurance.IsAcquire.HasValue && !insurance.IsAcquire.Value)
                {
                    //when this transaction is adjustment, we need to insert the updated version of the transaction as well

                    var updatedTransaction = new InsuranceTransaction()
                    {
                        TransactionTime = insurance.PurchasedOn.Value,
                        Id = insurance.Id,
                        NumberOfUnits = 1,//adjustment will always be purchase
                        AmountPerUnit = insurance.AmountInsured.GetValueOrDefault(),
                        LiabilityTransactionType = LiabilityTransactionType.Adjustment
                    };
                    activity.Transactions.Add(updatedTransaction);
                }


                foreach (var transactionExpense in relevantExpenses)
                {
                    activity.Expenses.Add(new FinancialActivityCostRecord()
                    {
                        Id = transactionExpense.Id,
                        Amount = transactionExpense.Amount.GetValueOrDefault(),
                        ActivityCostType = TransactionExpenseType.LiabilityProcessingFee,
                        Transaction = activity.Transactions.Last()
                    });
                }

                result.Add(activity);
            }
            #endregion
            #region repayment insertion
            foreach (var repaymentRecord in allRepayments.Where(p => p.InterestAmount > 0))
            {
                var activity = new FinancialActivity()
                {
                    Id = repaymentRecord.Id,
                    Transactions = new List<TransactionBase>(),
                    Expenses = new List<FinancialActivityCostRecord>(),
                    ActivityDate = repaymentRecord.PaymentOn.Value,
                    Incomes = new List<IncomeRecordBase>()
                };
                activity.Expenses.Add(new FinancialActivityCostRecord()
                {
                    Id = repaymentRecord.Id,
                    Amount = repaymentRecord.InterestAmount.GetValueOrDefault(),
                    Transaction = result.Any(r => r.Transactions.Count > 0) ?
                    result.Where(r => r.Transactions.Count > 0).OrderByDescending(r => r.ActivityDate)
                    .FirstOrDefault().Transactions.Last() : null,
                    ActivityCostType = TransactionExpenseType.LiabilityInterestRepayment
                });

                result.Add(activity);
            }
            #endregion
            return result;
        }


        public List<ActivityBase> GetInsuranceActivitiesSync(string insuranceId, DateTime beforeDate)
        {

            #region retrievals
            var insurance_old = _db.InsuranceTransactions.Local.FirstOrDefault(ins => ins.Id == insuranceId && ins.PurchasedOn.HasValue && ins.PurchasedOn.Value <= beforeDate) ??
                _db.InsuranceTransactions.Where(ins => ins.Id == insuranceId && ins.PurchasedOn.HasValue && ins.PurchasedOn.Value <= beforeDate).FirstOrDefault();
            if (insurance_old == null)
            {
                ProfileCannotBefound(insuranceId, beforeDate, "Insurance");
            }
            var key = insurance_old.PolicyNumber + "*" + insurance_old.NameOfPolicy;

            var allInsurance =
                _db.InsuranceTransactions.Local.Where(
                    ins =>
                        ins.PolicyNumber == insurance_old.PolicyNumber &&
                        ins.NameOfPolicy == insurance_old.NameOfPolicy && ins.PurchasedOn.HasValue && ins.PurchasedOn.Value <= beforeDate).ToList();
            allInsurance.AddRange(_db.InsuranceTransactions.Where(ins =>
                            ins.PolicyNumber == insurance_old.PolicyNumber &&
                            ins.NameOfPolicy == insurance_old.NameOfPolicy && ins.PurchasedOn.HasValue && ins.PurchasedOn.Value <= beforeDate).ToList());
            var orderedInsurance = allInsurance.OrderBy(ins => ins.PurchasedOn).ToList();
            var allRepayments =
                _db.RepaymentRecords.Local.Where(
                    r =>
                        r.CorrespondingLiabilityGroupingKey == key && r.PaymentOn.HasValue &&
                        r.PaymentOn.Value <= beforeDate).ToList();
            allRepayments.AddRange(_db.RepaymentRecords.Where(r => r.CorrespondingLiabilityGroupingKey == key
            && r.PaymentOn.HasValue
            && r.PaymentOn.Value <= beforeDate).ToList());
            List<ActivityBase> result = new List<ActivityBase>();
            #endregion
            #region  grant loan activity and adjustments
            for (int i = 0; i < orderedInsurance.Count; i++)
            {
                var insurance = orderedInsurance[i];
                FinancialActivity activity = new FinancialActivity()
                {
                    Id = insurance.Id,
                    Expenses = new List<FinancialActivityCostRecord>(),
                    ActivityDate = insurance.PurchasedOn.Value,
                    Incomes = new List<IncomeRecordBase>(),
                    Transactions = new List<TransactionBase>()
                };
                var relevantExpenses =
                    _db.TransactionExpenses.Local.Where(ex => ex.CorrespondingTransactionId == insurance.Id).ToList();
                relevantExpenses.AddRange(
                    _db.TransactionExpenses.Where(ex => ex.CorrespondingTransactionId == insurance.Id).ToList());
                var transaction = new InsuranceTransaction()
                {
                    Id = insurance.Id,
                    NumberOfUnits = i == 0 ? 1 : -1,//will assume loan removal until we know for sure that this loan has been adjusted with new amount
                    AmountPerUnit = i == 0 ? insurance.AmountInsured.GetValueOrDefault() : orderedInsurance[i - 1].AmountInsured.GetValueOrDefault(),
                    LiabilityTransactionType = i == 0 ? LiabilityTransactionType.Acquire :
                    i == (allInsurance.Count - 1) && insurance.IsAcquire.HasValue && !insurance.IsAcquire.Value ? LiabilityTransactionType.Payout :
                    LiabilityTransactionType.Adjustment,
                    TransactionTime = insurance.PurchasedOn.Value
                };

                activity.Transactions.Add(transaction);


                if (i != 0 && i != (allInsurance.Count - 1) && insurance.IsAcquire.HasValue && !insurance.IsAcquire.Value)
                {
                    //when this transaction is adjustment, we need to insert the updated version of the transaction as well

                    var updatedTransaction = new InsuranceTransaction()
                    {
                        TransactionTime = insurance.PurchasedOn.Value,
                        Id = insurance.Id,
                        NumberOfUnits = 1,//adjustment will always be purchase
                        AmountPerUnit = insurance.AmountInsured.GetValueOrDefault(),
                        LiabilityTransactionType = LiabilityTransactionType.Adjustment
                    };
                    activity.Transactions.Add(updatedTransaction);
                }


                foreach (var transactionExpense in relevantExpenses)
                {
                    activity.Expenses.Add(new FinancialActivityCostRecord()
                    {
                        Id = transactionExpense.Id,
                        Amount = transactionExpense.Amount.GetValueOrDefault(),
                        ActivityCostType = TransactionExpenseType.LiabilityProcessingFee,
                        Transaction = activity.Transactions.Last()
                    });
                }

                result.Add(activity);
            }
            #endregion
            #region repayment insertion
            foreach (var repaymentRecord in allRepayments.Where(p => p.InterestAmount > 0))
            {
                var activity = new FinancialActivity()
                {
                    Id = repaymentRecord.Id,
                    Transactions = new List<TransactionBase>(),
                    Expenses = new List<FinancialActivityCostRecord>(),
                    ActivityDate = repaymentRecord.PaymentOn.Value,
                    Incomes = new List<IncomeRecordBase>()
                };
                activity.Expenses.Add(new FinancialActivityCostRecord()
                {
                    Id = repaymentRecord.Id,
                    Amount = repaymentRecord.InterestAmount.GetValueOrDefault(),
                    Transaction = result.Any(r => r.Transactions.Count > 0) ?
                    result.Where(r => r.Transactions.Count > 0).OrderByDescending(r => r.ActivityDate)
                    .FirstOrDefault().Transactions.Last() : null,
                    ActivityCostType = TransactionExpenseType.LiabilityInterestRepayment
                });

                result.Add(activity);
            }
            #endregion
            return result;
        }


        public async Task<List<ActivityBase>> GetMarginLendingActivities(string marginLendingId, DateTime beforeDate)
        {

            #region retrievals
            var marginLending_old =
                _db.MarginLendingTransactions.FirstOrDefault(m => m.Id == marginLendingId
                && m.GrantedOn.HasValue
                && m.GrantedOn.Value <= beforeDate) ??
                await _db.MarginLendingTransactions.Where(m => m.Id == marginLendingId
                && m.GrantedOn.HasValue
                && m.GrantedOn.Value <= beforeDate)
                .FirstOrDefaultAsync();

            if (marginLending_old == null)
            {
                ProfileCannotBefound(marginLendingId, beforeDate, "Margin lending");
            }
            var allMarginLendings =
                _db.MarginLendingTransactions.Local.Where(m => m.Account.AccountId == marginLending_old.Account.AccountId
                && m.GrantedOn.HasValue
                && m.GrantedOn.Value <= beforeDate)
                    .ToList();
            //allMarginLendings.AddRange(await _db.MarginLendingTransactions.Where(m => m.Account.AccountId == marginLending_old.Account.AccountId
            //&& m.GrantedOn.HasValue && m.GrantedOn.Value <= beforeDate)
            //.Include(m => m.LoanValueRatios)
            //.ToListAsync());

            var allRepayments =
                _db.RepaymentRecords.Local.Where(
                    r =>
                        r.CorrespondingLiabilityGroupingKey == marginLending_old.Account.AccountId &&
                        r.PaymentOn.HasValue && r.PaymentOn.Value <= beforeDate).ToList();


            allRepayments.AddRange(
                await
                    _db.RepaymentRecords.Where(
                        r =>
                            r.CorrespondingLiabilityGroupingKey == marginLending_old.Account.AccountId &&
                            r.PaymentOn.HasValue && r.PaymentOn.Value <= beforeDate).ToListAsync());

            var orderedMarginLending = allMarginLendings.OrderBy(c => c.GrantedOn).ToList();
            #endregion
            #region loan acquire and adjustment
            List<ActivityBase> result = new List<ActivityBase>();
            for (int i = 0; i < orderedMarginLending.Count; i++)
            {
                var marginLending = orderedMarginLending[i];

                //var relevantExpenses =
                //    _db.TransactionExpenses.Local.Where(ex => ex.CorrespondingTransactionId == marginLending.Id)
                //        .ToList();
                //relevantExpenses.AddRange(await _db.TransactionExpenses.Where(ex => ex.CorrespondingTransactionId == marginLending.Id).ToListAsync());



                var activity = new FinancialActivity()
                {
                    Id = marginLending.Id,
                    Transactions = new List<TransactionBase>(),
                    ActivityDate = marginLending.GrantedOn.Value,
                    Expenses = new List<FinancialActivityCostRecord>(),
                    Incomes = new List<IncomeRecordBase>()
                };

                var transaction = new MarginlendingTransaction()
                {
                    Id = marginLending.Id,
                    NumberOfUnits = i == 0 ? 1 : -1,
                    AmountPerUnit = i == 0 ? marginLending.LoanAmount.GetValueOrDefault() : orderedMarginLending[i - 1].LoanAmount.GetValueOrDefault(),
                    LiabilityTransactionType = i == 0 ? LiabilityTransactionType.Acquire :
                    i == (orderedMarginLending.Count - 1) && marginLending.IsAcquire.HasValue && !marginLending.IsAcquire.Value ? LiabilityTransactionType.Payout :
                    LiabilityTransactionType.Adjustment,
                    TransactionTime = marginLending.GrantedOn.Value,
                    AssetLvRs = new Dictionary<string, double>()
                };
                //populate lvrs
                //if (marginLending.LoanValueRatios.Any())
                //{
                //    transaction.AssetLvRs = marginLending.LoanValueRatios.GroupBy(l => l.EquityId)
                //       .ToDictionary(g => g.Key, g => g.OrderByDescending(r => r.ActiveDate).FirstOrDefault().Ratio);

                //}
                activity.Transactions.Add(transaction);
                if (transaction.LiabilityTransactionType == LiabilityTransactionType.Adjustment)
                {
                    var updatedTransaction = new MarginlendingTransaction()
                    {
                        Id = marginLending.Id,
                        NumberOfUnits = 1,
                        AmountPerUnit = marginLending.LoanAmount.GetValueOrDefault(),
                        LiabilityTransactionType = LiabilityTransactionType.Adjustment,
                        TransactionTime = marginLending.GrantedOn.Value,
                        AssetLvRs = transaction.AssetLvRs
                    };
                    activity.Transactions.Add(updatedTransaction);
                }



                //foreach (var transactionExpense in relevantExpenses)
                //{
                //    activity.Expenses.Add(new FinancialActivityCostRecord()
                //    {
                //        Id = transactionExpense.Id,
                //        Transaction = activity.Transactions.Last(),
                //        ActivityCostType = TransactionExpenseType.LiabilityProcessingFee,
                //        Amount = transactionExpense.Amount.GetValueOrDefault()
                //    });
                //}


                result.Add(activity);
            }
            #endregion
            #region loan repayment
            foreach (var repaymentRecord in allRepayments.Where(p => p.InterestAmount > 0))
            {
                var activity = new FinancialActivity()
                {
                    Id = repaymentRecord.Id,
                    Transactions = new List<TransactionBase>(),
                    ActivityDate = repaymentRecord.PaymentOn.Value,
                    Expenses = new List<FinancialActivityCostRecord>(),
                    Incomes = new List<IncomeRecordBase>()
                };
                activity.Expenses.Add(new FinancialActivityCostRecord()
                {
                    Id = repaymentRecord.Id,
                    ActivityCostType = TransactionExpenseType.LiabilityInterestRepayment,
                    Transaction = result.Any(r => r.Transactions.Count > 0) ? result.Where(r => r.Transactions.Count > 0).OrderByDescending(r => r.ActivityDate).FirstOrDefault().Transactions.Last() : null,
                    Amount = repaymentRecord.InterestAmount.GetValueOrDefault()
                });

                result.Add(activity);
            }
            #endregion
            return result;
        }




        public List<ActivityBase> GetMarginLendingActivitiesSync(string marginLendingId, DateTime beforeDate)
        {

            #region retrievals
            var marginLending_old =
                _db.MarginLendingTransactions.FirstOrDefault(m => m.Id == marginLendingId
                && m.GrantedOn.HasValue
                && m.GrantedOn.Value <= beforeDate) ?? _db.MarginLendingTransactions.Where(m => m.Id == marginLendingId
                && m.GrantedOn.HasValue
                && m.GrantedOn.Value <= beforeDate)
                .FirstOrDefault();

            if (marginLending_old == null)
            {
                ProfileCannotBefound(marginLendingId, beforeDate, "Margin lending");
            }
            var allMarginLendings =
                _db.MarginLendingTransactions.Local.Where(m => m.Account.AccountId == marginLending_old.Account.AccountId
                && m.GrantedOn.HasValue
                && m.GrantedOn.Value <= beforeDate)
                    .ToList();
            //allMarginLendings.AddRange(_db.MarginLendingTransactions.Where(m => m.Account.AccountId == marginLending_old.Account.AccountId
            //&& m.GrantedOn.HasValue && m.GrantedOn.Value <= beforeDate)
            //.Include(m => m.LoanValueRatios)
            //.ToList());

            var allRepayments =
                _db.RepaymentRecords.Local.Where(
                    r =>
                        r.CorrespondingLiabilityGroupingKey == marginLending_old.Account.AccountId &&
                        r.PaymentOn.HasValue && r.PaymentOn.Value <= beforeDate).ToList();


            allRepayments.AddRange(
                    _db.RepaymentRecords.Where(
                        r =>
                            r.CorrespondingLiabilityGroupingKey == marginLending_old.Account.AccountId &&
                            r.PaymentOn.HasValue && r.PaymentOn.Value <= beforeDate).ToList());

            var orderedMarginLending = allMarginLendings.OrderBy(c => c.GrantedOn).ToList();
            #endregion
            #region loan acquire and adjustment
            List<ActivityBase> result = new List<ActivityBase>();
            for (int i = 0; i < orderedMarginLending.Count; i++)
            {
                var marginLending = orderedMarginLending[i];

                var relevantExpenses =
                    _db.TransactionExpenses.Local.Where(ex => ex.CorrespondingTransactionId == marginLending.Id)
                        .ToList();
                relevantExpenses.AddRange(_db.TransactionExpenses.Where(ex => ex.CorrespondingTransactionId == marginLending.Id).ToList());



                var activity = new FinancialActivity()
                {
                    Id = marginLending.Id,
                    Transactions = new List<TransactionBase>(),
                    ActivityDate = marginLending.GrantedOn.Value,
                    Expenses = new List<FinancialActivityCostRecord>(),
                    Incomes = new List<IncomeRecordBase>()
                };

                var transaction = new MarginlendingTransaction()
                {
                    Id = marginLending.Id,
                    NumberOfUnits = i == 0 ? 1 : -1,
                    AmountPerUnit = i == 0 ? marginLending.LoanAmount.GetValueOrDefault() : orderedMarginLending[i - 1].LoanAmount.GetValueOrDefault(),
                    LiabilityTransactionType = i == 0 ? LiabilityTransactionType.Acquire :
                    i == (orderedMarginLending.Count - 1) && marginLending.IsAcquire.HasValue && !marginLending.IsAcquire.Value ? LiabilityTransactionType.Payout :
                    LiabilityTransactionType.Adjustment,
                    TransactionTime = marginLending.GrantedOn.Value,
                    AssetLvRs = new Dictionary<string, double>()
                };
                //populate lvrs
                //if (marginLending.LoanValueRatios.Any())
                //{
                //    transaction.AssetLvRs = marginLending.LoanValueRatios.GroupBy(l => l.EquityId)
                //       .ToDictionary(g => g.Key, g => g.OrderByDescending(r => r.ActiveDate).FirstOrDefault().Ratio);

                //}
                activity.Transactions.Add(transaction);
                if (transaction.LiabilityTransactionType == LiabilityTransactionType.Adjustment)
                {
                    var updatedTransaction = new MarginlendingTransaction()
                    {
                        Id = marginLending.Id,
                        NumberOfUnits = 1,
                        AmountPerUnit = marginLending.LoanAmount.GetValueOrDefault(),
                        LiabilityTransactionType = LiabilityTransactionType.Adjustment,
                        TransactionTime = marginLending.GrantedOn.Value,
                        AssetLvRs = transaction.AssetLvRs
                    };
                    activity.Transactions.Add(updatedTransaction);
                }

                foreach (var transactionExpense in relevantExpenses)
                {
                    activity.Expenses.Add(new FinancialActivityCostRecord()
                    {
                        Id = transactionExpense.Id,
                        Transaction = activity.Transactions.Last(),
                        ActivityCostType = TransactionExpenseType.LiabilityProcessingFee,
                        Amount = transactionExpense.Amount.GetValueOrDefault()
                    });
                }


                result.Add(activity);
            }
            #endregion
            #region loan repayment
            foreach (var repaymentRecord in allRepayments.Where(p => p.InterestAmount > 0))
            {
                var activity = new FinancialActivity()
                {
                    Id = repaymentRecord.Id,
                    Transactions = new List<TransactionBase>(),
                    ActivityDate = repaymentRecord.PaymentOn.Value,
                    Expenses = new List<FinancialActivityCostRecord>(),
                    Incomes = new List<IncomeRecordBase>()
                };
                activity.Expenses.Add(new FinancialActivityCostRecord()
                {
                    Id = repaymentRecord.Id,
                    ActivityCostType = TransactionExpenseType.LiabilityInterestRepayment,
                    Transaction = result.Any(r => r.Transactions.Count > 0) ? result.Where(r => r.Transactions.Count > 0).OrderByDescending(r => r.ActivityDate).FirstOrDefault().Transactions.Last() : null,
                    Amount = repaymentRecord.InterestAmount.GetValueOrDefault()
                });

                result.Add(activity);
            }
            #endregion
            return result;
        }



        public async Task<List<ActivityBase>> GetMortgageLoanActivities(string mortgageId, DateTime beforeDate)
        {

            #region retrieval
            var homeLoan_last =
                _db.MortgageHomeLoanTransactions.Local.SingleOrDefault(
                    l => l.Id == mortgageId && l.LoanAquiredOn.HasValue && l.LoanAquiredOn.Value <= beforeDate) ??
                await
                    _db.MortgageHomeLoanTransactions.Where(
                        m => m.Id == mortgageId && m.LoanAquiredOn.HasValue && m.LoanAquiredOn.Value <= beforeDate)
                        .Include(l => l.CorrespondingProperty)
                        .Include(l => l.Account)
                        .FirstOrDefaultAsync();

            if (homeLoan_last == null)
            {
                ProfileCannotBefound(mortgageId, beforeDate, "Mortgage Loan");
            }

            var allHomeLoans =
                _db.MortgageHomeLoanTransactions.Local.Where(
                    l => l.CorrespondingProperty.PropertyId == homeLoan_last.CorrespondingProperty.PropertyId
                         && l.Account.AccountId == homeLoan_last.Account.AccountId && l.LoanAquiredOn.HasValue &&
                         l.LoanAquiredOn.Value <= beforeDate).ToList();
            allHomeLoans.AddRange(await _db.MortgageHomeLoanTransactions.Where(l => l.CorrespondingProperty.PropertyId == homeLoan_last.CorrespondingProperty.PropertyId
            && l.Account.AccountId == homeLoan_last.Account.AccountId && l.LoanAquiredOn.HasValue && l.LoanAquiredOn.Value <= beforeDate)
            .Include(l => l.CorrespondingProperty)
            .Include(l => l.Account)
            .ToListAsync());

            var key = homeLoan_last.CorrespondingProperty.PropertyId + "*" + homeLoan_last.Account.AccountId;


            var allRepayments =
                _db.RepaymentRecords.Local.Where(
                    r =>
                        r.CorrespondingLiabilityGroupingKey == key && r.PaymentOn.HasValue &&
                        r.PaymentOn.Value <= beforeDate)
                    .ToList();
            allRepayments.AddRange(await _db.RepaymentRecords.Where(r => r.CorrespondingLiabilityGroupingKey == key
            && r.PaymentOn.HasValue
            && r.PaymentOn.Value <= beforeDate).ToListAsync());
            var orderedHomeLoans = allHomeLoans.OrderBy(l => l.LoanAquiredOn).ToList();
            List<ActivityBase> result = new List<ActivityBase>();
            #endregion
            #region purchase or adjust loans
            for (int i = 0; i < orderedHomeLoans.Count; i++)
            {
                var homeLoan = orderedHomeLoans[i];
                var activity = new FinancialActivity()
                {
                    Id = homeLoan.Id,
                    Transactions = new List<TransactionBase>(),
                    ActivityDate = homeLoan.LoanAquiredOn.Value,
                    Expenses = new List<FinancialActivityCostRecord>(),
                    Incomes = new List<IncomeRecordBase>()
                };

                var transaction = new MortgageTransaction()
                {
                    Id = homeLoan.Id,
                    NumberOfUnits = i == 0 ? 1 : -1,
                    LiabilityTransactionType = i == 0 ? LiabilityTransactionType.Acquire :
                    i == (orderedHomeLoans.Count - 1) && homeLoan.IsAcquire.HasValue
                    && !homeLoan.IsAcquire.Value ? LiabilityTransactionType.Payout :
                    LiabilityTransactionType.Adjustment,
                    AmountPerUnit = i == 0 ? homeLoan.LoanAmount.GetValueOrDefault() : orderedHomeLoans[i - 1].LoanAmount.GetValueOrDefault(),
                    TransactionTime = homeLoan.LoanAquiredOn.Value,
                    PlaceId = homeLoan.CorrespondingProperty.GooglePlaceId,
                    State = homeLoan.CorrespondingProperty.State,
                    City = homeLoan.CorrespondingProperty.City,
                    FullAddress = homeLoan.CorrespondingProperty.FullAddress,
                    Longitude = homeLoan.CorrespondingProperty.Longitude.GetValueOrDefault(),
                    Latitude = homeLoan.CorrespondingProperty.Latitude.GetValueOrDefault(),
                    Postcode = homeLoan.CorrespondingProperty.Postcode,
                    StreetAddress = homeLoan.CorrespondingProperty.StreetAddress,
                    Country = homeLoan.CorrespondingProperty.Country
                };

                activity.Transactions.Add(transaction);
                if (transaction.LiabilityTransactionType == LiabilityTransactionType.Adjustment)
                {
                    var updateTransaction = new MortgageTransaction()
                    {
                        Country = homeLoan.CorrespondingProperty.Country,
                        Id = homeLoan.Id,
                        NumberOfUnits = 1,
                        LiabilityTransactionType = LiabilityTransactionType.Adjustment,
                        AmountPerUnit = homeLoan.LoanAmount.GetValueOrDefault(),
                        TransactionTime = homeLoan.LoanAquiredOn.Value,
                        PlaceId = homeLoan.CorrespondingProperty.GooglePlaceId,
                        State = homeLoan.CorrespondingProperty.State,
                        City = homeLoan.CorrespondingProperty.City,
                        FullAddress = homeLoan.CorrespondingProperty.FullAddress,
                        Longitude = homeLoan.CorrespondingProperty.Longitude.GetValueOrDefault(),
                        Latitude = homeLoan.CorrespondingProperty.Latitude.GetValueOrDefault(),
                        Postcode = homeLoan.CorrespondingProperty.Postcode,
                        StreetAddress = homeLoan.CorrespondingProperty.StreetAddress
                    };
                    activity.Transactions.Add(updateTransaction);
                }
                result.Add(activity);
            }
            #endregion
            #region repayment records

            foreach (var payment in allRepayments.Where(p => p.InterestAmount > 0))
            {
                //todo payment here
                var activity = new FinancialActivity()
                {
                    Id = payment.Id,
                    Transactions = new List<TransactionBase>(),
                    ActivityDate = payment.PaymentOn.Value,
                    Expenses = new List<FinancialActivityCostRecord>(),
                    Incomes = new List<IncomeRecordBase>()
                };

                activity.Expenses.Add(new FinancialActivityCostRecord()
                {
                    Id = payment.Id,
                    Transaction = result.Any(r => r.Transactions.Count > 0) ?
                    result.Where(r => r.Transactions.Count > 0).OrderByDescending(r => r.Transactions)
                    .FirstOrDefault()
                    .Transactions.Last() : null,
                    ActivityCostType = TransactionExpenseType.LiabilityInterestRepayment,
                    Amount = payment.InterestAmount.GetValueOrDefault()
                });
                result.Add(activity);
            }
            #endregion
            return result;

        }




        public List<ActivityBase> GetMortgageLoanActivitiesSync(string mortgageId, DateTime beforeDate)
        {

            #region retrieval
            var homeLoan_last =
                _db.MortgageHomeLoanTransactions.Local.SingleOrDefault(
                    l => l.Id == mortgageId && l.LoanAquiredOn.HasValue && l.LoanAquiredOn.Value <= beforeDate) ??
                    _db.MortgageHomeLoanTransactions.Where(
                        m => m.Id == mortgageId && m.LoanAquiredOn.HasValue && m.LoanAquiredOn.Value <= beforeDate)
                        .Include(l => l.CorrespondingProperty)
                        .Include(l => l.Account)
                        .FirstOrDefault();

            if (homeLoan_last == null)
            {
                ProfileCannotBefound(mortgageId, beforeDate, "Mortgage Loan");
            }

            var allHomeLoans =
                _db.MortgageHomeLoanTransactions.Local.Where(
                    l => l.CorrespondingProperty.PropertyId == homeLoan_last.CorrespondingProperty.PropertyId
                         && l.Account.AccountId == homeLoan_last.Account.AccountId && l.LoanAquiredOn.HasValue &&
                         l.LoanAquiredOn.Value <= beforeDate).ToList();
            allHomeLoans.AddRange(_db.MortgageHomeLoanTransactions.Where(l => l.CorrespondingProperty.PropertyId == homeLoan_last.CorrespondingProperty.PropertyId
            && l.Account.AccountId == homeLoan_last.Account.AccountId && l.LoanAquiredOn.HasValue && l.LoanAquiredOn.Value <= beforeDate)
            .Include(l => l.CorrespondingProperty)
            .Include(l => l.Account)
            .ToList());

            var key = homeLoan_last.CorrespondingProperty.PropertyId + "*" + homeLoan_last.Account.AccountId;


            var allRepayments =
                _db.RepaymentRecords.Local.Where(
                    r =>
                        r.CorrespondingLiabilityGroupingKey == key && r.PaymentOn.HasValue &&
                        r.PaymentOn.Value <= beforeDate)
                    .ToList();
            allRepayments.AddRange(_db.RepaymentRecords.Where(r => r.CorrespondingLiabilityGroupingKey == key
            && r.PaymentOn.HasValue
            && r.PaymentOn.Value <= beforeDate).ToList());
            var orderedHomeLoans = allHomeLoans.OrderBy(l => l.LoanAquiredOn).ToList();
            List<ActivityBase> result = new List<ActivityBase>();
            #endregion
            #region purchase or adjust loans
            for (int i = 0; i < orderedHomeLoans.Count; i++)
            {
                var homeLoan = orderedHomeLoans[i];
                var activity = new FinancialActivity()
                {
                    Id = homeLoan.Id,
                    Transactions = new List<TransactionBase>(),
                    ActivityDate = homeLoan.LoanAquiredOn.Value,
                    Expenses = new List<FinancialActivityCostRecord>(),
                    Incomes = new List<IncomeRecordBase>()
                };

                var transaction = new MortgageTransaction()
                {
                    Id = homeLoan.Id,
                    NumberOfUnits = i == 0 ? 1 : -1,
                    LiabilityTransactionType = i == 0 ? LiabilityTransactionType.Acquire :
                    i == (orderedHomeLoans.Count - 1) && homeLoan.IsAcquire.HasValue
                    && !homeLoan.IsAcquire.Value ? LiabilityTransactionType.Payout :
                    LiabilityTransactionType.Adjustment,
                    AmountPerUnit = i == 0 ? homeLoan.LoanAmount.GetValueOrDefault() : orderedHomeLoans[i - 1].LoanAmount.GetValueOrDefault(),
                    TransactionTime = homeLoan.LoanAquiredOn.Value,
                    PlaceId = homeLoan.CorrespondingProperty.GooglePlaceId,
                    State = homeLoan.CorrespondingProperty.State,
                    City = homeLoan.CorrespondingProperty.City,
                    FullAddress = homeLoan.CorrespondingProperty.FullAddress,
                    Longitude = homeLoan.CorrespondingProperty.Longitude.GetValueOrDefault(),
                    Latitude = homeLoan.CorrespondingProperty.Latitude.GetValueOrDefault(),
                    Postcode = homeLoan.CorrespondingProperty.Postcode,
                    StreetAddress = homeLoan.CorrespondingProperty.StreetAddress,
                    Country = homeLoan.CorrespondingProperty.Country
                };

                activity.Transactions.Add(transaction);
                if (transaction.LiabilityTransactionType == LiabilityTransactionType.Adjustment)
                {
                    var updateTransaction = new MortgageTransaction()
                    {
                        Country = homeLoan.CorrespondingProperty.Country,
                        Id = homeLoan.Id,
                        NumberOfUnits = 1,
                        LiabilityTransactionType = LiabilityTransactionType.Adjustment,
                        AmountPerUnit = homeLoan.LoanAmount.GetValueOrDefault(),
                        TransactionTime = homeLoan.LoanAquiredOn.Value,
                        PlaceId = homeLoan.CorrespondingProperty.GooglePlaceId,
                        State = homeLoan.CorrespondingProperty.State,
                        City = homeLoan.CorrespondingProperty.City,
                        FullAddress = homeLoan.CorrespondingProperty.FullAddress,
                        Longitude = homeLoan.CorrespondingProperty.Longitude.GetValueOrDefault(),
                        Latitude = homeLoan.CorrespondingProperty.Latitude.GetValueOrDefault(),
                        Postcode = homeLoan.CorrespondingProperty.Postcode,
                        StreetAddress = homeLoan.CorrespondingProperty.StreetAddress
                    };
                    activity.Transactions.Add(updateTransaction);
                }
                result.Add(activity);
            }
            #endregion
            #region repayment records

            foreach (var payment in allRepayments.Where(p => p.InterestAmount > 0))
            {
                //todo payment here
                var activity = new FinancialActivity()
                {
                    Id = payment.Id,
                    Transactions = new List<TransactionBase>(),
                    ActivityDate = payment.PaymentOn.Value,
                    Expenses = new List<FinancialActivityCostRecord>(),
                    Incomes = new List<IncomeRecordBase>()
                };

                activity.Expenses.Add(new FinancialActivityCostRecord()
                {
                    Id = payment.Id,
                    Transaction = result.Any(r => r.Transactions.Count > 0) ?
                    result.Where(r => r.Transactions.Count > 0).OrderByDescending(r => r.Transactions)
                    .FirstOrDefault()
                    .Transactions.Last() : null,
                    ActivityCostType = TransactionExpenseType.LiabilityInterestRepayment,
                    Amount = payment.InterestAmount.GetValueOrDefault()
                });
                result.Add(activity);
            }
            #endregion
            return result;

        }



        public async Task RecordRepayment(RepaymentCreation record)
        {

            var account = _db.Accounts.Local.SingleOrDefault(acc => acc.AccountNumber == record.AccountNumber) ??
                          await _db.Accounts.Where(acc => acc.AccountNumber == record.AccountNumber)
                              .Include(acc => acc.RepaymentRecords).FirstOrDefaultAsync();


            _db.RepaymentRecords.Add(new Edis.Db.RepaymentRecord()
            {
                PaymentOn = record.PaymentOn,
                PrincipleAmount = record.PrincipleAmount,
                AccountId = account.AccountId,
                Id = Guid.NewGuid().ToString(),
                CorrespondingLiabilityGroupingKey = record.LiabilityTypes == LiabilityTypes.Insurance ?
                (record.PolicyNumber + "*" + record.PolicyName) : record.LiabilityTypes == LiabilityTypes.MarginLending ?
                account.AccountId : (record.PropertyId + "*" + account.AccountId),
                CreatedOn = DateTime.Now,
                Account = account,
                InterestAmount = record.InterestAmount,
                LiabilityType = record.LiabilityTypes
            });

            await _db.SaveChangesAsync();
        }



        public void RecordRepaymentSync(RepaymentCreation record)
        {

            var account = _db.Accounts.Local.SingleOrDefault(acc => acc.AccountNumber == record.AccountNumber) ??
                          _db.Accounts.Where(acc => acc.AccountNumber == record.AccountNumber)
                              .Include(acc => acc.RepaymentRecords).FirstOrDefault();


            _db.RepaymentRecords.Add(new Edis.Db.RepaymentRecord()
            {
                PaymentOn = record.PaymentOn,
                PrincipleAmount = record.PrincipleAmount,
                AccountId = account.AccountId,
                Id = Guid.NewGuid().ToString(),
                CorrespondingLiabilityGroupingKey = record.LiabilityTypes == LiabilityTypes.Insurance ?
                (record.PolicyNumber + "*" + record.PolicyName) : record.LiabilityTypes == LiabilityTypes.MarginLending ?
                account.AccountId : (record.PropertyId + "*" + account.AccountId),
                CreatedOn = DateTime.Now,
                Account = account,
                InterestAmount = record.InterestAmount,
                LiabilityType = record.LiabilityTypes
            });

            _db.SaveChanges();
        }

        public List<Account> GetAllAccountsByClientGroupId(string clientGroupId) {
            List<Account> accounts = new List<Account>();

            var clientGroup = getClientGroupByGroupId(clientGroupId);
            List<GroupAccount> GroupAccounts = GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
            List<ClientAccount> clientAccounts = new List<ClientAccount>();
            clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

            GroupAccounts.ForEach(g => accounts.AddRange(_db.Accounts.Where(a => a.AccountNumber == g.AccountNumber)));
            clientAccounts.ForEach(c => accounts.AddRange(_db.Accounts.Where(a => a.AccountNumber == c.AccountNumber)));

            return accounts;
        }

        public List<MarginLending> GetAllMarginLendingByAssetType(AssetTypes assetType, string clientGroupId) {
            List<MarginLending> marginLendings = new List<MarginLending>();

            List<MarginLendingTransaction> transactions = new List<MarginLendingTransaction>();

            if(string.IsNullOrEmpty(clientGroupId)){
                return null;
            }

            var accounts = GetAllAccountsByClientGroupId(clientGroupId);

            accounts.ForEach(a => 
                    transactions.AddRange(_db.MarginLendingTransactions.Where(m => m.AssetTypes == assetType && m.AccountId == a.AccountId).ToList())                
                );

            transactions.ForEach(t => {
                AssetBase asset = null;
                var eTransaction = _db.EquityTransactions.FirstOrDefault(e => e.Id == t.EquityTransactionId);

                switch (t.AssetTypes) {
                    case AssetTypes.AustralianEquity:
                        var aeEquity = _db.Equities.FirstOrDefault(e => e.AssetId == t.AssetId);
                        
                        asset = new AustralianEquity(this) {
                            Id = aeEquity.AssetId,
                            EquityType = aeEquity.EquityType,
                            Ticker = aeEquity.Ticker,
                            LatestPrice = eTransaction == null ? 0 : eTransaction.UnitPriceAtPurchase == null? 0 : (double)eTransaction.UnitPriceAtPurchase,
                            TotalNumberOfUnits = eTransaction == null? 0 : (double)eTransaction.NumberOfUnits
                        };
                        break;
                    case AssetTypes.InternationalEquity:
                        var ieEquity = _db.Equities.FirstOrDefault(e => e.AssetId == t.AssetId);
                        asset = new InternationalEquity(this) {
                            Id = ieEquity.AssetId,
                            EquityType = ieEquity.EquityType,
                            Ticker = ieEquity.Ticker,
                            LatestPrice = eTransaction == null ? 0 : eTransaction.UnitPriceAtPurchase == null ? 0 : (double)eTransaction.UnitPriceAtPurchase,
                            TotalNumberOfUnits = eTransaction == null ? 0 : (double)eTransaction.NumberOfUnits
                        };
                        break;
                    case AssetTypes.ManagedInvestments:
                        var miEquity = _db.Equities.FirstOrDefault(e => e.AssetId == t.AssetId);
                        asset = new InternationalEquity(this) {
                            Id = miEquity.AssetId,
                            EquityType = miEquity.EquityType,
                            Ticker = miEquity.Ticker,
                            LatestPrice = eTransaction == null ? 0 : eTransaction.UnitPriceAtPurchase == null ? 0 : (double)eTransaction.UnitPriceAtPurchase,
                            TotalNumberOfUnits = eTransaction == null ? 0 : (double)eTransaction.NumberOfUnits
                        };
                        break;
                }

                marginLendings.Add(new MarginLending(this) {
                    LoanAmount = t.LoanAmount == null ? 0 : (double)t.LoanAmount,
                    LoanValueRatio = t.Ratio,
                    Asset = asset
                });
            });

            return marginLendings;
        }

        public MarginLending GetMarginLendingForAccountAsset(string assetId, string accountNumber) {
            var transactions = _db.MarginLendingTransactions.Where(t => t.AssetId == assetId && t.Account.AccountNumber == accountNumber).ToList();
            
            if(transactions.Count == 0){
                return null;
            }
            double? totalLoanAmount = 0;
            double netCostValue = 0;
            double totalPrice = 0;

            transactions.ForEach(t => {
                var eTransaction = _db.EquityTransactions.FirstOrDefault(e => e.Id == t.EquityTransactionId);
                totalLoanAmount += t.LoanAmount;
                netCostValue += (double)(eTransaction.UnitPriceAtPurchase * eTransaction.NumberOfUnits - t.LoanAmount);
                totalPrice += (double)(eTransaction.UnitPriceAtPurchase * eTransaction.NumberOfUnits);
            });

            
            return new MarginLending(this) {
                //Id = _db.Accounts.SingleOrDefault(a => a.AccountId == transaction.AccountId).MarginLenderId,
                LoanAmount = totalLoanAmount == null? 0 : (double)totalLoanAmount,
                LoanValueRatio = (double)(totalLoanAmount / totalPrice),
                NetCostValue = netCostValue
            };
        }


        public List<MarginLenderPasser> GetMarginLendersByTicker(string ticker) {
            List<MarginLenderPasser> lenders = new List<MarginLenderPasser>();
            var marginLenders = _db.MarginLenders.Include(l => l.Ratios).ToList();

            foreach(var lender in marginLenders){
                var ratios = lender.Ratios.Where(r => r.Ticker == ticker).ToList();
                List<LoanValueRatioPasser> lvr = new List<LoanValueRatioPasser>();
                ratios.ForEach(r => {
                    lvr.Add(new LoanValueRatioPasser{
                        Ticker = r.Ticker,
                        CreatedOn = r.CreatedOn,
                        MaxRatio = r.MaxRatio,
                        ActiveDate = r.ActiveDate,
                        AssetTypes = r.AssetTypes
                    });
                });

                lenders.Add(new MarginLenderPasser { 
                    LenderName = lender.LenderName,
                    Ratios = lvr
                });
            }
            return lenders;
        }


        #region liability helpers
        private async Task GenerateInsuranceForAccount(List<LiabilityBase> result, Account account, DateTime beforeDate)
        {

            var insuranceGroups = account.Insurances.GroupBy(ins => new { ins.NameOfPolicy, ins.PolicyNumber });
            foreach (var insurance in insuranceGroups)
            {
                var key = insurance.Key.PolicyNumber + "*" + insurance.Key.NameOfPolicy;

                var allPayments = _db.RepaymentRecords.Local.Where(
                    r => r.CorrespondingLiabilityGroupingKey == key && r.PaymentOn.HasValue
                         && r.PaymentOn.Value <= beforeDate).ToList();
                allPayments.AddRange(await _db.RepaymentRecords.Where(r => r.CorrespondingLiabilityGroupingKey == key
                                                                      && r.PaymentOn.HasValue &&
                                                                      r.PaymentOn.Value <= beforeDate).ToListAsync());


                var totalPaid = allPayments.Distinct().Sum(p => p.PrincipleAmount).GetValueOrDefault();
                result.Add(new Insurance(this)
                {
                    Id = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().Id,
                    GrantedOn = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().CreatedOn.Value,
                    ExpiryDate = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().ExpiryDate.Value,
                    PolicyType = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().PolicyType,
                    AmountInsured = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().AmountInsured.GetValueOrDefault(),
                    InsuranceType = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().InsuranceType,
                    Issurer = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().Issuer,
                    NameOfPolicy = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().NameOfPolicy,
                    PolicyAddress = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().PolicyAddress,
                    PolicyName = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().NameOfPolicy,
                    PolicyNumber = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().PolicyNumber,
                    Premium = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().Premium.GetValueOrDefault(),
                    CurrentBalance = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().AmountInsured.GetValueOrDefault() - totalPaid
                });
            }
        }

        private void GenerateInsuranceForAccountSync(List<LiabilityBase> result, Account account, DateTime beforeDate)
        {

            var insuranceGroups = account.Insurances.GroupBy(ins => new { ins.NameOfPolicy, ins.PolicyNumber });
            foreach (var insurance in insuranceGroups)
            {
                var key = insurance.Key.PolicyNumber + "*" + insurance.Key.NameOfPolicy;

                var allPayments = _db.RepaymentRecords.Local.Where(
                    r => r.CorrespondingLiabilityGroupingKey == key && r.PaymentOn.HasValue
                         && r.PaymentOn.Value <= beforeDate).ToList();
                allPayments.AddRange(_db.RepaymentRecords.Where(r => r.CorrespondingLiabilityGroupingKey == key
                                                                      && r.PaymentOn.HasValue &&
                                                                      r.PaymentOn.Value <= beforeDate).ToList());


                var totalPaid = allPayments.Distinct().Sum(p => p.PrincipleAmount).GetValueOrDefault();
                result.Add(new Insurance(this)
                {
                    Id = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().Id,
                    GrantedOn = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().CreatedOn.Value,
                    ExpiryDate = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().ExpiryDate.Value,
                    PolicyType = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().PolicyType,
                    AmountInsured = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().AmountInsured.GetValueOrDefault(),
                    InsuranceType = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().InsuranceType,
                    Issurer = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().Issuer,
                    NameOfPolicy = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().NameOfPolicy,
                    PolicyAddress = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().PolicyAddress,
                    PolicyName = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().NameOfPolicy,
                    PolicyNumber = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().PolicyNumber,
                    Premium = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().Premium.GetValueOrDefault(),
                    CurrentBalance = insurance.OrderByDescending(ins => ins.PurchasedOn).FirstOrDefault().AmountInsured.GetValueOrDefault() - totalPaid
                });
            }
        }


        private async Task GenerateMortgageAndHomeLoanForAccount(DateTime beforeDate, Account account, List<LiabilityBase> result)
        {
            foreach (var group in account.MortgageHomeLoans.GroupBy(m => new { m.CorrespondingProperty.PropertyId, m.Account.AccountId }))
            {
                var mortgageHomeLoan = group.OrderByDescending(g => g.LoanAquiredOn).FirstOrDefault();
                var clientGroup =
                    _db.ClientGroups.Local
                        .FirstOrDefault(g => g.GroupAccounts.Any(ac => ac.AccountId == account.AccountId) ||
                                             _db.Clients.Any(
                                                 c =>
                                                     c.ClientGroupId == g.ClientGroupId &&
                                                     c.Accounts.Any(acc => acc.AccountId == account.AccountId))) ??
                    await
                        _db.ClientGroups.Where(
                            g =>
                                g.GroupAccounts.Any(ac => ac.AccountId == account.AccountId) ||
                                _db.Clients.Any(
                                    c =>
                                        c.ClientGroupId == g.ClientGroupId &&
                                        c.Accounts.Any(acc => acc.AccountId == account.AccountId)))
                            .FirstOrDefaultAsync();


                var clientAverageAge =
                    _db.Clients.Where(c => c.ClientGroupId == clientGroup.ClientGroupId)
                        .ToList()
                        .Sum(c => c.Dob == null || c.Dob.Value == null ? 0 : (beforeDate - c.Dob.Value).Days) / 365 /
                    _db.Clients.Count(c => c.ClientGroupId == clientGroup.ClientGroupId);

                var key = group.Key.PropertyId + "*" + group.Key.AccountId;

                var allPayments = _db.RepaymentRecords.Local.Where(
                    r => r.CorrespondingLiabilityGroupingKey == key && r.PaymentOn.HasValue
                         && r.PaymentOn.Value <= beforeDate).ToList();
                allPayments.AddRange(await
                                     _db.RepaymentRecords.Where(
                                         r =>
                                             r.CorrespondingLiabilityGroupingKey == key && r.PaymentOn.HasValue &&
                                             r.PaymentOn.Value <= beforeDate).ToListAsync());




                var amountPaid = allPayments.Distinct().Sum(p => p.PrincipleAmount).GetValueOrDefault();



                MortgageAndHomeLiability mortgage = new MortgageAndHomeLiability(this)
                {
                    GrantedOn = mortgageHomeLoan.LoanAquiredOn.Value,
                    CurrentFiancialYearInterest = mortgageHomeLoan.InterestRate.Any()
                        ? mortgageHomeLoan.InterestRate.OrderByDescending(r => r.EffectiveFrom)
                            .FirstOrDefault()
                            .Rate.GetValueOrDefault()
                        : 0,
                    Id = mortgageHomeLoan.Id,
                    TypeOfMortgageRates = mortgageHomeLoan.TypeOfMortgageRates,
                    Property = new DirectProperty(this)
                    {
                        Id = mortgageHomeLoan.PropertyId,
                        PropertyType = mortgageHomeLoan.CorrespondingProperty.PropertyType,
                        AbilityToPayAboveCurrentInterestRate =
                            GetAbilityToPayAboveCurrentInterestRateForAccount(account.AccountId,
                                mortgageHomeLoan.CorrespondingProperty.PropertyId),
                        City = mortgageHomeLoan.CorrespondingProperty.City,
                        ClientAccountId = account.AccountId,
                        ClientAverageAge = clientAverageAge,
                        Country = mortgageHomeLoan.CorrespondingProperty.Country,
                        FullAddress = mortgageHomeLoan.CorrespondingProperty.FullAddress,
                        LatestPrice =
                            mortgageHomeLoan.CorrespondingProperty.Prices.Any()
                                ? mortgageHomeLoan.CorrespondingProperty.Prices.OrderByDescending(p => p.CreatedOn)
                                    .FirstOrDefault()
                                    .Price.GetValueOrDefault()
                                : 0,
                        Latitude = mortgageHomeLoan.CorrespondingProperty.Latitude,
                        Longitude = mortgageHomeLoan.CorrespondingProperty.Longitude,
                        PlaceId = mortgageHomeLoan.CorrespondingProperty.GooglePlaceId,
                        Postcode = mortgageHomeLoan.CorrespondingProperty.Postcode,
                        PropertyLeverage =
                            GetPropertyLeverageForAccount(account.AccountId, mortgageHomeLoan.CorrespondingProperty.PropertyId),
                        State = mortgageHomeLoan.CorrespondingProperty.State,
                        StreetAddress = mortgageHomeLoan.CorrespondingProperty.StreetAddress,
                        TotalNumberOfUnits =
                            mortgageHomeLoan.CorrespondingProperty.PropertyTransactions.Count(
                                p => p.IsBuy.HasValue && p.IsBuy.Value)
                            -
                            mortgageHomeLoan.CorrespondingProperty.PropertyTransactions.Count(
                                p => p.IsBuy.HasValue && !p.IsBuy.Value),
                        YearsToRetirement = RetirementAge - clientAverageAge
                    },
                    CurrencyType = CurrencyType.AustralianDollar,
                    CurrentBalance = mortgageHomeLoan.LoanAmount.GetValueOrDefault() - amountPaid,
                    ExpiryDate = mortgageHomeLoan.LoanExpiryDate.Value,
                    LoanContractTermInYears = (mortgageHomeLoan.LoanAquiredOn.Value - mortgageHomeLoan.LoanExpiryDate.Value).TotalDays / 365,
                    LoanProviderInstitution = mortgageHomeLoan.Institution,
                    LoanRepaymentType = mortgageHomeLoan.LoanRepaymentType
                };



                result.Add(mortgage);
            }
        }



        private void GenerateMortgageAndHomeLoanForAccountSync(DateTime beforeDate, Account account, List<LiabilityBase> result)
        {
            foreach (var group in account.MortgageHomeLoans.GroupBy(m => new { m.CorrespondingProperty.PropertyId, m.Account.AccountId }))
            {
                var mortgageHomeLoan = group.OrderByDescending(g => g.LoanAquiredOn).FirstOrDefault();
                var clientGroup =
                    _db.ClientGroups.Local
                        .FirstOrDefault(g => g.GroupAccounts.Any(ac => ac.AccountId == account.AccountId) ||
                                             _db.Clients.Any(
                                                 c =>
                                                     c.ClientGroupId == g.ClientGroupId &&
                                                     c.Accounts.Any(acc => acc.AccountId == account.AccountId))) ??
                        _db.ClientGroups.Where(
                            g =>
                                g.GroupAccounts.Any(ac => ac.AccountId == account.AccountId) ||
                                _db.Clients.Any(
                                    c =>
                                        c.ClientGroupId == g.ClientGroupId &&
                                        c.Accounts.Any(acc => acc.AccountId == account.AccountId)))
                            .FirstOrDefault();


                var clientAverageAge =
                    _db.Clients.Where(c => c.ClientGroupId == clientGroup.ClientGroupId)
                        .ToList()
                        .Sum(c => c.Dob == null || c.Dob.Value == null ? 0 : (beforeDate - c.Dob.Value).Days) / 365 /                 //Dob == null ???
                    _db.Clients.Count(c => c.ClientGroupId == clientGroup.ClientGroupId);

                var key = group.Key.PropertyId + "*" + group.Key.AccountId;

                var allPayments = _db.RepaymentRecords.Local.Where(
                    r => r.CorrespondingLiabilityGroupingKey == key && r.PaymentOn.HasValue
                         && r.PaymentOn.Value <= beforeDate).ToList();
                allPayments.AddRange(_db.RepaymentRecords.Where(
                                         r =>
                                             r.CorrespondingLiabilityGroupingKey == key && r.PaymentOn.HasValue &&
                                             r.PaymentOn.Value <= beforeDate).ToList());

                var amountPaid = allPayments.Distinct().Sum(p => p.PrincipleAmount).GetValueOrDefault();

                MortgageAndHomeLiability mortgage = new MortgageAndHomeLiability(this)
                {
                    GrantedOn = mortgageHomeLoan.LoanAquiredOn.Value,
                    CurrentFiancialYearInterest = mortgageHomeLoan.InterestRate.Any()
                        ? mortgageHomeLoan.InterestRate.OrderByDescending(r => r.EffectiveFrom)
                            .FirstOrDefault()
                            .Rate.GetValueOrDefault()
                        : 0,
                    Id = mortgageHomeLoan.Id,
                    TypeOfMortgageRates = mortgageHomeLoan.TypeOfMortgageRates,
                    Property = new DirectProperty(this)
                    {
                        Id = mortgageHomeLoan.PropertyId,
                        PropertyType = mortgageHomeLoan.CorrespondingProperty.PropertyType,
                        AbilityToPayAboveCurrentInterestRate =
                            GetAbilityToPayAboveCurrentInterestRateForAccount(account.AccountId,
                                mortgageHomeLoan.CorrespondingProperty.PropertyId),
                        City = mortgageHomeLoan.CorrespondingProperty.City,
                        ClientAccountId = account.AccountId,
                        ClientAverageAge = clientAverageAge,
                        Country = mortgageHomeLoan.CorrespondingProperty.Country,
                        FullAddress = mortgageHomeLoan.CorrespondingProperty.FullAddress,
                        LatestPrice =
                            mortgageHomeLoan.CorrespondingProperty.Prices.Any()
                                ? mortgageHomeLoan.CorrespondingProperty.Prices.OrderByDescending(p => p.CreatedOn)
                                    .FirstOrDefault()
                                    .Price.GetValueOrDefault()
                                : 0,
                        Latitude = mortgageHomeLoan.CorrespondingProperty.Latitude,
                        Longitude = mortgageHomeLoan.CorrespondingProperty.Longitude,
                        PlaceId = mortgageHomeLoan.CorrespondingProperty.GooglePlaceId,
                        Postcode = mortgageHomeLoan.CorrespondingProperty.Postcode,
                        PropertyLeverage =
                            GetPropertyLeverageForAccount(account.AccountId, mortgageHomeLoan.CorrespondingProperty.PropertyId),
                        State = mortgageHomeLoan.CorrespondingProperty.State,
                        StreetAddress = mortgageHomeLoan.CorrespondingProperty.StreetAddress,
                        TotalNumberOfUnits =
                            mortgageHomeLoan.CorrespondingProperty.PropertyTransactions.Count(
                                p => p.IsBuy.HasValue && p.IsBuy.Value)
                            -
                            mortgageHomeLoan.CorrespondingProperty.PropertyTransactions.Count(
                                p => p.IsBuy.HasValue && !p.IsBuy.Value),
                        YearsToRetirement = RetirementAge - clientAverageAge
                    },
                    CurrencyType = CurrencyType.AustralianDollar,
                    CurrentBalance = mortgageHomeLoan.LoanAmount.GetValueOrDefault() - amountPaid,
                    ExpiryDate = mortgageHomeLoan.LoanExpiryDate.Value,
                    LoanContractTermInYears = (mortgageHomeLoan.LoanAquiredOn.Value - mortgageHomeLoan.LoanExpiryDate.Value).TotalDays / 365,
                    LoanProviderInstitution = mortgageHomeLoan.Institution,
                    LoanRepaymentType = mortgageHomeLoan.LoanRepaymentType
                };

                result.Add(mortgage);
            }
        }

        /// <summary>
        /// Only retrieve one margin lending loan, as there should always be at most 1 margin lending per account.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="result"></param>
        private async Task GenerateMarginLendingForAccount(Account account, List<LiabilityBase> result, DateTime beforeDate)
        {
            var oneYearAgo = DateTime.Now.AddYears(-1);
            foreach (var group in account.MarginLendings.GroupBy(m => m.Account.AccountId))
            {

                var lending = group.OrderByDescending(c => c.GrantedOn).FirstOrDefault();
                var key = group.Key;


                var allPayments = _db.RepaymentRecords.Local.Where(p => p.CorrespondingLiabilityGroupingKey == key
                                                                        && p.PaymentOn.HasValue &&
                                                                        p.PaymentOn.Value <= beforeDate).ToList();
                allPayments.AddRange(await
                                     _db.RepaymentRecords.Where(
                                         r => r.CorrespondingLiabilityGroupingKey == key && r.PaymentOn.HasValue
                                              && r.PaymentOn.Value <= beforeDate).ToListAsync());


                var paidAmount = allPayments.Distinct().Sum(p => p.PrincipleAmount).GetValueOrDefault();


                MarginLending marginLending = new MarginLending(this)
                {
                    GrantedOn = lending.GrantedOn.Value,
                    ExpiryDate = lending.ExpiryDate.Value,
                    Id = lending.Id,
                    LoanAmount = lending.LoanAmount.Value,
                    Securities = new List<Security>(),
                    CurrentBalance = lending.LoanAmount.Value - paidAmount,
                    CurrentInterestRate = lending.LiabilityRates.OrderByDescending(r => r.EffectiveFrom).FirstOrDefault().Rate.GetValueOrDefault()
                };
                //foreach (var ratio in lending.LoanValueRatios)
                //{

                //    #region insert fixed income
                //    if (ratio.AssetTypes == AssetTypes.FixedIncomeInvestments)
                //    {
                //        var bond = _db.Bonds.Local.SingleOrDefault(b => b.BondId == ratio.EquityId) ??
                //                   await _db.Bonds.Where(b => b.BondId == ratio.EquityId)
                //                   .Include(b => b.Prices)
                //                   .Include(b => b.BondTransactions)
                //                   .Include(b => b.CouponPayments)
                //                   .Include(b => b.ResearchValues)
                //                   .SingleOrDefaultAsync();
                //        marginLending.Securities.Add(new Security()
                //        {
                //            LoanValueRatio = lending.LoanValueRatios.Any(r => r.EquityId == bond.BondId)
                //            ? lending.LoanValueRatios.FirstOrDefault(r => r.EquityId == bond.BondId).Ratio
                //            : 0,
                //            Asset = new FixedIncome(this)
                //            {
                //                TotalNumberOfUnits = bond.BondTransactions.Sum(t => t.NumberOfUnits).GetValueOrDefault(),
                //                Ticker = bond.Ticker,
                //                Id = bond.BondId,
                //                CouponFrequency = bond.Frequency,
                //                LatestPrice =
                //                bond.Prices.Any()
                //                    ? bond.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault()
                //                    : 0,
                //                ClientAccountId = account.AccountId,
                //                BondType = bond.BondType,
                //                //todo possible performance hazard 
                //                BoundDetails = GetBondDetails(bond.Ticker).Result,
                //                CouponRate =
                //                bond.CouponPayments.Where(c => c.PaymentOn <= DateTime.Now && c.PaymentOn <= oneYearAgo)
                //                    .Sum(p => p.Amount) /
                //                bond.BondTransactions.Sum(b => b.NumberOfUnits) /
                //                bond.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price,
                //                FixedIncomeName = bond.Name,
                //                Issuer = bond.Issuer
                //            }
                //        });
                //    }
                //    #endregion

                //    #region australian equity
                //    if (ratio.AssetTypes == AssetTypes.AustralianEquity)
                //    {
                //        var australianEquity = _db.Equities.Local.SingleOrDefault(
                //            eq => eq.EquityType == EquityTypes.AustralianEquity
                //                  && eq.AssetId == ratio.EquityId) ??
                //                               await
                //                                   _db.Equities.Where(
                //                                       eq =>
                //                                           eq.EquityType == EquityTypes.AustralianEquity &&
                //                                           eq.AssetId == ratio.EquityId)
                //                                       .Include(eq => eq.EquityTransactions)
                //                                       .Include(eq => eq.Dividends)
                //                                       .Include(eq => eq.Prices)
                //                                       .Include(eq => eq.ResearchValues)
                //                                       .SingleOrDefaultAsync();
                //        marginLending.Securities.Add(new Security()
                //        {
                //            LoanValueRatio = ratio.Ratio,
                //            Asset = new AustralianEquity(this)
                //            {
                //                Id = australianEquity.AssetId,
                //                ClientAccountId = account.AccountId,
                //                F0Ratios = await GetF0RatiosForEquity(australianEquity.Ticker),
                //                Ticker = australianEquity.Ticker,
                //                Name = australianEquity.Name,
                //                F1Recommendation = await GetF1RatiosForEquity(australianEquity.Ticker),
                //                LatestPrice = australianEquity.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault(),
                //                Sector = australianEquity.Sector,
                //                TotalNumberOfUnits = australianEquity.EquityTransactions.Sum(t => t.NumberOfUnits).GetValueOrDefault()
                //            }
                //        });
                //    }
                //    #endregion

                //    #region international equity
                //    if (ratio.AssetTypes == AssetTypes.InternationalEquity)
                //    {
                //        var internationalEquity =
                //            _db.Equities.Local.SingleOrDefault(eq => eq.AssetId == ratio.EquityId) ??
                //            await _db.Equities.Where(eq => eq.AssetId == ratio.EquityId)
                //                .Include(ins => ins.Dividends)
                //                .Include(ins => ins.EquityTransactions)
                //                .Include(ins => ins.Prices)
                //                .Include(ins => ins.ResearchValues)
                //                .FirstOrDefaultAsync();

                //        marginLending.Securities.Add(new Security()
                //        {
                //            LoanValueRatio = ratio.Ratio,
                //            Asset = new InternationalEquity(this)
                //            {
                //                Id = internationalEquity.AssetId,
                //                Ticker = internationalEquity.Ticker,
                //                Name = internationalEquity.Name,
                //                TotalNumberOfUnits = internationalEquity.EquityTransactions.Sum(t => t.NumberOfUnits).GetValueOrDefault(),
                //                LatestPrice = internationalEquity.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault(),
                //                ClientAccountId = account.AccountId,
                //                F1Recommendation = await GetF1RatiosForEquity(internationalEquity.Ticker),
                //                Sector = internationalEquity.Sector,
                //                F0Ratios = await GetF0RatiosForEquity(internationalEquity.Ticker)
                //            }
                //        });
                //    }
                //    #endregion


                //    #region managed investment
                //    if (ratio.AssetTypes == AssetTypes.ManagedInvestments)
                //    {
                //        var managedInvestment = _db.Equities.Local.FirstOrDefault(eq => eq.AssetId == ratio.EquityId) ??
                //                                await _db.Equities.Where(eq => eq.AssetId == ratio.EquityId)
                //                                    .Include(eq => eq.Dividends)
                //                                    .Include(eq => eq.EquityTransactions)
                //                                    .Include(eq => eq.Prices)
                //                                    .SingleOrDefaultAsync();
                //        marginLending.Securities.Add(new Security()
                //        {
                //            LoanValueRatio = ratio.MaxRatio,
                //            Asset = new ManagedInvestment(this)
                //            {
                //                Ticker = managedInvestment.Ticker,
                //                Name = managedInvestment.Name,
                //                Id = managedInvestment.AssetId,
                //                TotalNumberOfUnits = managedInvestment.EquityTransactions.Sum(t => t.NumberOfUnits).GetValueOrDefault(),
                //                LatestPrice = managedInvestment.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault(),
                //                ClientAccountId = account.AccountId,
                //                F1Recommendation = await GetF1RatiosForEquity(managedInvestment.Ticker),
                //                Sector = managedInvestment.Sector,
                //                F0Ratios = await GetF0RatiosForEquity(managedInvestment.Ticker),
                //                FundAllocation = GetFundAllocationForManagedFund(managedInvestment.AssetId),
                //            }
                //        });
                //    }
                //    #endregion


                //}
                result.Add(marginLending);
            }
        }


        private void GenerateMarginLendingForAccountSync(Account account, List<LiabilityBase> result, DateTime beforeDate)
        {
            var oneYearAgo = DateTime.Now.AddYears(-1);
            foreach (var group in account.MarginLendings.GroupBy(m => m.Account.AccountId))
            {

                var lending = group.OrderByDescending(c => c.GrantedOn).FirstOrDefault();
                var key = group.Key;


                var allPayments = _db.RepaymentRecords.Local.Where(p => p.CorrespondingLiabilityGroupingKey == key
                                                                        && p.PaymentOn.HasValue &&
                                                                        p.PaymentOn.Value <= beforeDate).ToList();
                allPayments.AddRange(_db.RepaymentRecords.Where(
                                         r => r.CorrespondingLiabilityGroupingKey == key && r.PaymentOn.HasValue
                                              && r.PaymentOn.Value <= beforeDate).ToList());


                var paidAmount = allPayments.Distinct().Sum(p => p.PrincipleAmount).GetValueOrDefault();


                MarginLending marginLending = new MarginLending(this)
                {
                    GrantedOn = lending.GrantedOn.Value,
                    ExpiryDate = lending.ExpiryDate.Value,
                    Id = lending.Id,
                    LoanAmount = lending.LoanAmount.Value,
                    Securities = new List<Security>(),
                    CurrentBalance = lending.LoanAmount.Value - paidAmount,
                    CurrentInterestRate = lending.LiabilityRates.Count == 0 ? 0 : lending.LiabilityRates.OrderByDescending(r => r.EffectiveFrom).FirstOrDefault().Rate.GetValueOrDefault()
                };

                if (lending.AssetTypes == AssetTypes.FixedIncomeInvestments) {
                    var bond = _db.Bonds.Local.SingleOrDefault(b => b.BondId == lending.AssetId) ??
                                   _db.Bonds.Where(b => b.BondId == lending.AssetId)
                                   .Include(b => b.Prices)
                                   .Include(b => b.BondTransactions)
                                   .Include(b => b.CouponPayments)
                                   .Include(b => b.ResearchValues)
                                   .SingleOrDefault();
                    marginLending.Securities.Add(new Security() {
                        LoanValueRatio = lending.Ratio,
                        Asset = new FixedIncome(this) {
                            TotalNumberOfUnits = bond.BondTransactions.Sum(t => t.NumberOfUnits).GetValueOrDefault(),
                            Ticker = bond.Ticker,
                            Id = bond.BondId,
                            CouponFrequency = bond.Frequency,
                            LatestPrice =
                            bond.Prices.Any()
                                ? bond.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault()
                                : 0,
                            ClientAccountId = account.AccountId,
                            BondType = bond.BondType,
                            //todo possible performance hazard 
                            BoundDetails = GetBondDetails(bond.Ticker).Result,
                            CouponRate =
                            bond.CouponPayments.Where(c => c.PaymentOn <= DateTime.Now && c.PaymentOn <= oneYearAgo)
                                .Sum(p => p.Amount) /
                            bond.BondTransactions.Sum(b => b.NumberOfUnits) /
                            bond.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price,
                            FixedIncomeName = bond.Name,
                            Issuer = bond.Issuer
                        }
                    });
                }

                if (lending.AssetTypes == AssetTypes.AustralianEquity) {
                    var australianEquity = _db.Equities.Local.SingleOrDefault(
                        eq => eq.EquityType == EquityTypes.AustralianEquity
                              && eq.AssetId == lending.AssetId) ??
                                               _db.Equities.Where(
                                                   eq =>
                                                       eq.EquityType == EquityTypes.AustralianEquity &&
                                                       eq.AssetId == lending.AssetId)
                                                   .Include(eq => eq.EquityTransactions)
                                                   .Include(eq => eq.Dividends)
                                                   .Include(eq => eq.Prices)
                                                   .Include(eq => eq.ResearchValues)
                                                   .SingleOrDefault();
                    marginLending.Securities.Add(new Security() {
                        LoanValueRatio = lending.Ratio,
                        Asset = new AustralianEquity(this) {
                            Id = australianEquity.AssetId,
                            Name = australianEquity.Name,
                            ClientAccountId = account.AccountId,
                            F0Ratios = GetF0RatiosForEquitySync(australianEquity.Ticker),
                            Ticker = australianEquity.Ticker,
                            F1Recommendation = GetF1RatiosForEquitySync(australianEquity.Ticker),
                            LatestPrice = australianEquity.Prices.Count == 0 ? 0 : australianEquity.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault(),
                            Sector = australianEquity.Sector,
                            TotalNumberOfUnits = australianEquity.EquityTransactions.Sum(t => t.NumberOfUnits).GetValueOrDefault()
                        }
                    });
                }

                if (lending.AssetTypes == AssetTypes.InternationalEquity) {
                    var internationalEquity =
                        _db.Equities.Local.SingleOrDefault(eq => eq.AssetId == lending.AssetId) ??
                        _db.Equities.Where(eq => eq.AssetId == lending.AssetId)
                            .Include(ins => ins.Dividends)
                            .Include(ins => ins.EquityTransactions)
                            .Include(ins => ins.Prices)
                            .Include(ins => ins.ResearchValues)
                            .FirstOrDefault();

                    marginLending.Securities.Add(new Security() {
                        LoanValueRatio = lending.Ratio,
                        Asset = new InternationalEquity(this) {
                            Id = internationalEquity.AssetId,
                            Ticker = internationalEquity.Ticker,
                            Name = internationalEquity.Name,
                            TotalNumberOfUnits = internationalEquity.EquityTransactions.Sum(t => t.NumberOfUnits).GetValueOrDefault(),
                            LatestPrice = internationalEquity.Prices.Count == 0 ? 0 : internationalEquity.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault(),
                            ClientAccountId = account.AccountId,
                            F1Recommendation = GetF1RatiosForEquitySync(internationalEquity.Ticker),
                            Sector = internationalEquity.Sector,
                            F0Ratios = GetF0RatiosForEquitySync(internationalEquity.Ticker)
                        }
                    });
                }

                if (lending.AssetTypes == AssetTypes.ManagedInvestments) {
                    var managedInvestment = _db.Equities.Local.FirstOrDefault(eq => eq.AssetId == lending.AssetId) ??
                                            _db.Equities.Where(eq => eq.AssetId == lending.AssetId)
                                                .Include(eq => eq.Dividends)
                                                .Include(eq => eq.EquityTransactions)
                                                .Include(eq => eq.Prices)
                                                .SingleOrDefault();
                    marginLending.Securities.Add(new Security() {
                        LoanValueRatio = lending.Ratio,
                        Asset = new ManagedInvestment(this) {
                            Ticker = managedInvestment.Ticker,
                            Name = managedInvestment.Name,
                            Id = managedInvestment.AssetId,
                            TotalNumberOfUnits = managedInvestment.EquityTransactions.Sum(t => t.NumberOfUnits).GetValueOrDefault(),
                            LatestPrice = managedInvestment.Prices.Count == 0 ? 0 : managedInvestment.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault(),
                            ClientAccountId = account.AccountId,
                            F1Recommendation = GetF1RatiosForEquitySync(managedInvestment.Ticker),
                            Sector = managedInvestment.Sector,
                            F0Ratios = GetF0RatiosForEquitySync(managedInvestment.Ticker),
                            FundAllocation = GetFundAllocationForManagedFund(managedInvestment.AssetId),
                        }
                    });
                }

                //foreach (var ratio in lending.LoanValueRatios)
                //{

                //    #region insert fixed income
                //    if (ratio.AssetTypes == AssetTypes.FixedIncomeInvestments)
                //    {
                //        var bond = _db.Bonds.Local.SingleOrDefault(b => b.BondId == ratio.EquityId) ??
                //                   _db.Bonds.Where(b => b.BondId == ratio.EquityId)
                //                   .Include(b => b.Prices)
                //                   .Include(b => b.BondTransactions)
                //                   .Include(b => b.CouponPayments)
                //                   .Include(b => b.ResearchValues)
                //                   .SingleOrDefault();
                //        marginLending.Securities.Add(new Security()
                //        {
                //            LoanValueRatio = lending.LoanValueRatios.Any(r => r.EquityId == bond.BondId)
                //            ? lending.LoanValueRatios.FirstOrDefault(r => r.EquityId == bond.BondId).Ratio
                //            : 0,
                //            Asset = new FixedIncome(this)
                //            {
                //                TotalNumberOfUnits = bond.BondTransactions.Sum(t => t.NumberOfUnits).GetValueOrDefault(),
                //                Ticker = bond.Ticker,
                //                Id = bond.BondId,
                //                CouponFrequency = bond.Frequency,
                //                LatestPrice =
                //                bond.Prices.Any()
                //                    ? bond.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault()
                //                    : 0,
                //                ClientAccountId = account.AccountId,
                //                BondType = bond.BondType,
                //                //todo possible performance hazard 
                //                BoundDetails = GetBondDetails(bond.Ticker).Result,
                //                CouponRate =
                //                bond.CouponPayments.Where(c => c.PaymentOn <= DateTime.Now && c.PaymentOn <= oneYearAgo)
                //                    .Sum(p => p.Amount) /
                //                bond.BondTransactions.Sum(b => b.NumberOfUnits) /
                //                bond.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price,
                //                FixedIncomeName = bond.Name,
                //                Issuer = bond.Issuer
                //            }
                //        });
                //    }
                //    #endregion

                //    #region australian equity
                //    if (ratio.AssetTypes == AssetTypes.AustralianEquity)
                //    {
                //        var australianEquity = _db.Equities.Local.SingleOrDefault(
                //            eq => eq.EquityType == EquityTypes.AustralianEquity
                //                  && eq.AssetId == ratio.EquityId) ??
                //                                   _db.Equities.Where(
                //                                       eq =>
                //                                           eq.EquityType == EquityTypes.AustralianEquity &&
                //                                           eq.AssetId == ratio.EquityId)
                //                                       .Include(eq => eq.EquityTransactions)
                //                                       .Include(eq => eq.Dividends)
                //                                       .Include(eq => eq.Prices)
                //                                       .Include(eq => eq.ResearchValues)
                //                                       .SingleOrDefault();
                //        marginLending.Securities.Add(new Security()
                //        {
                //            LoanValueRatio = ratio.Ratio,
                //            Asset = new AustralianEquity(this)
                //            {
                //                Id = australianEquity.AssetId,
                //                Name = australianEquity.Name,
                //                ClientAccountId = account.AccountId,
                //                F0Ratios = GetF0RatiosForEquitySync(australianEquity.Ticker),
                //                Ticker = australianEquity.Ticker,
                //                F1Recommendation = GetF1RatiosForEquitySync(australianEquity.Ticker),
                //                LatestPrice = australianEquity.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault(),
                //                Sector = australianEquity.Sector,
                //                TotalNumberOfUnits = australianEquity.EquityTransactions.Sum(t => t.NumberOfUnits).GetValueOrDefault()
                //            }
                //        });
                //    }
                //    #endregion

                //    #region international equity
                //    if (ratio.AssetTypes == AssetTypes.InternationalEquity)
                //    {
                //        var internationalEquity =
                //            _db.Equities.Local.SingleOrDefault(eq => eq.AssetId == ratio.EquityId) ??
                //            _db.Equities.Where(eq => eq.AssetId == ratio.EquityId)
                //                .Include(ins => ins.Dividends)
                //                .Include(ins => ins.EquityTransactions)
                //                .Include(ins => ins.Prices)
                //                .Include(ins => ins.ResearchValues)
                //                .FirstOrDefault();

                //        marginLending.Securities.Add(new Security()
                //        {
                //            LoanValueRatio = ratio.Ratio,
                //            Asset = new InternationalEquity(this)
                //            {
                //                Id = internationalEquity.AssetId,
                //                Ticker = internationalEquity.Ticker,
                //                Name = internationalEquity.Name,
                //                TotalNumberOfUnits = internationalEquity.EquityTransactions.Sum(t => t.NumberOfUnits).GetValueOrDefault(),
                //                LatestPrice = internationalEquity.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault(),
                //                ClientAccountId = account.AccountId,
                //                F1Recommendation = GetF1RatiosForEquitySync(internationalEquity.Ticker),
                //                Sector = internationalEquity.Sector,
                //                F0Ratios = GetF0RatiosForEquitySync(internationalEquity.Ticker)
                //            }
                //        });
                //    }
                //    #endregion


                //    #region managed investment
                //    if (ratio.AssetTypes == AssetTypes.ManagedInvestments)
                //    {
                //        var managedInvestment = _db.Equities.Local.FirstOrDefault(eq => eq.AssetId == ratio.EquityId) ??
                //                                _db.Equities.Where(eq => eq.AssetId == ratio.EquityId)
                //                                    .Include(eq => eq.Dividends)
                //                                    .Include(eq => eq.EquityTransactions)
                //                                    .Include(eq => eq.Prices)
                //                                    .SingleOrDefault();
                //        marginLending.Securities.Add(new Security()
                //        {
                //            LoanValueRatio = ratio.Ratio,
                //            Asset = new ManagedInvestment(this)
                //            {
                //                Ticker = managedInvestment.Ticker,
                //                Name = managedInvestment.Name,
                //                Id = managedInvestment.AssetId,
                //                TotalNumberOfUnits = managedInvestment.EquityTransactions.Sum(t => t.NumberOfUnits).GetValueOrDefault(),
                //                LatestPrice = managedInvestment.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault(),
                //                ClientAccountId = account.AccountId,
                //                F1Recommendation = GetF1RatiosForEquitySync(managedInvestment.Ticker),
                //                Sector = managedInvestment.Sector,
                //                F0Ratios = GetF0RatiosForEquitySync(managedInvestment.Ticker),
                //                FundAllocation = GetFundAllocationForManagedFund(managedInvestment.AssetId),
                //            }
                //        });
                //    }
                //    #endregion


                //}
                result.Add(marginLending);
            }
        }


        #endregion
        private async Task<Adviser> GetAdviserProfile(string id, DateTime todate)
        {
            var dbAdviser =
                _db.Advisers.Local.SingleOrDefault(
                    ad => ad.AdviserId == id && ad.CreatedOn.HasValue && ad.CreatedOn.Value <= todate) ??
                   await _db.Advisers.Where(ad => ad.AdviserId == id && ad.CreatedOn.HasValue && ad.CreatedOn.Value <= todate)
                        .SingleOrDefaultAsync();
            if (dbAdviser == null)
                ProfileCannotBefound(id, todate, "adviser");
            var adviser = new Adviser(this)
            {
                Id = dbAdviser.AdviserId,
                AdviserNumber = dbAdviser.AdviserNumber,
                FirstName = dbAdviser.FirstName,
                LastName = dbAdviser.LastName
            };
            return adviser;
        }

        private Adviser GetAdviserProfileSync(string id, DateTime todate)
        {
            var dbAdviser =
                _db.Advisers.Local.SingleOrDefault(
                    ad => ad.AdviserId == id && ad.CreatedOn.HasValue && ad.CreatedOn.Value <= todate) ??
                   _db.Advisers.Where(ad => ad.AdviserId == id && ad.CreatedOn.HasValue && ad.CreatedOn.Value <= todate)
                        .SingleOrDefault();
            if (dbAdviser == null)
                ProfileCannotBefound(id, todate, "adviser");
            var adviser = new Adviser(this)
            {
                Id = dbAdviser.AdviserId,
                AdviserNumber = dbAdviser.AdviserNumber,
                FirstName = dbAdviser.FirstName,
                LastName = dbAdviser.LastName,
                ABNACN = dbAdviser.ABNACN,
                AddressLn1 = dbAdviser.AddressLn1,
                AddressLn2 = dbAdviser.AddressLn2,
                AddressLn3 = dbAdviser.AddressLn3,
                AnnualIncomeLevelId = dbAdviser.AnnualIncomeLevelId,
                ApproximateNumberOfClients = dbAdviser.ApproximateNumberOfClients,
                Asfl = dbAdviser.Asfl,
                AuthorizedRepresentativeNumber = dbAdviser.AuthorizedRepresentativeNumber,
                BusinessFax = dbAdviser.BusinessFax,
                BusinessMobile = dbAdviser.BusinessMobile,
                BusinessPhone = dbAdviser.BusinessPhone,
                CAFDescription = dbAdviser.CAFDescription,
                CAFId = dbAdviser.CAFId,
                CAFSelected = dbAdviser.CAFSelected,
                CompanyName = dbAdviser.CompanyName,
                Country = dbAdviser.Country,
                CourseStatus = dbAdviser.CourseStatus,
                CourseTitle = dbAdviser.CourseTitle,
                CreatedOn = dbAdviser.CreatedOn,
                CurrentTitle = dbAdviser.CurrentTitle,
                DAddressLine1 = dbAdviser.DAddressLine1,
                DAddressLine2 = dbAdviser.DAddressLine2,
                DAddressLine3 = dbAdviser.DAddressLine3,
                DCountry = dbAdviser.DCountry,
                DealerGroupHasDerivativesLicense = dbAdviser.DealerGroupHasDerivativesLicense,
                DealerGroupName = dbAdviser.DealerGroupName,
                DPostcode = dbAdviser.DPostcode,
                DState = dbAdviser.DState,
                DSuburb = dbAdviser.DSuburb,
                EducationLevelId = dbAdviser.EducationLevelId,
                ExperienceStartDate = dbAdviser.ExperienceStartDate,
                Fax = dbAdviser.Fax,
                Gender = dbAdviser.Gender,
                GroupName = dbAdviser.GroupName,
                Image = dbAdviser.Image,
                ImageMimeType = dbAdviser.ImageMimeType,
                IndustryExperienceStartDate = dbAdviser.IndustryExperienceStartDate,
                Institution = dbAdviser.Institution,
                InvestibleAssetLevel = dbAdviser.InvestibleAssetLevel,
                IsAuthorizedRepresentative = dbAdviser.IsAuthorizedRepresentative,
                LastUpdate = dbAdviser.LastUpdate,
                Lat = dbAdviser.Lat,
                Lng = dbAdviser.Lng,
                MiddleName = dbAdviser.MiddleName,
                Mobile = dbAdviser.Mobile,
                NewsLetterSelected = dbAdviser.NewsLetterSelected,
                NewsLetterServiceId = dbAdviser.NewsLetterServiceId,
                NewsLetterServiceName = dbAdviser.NewsLetterServiceName,
                NumberOfClientsId = dbAdviser.NumberOfClientsId,
                Phone = dbAdviser.Phone,
                PostCode = dbAdviser.PostCode,
                ProfessiontypeId = dbAdviser.ProfessiontypeId,
                Providing = dbAdviser.Providing,
                RemunerationMethod = dbAdviser.RemunerationMethod,
                RemunerationMethodSpecified = dbAdviser.RemunerationMethodSpecified,
                RoleAndServicesSummary = dbAdviser.RoleAndServicesSummary,
                ServiceId = dbAdviser.ServiceId,
                ServiceName = dbAdviser.ServiceName,
                State = dbAdviser.State,
                Suburb = dbAdviser.Suburb,
                Title = dbAdviser.Title,
                TotalAssetLevel = dbAdviser.TotalAssetLevel,
                TotalAssetLevelId = dbAdviser.TotalAssetLevelId,
                TotalAssetUnderManagement = dbAdviser.TotalAssetUnderManagement,
                TotalDirectAustralianEquitiesUnderManagement = dbAdviser.TotalDirectAustralianEquitiesUnderManagement,
                TotalDirectFixedInterestUnderManagement = dbAdviser.TotalDirectFixedInterestUnderManagement,
                TotalDirectInterantionalEquitiesUnderManagement = dbAdviser.TotalDirectInterantionalEquitiesUnderManagement,
                TotalDirectLendingBookInterestUnderManagement = dbAdviser.TotalDirectLendingBookInterestUnderManagement,
                TotalInvestmentUndermanagement = dbAdviser.TotalInvestmentUndermanagement,
                VerifiedId = dbAdviser.VerifiedId
            };
            return adviser;
        }


        private async Task<Client> GetClientProfile(string id, DateTime todate)
        {
            var dbClient =
                _db.Clients.Local
                    .FirstOrDefault(c => c.ClientId == id && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate) ??
                await _db.Clients.Where(c => c.ClientId == id && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate)
                    .Include(c => c.Accounts)
                    .FirstOrDefaultAsync();
            if (dbClient == null)
                ProfileCannotBefound(id, todate, "client");

            var client = new Client(this)
            {
                FirstName = dbClient.FirstName,

                Age = (int)((todate - dbClient.Dob.Value).TotalDays / 365),
                ClientGroupNumber = dbClient.ClientGroup.GroupNumber,
                LastName = dbClient.LastName,
                ClientNumber = dbClient.ClientNumber,
                Id = dbClient.ClientId,
                ABN = dbClient.ABN,
                ACN = dbClient.ACN,
                Address = dbClient.Address,
                ClientGroupId = dbClient.ClientGroupId,
                ClientType = dbClient.ClientType,
                CreatedOn = dbClient.CreatedOn,
                Dob = dbClient.Dob,
                Email = dbClient.Email,
                EntityName = dbClient.EntityName
            };

            return client;
        }



        private Client GetClientProfileSync(string id, DateTime todate)
        {
            var dbClient =
                _db.Clients.Local
                    .FirstOrDefault(c => c.ClientId == id && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate) ??
                 _db.Clients.Where(c => c.ClientId == id && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate)
                    .Include(c => c.Accounts)
                    .FirstOrDefault();
            if (dbClient == null)
                ProfileCannotBefound(id, todate, "client");

            var client = new Client(this)
            {
                FirstName = dbClient.FirstName,
                ClientType = dbClient.ClientType,
                ABN = dbClient.ABN,
                ACN = dbClient.ACN,
                Address = dbClient.Address,
                ClientGroupId = dbClient.ClientGroupId,
                CreatedOn = dbClient.CreatedOn,
                Dob = dbClient.Dob,
                Email = dbClient.Email,
                EntityType = dbClient.EntityType,
                Fax = dbClient.Fax,
                Gender = dbClient.Gender,
                MiddleName = dbClient.MiddleName,
                Mobile = dbClient.Mobile,
                Phone = dbClient.Phone,
                LastName = dbClient.LastName,
                EntityName = dbClient.EntityName,
                Age = dbClient.Dob == null ? 0 : (int)((todate - dbClient.Dob.Value).TotalDays / 365),
                ClientGroupNumber = dbClient.ClientGroup.GroupNumber,
                ClientNumber = dbClient.ClientNumber,
                Id = dbClient.ClientId
            };

            return client;
        }


        private ClientGroup GetClientGroupProfileSync(string id, DateTime todate)
        {
            var dbGroup =
                _db.ClientGroups.Local
                    .FirstOrDefault(c => c.ClientGroupId == id && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate) ??
                    _db.ClientGroups.Where(
                        c => c.ClientGroupId == id && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate)
                        .Include(c => c.GroupAccounts)
                        .FirstOrDefault();
            if (dbGroup == null)
                ProfileCannotBefound(id, todate, "client group");
            var group = new ClientGroup(this)
            {
                Id = id,
                ClientGroupNumber = dbGroup.GroupNumber,
                GroupName = dbGroup.GroupName,
                MainClientId = dbGroup.MainClientId
            };

            return group;
        }


        private async Task<ClientGroup> GetClientGroupProfile(string id, DateTime todate)
        {
            var dbGroup =
                _db.ClientGroups.Local
                    .FirstOrDefault(c => c.ClientGroupId == id && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate) ??
                await
                    _db.ClientGroups.Where(
                        c => c.ClientGroupId == id && c.CreatedOn.HasValue && c.CreatedOn.Value <= todate)
                        .Include(c => c.GroupAccounts)
                        .FirstOrDefaultAsync();
            if (dbGroup == null)
                ProfileCannotBefound(id, todate, "client group");
            var group = new ClientGroup(this)
            {
                Id = id,
                ClientGroupNumber = dbGroup.GroupNumber,

                MainClientId = dbGroup.MainClientId
            };

            return group;
        }
        private static void ProfileCannotBefound(string id, DateTime todate, string entity)
        {
            throw new Exception("Cannot find " + entity + " with identifier " + id + " before " +
                                todate.ToLongTimeString());
        }
        private string GenerateUniqueAdviserNumber()
        {
            var adviserNumber = MemberNumberGenerator(MemberNumberDigits);
            var numberOfTries = 0;
            var numbers = new List<string>();
            while (_db.Advisers.Any(a => a.AdviserNumber == adviserNumber) && numberOfTries < MaxNumberOfRetries)
            {
                adviserNumber = MemberNumberGenerator(MemberNumberDigits);
                numbers.Add(adviserNumber);
                numberOfTries++;
            }
            if (_db.Advisers.Any(a => a.AdviserNumber == adviserNumber))
            {
                throw new Exception("Cannot generate unique adviser number with " + numberOfTries + " tries:" +
                                    string.Join(",", numbers));
            }
            return adviserNumber;
        }
        private string GenerateUniqueClientNumber()
        {
            var clientNumber = MemberNumberGenerator(MemberNumberDigits);
            var numberOftries = 0;
            var numbers = new List<string>();
            while (numberOftries <= MaxNumberOfRetries && _db.Clients.Any(c => c.ClientNumber == clientNumber))
            {
                clientNumber = MemberNumberGenerator(MemberNumberDigits);
                numbers.Add(clientNumber);
                numberOftries++;
            }
            if (_db.Clients.Any(c => c.ClientNumber == clientNumber))
            {
                throw new Exception("Cannot generate unique client number with " + numberOftries + " tries:" +
                                    string.Join(",", numbers));
            }
            return clientNumber;
        }
        private string GenerateUniqueClientGroupNumber()
        {
            var groupNumber = MemberNumberGenerator(MemberNumberDigits);
            var numberOftries = 0;
            var numbers = new List<string>();
            while (numberOftries <= MaxNumberOfRetries && _db.ClientGroups.Any(c => c.GroupNumber == groupNumber))
            {
                groupNumber = MemberNumberGenerator(MemberNumberDigits);
                numbers.Add(groupNumber);
                numberOftries++;
            }
            if (_db.ClientGroups.Any(c => c.GroupNumber == groupNumber))
            {
                throw new Exception("Cannot generate unique client group number with " + numberOftries + " tries:" +
                                    string.Join(",", numbers));
            }
            return groupNumber;
        }
        private string GenerateUniqueAccountNumber()
        {
            var accountNumber = MemberNumberGenerator(MemberNumberDigits);
            var numberOftries = 0;
            var numbers = new List<string>();
            while (numberOftries <= MaxNumberOfRetries && _db.Accounts.Any(c => c.AccountNumber == accountNumber))
            {
                accountNumber = MemberNumberGenerator(MemberNumberDigits);
                numbers.Add(accountNumber);
                numberOftries++;
            }
            if (_db.Accounts.Any(c => c.AccountNumber == accountNumber))
            {
                throw new Exception("Cannot generate unique account number:" + string.Join(",", numbers));
            }
            return accountNumber;
        }
        private string MemberNumberGenerator(int numberOfDigits)
        {
            var result = "";
            for (var i = 0; i < numberOfDigits; i++)
            {
                result += _rdm.Next(10).ToString();
            }
            return result;
        }
        #region equity activity helpers

        private static void CollectEquityDividends(DateTime to, Account account, List<ActivityBase> result)
        {
            foreach (
                var payment in account.EquityPayments.Where(pay => pay.PaymentOn.HasValue && pay.PaymentOn.Value <= to))
            {
                var activity = new FinancialActivity
                {
                    Id = payment.Id,
                    Incomes = new List<IncomeRecordBase>(),
                    Transactions = new List<TransactionBase>(),
                    Expenses = new List<FinancialActivityCostRecord>(),
                    ActivityDate = payment.PaymentOn.GetValueOrDefault()
                };

                activity.Incomes.Add(new DividenRecord
                {
                    Id = payment.Id,
                    Amount = payment.Amount.GetValueOrDefault(),
                    Ticker = payment.Equity.Ticker,
                    Franking = payment.FrankingCredit.GetValueOrDefault(),
                    RecordTime = payment.PaymentOn.GetValueOrDefault()
                });
                result.Add(activity);
            }
        }

        private void CollectEquityTransactions(DateTime to, Account account, List<ActivityBase> result, string equityId)
        {
            foreach (
                var transaction in
                    account.EquityTransactions.Where(t => t.TransactionDate.HasValue && t.TransactionDate.Value <= to && t.EquityId==equityId))
            {
                var activity = new FinancialActivity
                {
                    Id = transaction.Id,
                    Incomes = new List<IncomeRecordBase>(),
                    Transactions = new List<TransactionBase>(),
                    ActivityDate = transaction.TransactionDate.GetValueOrDefault(),
                    Expenses = new List<FinancialActivityCostRecord>()
                };

                activity.Transactions.Add(new Domain.Portfolio.Entities.Transactions.EquityTradingTransaction
                {
                    Id = transaction.Id,
                    Ticker = transaction.Equity.Ticker,
                    NumberOfUnits = transaction.NumberOfUnits.GetValueOrDefault(),
                    TransactionTime = transaction.TransactionDate.GetValueOrDefault(),
                    AmountPerUnit = transaction.UnitPriceAtPurchase.GetValueOrDefault()
                });
                var expenses =
                    _db.TransactionExpenses.Where(
                        p =>
                            p.TransactionType == TransactionType.EquityTransaction &&
                            p.CorrespondingTransactionId == transaction.Id);
                foreach (var transactionExpense in expenses)
                {
                    var expense = new FinancialActivityCostRecord
                    {
                        Id = transactionExpense.Id,
                        Amount = transactionExpense.Amount.GetValueOrDefault(),
                        ActivityCostType = transactionExpense.ExpenseType,
                        Transaction = activity.Transactions.FirstOrDefault()
                    };
                    activity.Expenses.Add(expense);
                }
                result.Add(activity);
            }
        }

        #endregion
        #region property activity helper

        private static void CollectPropertyRentals(DateTime to, Account account, List<ActivityBase> result)
        {
            foreach (
                var rental in account.DirectPropertyPayments.Where(p => p.PaymentOn.HasValue && p.PaymentOn.Value <= to)
                )
            {
                var activity = new FinancialActivity
                {
                    Id = rental.Id,
                    Incomes = new List<IncomeRecordBase>(),
                    Transactions = new List<TransactionBase>(),
                    Expenses = new List<FinancialActivityCostRecord>(),
                    ActivityDate = rental.PaymentOn.GetValueOrDefault()
                };

                activity.Incomes.Add(new PropertyRentalRecord
                {
                    Id = rental.Id,
                    Amount = rental.Amount.GetValueOrDefault(),
                    PlaceId = rental.PropertyAddress.PropertyId,
                    RecordTime = rental.PaymentOn.GetValueOrDefault()
                });
                result.Add(activity);
            }
        }

        private void CollectPropertyTransactions(DateTime to, Account account, List<ActivityBase> result)
        {
            foreach (
                var transaction in
                    account.PropertyTransactions.Where(t => t.TransactionDate.HasValue && t.TransactionDate.Value <= to)
                )
            {
                var activity = new FinancialActivity
                {
                    Id = transaction.Id,
                    Incomes = new List<IncomeRecordBase>(),
                    Transactions = new List<TransactionBase>(),
                    ActivityDate = transaction.TransactionDate.GetValueOrDefault(),
                    Expenses = new List<FinancialActivityCostRecord>()
                };

                activity.Transactions.Add(new Domain.Portfolio.Entities.Transactions.PropertyTradingTransaction
                {
                    Id = transaction.Id,
                    NumberOfUnits = transaction.IsBuy.HasValue && transaction.IsBuy.Value ? 1 : -1,
                    //PlaceId = transaction.PropertyAddress.PropertyId,
                    TransactionTime = transaction.TransactionDate.GetValueOrDefault(),
                    //FullAddress = transaction.PropertyAddress.FullAddress,
                    AmountPerUnit = transaction.Price.GetValueOrDefault(),
                    //State = transaction.PropertyAddress.State,
                    //City = transaction.PropertyAddress.City,
                    //Longitude = transaction.PropertyAddress.Longitude.GetValueOrDefault(),
                    //Latitude = transaction.PropertyAddress.Latitude.GetValueOrDefault(),
                    //Postcode = transaction.PropertyAddress.Postcode,
                    //StreetAddress = transaction.PropertyAddress.StreetAddress,
                    //Country = transaction.PropertyAddress.Country
                });
                var expenses =
                    _db.TransactionExpenses.Where(
                        p =>
                            p.TransactionType == TransactionType.PropertyTransaction &&
                            p.CorrespondingTransactionId == transaction.Id);
                foreach (var transactionExpense in expenses)
                {
                    var expense = new FinancialActivityCostRecord
                    {
                        Id = transactionExpense.Id,
                        Amount = transactionExpense.Amount.GetValueOrDefault(),
                        ActivityCostType = transactionExpense.ExpenseType,
                        Transaction = activity.Transactions.FirstOrDefault()
                    };
                    activity.Expenses.Add(expense);
                }
                result.Add(activity);
            }
        }

        #endregion
        #region bond activity helpers

        private static void CollectCouponPayments(DateTime toDate, Account account, List<ActivityBase> result)
        {
            foreach (
                var fixedIncomePayment in
                    account.FixedIncomePayments.Where(f => f.PaymentOn.HasValue && f.PaymentOn.Value <= toDate))
            {
                var activity = new FinancialActivity
                {
                    Id = fixedIncomePayment.Id,
                    Incomes = new List<IncomeRecordBase>(),
                    Transactions = new List<TransactionBase>(),
                    Expenses = new List<FinancialActivityCostRecord>(),
                    ActivityDate = fixedIncomePayment.PaymentOn.GetValueOrDefault()
                };
                activity.Incomes.Add(new CouponPaymentRecord
                {
                    Id = fixedIncomePayment.Id,
                    Amount = fixedIncomePayment.Amount.GetValueOrDefault(),
                    Ticker = fixedIncomePayment.Bond.Ticker,
                    RecordTime = fixedIncomePayment.PaymentOn.GetValueOrDefault()
                });
                result.Add(activity);
            }
        }

        private void CollectBondTransactions(DateTime toDate, Account account, List<ActivityBase> result)
        {
            foreach (
                var transaction in
                    account.BondTransactions.Where(t => t.TransactionDate.HasValue && t.TransactionDate.Value <= toDate)
                )
            {
                var activity = new FinancialActivity
                {
                    Id = transaction.Id,
                    Incomes = new List<IncomeRecordBase>(),
                    Transactions = new List<TransactionBase>(),
                    ActivityDate = transaction.TransactionDate.GetValueOrDefault(),
                    Expenses = new List<FinancialActivityCostRecord>()
                };

                activity.Transactions.Add(new Domain.Portfolio.Entities.Transactions.BondTradingTransaction
                {
                    Id = transaction.Id,
                    Ticker = transaction.Bond.Ticker,
                    NumberOfUnits = transaction.NumberOfUnits.GetValueOrDefault(),
                    TransactionTime = transaction.TransactionDate.GetValueOrDefault(),
                    AmountPerUnit = transaction.UnitPriceAtPurchase.GetValueOrDefault()
                });
                var expenses =
                    _db.TransactionExpenses.Where(
                        p =>
                            p.TransactionType == TransactionType.BondTransaction &&
                            p.CorrespondingTransactionId == transaction.Id);
                foreach (var transactionExpense in expenses)
                {
                    var expense = new FinancialActivityCostRecord
                    {
                        Id = transactionExpense.Id,
                        Amount = transactionExpense.Amount.GetValueOrDefault(),
                        ActivityCostType = transactionExpense.ExpenseType,
                        Transaction = activity.Transactions.FirstOrDefault()
                    };
                    activity.Expenses.Add(expense);
                }
                result.Add(activity);
            }
        }

        #endregion
        #region cash account activity helpers

        private void CollectCashAccountInterests(DateTime toDate, Account account, List<ActivityBase> result)
        {
            foreach (
                var cashpayment in
                    account.CashAndTermDepositPayments.Where(c => c.PaymentOn.HasValue && c.PaymentOn.Value <= toDate))
            {
                var activity = new FinancialActivity
                {
                    Id = cashpayment.Id,
                    Incomes = new List<IncomeRecordBase>(),
                    Transactions = new List<TransactionBase>(),
                    Expenses = new List<FinancialActivityCostRecord>(),
                    ActivityDate = cashpayment.PaymentOn.GetValueOrDefault()
                };
                activity.Incomes.Add(new InterestPaymentRecord
                {
                    Id = cashpayment.Id,
                    Amount = cashpayment.Amount.GetValueOrDefault(),
                    RecordTime = cashpayment.PaymentOn.GetValueOrDefault()
                });
                result.Add(activity);
            }
        }

        private void CollectCashTransactions(DateTime toDate, Account account, List<ActivityBase> result)
        {
            foreach (
                var transaction in
                    account.CashTransactions.Where(t => t.TransactionDate.HasValue && t.TransactionDate.Value <= toDate)
                )
            {
                var activity = new FinancialActivity
                {
                    Id = transaction.Id,
                    Incomes = new List<IncomeRecordBase>(),
                    Transactions = new List<TransactionBase>(),
                    ActivityDate = transaction.TransactionDate.GetValueOrDefault(),
                    Expenses = new List<FinancialActivityCostRecord>()
                };

                activity.Transactions.Add(new CashAccountTradingTransaction
                {
                    Id = transaction.Id,
                    NumberOfUnits = transaction.Amount.GetValueOrDefault() > 0 ? 1 : -1,
                    TransactionTime = transaction.TransactionDate.GetValueOrDefault(),
                    AmountPerUnit = transaction.Amount.GetValueOrDefault(),
                    Bsb = transaction.CashAccount.Bsb,
                    CashAccountNumber = transaction.CashAccount.AccountNumber,
                    CashAccountName = transaction.CashAccount.AccountName
                });
                var expenses =
                    _db.TransactionExpenses.Where(
                        p =>
                            p.TransactionType == TransactionType.CashTransaction &&
                            p.CorrespondingTransactionId == transaction.Id);
                foreach (var transactionExpense in expenses)
                {
                    var expense = new FinancialActivityCostRecord
                    {
                        Id = transactionExpense.Id,
                        Amount = transactionExpense.Amount.GetValueOrDefault(),
                        ActivityCostType = transactionExpense.ExpenseType,
                        Transaction = activity.Transactions.FirstOrDefault()
                    };
                    activity.Expenses.Add(expense);
                }
                result.Add(activity);
            }
        }

        #endregion
        #region record transaction helpers

        private async Task RecordPropertyTransaction(TransactionCreationBase transaction, Account account,
            Edis.Db.Adviser adviser)
        {
            var record = (PropertyTransactionCreation)transaction;
            var googleServiceResult = await Task.Run(() => new GoogleGeoService(record.FullAddress));
            var googlePlaceId = googleServiceResult.GetPlaceId();
            var property = await _db.Properties.Where(p => p.FullAddress == record.FullAddress
                                                          || p.GooglePlaceId == googlePlaceId)
                .Include(p => p.Prices)
                .FirstOrDefaultAsync();
            if (account.PropertyTransactions == null)
            {
                account.PropertyTransactions = new List<PropertyTransaction>();
            }
            if (property == null)
            {
                property = new Property
                {
                    Prices = new List<AssetPrice>(),
                    PropertyId = Guid.NewGuid().ToString(),
                    PropertyTransactions = new List<PropertyTransaction>(),
                    PropertyType = record.PropertyType,
                    FullAddress = record.FullAddress,
                    Latitude = googleServiceResult.GetCoordinatesLat(),
                    State = googleServiceResult.GetState(),
                    City = googleServiceResult.GetCity(),
                    Postcode = googleServiceResult.GetPostcode(),
                    StreetAddress = googleServiceResult.GetStreetNumber() + " " + googleServiceResult.GetStreetName(),
                    Country = googleServiceResult.GetCountry(),
                    Longitude = googleServiceResult.GetCoordinatesLng(),
                    GooglePlaceId = googlePlaceId,
                    Rentals = new List<Rental>()
                };
                _db.Properties.Add(property);
            }
            if (property.PropertyTransactions == null)
            {
                property.PropertyTransactions = new List<PropertyTransaction>();
            }
            var ptransaction = new PropertyTransaction
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                Price = record.Price,
                IsBuy = record.IsBuy,
                TransactionDate = record.TransactionDate,
                PropertyAddress = property
            };
            if (property.Prices == null)
            {
                property.Prices = new List<AssetPrice>();
            }
            var price = property.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault();
            if (price == null || price.Price != record.Price)
            {
                property.Prices.Add(new AssetPrice
                {
                    Id = Guid.NewGuid().ToString(),
                    CorrespondingAssetKey = property.PropertyId,
                    CreatedOn = DateTime.Now,
                    Price = record.Price,
                    AssetType = AssetTypes.DirectAndListedProperty
                });
            }
            property.PropertyTransactions.Add(ptransaction);
            account.PropertyTransactions.Add(ptransaction);
            if (record.FeesRecords != null && record.FeesRecords.Count > 0)
            {
                foreach (var transactionFeeRecord in record.FeesRecords)
                {
                    var expense = new TransactionExpense
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = DateTime.Now,
                        Amount = transactionFeeRecord.Amount,
                        AdviserId = adviser.AdviserId,
                        IncurredOn = record.TransactionDate,
                        TransactionType = TransactionType.PropertyTransaction,
                        CorrespondingTransactionId = ptransaction.Id,
                        ExpenseType = transactionFeeRecord.TransactionExpenseType
                    };
                    _db.TransactionExpenses.Add(expense);
                }
            }
        }


        private void RecordPropertyTransactionSync(TransactionCreationBase transaction, Account account,          //added
            Edis.Db.Adviser adviser)
        {
            var record = (PropertyTransactionCreation)transaction;
            var googleServiceResult = new GoogleGeoService(record.FullAddress);
            var googlePlaceId = googleServiceResult.GetPlaceId();
            var property = _db.Properties.Where(p => p.FullAddress == record.FullAddress
                                                          || p.GooglePlaceId == googlePlaceId)
                .Include(p => p.Prices)
                .FirstOrDefault();
            if (account.PropertyTransactions == null)
            {
                account.PropertyTransactions = new List<PropertyTransaction>();
            }
            if (property == null)
            {
                property = new Property
                {
                    Prices = new List<AssetPrice>(),
                    PropertyId = Guid.NewGuid().ToString(),
                    PropertyTransactions = new List<PropertyTransaction>(),
                    PropertyType = record.PropertyType,
                    FullAddress = record.FullAddress,
                    Latitude = googleServiceResult.GetCoordinatesLat(),
                    State = googleServiceResult.GetState(),
                    City = googleServiceResult.GetCity(),
                    Postcode = googleServiceResult.GetPostcode(),
                    StreetAddress = googleServiceResult.GetStreetNumber() + " " + googleServiceResult.GetStreetName(),
                    Country = googleServiceResult.GetCountry(),
                    Longitude = googleServiceResult.GetCoordinatesLng(),
                    GooglePlaceId = googlePlaceId,
                    Rentals = new List<Rental>()
                };
                _db.Properties.Add(property);
            }
            if (property.PropertyTransactions == null)
            {
                property.PropertyTransactions = new List<PropertyTransaction>();
            }
            var ptransaction = new PropertyTransaction
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                Price = record.Price,
                IsBuy = record.IsBuy,
                TransactionDate = record.TransactionDate,
                PropertyAddress = property
            };
            if (property.Prices == null)
            {
                property.Prices = new List<AssetPrice>();
            }
            var price = property.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault();
            if (price == null || price.Price != record.Price)
            {
                property.Prices.Add(new AssetPrice
                {
                    Id = Guid.NewGuid().ToString(),
                    CorrespondingAssetKey = property.PropertyId,
                    CreatedOn = DateTime.Now,
                    Price = record.Price,
                    AssetType = AssetTypes.DirectAndListedProperty
                });
            }
            property.PropertyTransactions.Add(ptransaction);
            account.PropertyTransactions.Add(ptransaction);
            if (record.FeesRecords != null && record.FeesRecords.Count > 0)
            {
                foreach (var transactionFeeRecord in record.FeesRecords)
                {
                    var expense = new TransactionExpense
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = DateTime.Now,
                        Amount = transactionFeeRecord.Amount,
                        AdviserId = adviser.AdviserId,
                        IncurredOn = record.TransactionDate,
                        TransactionType = TransactionType.PropertyTransaction,
                        CorrespondingTransactionId = ptransaction.Id,
                        ExpenseType = transactionFeeRecord.TransactionExpenseType
                    };
                    _db.TransactionExpenses.Add(expense);
                }
            }

            var accountToMakeTrans = GetClientAccountSync(account.AccountNumber, DateTime.Now);
            record.loan.PropertyId = property.PropertyId;
            accountToMakeTrans.MakeTransactionSync(record.loan);
        }

        private async Task RecordEquityTransaction(TransactionCreationBase transaction, Account account,
            Edis.Db.Adviser adviser)
        {
            var record = (EquityTransactionCreation)transaction;
            var equity = await _db.Equities.Where(e => e.Ticker == record.Ticker)
                .Include(e => e.Prices)
                .FirstOrDefaultAsync();
            if (account.EquityTransactions == null)
            {
                account.EquityTransactions = new List<EquityTransaction>();
            }
            if (equity == null)
            {
                equity = new Equity
                {
                    AssetId = Guid.NewGuid().ToString(),
                    Sector = record.Sector,
                    EquityTransactions = new List<EquityTransaction>(),
                    Ticker = record.Ticker,
                    EquityType = record.EquityType,
                    Prices = new List<AssetPrice>(),
                    Name = record.Name,
                    Dividends = new List<Dividend>()
                };
                _db.Equities.Add(equity);
            }
            if (equity.EquityTransactions == null)
            {
                equity.EquityTransactions = new List<EquityTransaction>();
            }
            var etransaction = new EquityTransaction
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                NumberOfUnits = record.NumberOfUnits,
                TransactionDate = record.TransactionDate,
                UnitPriceAtPurchase = record.Price
            };
            if (equity.Prices == null)
            {
                equity.Prices = new List<AssetPrice>();
            }
            var price = equity.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault();
            if (price == null || price.Price != record.Price)
            {
                equity.Prices.Add(new AssetPrice
                {
                    CorrespondingAssetKey = equity.AssetId,
                    Id = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.Now,
                    Price = record.Price,
                    AssetType = record.EquityType == EquityTypes.AustralianEquity
                        ? AssetTypes.AustralianEquity
                        : record.EquityType == EquityTypes.InternationalEquity
                            ? AssetTypes.InternationalEquity
                            : AssetTypes.ManagedInvestments
                });
            }
            equity.EquityTransactions.Add(etransaction);
            account.EquityTransactions.Add(etransaction);
            if (record.FeesRecords != null && record.FeesRecords.Count > 0)
            {
                foreach (var transactionFeeRecord in record.FeesRecords)
                {
                    var expense = new TransactionExpense
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = DateTime.Now,
                        Amount = transactionFeeRecord.Amount,
                        AdviserId = adviser.AdviserId,
                        IncurredOn = record.TransactionDate,
                        TransactionType = TransactionType.EquityTransaction,
                        CorrespondingTransactionId = etransaction.Id,
                        ExpenseType = transactionFeeRecord.TransactionExpenseType
                    };
                    _db.TransactionExpenses.Add(expense);
                }
            }
        }

        private void RecordEquityTransactionSync(TransactionCreationBase transaction, Account account,            //added
            Edis.Db.Adviser adviser)
        {
            var record = (EquityTransactionCreation)transaction;
            var equity = _db.Equities.Where(e => e.Ticker == record.Ticker)
                .Include(e => e.Prices)
                .FirstOrDefault();
            if (account.EquityTransactions == null)
            {
                account.EquityTransactions = new List<EquityTransaction>();
            }
            if (equity == null)
            {
                equity = new Equity
                {
                    AssetId = Guid.NewGuid().ToString(),
                    Sector = record.Sector,
                    EquityTransactions = new List<EquityTransaction>(),
                    Ticker = record.Ticker,
                    EquityType = record.EquityType,
                    Prices = new List<AssetPrice>(),
                    Name = record.Name,
                    Dividends = new List<Dividend>()
                };
                _db.Equities.Add(equity);
            }
            if (equity.EquityTransactions == null)
            {
                equity.EquityTransactions = new List<EquityTransaction>();
            }
            var etransaction = new EquityTransaction
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                NumberOfUnits = record.NumberOfUnits,
                TransactionDate = record.TransactionDate,
                UnitPriceAtPurchase = record.Price
            };
            if (equity.Prices == null)
            {
                equity.Prices = new List<AssetPrice>();
            }
            var price = equity.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault();

            //Assets price commented out

            //if (price == null || price.Price != record.Price)
            //{
            //    equity.Prices.Add(new AssetPrice
            //    {
            //        CorrespondingAssetKey = equity.AssetId,
            //        Id = Guid.NewGuid().ToString(),
            //        CreatedOn = DateTime.Now,
            //        Price = record.Price,
            //        AssetType = record.EquityType == EquityTypes.AustralianEquity
            //            ? AssetTypes.AustralianEquity
            //            : record.EquityType == EquityTypes.InternationalEquity
            //                ? AssetTypes.InternationalEquity
            //                : AssetTypes.ManagedInvestments
            //    });
            //}
            equity.EquityTransactions.Add(etransaction);
            account.EquityTransactions.Add(etransaction);
            if (record.FeesRecords != null && record.FeesRecords.Count > 0)
            {
                foreach (var transactionFeeRecord in record.FeesRecords)
                {
                    var expense = new TransactionExpense
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = DateTime.Now,
                        Amount = transactionFeeRecord.Amount,
                        AdviserId = adviser.AdviserId,
                        IncurredOn = record.TransactionDate,
                        TransactionType = TransactionType.EquityTransaction,
                        CorrespondingTransactionId = etransaction.Id,
                        ExpenseType = transactionFeeRecord.TransactionExpenseType
                    };
                    _db.TransactionExpenses.Add(expense);
                }
            }

            AssetTypes type = AssetTypes.AustralianEquity;
            switch (equity.EquityType) {
                case EquityTypes.AustralianEquity:
                    type = AssetTypes.AustralianEquity;
                    break;
                case EquityTypes.InternationalEquity:
                    type = AssetTypes.InternationalEquity;
                    break;
                case EquityTypes.ManagedInvestments:
                    type = AssetTypes.ManagedInvestments;
                    break;
            }

            var accountToMakeTrans = GetClientAccountSync(account.AccountNumber, DateTime.Now);
            accountToMakeTrans.MakeTransactionSync(new MarginLendingTransactionCreation { 
                AssetId = equity.AssetId,
                AssetTypes = type,
                GrantedOn = DateTime.Now,
                IsAcquire = false,
                LoanAmount = record.LoanAmount,
                ExpiryDate = DateTime.Now.AddDays(365),
                InterestRate = 0,
                Ratio = record.NumberOfUnits * record.Price == 0 ? 0 : (record.LoanAmount / (record.NumberOfUnits * record.Price)),
                EquityTransactionId = etransaction.Id
            });


        }

        private async Task RecordCashAccountTransaction(TransactionCreationBase transaction, Edis.Db.Adviser adviser,
            Account account)
        {
            var record = (CashAccountTransactionAccountCreation)transaction;
            var cashAccount =
                await
                    _db.CashAccounts.Where(
                        acc =>
                            acc.AccountName == record.CashAccountName &&
                            acc.AccountNumber == record.CashAccountNumber)
                        .Include(c => c.CashTransactions)
                        .SingleOrDefaultAsync();
            if (cashAccount == null)
            {
                cashAccount = new CashAccount
                {
                    Id = Guid.NewGuid().ToString(),
                    AccountNumber = record.CashAccountNumber,
                    CurrencyType = record.CurrencyType,
                    CashTransactions = new List<CashTransaction>(),
                    Frequency = record.Frequency,
                    CashAccountType = record.CashAccountType,
                    TermsInMonths = record.TermsInMonths,
                    AnnualInterest = record.AnnualInterestSoFar,
                    InterestRate = record.InterestRate,
                    FaceValue = record.Amount,
                    AccountName = record.CashAccountName,
                    MaturityDate = record.MaturityDate,
                    Bsb = record.Bsb,
                    Interests = new List<Interest>()
                };
                _db.CashAccounts.Add(cashAccount);
            }

            if (cashAccount.CashTransactions == null)
            {
                cashAccount.CashTransactions = new List<CashTransaction>();
            }
            var ctransaction = new CashTransaction
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                Amount = record.Amount,
                TransactionDate = record.TransactionDate,
                CashAccount = cashAccount
            };
            cashAccount.CashTransactions.Add(ctransaction);
            account.CashTransactions.Add(ctransaction);
            if (record.TransactionFeeRecords != null && record.TransactionFeeRecords.Count > 0)
            {
                foreach (var transactionFeeRecord in record.TransactionFeeRecords)
                {
                    var expense = new TransactionExpense
                    {
                        Amount = transactionFeeRecord.Amount,
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = DateTime.Now,
                        AdviserId = adviser.AdviserId,
                        IncurredOn = record.TransactionDate,
                        TransactionType = TransactionType.CashTransaction,
                        CorrespondingTransactionId = ctransaction.Id,
                        ExpenseType = transactionFeeRecord.TransactionExpenseType
                    };
                    _db.TransactionExpenses.Add(expense);
                }
            }
        }

        private void RecordCashAccountTransactionSync(TransactionCreationBase transaction, Edis.Db.Adviser adviser,       //added
            Account account)
        {
            var record = (CashAccountTransactionAccountCreation)transaction;
            var cashAccount =
                    _db.CashAccounts.Where(
                        acc =>
                            acc.AccountName == record.CashAccountName &&
                            acc.AccountNumber == record.CashAccountNumber)
                        .Include(c => c.CashTransactions)
                        .SingleOrDefault();
            if (cashAccount == null)
            {
                cashAccount = new CashAccount
                {
                    Id = Guid.NewGuid().ToString(),
                    AccountNumber = record.CashAccountNumber,
                    CurrencyType = record.CurrencyType,
                    CashTransactions = new List<CashTransaction>(),
                    Frequency = record.Frequency,
                    CashAccountType = record.CashAccountType,
                    TermsInMonths = record.TermsInMonths,
                    AnnualInterest = record.AnnualInterestSoFar,
                    InterestRate = record.InterestRate,
                    FaceValue = record.Amount,
                    AccountName = record.CashAccountName,
                    MaturityDate = record.MaturityDate,
                    Bsb = record.Bsb,
                    Interests = new List<Interest>()
                };
                _db.CashAccounts.Add(cashAccount);
            }

            if (cashAccount.CashTransactions == null)
            {
                cashAccount.CashTransactions = new List<CashTransaction>();
            }
            var ctransaction = new CashTransaction
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                Amount = record.Amount,
                TransactionDate = record.TransactionDate,
                CashAccount = cashAccount
            };
            cashAccount.CashTransactions.Add(ctransaction);
            account.CashTransactions.Add(ctransaction);
            if (record.TransactionFeeRecords != null && record.TransactionFeeRecords.Count > 0)
            {
                foreach (var transactionFeeRecord in record.TransactionFeeRecords)
                {
                    var expense = new TransactionExpense
                    {
                        Amount = transactionFeeRecord.Amount,
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = DateTime.Now,
                        AdviserId = adviser.AdviserId,
                        IncurredOn = record.TransactionDate,
                        TransactionType = TransactionType.CashTransaction,
                        CorrespondingTransactionId = ctransaction.Id,
                        ExpenseType = transactionFeeRecord.TransactionExpenseType
                    };
                    _db.TransactionExpenses.Add(expense);
                }
            }

        }


        private async Task RecordBondTransaction(TransactionCreationBase transaction, Account account,
            Edis.Db.Adviser adviser)
        {
            var record = (BondTransactionCreation)transaction;
            var bond = await _db.Bonds.Where(b => b.Ticker == record.Ticker)
                .Include(b => b.Prices)
                .FirstOrDefaultAsync();
            if (account.BondTransactions == null)
            {
                account.BondTransactions = new List<BondTransaction>();
            }
            if (bond == null)
            {
                bond = new Bond
                {
                    Ticker = record.Ticker,
                    BondTransactions = new List<BondTransaction>(),
                    BondId = Guid.NewGuid().ToString(),
                    Frequency = record.Frequency,
                    Issuer = record.Issuer,
                    BondType = record.BondType,
                    Name = record.BondName,
                    CouponPayments = new List<CouponPayment>(),
                    Prices = new List<AssetPrice>()
                };
                _db.Bonds.Add(bond);
            }
            if (bond.BondTransactions == null)
            {
                bond.BondTransactions = new List<BondTransaction>();
            }
            var btransaction = new BondTransaction
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                NumberOfUnits = record.NumberOfUnits,
                Bond = bond,
                TransactionDate = record.TransactionDate,
                UnitPriceAtPurchase = record.UnitPrice
            };
            if (bond.Prices == null)
            {
                bond.Prices = new List<AssetPrice>();
            }
            var price = bond.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault();
            if (price == null || price.Price != record.UnitPrice)
            {
                bond.Prices.Add(new AssetPrice
                {
                    CorrespondingAssetKey = bond.BondId,
                    Id = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.Now,
                    Price = record.UnitPrice,
                    AssetType = AssetTypes.FixedIncomeInvestments
                });
            }
            bond.BondTransactions.Add(btransaction);
            account.BondTransactions.Add(btransaction);
            if (record.TransactionFeeRecords != null && record.TransactionFeeRecords.Count > 0)
            {
                foreach (var transactionFeeRecord in record.TransactionFeeRecords)
                {
                    var expense = new TransactionExpense
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = DateTime.Now,
                        Amount = transactionFeeRecord.Amount,
                        AdviserId = adviser.AdviserId,
                        CorrespondingTransactionId = btransaction.Id,
                        ExpenseType = transactionFeeRecord.TransactionExpenseType,
                        IncurredOn = record.TransactionDate,
                        TransactionType = TransactionType.BondTransaction
                    };
                    _db.TransactionExpenses.Add(expense);
                }
            }
        }



        private void RecordBondTransactionSync(TransactionCreationBase transaction, Account account,      //added
            Edis.Db.Adviser adviser)
        {
            var record = (BondTransactionCreation)transaction;
            var bond = _db.Bonds.Where(b => b.Ticker == record.Ticker)
                //.Include(b => b.Prices)
                .FirstOrDefault();
            if (account.BondTransactions == null)
            {
                account.BondTransactions = new List<BondTransaction>();
            }
            if (bond == null)
            {
                bond = new Bond
                {
                    Ticker = record.Ticker,
                    BondTransactions = new List<BondTransaction>(),
                    BondId = Guid.NewGuid().ToString(),
                    Frequency = record.Frequency,
                    Issuer = record.Issuer,
                    BondType = record.BondType,
                    Name = record.BondName,
                    CouponPayments = new List<CouponPayment>(),
                    Prices = new List<AssetPrice>()
                };
                _db.Bonds.Add(bond);
            }
            if (bond.BondTransactions == null)
            {
                bond.BondTransactions = new List<BondTransaction>();
            }
            var btransaction = new BondTransaction
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                NumberOfUnits = record.NumberOfUnits,
                Bond = bond,
                TransactionDate = record.TransactionDate,
                UnitPriceAtPurchase = record.UnitPrice
            };
            if (bond.Prices == null)
            {
                bond.Prices = new List<AssetPrice>();
            }
            var price = bond.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault();
            if (price == null || price.Price != record.UnitPrice)
            {
                bond.Prices.Add(new AssetPrice
                {
                    CorrespondingAssetKey = bond.BondId,
                    Id = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.Now,
                    Price = record.UnitPrice,
                    AssetType = AssetTypes.FixedIncomeInvestments
                });
            }
            bond.BondTransactions.Add(btransaction);
            account.BondTransactions.Add(btransaction);
            if (record.TransactionFeeRecords != null && record.TransactionFeeRecords.Count > 0)
            {
                foreach (var transactionFeeRecord in record.TransactionFeeRecords)
                {
                    var expense = new TransactionExpense
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = DateTime.Now,
                        Amount = transactionFeeRecord.Amount,
                        AdviserId = adviser.AdviserId,
                        CorrespondingTransactionId = btransaction.Id,
                        ExpenseType = transactionFeeRecord.TransactionExpenseType,
                        IncurredOn = record.TransactionDate,
                        TransactionType = TransactionType.BondTransaction
                    };
                    _db.TransactionExpenses.Add(expense);
                }
            }
        }

        #endregion
        #region account construction helpers
        private async Task<ClientAccount> GenerateClientAccount(string accountId, DateTime? todate)
        {
            var beforeDate = todate ?? DateTime.Now;
            var dbAccount =
                _db.Accounts.Local.SingleOrDefault(
                    a => a.AccountId == accountId && a.CreatedOn.HasValue && a.CreatedOn.Value <= beforeDate) ??
                await
                    _db.Accounts.Where(
                        a => a.AccountId == accountId && a.CreatedOn.HasValue && a.CreatedOn.Value <= beforeDate)
                        .Include(a => a.BondTransactions.Select(b => b.Bond))
                        .Include(a => a.CashTransactions.Select(c => c.CashAccount))
                        .Include(a => a.EquityTransactions.Select(e => e.Equity))
                        .Include(a => a.PropertyTransactions.Select(p => p.PropertyAddress))
                        .FirstOrDefaultAsync();

            //todo this part of population needs to be revised with collections to be lazy loaded instead
            var clientAccount = new ClientAccount(this)
            {
                Id = accountId,
                AccountNumber = dbAccount.AccountNumber,
                ConsultancyActivities = new List<ConsultancyActivity>()
            };

            return clientAccount;
        }

        private ClientAccount GenerateClientAccountSync(string accountId, DateTime? todate)             //added
        {
            var beforeDate = todate ?? DateTime.Now;
            var dbAccount =
                _db.Accounts.Local.SingleOrDefault(
                    a => a.AccountId == accountId && a.CreatedOn.HasValue && a.CreatedOn.Value <= beforeDate) ??
                    _db.Accounts.Where(
                        a => a.AccountId == accountId && a.CreatedOn.HasValue && a.CreatedOn.Value <= beforeDate)
                        .Include(a => a.BondTransactions.Select(b => b.Bond))
                        .Include(a => a.CashTransactions.Select(c => c.CashAccount))
                        .Include(a => a.EquityTransactions.Select(e => e.Equity))
                        .Include(a => a.PropertyTransactions.Select(p => p.PropertyAddress))
                        .FirstOrDefault();

            //todo this part of population needs to be revised with collections to be lazy loaded instead
            var clientAccount = new ClientAccount(this)
            {
                Id = accountId,
                AccountNumber = dbAccount.AccountNumber,
                ConsultancyActivities = new List<ConsultancyActivity>(),
                AccountNameOrInfo = dbAccount.AccountInfo
            };

            return clientAccount;
        }

        private async Task<GroupAccount> GenerateClientGroupAccount(string accountId)
        {
            var dbAccount = await _db.Accounts.Where(a => a.AccountId == accountId)
                .FirstOrDefaultAsync();

            //todo this part of generation needs to be revised
            var groupAccount = new GroupAccount(this)
            {
                Id = accountId,
                AccountNumber = dbAccount.AccountNumber,
                ConsultancyActivities = new List<ConsultancyActivity>()
            };
            return groupAccount;
        }

        private GroupAccount GenerateClientGroupAccountSync(string accountId)       //added
        {
            var dbAccount = _db.Accounts.Where(a => a.AccountId == accountId)
                .FirstOrDefault();

            //todo this part of generation needs to be revised
            var groupAccount = new GroupAccount(this)
            {
                Id = accountId,
                AccountNumber = dbAccount.AccountNumber,
                ConsultancyActivities = new List<ConsultancyActivity>(),
                AccountNameOrInfo = dbAccount.AccountInfo
            };
            return groupAccount;
        }

        private async Task<List<DirectProperty>> GenerateDirectPropertyForAccount(string accountId, DateTime todate,
            Account dbAccount)
        {
            var result = new List<DirectProperty>();
            var clientGroup = await _db.ClientGroups
                .FirstOrDefaultAsync(g => g.GroupAccounts.Any(a => a.AccountId == accountId)
                                          || _db.Clients.Any(c => c.ClientGroupId == g.ClientGroupId
                                                                 && c.Accounts.Any(ca => ca.AccountId == accountId)));
            if (clientGroup != null && _db.Clients.Any(c => c.ClientGroupId == clientGroup.ClientGroupId))
            {
                var propertyGroups =
                    dbAccount.PropertyTransactions.Where(p => p.TransactionDate <= todate)
                        .GroupBy(p => p.PropertyAddress.PropertyId);
                foreach (var propertyGroup in propertyGroups)
                {
                    var transaction = propertyGroup.FirstOrDefault();
                    if (transaction != null)
                    {
                        var property = transaction.PropertyAddress;
                        var clientAverageAge =
                            _db.Clients.Where(c => c.ClientGroupId == clientGroup.ClientGroupId)
                                .ToList()
                                .Sum(c => c.Dob == null || c.Dob.Value == null ? 0 : (todate - c.Dob.Value).Days) / 365 /
                            _db.Clients.Count(c => c.ClientGroupId == clientGroup.ClientGroupId);

                        var yearsToRetirement = RetirementAge - clientAverageAge;

                        var directProperty = new DirectProperty(this)
                        {
                            YearsToRetirement = yearsToRetirement,
                            AbilityToPayAboveCurrentInterestRate =
                                GetAbilityToPayAboveCurrentInterestRateForAccount(accountId, property.PropertyId),
                            PropertyLeverage = GetPropertyLeverageForAccount(accountId, property.PropertyId),
                            TotalNumberOfUnits =
                                propertyGroup.Count(t => t.IsBuy.HasValue && t.IsBuy.Value) -
                                propertyGroup.Count(t => t.IsBuy.HasValue && !t.IsBuy.Value),
                            ClientAverageAge = clientAverageAge,
                            PlaceId = property.GooglePlaceId,
                            Id = property.PropertyId,
                            LatestPrice =
                                property.Prices.Any()
                                    ? property.Prices.OrderByDescending(p => p.CreatedOn)
                                        .FirstOrDefault()
                                        .Price.GetValueOrDefault()
                                    : 0,
                            ClientAccountId = accountId,
                            State = property.State,
                            PropertyType = property.PropertyType,
                            City = property.City,
                            Country = property.Country,
                            FullAddress = property.FullAddress,
                            Latitude = property.Latitude,
                            Longitude = property.Longitude,
                            Postcode = property.Postcode,
                            StreetAddress = property.StreetAddress
                        };
                        result.Add(directProperty);
                    }
                    else
                    {
                        throw new Exception("Transaction for property " + propertyGroup.Key + " cannot be found");
                    }
                }
                return result;
            }
            throw new Exception("Cannot find client group for account " + accountId);
        }

        private List<DirectProperty> GenerateDirectPropertyForAccountSync(string accountId, DateTime todate,                //added
            Account dbAccount)
        {
            var result = new List<DirectProperty>();
            var clientGroup = _db.ClientGroups
                .FirstOrDefault(g => g.GroupAccounts.Any(a => a.AccountId == accountId)
                                          || _db.Clients.Any(c => c.ClientGroupId == g.ClientGroupId
                                                                 && c.Accounts.Any(ca => ca.AccountId == accountId)));
            if (clientGroup != null && _db.Clients.Any(c => c.ClientGroupId == clientGroup.ClientGroupId))
            {
                var propertyGroups =
                    dbAccount.PropertyTransactions.Where(p => p.TransactionDate <= todate)
                        .GroupBy(p => p.PropertyAddress.PropertyId);
                foreach (var propertyGroup in propertyGroups)
                {
                    var transaction = propertyGroup.FirstOrDefault();
                    if (transaction != null)
                    {
                        var property = transaction.PropertyAddress;

                        var clientAverageAge =
                            _db.Clients.Where(c => c.ClientGroupId == clientGroup.ClientGroupId)
                                .ToList()
                                .Sum(c => (c.Dob == null || c.Dob.Value == null ? 0 : (todate - c.Dob.Value).Days)) / 365 /
                            _db.Clients.Count(c => c.ClientGroupId == clientGroup.ClientGroupId);

                        var yearsToRetirement = RetirementAge - clientAverageAge;

                        var directProperty = new DirectProperty(this)
                        {
                            YearsToRetirement = yearsToRetirement,
                            AbilityToPayAboveCurrentInterestRate =
                                GetAbilityToPayAboveCurrentInterestRateForAccount(accountId, property.PropertyId),
                            PropertyLeverage = GetPropertyLeverageForAccount(accountId, property.PropertyId),
                            TotalNumberOfUnits =
                                propertyGroup.Count(t => t.IsBuy.HasValue && t.IsBuy.Value) -
                                propertyGroup.Count(t => t.IsBuy.HasValue && !t.IsBuy.Value),
                            ClientAverageAge = clientAverageAge,
                            PlaceId = property.GooglePlaceId,
                            Id = property.PropertyId,
                            LatestPrice =
                                property.Prices.Any()
                                    ? property.Prices.OrderByDescending(p => p.CreatedOn)
                                        .FirstOrDefault()
                                        .Price.GetValueOrDefault()
                                    : 0,
                            ClientAccountId = accountId,
                            State = property.State,
                            PropertyType = property.PropertyType,
                            City = property.City,
                            Country = property.Country,
                            FullAddress = property.FullAddress,
                            Latitude = property.Latitude,
                            Longitude = property.Longitude,
                            Postcode = property.Postcode,
                            StreetAddress = property.StreetAddress
                        };
                        result.Add(directProperty);
                    }
                    else
                    {
                        throw new Exception("Transaction for property " + propertyGroup.Key + " cannot be found");
                    }
                }
                return result;
            }
            throw new Exception("Cannot find client group for account " + accountId);
        }



        private double GetPropertyLeverageForAccount(string accountId, string propertyId)
        {
            //TODO: need actual implementation
            return 0;
        }
        private double GetAbilityToPayAboveCurrentInterestRateForAccount(string accountId, string propertyId)
        {
            //TODO: need actual implementation
            return 0;
        }
        private async Task<List<FixedIncome>> GenerateFixedIncomeForAccount(string accountId, DateTime todate, Account dbAccount)
        {
            var bondGroups =
                dbAccount.BondTransactions.Where(t => t.TransactionDate <= todate).GroupBy(b => b.Bond.BondId);
            var oneYearAgo = DateTime.Now.AddYears(-1);
            return (from bondGroup in bondGroups
                    let transaction = bondGroup.FirstOrDefault()
                    where transaction != null
                    let bond = transaction.Bond
                    select new FixedIncome(this)
                    {
                        TotalNumberOfUnits = bondGroup.Sum(b => b.NumberOfUnits).GetValueOrDefault(),
                        Ticker = bond.Ticker,
                        Id = bond.BondId,
                        CouponFrequency = bond.Frequency,
                        LatestPrice =
                            bond.Prices.Any()
                                ? bond.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault()
                                : 0,
                        ClientAccountId = accountId,
                        BondType = bond.BondType,
                        //todo possible performance hazard 
                        BoundDetails = GetBondDetails(bond.Ticker).Result,
                        CouponRate = bond.CouponPayments.Where(c => c.PaymentOn <= DateTime.Now && c.PaymentOn <= oneYearAgo).Sum(p => p.Amount) / bondGroup.Sum(b => b.NumberOfUnits) / bond.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price,
                        FixedIncomeName = bond.Name,
                        Issuer = bond.Issuer
                    }).ToList();
        }

        private List<FixedIncome> GenerateFixedIncomeForAccountSync(string accountId, DateTime todate, Account dbAccount)               //added
        {
            var bondGroups =
                dbAccount.BondTransactions.Where(t => t.TransactionDate <= todate).GroupBy(b => b.Bond.BondId);
            var oneYearAgo = DateTime.Now.AddYears(-1);
            return (from bondGroup in bondGroups
                    let transaction = bondGroup.FirstOrDefault()
                    where transaction != null
                    let bond = transaction.Bond
                    select new FixedIncome(this)
                    {
                        TotalNumberOfUnits = bondGroup.Sum(b => b.NumberOfUnits).GetValueOrDefault(),
                        Ticker = bond.Ticker,
                        Id = bond.BondId,
                        CouponFrequency = bond.Frequency,
                        LatestPrice =
                            bond.Prices.Any()
                                ? bond.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault()
                                : 0,
                        ClientAccountId = accountId,
                        BondType = bond.BondType,
                        //todo possible performance hazard 
                        BoundDetails = GetBondDetails(bond.Ticker).Result,
                        CouponRate = bond.CouponPayments.Where(c => c.PaymentOn <= DateTime.Now && c.PaymentOn <= oneYearAgo).Sum(p => p.Amount) / bondGroup.Sum(b => b.NumberOfUnits) / bond.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price,
                        FixedIncomeName = bond.Name,
                        Issuer = bond.Issuer
                    }).ToList();
        }




        private List<Cash> GenerateCashAssetForAccount(string accountId, DateTime todate, Account dbAccount)
        {
            var result = new List<Cash>();
            var testCashAccounts = dbAccount.CashTransactions;
            var cashAccounts =
                dbAccount.CashTransactions.Where(t => t.TransactionDate <= todate).GroupBy(c => c.CashAccount.Id);

            List<CashTransaction> cashTransactions = cashAccounts.SelectMany(group => group).ToList();

            foreach (var transaction in cashTransactions)
            {
                if (transaction != null)
                {
                    var account = transaction.CashAccount;

                    Debug.Assert(account.FaceValue != null, "account.FaceValue != null");

                    var cash = new Cash(this)
                    {
                        AnnualInterest = account.AnnualInterest,
                        Bsb = account.Bsb,
                        CashAccountName = account.AccountName,
                        CashAccountNumber = account.AccountNumber,
                        CashAccountType = account.CashAccountType,
                        CurrencyType = account.CurrencyType,
                        FaceValue = account.FaceValue.Value,
                        InterestFrequency = account.Frequency,
                        InterestRate = account.InterestRate,
                        MaturityDate = account.MaturityDate,
                        TotalNumberOfUnits = 1,
                        Id = account.Id,
                        LatestPrice = transaction.Amount ?? 0,
                        TermOfRatesMonth = account.TermsInMonths,
                        ClientAccountId = accountId
                    };

                    result.Add(cash);
                }
                else
                {
                    throw new Exception("Cash account " + transaction.Id + " cannot be found");
                }
            }
            return result;
        }
        private async Task<List<ManagedInvestment>> GenerateManagedFundForAccount(string accountId, DateTime todate,
            Account dbAccount)
        {
            var result = new List<ManagedInvestment>();
            var assetGroups = dbAccount.EquityTransactions.Where(t => t.TransactionDate <= todate
                                                                      &&
                                                                      t.Equity.EquityType ==
                                                                      EquityTypes.ManagedInvestments)
                .GroupBy(e => e.Equity.AssetId);
            foreach (var assetGroup in assetGroups)
            {
                var transaction = assetGroup.FirstOrDefault();
                if (transaction != null)
                {
                    var equity = transaction.Equity;
                    var managedInvestment = new ManagedInvestment(this);
                    managedInvestment.FundAllocation = GetFundAllocationForManagedFund(equity.AssetId);
                    managedInvestment.F0Ratios = await GetF0RatiosForEquity(equity.Ticker);
                    managedInvestment.ClientAccountId = accountId;
                    managedInvestment.F1Recommendation = await GetF1RatiosForEquity(equity.Ticker);
                    managedInvestment.Id = equity.AssetId;
                    managedInvestment.Name = equity.Name;
                    managedInvestment.LatestPrice =
                        transaction.Equity.Prices.OrderByDescending(p => p.CreatedOn)
                            .FirstOrDefault()
                            .Price.GetValueOrDefault();
                    managedInvestment.Sector = equity.Sector;
                    managedInvestment.Ticker = equity.Ticker;
                    managedInvestment.TotalNumberOfUnits = assetGroup.Sum(a => a.NumberOfUnits).GetValueOrDefault();
                    result.Add(managedInvestment);
                }
                else
                {
                    throw new Exception("Equity " + assetGroup.Key + " cannot be found");
                }
            }
            return result;
        }

        private List<ManagedInvestment> GenerateManagedFundForAccountSync(string accountId, DateTime todate,            //added
            Account dbAccount)
        {
            var result = new List<ManagedInvestment>();
            var assetGroups = dbAccount.EquityTransactions.Where(t => t.TransactionDate <= todate
                                                                      &&
                                                                      t.Equity.EquityType ==
                                                                      EquityTypes.ManagedInvestments)
                .GroupBy(e => e.Equity.AssetId);
            foreach (var assetGroup in assetGroups)
            {
                var transaction = assetGroup.FirstOrDefault();
                if (transaction != null)
                {
                    var equity = transaction.Equity;
                    var managedInvestment = new ManagedInvestment(this);
                    managedInvestment.FundAllocation = GetFundAllocationForManagedFund(equity.AssetId);
                    managedInvestment.F0Ratios = GetF0RatiosForEquitySync(equity.Ticker);
                    managedInvestment.ClientAccountId = accountId;
                    managedInvestment.F1Recommendation = GetF1RatiosForEquitySync(equity.Ticker);
                    managedInvestment.Id = equity.AssetId;
                    managedInvestment.Name = equity.Name;
                    managedInvestment.LatestPrice =
                        transaction.Equity.Prices.OrderByDescending(p => p.CreatedOn)
                            .FirstOrDefault()
                            .Price.GetValueOrDefault();
                    managedInvestment.Sector = equity.Sector;
                    managedInvestment.Ticker = equity.Ticker;
                    managedInvestment.TotalNumberOfUnits = assetGroup.Sum(a => a.NumberOfUnits).GetValueOrDefault();
                    result.Add(managedInvestment);
                }
                else
                {
                    throw new Exception("Equity " + assetGroup.Key + " cannot be found");
                }
            }
            return result;
        }

        private async Task<List<InternationalEquity>> GenerateInternationalEquityForAccount(string accountId, DateTime todate,
            Account dbAccount)
        {
            var result = new List<InternationalEquity>();
            var assetGroups = dbAccount.EquityTransactions.Where(t => t.TransactionDate <= todate
                                                                      &&
                                                                      t.Equity.EquityType ==
                                                                      EquityTypes.InternationalEquity)
                .GroupBy(e => e.Equity.AssetId);
            foreach (var assetGroup in assetGroups)
            {
                var transaction = assetGroup.FirstOrDefault();
                if (transaction != null)
                {
                    var equity = transaction.Equity;
                    var internationalEquity = new InternationalEquity(this);
                    internationalEquity.F0Ratios = await GetF0RatiosForEquity(equity.Ticker);
                    internationalEquity.ClientAccountId = accountId;
                    internationalEquity.F1Recommendation = await GetF1RatiosForEquity(equity.Ticker);
                    internationalEquity.Id = equity.AssetId;
                    internationalEquity.Name = equity.Name;
                    internationalEquity.LatestPrice =
                        transaction.Equity.Prices.OrderByDescending(p => p.CreatedOn)
                            .FirstOrDefault()
                            .Price.GetValueOrDefault();
                    internationalEquity.Sector = equity.Sector;
                    internationalEquity.Ticker = equity.Ticker;
                    internationalEquity.TotalNumberOfUnits = assetGroup.Sum(a => a.NumberOfUnits).GetValueOrDefault();
                    result.Add(internationalEquity);
                }
                else
                {
                    throw new Exception("Equity " + assetGroup.Key + " cannot be found");
                }
            }
            return result;
        }

        private List<InternationalEquity> GenerateInternationalEquityForAccountSync(string accountId, DateTime todate,          //added
            Account dbAccount)
        {
            var result = new List<InternationalEquity>();
            var assetGroups = dbAccount.EquityTransactions.Where(t => t.TransactionDate <= todate
                                                                      &&
                                                                      t.Equity.EquityType ==
                                                                      EquityTypes.InternationalEquity)
                .GroupBy(e => e.Equity.AssetId);
            foreach (var assetGroup in assetGroups)
            {
                var transaction = assetGroup.FirstOrDefault();
                if (transaction != null)
                {
                    var equity = transaction.Equity;
                    var internationalEquity = new InternationalEquity(this);
                    internationalEquity.F0Ratios = GetF0RatiosForEquitySync(equity.Ticker);
                    internationalEquity.ClientAccountId = accountId;
                    internationalEquity.F1Recommendation = GetF1RatiosForEquitySync(equity.Ticker);
                    internationalEquity.Id = equity.AssetId;
                    internationalEquity.Name = equity.Name;
                    internationalEquity.LatestPrice = transaction.Equity.Prices.Count == 0 ? 0 :
                        transaction.Equity.Prices.OrderByDescending(p => p.CreatedOn)
                            .FirstOrDefault()
                            .Price.GetValueOrDefault();
                    internationalEquity.Sector = equity.Sector;
                    internationalEquity.Ticker = equity.Ticker;
                    internationalEquity.TotalNumberOfUnits = assetGroup.Sum(a => a.NumberOfUnits).GetValueOrDefault();
                    internationalEquity.EquityType = EquityTypes.InternationalEquity;
                    result.Add(internationalEquity);
                }
                else
                {
                    throw new Exception("Equity " + assetGroup.Key + " cannot be found");
                }
            }
            return result;
        }


        private async Task<List<AustralianEquity>> GenerateAustralianEquityForAccount(string accountId, DateTime todate,
            Account dbAccount)
        {
            var result = new List<AustralianEquity>();
            var assetGroups = dbAccount.EquityTransactions.Where(t => t.TransactionDate <= todate
                                                                      &&
                                                                      t.Equity.EquityType ==
                                                                      EquityTypes.AustralianEquity)
                .GroupBy(e => e.Equity.AssetId);
            foreach (var assetGroup in assetGroups)
            {
                var transaction = assetGroup.FirstOrDefault();
                if (transaction != null)
                {
                    var equity = transaction.Equity;
                    var australianEquity = new AustralianEquity(this);
                    australianEquity.F0Ratios = await GetF0RatiosForEquity(equity.Ticker);
                    australianEquity.ClientAccountId = accountId;
                    australianEquity.F1Recommendation = await GetF1RatiosForEquity(equity.Ticker);
                    australianEquity.Id = equity.AssetId;
                    australianEquity.Name = equity.Name;
                    australianEquity.LatestPrice =
                        transaction.Equity.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault()
                            .Price.GetValueOrDefault();
                    australianEquity.Sector = equity.Sector;
                    australianEquity.Ticker = equity.Ticker;
                    australianEquity.TotalNumberOfUnits = assetGroup.Sum(a => a.NumberOfUnits).GetValueOrDefault();
                    result.Add(australianEquity);
                }
                else
                {
                    throw new Exception("Equity " + assetGroup.Key + " cannot be found");
                }
            }
            return result;
        }

        private List<AustralianEquity> GenerateAustralianEquityForAccountSync(string accountId, DateTime todate,            //added
            Account dbAccount)
        {
            var result = new List<AustralianEquity>();
            var assetGroups = dbAccount.EquityTransactions.Where(t => t.TransactionDate <= todate
                                                                      &&
                                                                      t.Equity.EquityType ==
                                                                      EquityTypes.AustralianEquity)
                .GroupBy(e => e.Equity.AssetId);
            foreach (var assetGroup in assetGroups)
            {
                var transaction = assetGroup.FirstOrDefault();
                if (transaction != null)
                {
                    var equity = transaction.Equity;
                    var australianEquity = new AustralianEquity(this);
                    australianEquity.F0Ratios = GetF0RatiosForEquitySync(equity.Ticker);
                    australianEquity.ClientAccountId = accountId;
                    australianEquity.F1Recommendation = GetF1RatiosForEquitySync(equity.Ticker);
                    australianEquity.Id = equity.AssetId;
                    australianEquity.Name = equity.Name;
                    australianEquity.LatestPrice =
                        transaction.Equity.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault()
                            .Price.GetValueOrDefault();
                    australianEquity.Sector = equity.Sector;
                    australianEquity.Ticker = equity.Ticker;
                    australianEquity.TotalNumberOfUnits = assetGroup.Sum(a => a.NumberOfUnits).GetValueOrDefault();
                    australianEquity.EquityType = EquityTypes.AustralianEquity;
                    result.Add(australianEquity);
                }
                else
                {
                    throw new Exception("Equity " + assetGroup.Key + " cannot be found");
                }
            }
            return result;
        }

        private FundAllocation GetFundAllocationForManagedFund(string assetId)
        {
            //TODO: need actual allocation retrieval
            return new FundAllocation();
        }
        private async Task<BondDetails> GetBondDetails(string ticker)
        {
            var bondDetails = new BondDetails()
            {
                BondRating = (CreditRating)(int)(await GetResearchValueForBond(Nameof<BondDetails>.Property(b => b.BondRating), ticker)).GetValueOrDefault(),
                Priority = (await GetResearchValueForBond(Nameof<BondDetails>.Property(b => b.Priority), ticker)).GetValueOrDefault(),
                RatingAgency = await GetLatestIssuerForBondResearchValue(Nameof<BondDetails>.Property(b => b.RatingAgency), ticker),
                RedemptionFeatures = (await GetResearchValueForBond(Nameof<BondDetails>.Property(b => b.RedemptionFeatures), ticker)).GetValueOrDefault()
            };
            return bondDetails;
        }

        private BondDetails GetBondDetailsSync(string ticker)
        {
            var bondDetails = new BondDetails()
            {
                BondRating = (CreditRating)(int)(GetResearchValueForBondSync(Nameof<BondDetails>.Property(b => b.BondRating), ticker)).GetValueOrDefault(),
                Priority = (GetResearchValueForBondSync(Nameof<BondDetails>.Property(b => b.Priority), ticker)).GetValueOrDefault(),
                RatingAgency = GetLatestIssuerForBondResearchValueSync(Nameof<BondDetails>.Property(b => b.RatingAgency), ticker),
                RedemptionFeatures = (GetResearchValueForBondSync(Nameof<BondDetails>.Property(b => b.RedemptionFeatures), ticker)).GetValueOrDefault()
            };
            return bondDetails;
        }

        private async Task<Recommendation> GetF1RatiosForEquity(string ticker)
        {

            var recommendation = new Recommendation()
            {
                MorningstarRecommendation = (MorningStarRecommendation)(int)(await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.MorningstarRecommendation), ticker)).GetValueOrDefault(),
                MorningStarAnalyst = (MorningStarAnalyst)(int)(await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.MorningStarAnalyst), ticker)).GetValueOrDefault(),
                EpsGrowth = (await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.EpsGrowth), ticker)).GetValueOrDefault(),
                FiveYearTotalReturn = await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.FiveYearTotalReturn), ticker),
                DividendYield = (await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.DividendYield), ticker)).GetValueOrDefault(),
                DebtEquityRatio = (await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.DebtEquityRatio), ticker)).GetValueOrDefault(),
                CreditRating = (CreditRating)(int)(await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.CreditRating), ticker)).GetValueOrDefault(),
                OneYearBeta = await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearBeta), ticker),
                YearsSinceInception = await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.YearsSinceInception), ticker),
                OneYearInformationRatio = (await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearInformationRatio), ticker)).GetValueOrDefault(),
                OneYearSharpRatio = await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearSharpRatio), ticker),
                PerformanceFee = await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.PerformanceFee), ticker),
                MaxManagementExpenseRatio = await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.MaxManagementExpenseRatio), ticker),
                OneYearTrackingError = await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearTrackingError), ticker),
                OneYearAlpha = await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearAlpha), ticker),
                FairValueVariation = await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.FairValueVariation), ticker),
                PriceEarningRatio = (await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.PriceEarningRatio), ticker)).GetValueOrDefault(),
                ReturnOnAsset = (await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.ReturnOnAsset), ticker)).GetValueOrDefault(),
                ReturnOnEquity = (await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.ReturnOnEquity), ticker)).GetValueOrDefault(),
                Frank = (await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.Frank), ticker)).GetValueOrDefault(),
                InterestCover = (await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.InterestCover), ticker)).GetValueOrDefault(),
                OneYearReturn = await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearReturn), ticker),
                IntrinsicValue = (await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.IntrinsicValue), ticker)).GetValueOrDefault(),
                FinancialLeverage = await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.FinancialLeverage), ticker),
                OneYearRevenueGrowth = await GetResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearRevenueGrowth), ticker)
            };
            return recommendation;
        }

        private Recommendation GetF1RatiosForEquitySync(string ticker)              //added
        {

            var recommendation = new Recommendation()
            {
                MorningstarRecommendation = (MorningStarRecommendation)(int)(GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.MorningstarRecommendation), ticker)).GetValueOrDefault(),
                MorningStarAnalyst = (MorningStarAnalyst)(int)(GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.MorningStarAnalyst), ticker)).GetValueOrDefault(),
                EpsGrowth = (GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.EpsGrowth), ticker)).GetValueOrDefault(),
                FiveYearTotalReturn = GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.FiveYearTotalReturn), ticker),
                DividendYield = (GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.DividendYield), ticker)).GetValueOrDefault(),
                DebtEquityRatio = (GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.DebtEquityRatio), ticker)).GetValueOrDefault(),
                CreditRating = (CreditRating)(int)(GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.CreditRating), ticker)).GetValueOrDefault(),
                OneYearBeta = GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.OneYearBeta), ticker),
                YearsSinceInception = GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.YearsSinceInception), ticker),
                OneYearInformationRatio = (GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.OneYearInformationRatio), ticker)).GetValueOrDefault(),
                OneYearSharpRatio = GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.OneYearSharpRatio), ticker),
                PerformanceFee = GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.PerformanceFee), ticker),
                MaxManagementExpenseRatio = GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.MaxManagementExpenseRatio), ticker),
                OneYearTrackingError = GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.OneYearTrackingError), ticker),
                OneYearAlpha = GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.OneYearAlpha), ticker),
                FairValueVariation = GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.FairValueVariation), ticker),
                PriceEarningRatio = (GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.PriceEarningRatio), ticker)).GetValueOrDefault(),
                ReturnOnAsset = (GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.ReturnOnAsset), ticker)).GetValueOrDefault(),
                ReturnOnEquity = (GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.ReturnOnEquity), ticker)).GetValueOrDefault(),
                Frank = (GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.Frank), ticker)).GetValueOrDefault(),
                InterestCover = (GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.InterestCover), ticker)).GetValueOrDefault(),
                OneYearReturn = GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.OneYearReturn), ticker),
                IntrinsicValue = (GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.IntrinsicValue), ticker)).GetValueOrDefault(),
                FinancialLeverage = GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.FinancialLeverage), ticker),
                OneYearRevenueGrowth = GetResearchValueForEquitySync(Nameof<Recommendation>.Property(r => r.OneYearRevenueGrowth), ticker)
            };
            return recommendation;
        }





        /// <summary>
        /// Get F0 ratios for equity, will return 0 for properties that do not have any research value associated.
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        private async Task<Ratios> GetF0RatiosForEquity(string ticker)
        {

            var ratio = new Ratios()
            {
                FiveYearAlphaRatio = await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearAlphaRatio), ticker),
                EpsGrowth = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.EpsGrowth), ticker)).GetValueOrDefault(),
                DividendYield = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.DividendYield), ticker)).GetValueOrDefault(),
                BetaFiveYears = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.BetaFiveYears), ticker)).GetValueOrDefault(),
                DebtEquityRatio = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.DebtEquityRatio), ticker)).GetValueOrDefault(),
                CurrentRatio = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.CurrentRatio), ticker)).GetValueOrDefault(),
                Capitalisation = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.Capitalisation), ticker)).GetValueOrDefault(),
                FiveYearTrackingErrorRatio = await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearTrackingErrorRatio), ticker),
                FiveYearStandardDeviation = await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearStandardDeviation), ticker),
                QuickRatio = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.QuickRatio), ticker)).GetValueOrDefault(),
                GlobalCategory = await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.GlobalCategory), ticker),
                FiveYearSkewnessRatio = await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearSkewnessRatio), ticker),
                FundSize = await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FundSize), ticker),
                FiveYearSharpRatio = await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearSharpRatio), ticker),
                ReturnOnEquity = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.ReturnOnEquity), ticker)).GetValueOrDefault(),
                ReturnOnAsset = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.ReturnOnAsset), ticker)).GetValueOrDefault(),
                PriceEarningRatio = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.PriceEarningRatio), ticker)).GetValueOrDefault(),
                Frank = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.Frank), ticker)).GetValueOrDefault(),
                InterestCover = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.InterestCover), ticker)).GetValueOrDefault(),
                FiveYearReturn = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearReturn), ticker)).GetValueOrDefault(),
                OneYearReturn = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.OneYearReturn), ticker)).GetValueOrDefault(),
                Beta = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.Beta), ticker)).GetValueOrDefault(),
                FiveYearInformation = await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearInformation), ticker),
                EarningsStability = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.EarningsStability), ticker)).GetValueOrDefault(),
                PayoutRatio = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.PayoutRatio), ticker)).GetValueOrDefault(),
                ThreeYearReturn = (await GetResearchValueForEquity(Nameof<Ratios>.Property(r => r.ThreeYearReturn), ticker)).GetValueOrDefault()
            };



            return ratio;
        }

        private Ratios GetF0RatiosForEquitySync(string ticker)                  //added
        {

            var ratio = new Ratios()
            {
                FiveYearAlphaRatio = GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearAlphaRatio), ticker),
                EpsGrowth = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.EpsGrowth), ticker)).GetValueOrDefault(),
                DividendYield = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.DividendYield), ticker)).GetValueOrDefault(),
                BetaFiveYears = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.BetaFiveYears), ticker)).GetValueOrDefault(),
                DebtEquityRatio = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.DebtEquityRatio), ticker)).GetValueOrDefault(),
                CurrentRatio = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.CurrentRatio), ticker)).GetValueOrDefault(),
                Capitalisation = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.Capitalisation), ticker)).GetValueOrDefault(),
                FiveYearTrackingErrorRatio = GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearTrackingErrorRatio), ticker),
                FiveYearStandardDeviation = GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearStandardDeviation), ticker),
                QuickRatio = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.QuickRatio), ticker)).GetValueOrDefault(),
                GlobalCategory = GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.GlobalCategory), ticker),
                FiveYearSkewnessRatio = GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearSkewnessRatio), ticker),
                FundSize = GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FundSize), ticker),
                FiveYearSharpRatio = GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearSharpRatio), ticker),
                ReturnOnEquity = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.ReturnOnEquity), ticker)).GetValueOrDefault(),
                ReturnOnAsset = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.ReturnOnAsset), ticker)).GetValueOrDefault(),
                PriceEarningRatio = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.PriceEarningRatio), ticker)).GetValueOrDefault(),
                Frank = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.Frank), ticker)).GetValueOrDefault(),
                InterestCover = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.InterestCover), ticker)).GetValueOrDefault(),
                FiveYearReturn = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearReturn), ticker)).GetValueOrDefault(),
                OneYearReturn = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.OneYearReturn), ticker)).GetValueOrDefault(),
                Beta = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.Beta), ticker)).GetValueOrDefault(),
                FiveYearInformation = GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.FiveYearInformation), ticker),
                EarningsStability = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.EarningsStability), ticker)).GetValueOrDefault(),
                PayoutRatio = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.PayoutRatio), ticker)).GetValueOrDefault(),
                ThreeYearReturn = (GetResearchValueForEquitySync(Nameof<Ratios>.Property(r => r.ThreeYearReturn), ticker)).GetValueOrDefault()
            };
            return ratio;
        }

        #endregion

        public void CreateNewMessageSync(Message message, int senderRole)
        {
            var resource = _db.ResourcesReferences.SingleOrDefault(r => r.TokenValue == message.resourceToken);
            Edis.Db.Notes note = new Edis.Db.Notes
            {
                AccountId = message.accountId,
                AdviserId = message.adviserNumber,
                AssetTypeId = message.assetTypeId.ToString(),
                Product = message.productTypeId.ToString(),
                AssetClass = ((AssetTypes)message.assetTypeId).ToString(),
                ProductClass = ((ProductTypes)message.productTypeId).ToString(),
                Body = message.body,
                ClientId = message.clientId,
                DateCompleted = message.dateCompleted,
                DateCreated = DateTime.Now,
                TimeSpend = Convert.ToSingle(message.timespent),
                Reminder = message.reminder,
                Status = message.status,
                NoteSerial = message.noteSerial,
                DateDue = message.dateDue,
                DateModified = DateTime.Now,
                FollowupActions = message.followupActions,
                FollowupDate = message.followupDate,
                IsAccepted = message.isAccepted,
                ReminderDate = message.reminderDate,
                IsDeclined = message.isDeclined,
                NoteType = message.noteTypeId,
                NoteId = Guid.NewGuid().ToString(),
                Subject = message.subject,
                SenderRole = senderRole,
                Client = _db.Clients.SingleOrDefault(c => c.ClientNumber == message.clientId)
            };


            _db.Notes.Add(note);

            if (!string.IsNullOrEmpty(resource.ResourceUrl))
            {
                _db.Attachments.Add(new Edis.Db.Attachments
                {
                    AttachmentId = Guid.NewGuid().ToString(),
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    NoteId = note.NoteId,
                    Path = resource.ResourceUrl,
                    Title = note.Subject,
                    AttachmentType = resource.FileExtension
                });

            }

            _db.SaveChanges();
        }


        public List<Correspondence> GetNotesForAdviserByUserId(string adviserUserId, int noteType)
        {
            List<Correspondence> result = new List<Correspondence>();
            var adviser = _db.Advisers.SingleOrDefault(ad => ad.AdviserNumber == adviserUserId);
            var adviserId = adviser.AdviserId.ToString();
            var relevantNoteType = noteType;
            foreach (var note in _db.Notes.Where(n => n.AdviserId == adviserId &&          //n.AccountId ==> n.AdviserId
                n.NoteType == relevantNoteType && _db.NoteLinks.Where(l => l.NoteId2 == n.NoteId).Count() == 0))
            {
                var client = _db.Clients.SingleOrDefault(c => c.ClientId == note.Client.ClientId);
                //var resource = note.Attachments.FirstOrDefault();
                var resource = _db.Attachments.SingleOrDefault(a => a.NoteId == note.NoteId);
                #region create correspondence payload skeleton first
                Correspondence item = new Correspondence()
                {
                    adviserId = adviser.AdviserId.ToString(),
                    adviserName = adviser.FirstName + " " + adviser.LastName,
                    clientId = note.ClientId,
                    clientName = client.ClientType == BusinessLayerParameters.clientType_person ? client.FirstName + " " + client.LastName : client.EntityName,
                    date = note.DateCreated,
                    noteId = note.NoteId,
                    //path = resource == null ? "" : System.Web.VirtualPathUtility.ToAbsolute(resource.Path),
                    path = resource == null ? "" : resource.Path,
                    subject = note.Subject,
                    //typeName = CommonHelpers.GetEnumDescription((NoteTypes)note.NoteType),
                    typeName = ((NoteTypes)note.NoteType).ToString(),
                    type = resource == null ? "" : resource.AttachmentType,
                    conversations = new List<CorrespondenceConversation>(),
                    actionsRequired = note.FollowupActions,
                    assetClass = note.AssetClass,
                    completionDate = note.DateCompleted,
                    productClass = note.ProductClass
                };
                #endregion
                #region inject the initial conversation

                CorrespondenceConversation initial = new CorrespondenceConversation()
                {
                    content = note.Body,
                    createdOn = note.DateCreated,
                    senderRole = note.SenderRole,
                    senderName = note.SenderRole == BusinessLayerParameters.correspondenceSenderRole_adviser ? adviser.FirstName + " " + adviser.LastName
                    : (client.ClientType == BusinessLayerParameters.clientType_person ? client.FirstName + " " + client.LastName : client.EntityName)
                };
                item.conversations.Add(initial);
                #endregion
                #region insert all other conversations
                foreach (var subnote in _db.NoteLinks.Where(n => n.NoteId1 == note.NoteId))
                {
                    var subNoteContent = _db.Notes.SingleOrDefault(n => n.NoteId == subnote.NoteId2);
                    CorrespondenceConversation conversation = new CorrespondenceConversation()
                    {
                        content = subNoteContent.Body,
                        senderRole = subNoteContent.SenderRole,
                        createdOn = subNoteContent.DateCreated,
                        senderName = subNoteContent.SenderRole == BusinessLayerParameters.correspondenceSenderRole_adviser ? adviser.FirstName + " " + adviser.LastName
                        : (client.ClientType == BusinessLayerParameters.clientType_person ? client.FirstName + " " + client.LastName : client.EntityName)
                    };
                    item.conversations.Add(conversation);
                }


                #endregion
                item.conversations = item.conversations.OrderBy(s => s.createdOn).ToList();
                result.Add(item);
            }
            return result;
        }

        public void CreateMessageFollowup(CorrespondenceFollowup model, int senderRole)
        {
            var correspondingNote = _db.Notes.SingleOrDefault(n => n.NoteId == model.existingNoteId);
            Notes note = new Notes()
            {
                NoteId = Guid.NewGuid().ToString(),
                AdviserId= correspondingNote.AdviserId,
                AccountId = correspondingNote.AccountId,
                AssetClass = correspondingNote.AssetClass,
                AssetTypeId = correspondingNote.AssetTypeId,
                Body = model.body,
                IsAccepted = correspondingNote.IsAccepted,
                IsDeclined = correspondingNote.IsDeclined,
                NoteSerial = correspondingNote.NoteSerial,
                Product = correspondingNote.Product,
                Purpose = correspondingNote.Purpose,
                ProductClass = correspondingNote.ProductClass,
                Reminder = correspondingNote.Reminder,
                FollowupActions = correspondingNote.FollowupActions,
                FollowupDate = correspondingNote.FollowupDate,
                ReminderDate = correspondingNote.ReminderDate,
                Status = correspondingNote.Status,
                TimeSpend = correspondingNote.TimeSpend,
                ClientId = correspondingNote.ClientId,
                DateCompleted = correspondingNote.DateCompleted,
                DateCreated = DateTime.Now,
                DateDue = correspondingNote.DateDue,
                DateModified = DateTime.Now,
                NoteType = correspondingNote.NoteType,
                Subject = correspondingNote.Subject,
                SenderRole = senderRole
            };

            NoteLinks link = new NoteLinks()
            {
                Id = Guid.NewGuid().ToString(),
                DateCreated = DateTime.Now,
                NoteId1 = correspondingNote.NoteId,
                NoteId2 = note.NoteId
            };
            _db.Notes.Add(note);
            _db.NoteLinks.Add(link);
            _db.SaveChanges();
        }

        public List<Correspondence> GetNotesForClientByUserId(string clientUserId, int noteType)
        {
            List<Correspondence> result = new List<Correspondence>();
            var client = _db.Clients.SingleOrDefault(c => c.ClientNumber == clientUserId);
            var relevantNoteType = noteType;
            foreach (var note in _db.Notes.Where(n => n.ClientId == client.ClientId &&
                n.NoteType == relevantNoteType && _db.NoteLinks.Where(l => l.NoteId2 == n.NoteId).Count() == 0))
            {
                var adviser = _db.Advisers.FirstOrDefault(ad => ad.AdviserNumber.ToString() == note.AccountId);
                var resource = _db.Attachments.SingleOrDefault(a => a.NoteId == note.NoteId);
                #region create correspondence payload skeleton first
                Correspondence item = new Correspondence()
                {
                    adviserId = adviser.AdviserId.ToString(),
                    adviserName = adviser.FirstName + " " + adviser.LastName,
                    clientId = note.ClientId,
                    clientName = client.ClientType == BusinessLayerParameters.clientType_person ? client.FirstName + " " + client.LastName : client.EntityName,
                    date = note.DateCreated,
                    noteId = note.NoteId,
                    path = resource == null ? "" : resource.Path,
                    subject = note.Subject,
                    //typeName = CommonHelpers.GetEnumDescription((NoteTypes)note.NoteType),
                    typeName = ((NoteTypes)note.NoteType).ToString(),
                    type = resource == null ? "" : resource.AttachmentType,
                    conversations = new List<CorrespondenceConversation>(),
                    actionsRequired = note.FollowupActions,
                    assetClass = note.AssetClass,
                    completionDate = note.DateCompleted,
                    productClass = note.ProductClass
                };
                #endregion
                #region inject the initial conversation

                CorrespondenceConversation initial = new CorrespondenceConversation()
                {
                    content = note.Body,
                    createdOn = note.DateCreated,
                    senderRole = note.SenderRole,
                    senderName = note.SenderRole == BusinessLayerParameters.correspondenceSenderRole_adviser ? adviser.FirstName + " " + adviser.LastName
                    : (client.ClientType == BusinessLayerParameters.clientType_person ? client.FirstName + " " + client.LastName : client.EntityName)
                };
                item.conversations.Add(initial);
                #endregion
                #region insert all other conversations
                foreach (var subnote in _db.NoteLinks.Where(n => n.NoteId1 == note.NoteId))
                {
                    var subNoteContent = _db.Notes.SingleOrDefault(n => n.NoteId == subnote.NoteId2);
                    CorrespondenceConversation conversation = new CorrespondenceConversation()
                    {
                        content = subNoteContent.Body,
                        senderRole = subNoteContent.SenderRole,
                        createdOn = subNoteContent.DateCreated,
                        senderName = subNoteContent.SenderRole == BusinessLayerParameters.correspondenceSenderRole_adviser ? adviser.FirstName + " " + adviser.LastName
                        : (client.ClientType == BusinessLayerParameters.clientType_person ? client.FirstName + " " + client.LastName : client.EntityName)
                    };
                    item.conversations.Add(conversation);
                }


                #endregion
                item.conversations = item.conversations.OrderBy(s => s.createdOn).ToList();
                result.Add(item);
            }
            return result;
        }

        public void InsertEquityData(Domain.Portfolio.AggregateRoots.Asset.Equity equity) {
            _db.Equities.Add(new Equity
            {
                AssetId = Guid.NewGuid().ToString(),
                Sector = equity.Sector,
                EquityTransactions = new List<EquityTransaction>(),
                Ticker = equity.Ticker,
                EquityType = equity.EquityType,
                Prices = new List<AssetPrice>(),
                Name = equity.Name,
                Dividends = new List<Dividend>()
            });

            _db.SaveChanges();
        }

        public ClientAccount GetClientAccountById(string accountId) {
            var clientAccount =
                _db.Accounts.Local
                    .FirstOrDefault(
                        a => a.AccountId == accountId && a.CreatedOn.HasValue) ??
                _db.Accounts.Where(
                    a => a.AccountId == accountId && a.CreatedOn.HasValue)
                    .FirstOrDefault();

            return new ClientAccount(this) {
                AccountNameOrInfo = clientAccount.AccountInfo,
                AccountNumber = clientAccount.AccountNumber,
                AccountType = clientAccount.AccountType,
                Id = clientAccount.AccountId,
                MarginLenderId = clientAccount.MarginLenderId,
            };
        }

        public GroupAccount GetGroupAccountById(string accountId) {
            var groupAccount =
                _db.Accounts.Local
                    .FirstOrDefault(
                        a => a.AccountId == accountId && a.CreatedOn.HasValue) ??
                _db.Accounts.Where(
                    a => a.AccountId == accountId && a.CreatedOn.HasValue)
                    .FirstOrDefault();

            return new GroupAccount(this) {
                AccountNameOrInfo = groupAccount.AccountInfo,
                AccountNumber = groupAccount.AccountNumber,
                AccountType = groupAccount.AccountType,
                Id = groupAccount.AccountId,
                MarginLenderId = groupAccount.MarginLenderId
            };
        }



        public Equity getEquityByTicker(string ticker)
        {
            Equity equity = _db.Equities.SingleOrDefault(e => e.Ticker == ticker);
            return equity;
            //return new Equity
            //{
            //    AssetId = equity.AssetId
            //};
        }


        public Equity getEquityByTicker(string ticker, EquityTypes type) {
            Equity equity = _db.Equities.SingleOrDefault(e => e.Ticker == ticker && e.EquityType == type);

            return new Equity
            {
                AssetId = equity.AssetId
            };
        }

        public string GetCountryCodeByName(string countryName) {
            var countryCode = _db.CountryCodes.FirstOrDefault(c => c.CountryName.Contains(countryName));
            if (countryCode != null) {
                return countryCode.Code;
            }
            else{
                return null;
            }
        }

        public List<Domain.Portfolio.AggregateRoots.Asset.Equity> GetEquityForGroupAccountSync(string equityId, ClientGroup clientGroup) {

            List<GroupAccount> GroupAccounts = GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
            List<ClientAccount> clientAccounts = new List<ClientAccount>();
            clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

            List<Account> accounts = new List<Account>();
            GroupAccounts.ForEach(g => accounts.AddRange(_db.Accounts.Where(a => a.AccountNumber == g.AccountNumber)));
            clientAccounts.ForEach(c => accounts.AddRange(_db.Accounts.Where(a => a.AccountNumber == c.AccountNumber)));

            Equity equity = _db.Equities.SingleOrDefault(e => e.AssetId == equityId);
            List<Domain.Portfolio.AggregateRoots.Asset.Equity> equities = new List<Domain.Portfolio.AggregateRoots.Asset.Equity>();

            switch (equity.EquityType) {
                case EquityTypes.AustralianEquity:
                    accounts.ForEach(a => equities.AddRange(GenerateAustralianEquityForAccountSync(a.AccountId, DateTime.Now, a).Where(auE => auE.Id == equityId)));
                    break;
                case EquityTypes.InternationalEquity:
                    accounts.ForEach(a => equities.AddRange(GenerateInternationalEquityForAccountSync(a.AccountId, DateTime.Now, a).Where(inE => inE.Id == equityId)));
                    break;
            }

            return equities;
        }



        public List<Domain.Portfolio.AggregateRoots.Asset.Equity> GetEquityForClientAccountSync(string equityId, Client client) {

            List<Account> accounts = new List<Account>();
            client.GetAccountsSync().ForEach(c => accounts.AddRange(_db.Accounts.Where(a => a.AccountNumber == c.AccountNumber)));

            Equity equity = _db.Equities.SingleOrDefault(e => e.AssetId == equityId);
            List<Domain.Portfolio.AggregateRoots.Asset.Equity> equities = new List<Domain.Portfolio.AggregateRoots.Asset.Equity>();

            switch (equity.EquityType) {
                case EquityTypes.AustralianEquity:
                    accounts.ForEach(a => equities.AddRange(GenerateAustralianEquityForAccountSync(a.AccountId, DateTime.Now, a).Where(auE => auE.Id == equityId)));
                    break;
                case EquityTypes.InternationalEquity:
                    accounts.ForEach(a => equities.AddRange(GenerateInternationalEquityForAccountSync(a.AccountId, DateTime.Now, a).Where(inE => inE.Id == equityId)));
                    break;
            }

            return equities;
        }


        public int GetEquityUnitByEquityIdAndClientGroup(string equityId, string clientGroupId) {
            List<Account> accounts = GetAllAccountsByClientGroupId(clientGroupId);

            int numberOfUnit = 0;

            accounts.ForEach(a => {
                var transactions = a.EquityTransactions.Where(e => e.EquityId == equityId);
                numberOfUnit += transactions == null ? 0 : (int)transactions.Sum(e => e.NumberOfUnits);
            });

            return numberOfUnit;
        }

        public DateTime? GetLastPriceDateForEquity(string equityId) {
            return _db.Equities.SingleOrDefault(e => e.AssetId == equityId).Prices.Max(p => p.CreatedOn);
        }

        public Domain.Portfolio.AggregateRoots.Asset.Equity getEquityById(string equityId) {
            Equity equity = _db.Equities.SingleOrDefault(e => e.AssetId == equityId);

            var prices = equity.Prices.OrderByDescending(p => p.CreatedOn);
            var latestPrice = 0.0;
            if (prices.Count() != 0) {
                latestPrice = prices.FirstOrDefault().Price.GetValueOrDefault();
            }

            if(equity.EquityType == EquityTypes.AustralianEquity){
                return new Domain.Portfolio.AggregateRoots.Asset.AustralianEquity(this)
                {
                    Id = equity.AssetId,
                    Name = equity.Name,
                    EquityType = equity.EquityType,
                    Ticker = equity.Ticker,
                    Sector = equity.Sector,
                    LatestPrice = latestPrice,
                    F0Ratios = GetF0RatiosForEquitySync(equity.Ticker),
                    F1Recommendation = GetF1RatiosForEquitySync(equity.Ticker)
                };
            }else if(equity.EquityType == EquityTypes.InternationalEquity){
                return new Domain.Portfolio.AggregateRoots.Asset.InternationalEquity(this)
                {
                    Id = equity.AssetId,
                    Name = equity.Name,
                    EquityType = equity.EquityType,
                    Ticker = equity.Ticker,
                    Sector = equity.Sector,
                    LatestPrice = latestPrice,
                    F0Ratios = GetF0RatiosForEquitySync(equity.Ticker),
                    F1Recommendation = GetF1RatiosForEquitySync(equity.Ticker)
                };
            }
            else if (equity.EquityType == EquityTypes.ManagedInvestments)
            {
                return new Domain.Portfolio.AggregateRoots.Asset.ManagedInvestment(this)
                {
                    Id = equity.AssetId,
                    Name = equity.Name,
                    EquityType = equity.EquityType,
                    Ticker = equity.Ticker,
                    Sector = equity.Sector,
                    LatestPrice = latestPrice,
                    F0Ratios = GetF0RatiosForEquitySync(equity.Ticker),
                    F1Recommendation = GetF1RatiosForEquitySync(equity.Ticker)
                };
            }
            else {
                return null;
            }
        }

        public Domain.Portfolio.AggregateRoots.Asset.Equity getEquityByIdAndClientGroup(string equityId, string clientGroupId) {
            Equity equity = _db.Equities.SingleOrDefault(e => e.AssetId == equityId);

            var latestPrice = equity.Prices.OrderByDescending(p => p.CreatedOn).FirstOrDefault().Price.GetValueOrDefault();

            if (equity.EquityType == EquityTypes.AustralianEquity) {
                return new Domain.Portfolio.AggregateRoots.Asset.AustralianEquity(this) {
                    Id = equity.AssetId,
                    Name = equity.Name,
                    EquityType = equity.EquityType,
                    Ticker = equity.Ticker,
                    F0Ratios = GetF0RatiosForEquitySync(equity.Ticker),
                    F1Recommendation = GetF1RatiosForEquitySync(equity.Ticker),
                    Sector = equity.Sector,
                    TotalNumberOfUnits = GetEquityUnitByEquityIdAndClientGroup(equityId, clientGroupId),
                    LatestPrice = latestPrice
                };
            } else if (equity.EquityType == EquityTypes.InternationalEquity) {
                return new Domain.Portfolio.AggregateRoots.Asset.InternationalEquity(this) {
                    Id = equity.AssetId,
                    Name = equity.Name,
                    EquityType = equity.EquityType,
                    Ticker = equity.Ticker,
                    F0Ratios = GetF0RatiosForEquitySync(equity.Ticker),
                    F1Recommendation = GetF1RatiosForEquitySync(equity.Ticker),
                    Sector = equity.Sector,
                    TotalNumberOfUnits = GetEquityUnitByEquityIdAndClientGroup(equityId, clientGroupId),
                    LatestPrice = latestPrice
                };
            } else if (equity.EquityType == EquityTypes.ManagedInvestments) {
                return new Domain.Portfolio.AggregateRoots.Asset.ManagedInvestment(this) {
                    Id = equity.AssetId,
                    Name = equity.Name,
                    EquityType = equity.EquityType,
                    Ticker = equity.Ticker,
                    F0Ratios = GetF0RatiosForEquitySync(equity.Ticker),
                    F1Recommendation = GetF1RatiosForEquitySync(equity.Ticker),
                    Sector = equity.Sector,
                    TotalNumberOfUnits = GetEquityUnitByEquityIdAndClientGroup(equityId, clientGroupId),
                    LatestPrice = latestPrice
                };
            } else {
                return null;
            }
        }

        public Domain.Portfolio.AggregateRoots.Asset.Equity getFirstEquity()
        {
            Equity equity = _db.Equities.FirstOrDefault();
            if (equity.EquityType == EquityTypes.AustralianEquity)
            {
                return new Domain.Portfolio.AggregateRoots.Asset.AustralianEquity(this)
                {
                    Id = equity.AssetId,
                    Name = equity.Name,
                    EquityType = equity.EquityType,
                    Ticker = equity.Ticker
                };
            }
            else if (equity.EquityType == EquityTypes.InternationalEquity)
            {
                return new Domain.Portfolio.AggregateRoots.Asset.InternationalEquity(this)
                {
                    Id = equity.AssetId,
                    Name = equity.Name,
                    EquityType = equity.EquityType,
                    Ticker = equity.Ticker
                };
            }
            else if (equity.EquityType == EquityTypes.ManagedInvestments)
            {
                return new Domain.Portfolio.AggregateRoots.Asset.ManagedInvestment(this)
                {
                    Id = equity.AssetId,
                    Name = equity.Name,
                    EquityType = equity.EquityType,
                    Ticker = equity.Ticker
                };
            }
            else
            {
                return null;
            }
        }

        public List<Domain.Portfolio.AggregateRoots.Asset.AssetPrice> getPricesByEquityIdAndDates(string equityId, string periodId)
        {
            List<Domain.Portfolio.AggregateRoots.Asset.AssetPrice> assetPrices = new List<Domain.Portfolio.AggregateRoots.Asset.AssetPrice>();

            Equity equity = _db.Equities.SingleOrDefault(e => e.AssetId == equityId);

            List<AssetPrice> prices = new List<AssetPrice>();

            var allPrices = equity.Prices;
            if (allPrices.Count == 0) {
                return assetPrices;
            }

            DateTime lastDate = (DateTime)allPrices.Max(p => p.CreatedOn);

            if (periodId == Period.LastMonth.ToString())
            {
                for (int i = 0; i < 30; i++)
                {
                    var currentPrice = allPrices.SingleOrDefault(p => p.CreatedOn.Value.Date == lastDate.AddDays(-i));
                    if (currentPrice != null)
                    {
                        prices.Add(currentPrice);
                    }
                }
            }
            else if (periodId == Period.LastSixMonths.ToString())
            {
                for (int i = 0; i < 26; i++)
                {
                    var currentPrice = allPrices.SingleOrDefault(p => p.CreatedOn.Value.Date == lastDate.AddDays(-7 * i));
                    if (currentPrice != null)
                    {
                        prices.Add(currentPrice);
                    }
                }
            }
            else if (periodId == Period.LastTwelveMonths.ToString())
            {
                for (int i = 0; i < 26; i++)
                {
                    var currentPrice = allPrices.SingleOrDefault(p => p.CreatedOn.Value.Date == lastDate.AddDays(-14 * i));
                    if (currentPrice != null)
                    {
                        prices.Add(currentPrice);
                    }
                }
            }
            else if (periodId == Period.LastThreeYears.ToString())
            {
                for (int i = 0; i < 36; i++)
                {
                    var currentPrice = allPrices.SingleOrDefault(p => p.CreatedOn.Value.Date == lastDate.AddMonths(-i));
                    if (currentPrice != null)
                    {
                        prices.Add(currentPrice);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    var currentPrice = allPrices.SingleOrDefault(p => p.CreatedOn.Value.Date == lastDate.AddMonths(-6 * i));
                    if (currentPrice != null) {
                        prices.Add(currentPrice);
                    }
                }
            }



            //foreach (var date in dateList)
            //{
            //    prices.Add(equity.Prices.SingleOrDefault(p => p.CreatedOn.Value.Date == date));                
            //}

            var pricesOrder = prices.OrderByDescending(p => p.CreatedOn).ToList();
            foreach (var price in pricesOrder) {
                assetPrices.Add(new Domain.Portfolio.AggregateRoots.Asset.AssetPrice
                {
                    Id = price.Id,
                    AssetType = price.AssetType,
                    CorrespondingAssetKey = price.CorrespondingAssetKey,
                    CreatedOn = price.CreatedOn,
                    Price = price.Price
                });
            }

            //var pricesOrder = equity.Prices.ToList().OrderByDescending(p => p.CreatedOn).ToList();
            //for (int i = 0; i < 180; i++) {
            //    var price = pricesOrder[i];
            //    assetPrices.Add(new Domain.Portfolio.AggregateRoots.Asset.AssetPrice { 
            //        Id = price.Id,
            //        AssetType = price.AssetType,
            //        CorrespondingAssetKey = price.CorrespondingAssetKey,
            //        CreatedOn = price.CreatedOn,
            //        Price = price.Price
            //    });
            //}

            return assetPrices;
        }

        public List<Domain.Portfolio.AggregateRoots.Asset.Equity> GetAllEquitiesBySectorName(string sectorName)
        {
            //var equities = _db.Equities.Where(e => _db.Sectors.Where(s => s.SectorName == sectorName).Select(sc => sc.Id).ToList().Contains(e.Sector));
            var equities = _db.Equities.Where(e => e.Sector == sectorName);
            List<Domain.Portfolio.AggregateRoots.Asset.Equity> allEquities = new List<Domain.Portfolio.AggregateRoots.Asset.Equity>();
            foreach (var equity in equities)
            {
                switch (equity.EquityType)
                {
                    case EquityTypes.AustralianEquity:
                        allEquities.Add(new AustralianEquity(this)
                        {
                            Id = equity.AssetId,
                            Name = equity.Name,
                            EquityType = equity.EquityType,
                            Sector = equity.Sector,
                            Ticker = equity.Ticker
                        });
                        break;
                    case EquityTypes.InternationalEquity:
                        allEquities.Add(new InternationalEquity(this)
                        {
                            Id = equity.AssetId,
                            Name = equity.Name,
                            EquityType = equity.EquityType,
                            Sector = equity.Sector,
                            Ticker = equity.Ticker
                        });
                        break;
                    case EquityTypes.ManagedInvestments:
                        allEquities.Add(new ManagedInvestment(this)
                        {
                            Id = equity.AssetId,
                            Name = equity.Name,
                            EquityType = equity.EquityType,
                            Sector = equity.Sector,
                            Ticker = equity.Ticker
                        });
                        break;
                }
            }
            return allEquities;
        }

        public List<string> GetAllResearchStringValueByKey(string key) {
            return _db.ResearchValues.Where(r => r.Key == key).Select(re => re.StringValue).Distinct().ToList();
        }

        public List<Domain.Portfolio.AggregateRoots.Asset.Equity> GetAllEquitiesByResearchStringValue(string researchStringValue) {
            var equities = _db.Equities.Where(e => e.ResearchValues.Where(r => r.StringValue == researchStringValue).Any()).ToList();
            List<Domain.Portfolio.AggregateRoots.Asset.Equity> allEquities = new List<Domain.Portfolio.AggregateRoots.Asset.Equity>();
            foreach (var equity in equities) {
                switch (equity.EquityType) { 
                    case EquityTypes.AustralianEquity :
                        allEquities.Add(new AustralianEquity(this) {
                            Id = equity.AssetId,
                            Name = equity.Name,
                            EquityType = equity.EquityType,
                            Sector = equity.Sector,
                            Ticker = equity.Ticker
                        });
                        break;
                    case EquityTypes.InternationalEquity:
                        allEquities.Add(new InternationalEquity(this)
                        {
                            Id = equity.AssetId,
                            Name = equity.Name,
                            EquityType = equity.EquityType,
                            Sector = equity.Sector,
                            Ticker = equity.Ticker
                        });
                        break;
                    case EquityTypes.ManagedInvestments:
                        allEquities.Add(new ManagedInvestment(this)
                        {
                            Id = equity.AssetId,
                            Name = equity.Name,
                            EquityType = equity.EquityType,
                            Sector = equity.Sector,
                            Ticker = equity.Ticker
                        });
                        break;
                }
            }
            return allEquities;
        }

        public void InsertStockPricesData(List<AssetPrice> assetPrices) {
            string id = assetPrices[0].CorrespondingAssetKey;
            Equity equity = _db.Equities.SingleOrDefault(e => e.AssetId == id);

            foreach(var price in assetPrices){
                equity.Prices.Add(new AssetPrice { 
                    Id = Guid.NewGuid().ToString(),
                    AssetType = price.AssetType,
                    CreatedOn = price.CreatedOn,
                    CorrespondingAssetKey = price.CorrespondingAssetKey,
                    Price = price.Price
                });
            }
            _db.SaveChanges();
        }

        public void CreateRebalanceModel(RebalanceModel model) {
            var adviser = _db.Advisers.SingleOrDefault(a => a.AdviserNumber == model.AdviserId);
            var client = _db.Clients.SingleOrDefault(a => a.ClientNumber == model.ClientId);

            var clientGroup = _db.ClientGroups.SingleOrDefault(c => c.ClientGroupId == model.ClientGroupId);
            List<Edis.Db.Rebalance.TemplateDetailsItemParameter> parameters = new List<Edis.Db.Rebalance.TemplateDetailsItemParameter>();
            foreach(var parameter in model.TemplateDetailsItemParameters){
                parameters.Add(new Edis.Db.Rebalance.TemplateDetailsItemParameter
                { 
                    Id = Guid.NewGuid().ToString(),
                    EquityId = parameter.EquityId,
                    ItemName = parameter.ItemName,
                    CurrentWeighting = parameter.CurrentWeighting,
                    identityMetaKey = parameter.identityMetaKey
                });
            }

            Edis.Db.Rebalance.RebalanceModel newModel = new Edis.Db.Rebalance.RebalanceModel
            {
                ModelId = Guid.NewGuid().ToString(),
                ProfileId = model.ProfileId,
                Adviser = adviser,
                Client = client,
                ClientGroup = clientGroup,
                ModelName = model.ModelName,
                TemplateDetailsItemParameters = parameters
            };
            
            foreach(var parameter in newModel.TemplateDetailsItemParameters){
                //parameter.ModelRefId = newModel.ModelId;
            };

            _db.RebalanceModels.Add(newModel);


            _db.SaveChanges();
        }


        public void UpdateRebalanceModel(RebalanceModel model) {
            var currentModel = _db.RebalanceModels.SingleOrDefault(r => r.ModelId == model.ModelId);
            
            currentModel.ModelId = model.ModelId;
            currentModel.ModelName = model.ModelName;
            currentModel.ProfileId = model.ProfileId;
            _db.TemplateDetailsItemParameters.RemoveRange(currentModel.TemplateDetailsItemParameters);
            foreach(var parameter in model.TemplateDetailsItemParameters){
                currentModel.TemplateDetailsItemParameters.Add(new Edis.Db.Rebalance.TemplateDetailsItemParameter { 
                    Id = Guid.NewGuid().ToString(),
                    EquityId = parameter.EquityId,
                    ItemName = parameter.ItemName,
                    CurrentWeighting = parameter.CurrentWeighting,
                    identityMetaKey = parameter.identityMetaKey,
                    Model=currentModel
                });
            }
            _db.SaveChanges();         
        }

        public List<RebalanceModel> GetRebalanceModelById(string Id)
        {
            var adviser = _db.Advisers.SingleOrDefault(a => a.AdviserNumber == Id);
            var client = _db.Clients.SingleOrDefault(c => c.ClientNumber == Id);

            List<Edis.Db.Rebalance.RebalanceModel> models = null;
            if(adviser != null){
                models = _db.RebalanceModels.Where(r => r.Adviser.AdviserId == adviser.AdviserId).ToList();
            } else if (client != null) {
                models = _db.RebalanceModels.Where(r => r.Client.ClientId == client.ClientId).ToList();
            } else {
                return null;
            }
            
            List<RebalanceModel> results = new List<RebalanceModel>();

            foreach (var model in models)
            {
                List<TemplateDetailsItemParameter> parameters = new List<TemplateDetailsItemParameter>();
                foreach (var parameter in model.TemplateDetailsItemParameters)
                {

                    parameters.Add(new TemplateDetailsItemParameter
                    {
                        CurrentWeighting = parameter.CurrentWeighting,
                        EquityId = parameter.EquityId,
                        ItemName = parameter.ItemName,
                    });
                };

                results.Add(new RebalanceModel
                {
                    ProfileId = model.ProfileId,
                    ModelId = model.ModelId,
                    ModelName = model.ModelName,
                    TemplateDetailsItemParameters = parameters
                });
            }
            return results;
        }

        public RebalanceModel GetRebalanceModelByModelId(string modelId) {
            var savedMode = _db.RebalanceModels.SingleOrDefault(r => r.ModelId == modelId);
       

            List<TemplateDetailsItemParameter> parameters = new List<TemplateDetailsItemParameter>();

            foreach (var parameter in savedMode.TemplateDetailsItemParameters) {
                parameters.Add(new TemplateDetailsItemParameter { 
                    EquityId = parameter.EquityId,
                    CurrentWeighting = parameter.CurrentWeighting,
                    ItemName = parameter.ItemName,
                    identityMetaKey = parameter.identityMetaKey
                });
            };

            return new RebalanceModel() { 
                ProfileId = savedMode.ProfileId,
                ModelId = savedMode.ModelId,
                ModelName = savedMode.ModelName,
                TemplateDetailsItemParameters = parameters,
                ClientGroupId = savedMode.ClientGroup.ClientGroupId
            };
        }


        public string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }






        //  public void CreateNewReturnOfCapitalAction(ReturnOfCapitalCreationModel model)
        //{
        //    //to do 
        //    //get all associated cashAccounts to increase the capital 

        //    var accountsToAction = GetAllClientGroupsForAdviserSync(model.AdviserId, DateTime.Now);

        //    if (accountsToAction != null){ 
        //       foreach (var clientGroup in accountsToAction)
        //      {
        //            var GroupAccount =  GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now).FirstOrDefault();

        //            var amountToDeductOrIncrease = Convert.ToDouble(model.ReturnOfCapitalAmount);
        //            MakeCashTransactions(clientGroup.ClientGroupNumber, amountToDeductOrIncrease, GroupAccount);
        //            recordReturnOfCapitalHistory(model.AdviserId, GroupAccount.Id, model.ReturnOfCapitalAmount, model.ActionName, clientGroup.Id);
        //      };
        //    }
        //    _db.SaveChanges();
        //}







        public void CreateNewReturnOfCapitalAction(ReturnOfCapitalCreationModel model) {
            var accounts = model.AccountsInfo;
            foreach (var account in accounts) {
                var amountToDeductOrIncrease = Convert.ToDouble(account.ReturnAmount);
                MakeCashTransactions(account.AccountNumber, amountToDeductOrIncrease);
                recordReturnOfCapitalHistory(model.AdviserId, account.AccountNumber, amountToDeductOrIncrease.ToString(), model.ActionName,model.Ticker);
            }
            
        }
       
        public void CreateNewStockSplitAction(StockSplitCreationModel model) {
            //change total number of units to this passin value
            if (model.AccountsInfo != null) {
                var equity = getEquityByTicker(model.Ticker);
                foreach (var acc in model.AccountsInfo) {
                    var account = _db.Accounts.Where(a => a.AccountNumber == acc.AccountNumber).FirstOrDefault();
                    //var clientAccount = GetClientAccountSync(acc.AccountNumber, DateTime.Now);
                    var numberOfUnits = checkUnitOfSharesByEquityIdAndAssociatedAccount(equity.AssetId, account);
                    var sharetoMaketrans =Convert.ToInt16(acc.splitToUnit) - numberOfUnits;
                    ChangeStockShare(sharetoMaketrans, account, equity);
                    AddNewCorporateAction(CorporateActionType.StockSplit, model.AdviserId, account.AccountNumber, "0", model.ActionName, CorporateActionStatus.Mandatory, acc.splitToUnit, model.Ticker);
                }
            }
        }
        
        public void CreateNewBonusIssueAction(BonusIssueCreationModel model) {
            //simply increase number of units by this passin value
            if (model.Participants != null)
            {
                var equity = getEquityByTicker(model.Ticker);
                foreach (var acc in model.Participants)
                {
                    var account = _db.Accounts.Where(a => a.AccountNumber == acc.AccountNumber).FirstOrDefault();
                    ChangeStockShare(Convert.ToInt16(acc.ShareAmount), account, equity);
                    AddNewCorporateAction(CorporateActionType.BonusIssue, model.AdviserId, account.AccountNumber, "0", model.ActionName, CorporateActionStatus.Mandatory, acc.ShareAmount, model.Ticker);
                }
            }
        }
        
        public void CreateNewBuyBackProgramActionAdviseInital(BuyBackProgramCreationModel model) {
            //increase cash account total amount deduct total number of units
            if (model.Participants != null)
            {
                foreach (var account in model.Participants)
                {
                    AddNewCorporateAction(CorporateActionType.BuyBackProgram, model.AdviserId, account.AccountNumber, account.CashAmount, model.ActionName, CorporateActionStatus.Pending, account.ShareAmount, model.Ticker);
                }
            }

        }

        public void CreateNewRightsIssueActionAdviseInital(RightsIssueCreationModel model) {
            //deduct cash account total amount incease total number of units
            if (model.Participants != null)
            {
                foreach (var account in model.Participants)
                {
                    AddNewCorporateAction(CorporateActionType.RightsIssue, model.AdviserId, account.AccountNumber, account.CashAmount, model.ActionName, CorporateActionStatus.Pending, account.ShareAmount, model.Ticker);
                }
            }
        }

        private void AddNewCorporateAction(CorporateActionType actionType,string adviserId, string accountNumber, string cashAmount,string actionName, CorporateActionStatus status, string shareAmount, string ticker) {
            var newRecord = new CorperateActionHistory()
            {
                ActionType = actionType,
                AdviserId = adviserId,
                AssociatedAccountNumber = accountNumber,
                CashAdjustmentAmount = cashAmount,
                CorperateActionDate = DateTime.Now,
                CorperateActionName = actionName,
                Status = status,
                StockAdjustmentShareAmount = shareAmount,
                Ticker = ticker
            };
            _db.CorporateActions.Add(newRecord);
            _db.SaveChanges();
        }

        //private string CheckAccountIsAGroupAccountOrAClientAccount(Account account) {
        //    var isGroupAccount = _db.ClientGroups.FirstOrDefault(g => g.GroupAccounts.Any(a => a.AccountNumber == account.AccountNumber));
        //    if (isGroupAccount == null)
        //    {
        //        var hasClientAccount = _db.Clients.FirstOrDefault(c => c.Accounts.Any(a => a.AccountNumber == account.AccountNumber));
        //        if (hasClientAccount == null)
        //        {
        //            return "GroupAccountOnly";
        //        }

        //        return "GroupAccountAndClientAccount";
        //    }

        //   var clientAccount = _db.Clients.FirstOrDefault(c => c.Accounts.Any(a => a.AccountNumber == account.AccountNumber));
        //   if (clientAccount == null)
        //   {
        //       return "ClientAccountOnly";
        //   }          
        //    return "Nothing";
        //}
            
        private AccountCatergories CheckAccountIsAGroupAccountOrAClientAccount(Account account)
        {
            var isGroupAccount = _db.ClientGroups.FirstOrDefault(g => g.GroupAccounts.Any(a => a.AccountNumber == account.AccountNumber));
            if (isGroupAccount == null) {
                var isClientAccount = _db.Clients.FirstOrDefault(c => c.Accounts.Any(a => a.AccountNumber == account.AccountNumber));
                if (isClientAccount != null) {
                    return AccountCatergories.ClientAccount;
                }
                return AccountCatergories.Noway;
            }
            return AccountCatergories.GroupAccount;
        }

        private CashAccount GetCashAccountByGroupAccount(Account account) {
            var clientGroup = _db.ClientGroups.FirstOrDefault(g => g.GroupAccounts.Any(a => a.AccountNumber == account.AccountNumber));
            return _db.CashAccounts.Where(c => c.AccountNumber == clientGroup.GroupNumber).FirstOrDefault();
        }

        private CashAccount GetCashAccountByClientAccount(Account account) {
            var client = _db.Clients.FirstOrDefault(c => c.Accounts.Any(a => a.AccountNumber == account.AccountNumber));
            var clientGroup = _db.ClientGroups.Where(cg => cg.ClientGroupId == client.ClientGroupId).FirstOrDefault();
            return _db.CashAccounts.Where(ca => ca.AccountNumber == clientGroup.GroupNumber).FirstOrDefault();
        }

        public void MakeCashTransactions(string accountNumber, double amount) {
            var account = _db.Accounts.Where(acc => acc.AccountNumber == accountNumber).FirstOrDefault();
            var accountType = CheckAccountIsAGroupAccountOrAClientAccount(account);

            //get this account cash account
            var cashAccount = new CashAccount();
            if (accountType == AccountCatergories.ClientAccount) {
                cashAccount = GetCashAccountByClientAccount(account);
            }

            if (accountType == AccountCatergories.GroupAccount) {
                cashAccount = GetCashAccountByGroupAccount(account);
            }

            //because whatever this account is they have a client group then we can get this client group's cash account
            if (cashAccount.CashTransactions == null)
            {
                cashAccount.CashTransactions = new List<CashTransaction>();
            }
            CashTransaction cashTrans = new CashTransaction()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                CashAccount = cashAccount,
                CashAccountId = cashAccount.Id,
                Amount = amount,
                TransactionDate = DateTime.Now,
            };

            cashAccount.FaceValue += amount;
            cashAccount.CashTransactions.Add(cashTrans);
            account.CashTransactions.Add(cashTrans);
            _db.CashTransactions.Add(cashTrans);
            _db.SaveChanges();
        }

        private void recordReturnOfCapitalHistory(string AdviseId, string accountNumber, string amount, string actionName, string Ticker)
        {
            var newRecord = new CorperateActionHistory()
            {
                ActionType = CorporateActionType.ReturnOfCapital,
                AdviserId = AdviseId,
                AssociatedAccountNumber = accountNumber,
                CashAdjustmentAmount = amount,
                CorperateActionDate = DateTime.Now,
                CorperateActionName = actionName,
                Status = CorporateActionStatus.Mandatory, 
                Ticker = Ticker,
                StockAdjustmentShareAmount = "0",
               // ClientGroupId = ClientGroupId
            };
            _db.CorporateActions.Add(newRecord);
            _db.SaveChanges();

        }

        public void AdviserCreateNewReinvestmentAdviserInital(ReinvestmentPlanCreationModel model)
        {
            //to do pass all notifications to the clients that in this model
            //record this action as well 
            var allParticipants = model.Participants;
            foreach (var acc in allParticipants)
            {
                var newRecord = new CorperateActionHistory()
                {
                    AdviserId = model.AdviserId,
                    ActionType = CorporateActionType.ReinvestmentPlan,
                    Status = CorporateActionStatus.Pending,
                    AssociatedAccountNumber = acc.AccountNumber,
                    CashAdjustmentAmount = "0",
                    CorperateActionDate = model.ReinvestmentDate,
                    CorperateActionName = model.ActionName,
                    StockAdjustmentShareAmount = acc.ShareMount,
                    Ticker = model.Ticker,
                };
                _db.CorporateActions.Add(newRecord);
            }
            _db.SaveChanges();
            //then retieve this record at client side using account number or clientID
            //let the clients to decide whether this corperate action is gonna execute or not 
        }


        //public CashAccount GetCashAccountForAccount(string accountNumber) {
        //    return _db.CashAccounts.Where(ca => ca.AccountNumber == accountNumber).SingleOrDefault();
        //}


        //public List<ReturnOfCapital> GetAllReturnOfCapitalRecord(string AdviserId) {
        //   return _db.ReturnOfCapitals.Where(re => re.AdviserId == AdviserId).ToList();
        //}

        public List<CorperateActionHistory> GetReturnOfCapitalHistoryByAdviser(string AdviserId)
        {
            var result = _db.CorporateActions.Where(ca => ca.AdviserId == AdviserId && ca.ActionType == CorporateActionType.ReturnOfCapital).ToList();
            if (result == null) {
                return new List<CorperateActionHistory>();
            } else
                return result;
        }

        public List<CorperateActionHistory> GetReinvestmentPlanHistoryByAdviser(string AdviserId) {
            var result = _db.CorporateActions.Where(ca => ca.AdviserId == AdviserId && ca.ActionType == CorporateActionType.ReinvestmentPlan).ToList();
            if (result == null) {
                return new List<CorperateActionHistory>();
            } else
                return result;
        }

        public List<CorperateActionHistory> GetStockSplitHistoryByAdviser(string AdviserId) {
            var result = _db.CorporateActions.Where(ca => ca.AdviserId == AdviserId && ca.ActionType == CorporateActionType.StockSplit).ToList();
            if (result == null) {
                return new List<CorperateActionHistory>();
            } else
                return result;
        }

        public List<CorperateActionHistory> GetRightsIssueHistoryByAdviser(string AdviserId)
        {
            var result = _db.CorporateActions.Where(ca => ca.AdviserId == AdviserId && ca.ActionType == CorporateActionType.RightsIssue).ToList();
            if (result == null) {
                return new List<CorperateActionHistory>();
            } else
                return result;
        }

        public List<CorperateActionHistory> GetBuyBackProgramHistoryByAdviser(string AdviserId)
        {
            var result = _db.CorporateActions.Where(ca => ca.AdviserId == AdviserId && ca.ActionType == CorporateActionType.BuyBackProgram).ToList();
            if (result == null) {
                return new List<CorperateActionHistory>();
            } else
                return result;
        }

        public List<CorperateActionHistory> GetBonusIssueHistoryByAdviser(string AdviserId)
        {
            var result =  _db.CorporateActions.Where(ca => ca.AdviserId == AdviserId && ca.ActionType == CorporateActionType.BonusIssue).ToList();
            if (result == null) {
                return new List<CorperateActionHistory>();
            } else
                return result;
        }





        public List<PendingActionViewModel> GetAllPendingCorporateActionsForClient(string ClientNumber, ActionRetrieveType type) {
            var result = new List<PendingActionViewModel>();

            var clientGroup = CheckClientGroupMainClientIdByClientNumber(ClientNumber);
            
            if (clientGroup != null)
            {
                //MainClient Need to get all this client group Accounts
                var groupAccounts = clientGroup.GroupAccounts.ToList();
                if (groupAccounts != null) {
                    foreach (var acc in groupAccounts) {
                        var pendingActions = GetAllPendingActionsForAccount(acc.AccountNumber,type);
                        if (pendingActions != null)
                            result.AddRange(pendingActions);
                    }
                }
            }

            var clientAccounts = GetAccountsForClientByClientNumberSync(ClientNumber, DateTime.Now);

            if (clientAccounts != null)
            {
                foreach (var acc in clientAccounts)
                {
                    var pendingActions = GetAllPendingActionsForAccount(acc.AccountNumber, type);
                    if(pendingActions != null)
                        result.AddRange(pendingActions);
                }
            }
            
            return result;
        }

        private List<PendingActionViewModel> GetAllPendingActionsForAccount(string AccountNumber , ActionRetrieveType type) {
            var pendingActions = new List<CorperateActionHistory>();
            if (type == ActionRetrieveType.PendingRetrieve)
            {
                pendingActions = GetAllPendingActionForAccount(AccountNumber);
            }
            if (type == ActionRetrieveType.AllActionRetrieve) {
                pendingActions = GetAllActionsForAccount(AccountNumber);
            }
            var pendings = GetAllPendingActionsByCorporateActionHistories(pendingActions);
            
            return pendings;
        }

        private List<PendingActionViewModel> GetAllPendingActionsByCorporateActionHistories(List<CorperateActionHistory> thisPendingActions) {
            var result = new List<PendingActionViewModel>();
            if (thisPendingActions != null)
            {
                foreach (var action in thisPendingActions)
                {
                    var newRecord = new PendingActionViewModel()
                    {
                        ActionName = action.CorperateActionName,
                        Ticker = action.Ticker,
                        ShareAmount = action.StockAdjustmentShareAmount,
                        AccountNumber = action.AssociatedAccountNumber,
                        AdjustmentDate = action.CorperateActionDate,
                        CashAdjustments = action.CashAdjustmentAmount,
                        Status = GetEnumDescription(action.Status),
                        ActionType = GetEnumDescription(action.ActionType),
                        ActionId = action.Id,
                    };
                    result.Add(newRecord);
                }
            }
            return result;
        }

        private Edis.Db.ClientGroup CheckClientGroupMainClientIdByClientNumber(string ClientNumber) {
            var client = _db.Clients.Where(c => c.ClientNumber == ClientNumber).FirstOrDefault();
            var clientGroup = _db.ClientGroups.Where(ga => ga.MainClientId == client.ClientId).FirstOrDefault();
            return clientGroup;
        }

        private List<CorperateActionHistory> GetAllPendingActionForAccount(string accountNumber)
        {
            return _db.CorporateActions.Where(ca => ca.AssociatedAccountNumber == accountNumber && ca.Status == CorporateActionStatus.Pending).OrderBy(ca => ca.ActionType).ToList();      
        }

        private List<CorperateActionHistory> GetAllActionsForAccount(string accountNumber)
        {
            return _db.CorporateActions.Where(ca => ca.AssociatedAccountNumber == accountNumber).OrderBy(ca => ca.ActionType).ToList();
        }

        public List<ClientAccount> GetAccountsForClientByClientNumberSync(string ClientNumber, DateTime toDate)  
        {
            var client =
                    _db.Clients.Where(
                        c => c.ClientNumber == ClientNumber && c.CreatedOn.HasValue && c.CreatedOn.Value <= toDate)
                        .Include(c => c.Accounts)
                        .SingleOrDefault();
            if (client == null)
            {
                ProfileCannotBefound(ClientNumber, toDate, "Client");
            }
            var result = new List<ClientAccount>();
            //foreach (var account in client.Accounts.Where(acc => acc.AccountType == accountType))                                 //..........................................Account Type changed
            //{
            //    result.Add(GetClientAccountSync(account.AccountNumber, toDate));
            //}
            foreach (var account in client.Accounts)
            {
                result.Add(GetClientAccountSync(account.AccountNumber, toDate));
            }

            return result;
        }

        public List<CorperateActionParticipateAccountsModel> GetAllAdviserAccountAccordingToEquity(string Ticker, string AdviserId)
        {
            if(string.IsNullOrEmpty(Ticker)){
                return new List<CorperateActionParticipateAccountsModel>();
            }

            var equityId = getEquityByTicker(Ticker).AssetId;         
            //All groups that this adviser got
            var groups = GetAllClientGroupsForAdviserSync(AdviserId, DateTime.Now);
            //Acccounts wait to be checked
            List<Account> accounts = new List<Account>();
            foreach (var group in groups)
            {   
                var typeOfGroupAccount = GetAccountsForClientGroupSync(group.ClientGroupNumber, DateTime.Now);
                if (typeOfGroupAccount != null)//group account could be null
                {
                    //add all group accounts to accounts
                    typeOfGroupAccount.ForEach(g => accounts.AddRange(_db.Accounts.Where(a => a.AccountNumber == g.AccountNumber)));
                }
                //Then all clients within this group
                var clientAccountsWithinThisGroup = new List<ClientAccount>();
                group.GetClientsSync().ForEach(c => clientAccountsWithinThisGroup.AddRange(c.GetAccountsSync()));
                //add all clients' account to the accounts 
                clientAccountsWithinThisGroup.ForEach(c => accounts.AddRange(_db.Accounts.Where(a => a.AccountNumber == c.AccountNumber)));
            }
            return AccountsForCorperateActions(accounts, equityId, Ticker);
        }
       
        private List<CorperateActionParticipateAccountsModel> AccountsForCorperateActions(List<Account> accounts, string equityId, string Ticker) {

            var result = new List<CorperateActionParticipateAccountsModel>();
            if (accounts != null)
            {
                foreach (var account in accounts)
                {
                    var numberOfUnits = checkUnitOfSharesByEquityIdAndAssociatedAccount(equityId, account);
                    if (numberOfUnits > 0)
                    {
                        var newRecord = new CorperateActionParticipateAccountsModel()
                        {
                            AccountNumber = account.AccountNumber,
                            ShareAmount = numberOfUnits.ToString(),
                            Ticker = Ticker,
                            AccountName = account.AccountInfo,
                        };
                        result.Add(newRecord);
                    }
                }
            }
            return result;
        }

        private int checkUnitOfSharesByEquityIdAndAssociatedAccount(string equityId, Account account)
        {
            var allTrans = account.EquityTransactions.Where(et => et.EquityId == equityId).ToList();
            int numberOfUnits = 0;
            if (allTrans != null)
            {
                foreach (var transaction in allTrans)
                {
                    numberOfUnits += (int)transaction.NumberOfUnits;
                }
            }
            return numberOfUnits;
        }

        public void ClientAcceptCorporateAction(int ActionId) {
            var action = _db.CorporateActions.Where(ca => ca.Id == ActionId).FirstOrDefault();
            action.Status = CorporateActionStatus.Approved;
            //then do what this corporate Action needs to be done
            switch (action.ActionType){
                case CorporateActionType.ReinvestmentPlan:
                    MakeReinvestmentPlanAction(action);
                    break;
                case CorporateActionType.RightsIssue:
                    MakeAction(action);
                    break;
                case CorporateActionType.BuyBackProgram:
                    MakeAction(action);
                    break;
                default: break;
            }
        }

        private void MakeAction(CorperateActionHistory action) {
            var amount = Convert.ToDouble(action.CashAdjustmentAmount);
            MakeCashTransactions(action.AssociatedAccountNumber, amount);
            var account = _db.Accounts.Where(a => a.AccountNumber == action.AssociatedAccountNumber).FirstOrDefault();
            var equity = getEquityByTicker(action.Ticker);
            var numberOfUnits = Convert.ToInt32(action.StockAdjustmentShareAmount);
            ChangeStockShare(numberOfUnits, account, equity);
            _db.SaveChanges();
        }

        private void MakeReinvestmentPlanAction(CorperateActionHistory action) {
            //IncreaseNumberOfUnits
            var account = GetClientAccountSync(action.AssociatedAccountNumber,DateTime.Now);
            var equityId = getEquityByTicker(action.Ticker);
            var equity = getEquityById(equityId.AssetId);

            account.MakeTransactionSync(new EquityTransactionCreation() {

                EquityType = equity.EquityType,
                NumberOfUnits = Convert.ToInt32(action.StockAdjustmentShareAmount),
                Price = equity.LatestPrice,
                TransactionDate = DateTime.Now,
                Ticker = equity.Ticker,
                Sector = equity.Sector,
                Name = equity.Name,
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    //fee record needs to be implemented by deducting amount in cash account
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 100,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                }

            });
            _db.SaveChanges();
        }

        public void ClientRejectCorporateAction(int ActionId)
        {
            var action = _db.CorporateActions.Where(ca => ca.Id == ActionId).FirstOrDefault();
            action.Status = CorporateActionStatus.Rejected;
            //then do what this corporate Action needs to be done
            _db.SaveChanges();
        }

        private void ChangeStockShare(int NumberOfUnits, Account account, Equity equity) {
            var clientAccount = GetClientAccountSync(account.AccountNumber, DateTime.Now);
            if (clientAccount != null)
            {
                clientAccount.MakeTransactionSync(new EquityTransactionCreation()
                {
                    Name = equity.Name,
                    NumberOfUnits = NumberOfUnits,
                    Price = 0,
                    Sector = equity.Sector,
                    Ticker = equity.Ticker,
                    TransactionDate = DateTime.Now,
                    FeesRecords = new List<TransactionFeeRecordCreation>(),
                    //EquityType = EquityTypes.ManagedInvestments,
                    //FeesRecords = new List<TransactionFeeRecordCreation>() {
                    //     new TransactionFeeRecordCreation()
                    //    {
                    //        Amount = 100,
                    //        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    //    }
                    //},
                });
            }
            else {
                var groupAccount = GetClientGroupAccountSync(account.AccountNumber, DateTime.Now);
                if (groupAccount != null) {
                    groupAccount.MakeTransactionSync(new EquityTransactionCreation()
                    {
                        Name = equity.Name,
                        NumberOfUnits = NumberOfUnits,
                        Price = 0,
                        Sector = equity.Sector,
                        Ticker = equity.Ticker,
                        TransactionDate = DateTime.Now,
                        FeesRecords = new List<TransactionFeeRecordCreation>(),
                        //EquityType = EquityTypes.ManagedInvestments,
                        //FeesRecords = new List<TransactionFeeRecordCreation>() {
                        //     new TransactionFeeRecordCreation()
                        //    {
                        //        Amount = 100,
                        //        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                        //    }
                        //},
                    });
                }
            }
            _db.SaveChanges();
        }


        public List<ReturnModel> GetAllProperties() {
            return _db.Properties.ToList().OrderBy(p => p.City).Select(p => new ReturnModel {
                id = p.GooglePlaceId,
                value = p.StreetAddress + ", " + p.City + ", " + p.State + ", " + p.Country + ", " + p.Postcode 
            }).ToList();
        }

        public List<Domain.Portfolio.AggregateRoots.Asset.Equity> GetEquityTickersByType(EquityTypes type) {
            List<Domain.Portfolio.AggregateRoots.Asset.Equity> equities = new List<Domain.Portfolio.AggregateRoots.Asset.Equity>();

            var allEquities = _db.Equities.Where(e => e.EquityType == type).ToList();

            foreach (var equity in allEquities) {
                Domain.Portfolio.AggregateRoots.Asset.Equity subEquity = null;
                switch (equity.EquityType) {
                    case EquityTypes.AustralianEquity:
                        subEquity = new AustralianEquity(this);
                        break;
                    case EquityTypes.InternationalEquity:
                        subEquity = new InternationalEquity(this);
                        break;
                    case EquityTypes.ManagedInvestments:
                        subEquity = new ManagedInvestment(this);
                        break;
                }
                subEquity.Id = equity.AssetId;
                subEquity.Ticker = equity.Ticker;
                subEquity.Name = equity.Name;
                subEquity.Sector = equity.Sector;
                subEquity.EquityType = equity.EquityType;

                equities.Add(subEquity);
            }
            return equities;
        }

        public List<BondFeed> GetBondTickers() {
            return _db.Bonds.Select(b => new BondFeed { 
                Ticker = b.Ticker,
                CompanyName = b.Name
            }).OrderBy(b => b.Ticker).ToList();
        }

        public void FeedDataForEquities(EquityFeed equity) {
            _db.Equities.Add(new Equity {
                AssetId = Guid.NewGuid().ToString(),
                EquityType = (EquityTypes)Enum.Parse(typeof(EquityTypes), equity.EquityType),
                Sector = equity.Sector,
                Name = equity.CompanyName,
                Ticker = equity.Ticker
            });
            _db.SaveChanges();
                }

        public void FeedDataForBonds(BondFeed bond) {
            _db.Bonds.Add(new Bond { 
                BondId = Guid.NewGuid().ToString(),
                BondType = _db.BondTypes.FirstOrDefault(b => b.TypeName == bond.BondType).Id,
                Issuer = bond.Issuer,
                Name = bond.CompanyName,
                Frequency = (Frequency)Int32.Parse(bond.Frequency),
                Ticker = bond.Ticker
            });
            _db.SaveChanges();

            }

        public void FeedDataForAssetPrices(AssetPriceFeed assetPrice, AssetTypes type) { 
            switch(type){
                case AssetTypes.AustralianEquity:
                case AssetTypes.InternationalEquity:
                case AssetTypes.ManagedInvestments:
                    var equity = _db.Equities.FirstOrDefault(e => e.Ticker == assetPrice.Ticker);
                    equity.Prices.Add(new AssetPrice { 
                        Id = Guid.NewGuid().ToString(),
                        AssetType = type,
                        CreatedOn = assetPrice.TransactionDate,
                        Price = assetPrice.AssetPrice,
                        CorrespondingAssetKey = equity.AssetId
                    }); 
                    break;
                case AssetTypes.FixedIncomeInvestments:
                    var bond = _db.Bonds.FirstOrDefault(b => b.Ticker == assetPrice.Ticker);
                    bond.Prices.Add(new AssetPrice {
                        Id = Guid.NewGuid().ToString(),
                        AssetType = type,
                        CreatedOn = assetPrice.TransactionDate,
                        Price = assetPrice.AssetPrice,
                        CorrespondingAssetKey = bond.BondId
                    });
                    break;
                case AssetTypes.DirectAndListedProperty:
                    var property = _db.Properties.FirstOrDefault(p => p.GooglePlaceId == assetPrice.Address);
                    property.Prices.Add(new AssetPrice { 
                        Id = Guid.NewGuid().ToString(),
                        AssetType = type,
                        CreatedOn = assetPrice.TransactionDate,
                        Price = assetPrice.AssetPrice,
                        CorrespondingAssetKey = property.PropertyId
                    });
                    break;
            }
            _db.SaveChanges();
        }

        public void FeedDataForLoanValueRatios(LoanValueRatioFeed value) {
            var marginLender = _db.MarginLenders.FirstOrDefault(m => m.LenderId == value.Lender);

            marginLender.Ratios.Add(new LoanValueRatio { 
                Id = Guid.NewGuid().ToString(),
                Ticker = value.Ticker,
                AssetTypes = value.AssetType,
                CreatedOn = value.CreateOn,
                MaxRatio = value.Ratio,
                ActiveDate = value.CreateOn
            });

            _db.SaveChanges();
        }

        public void FeedDataForResearchValues(ResearchValueFeed value, AssetTypes type) {
            switch (type) {
                case AssetTypes.AustralianEquity:
                case AssetTypes.InternationalEquity:
                case AssetTypes.ManagedInvestments:
                    var equity = _db.Equities.FirstOrDefault(e => e.Ticker == value.Ticker);
                    equity.ResearchValues.Add(new ResearchValue { 
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = value.CreateDate,
                        Issuer = value.Issuer,
                        Key = value.Key,
                        StringValue = value.StringValue,
                        Value = value.Value
                    });
                    break;
                case AssetTypes.FixedIncomeInvestments:
                    var bond = _db.Bonds.FirstOrDefault(b => b.Ticker == value.Ticker);
                    bond.ResearchValues.Add(new ResearchValue {
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = value.CreateDate,
                        Issuer = value.Issuer,
                        Key = value.Key,
                        StringValue = value.StringValue,
                        Value = value.Value
                    });
                    break;
                case AssetTypes.DirectAndListedProperty:
                    var property = _db.Properties.FirstOrDefault(p => p.GooglePlaceId == value.Address);
                    property.ResearchValues.Add(new ResearchValue {
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = value.CreateDate,
                        Issuer = value.Issuer,
                        Key = value.Key,
                        StringValue = value.StringValue,
                        Value = value.Value
                    });
                    break;
            }
            _db.SaveChanges();
        }

        public void FeedDataForBondTypes(string typeName) {
            _db.BondTypes.Add(new BondType { 
                Id = Guid.NewGuid().ToString(),
                TypeName = typeName
            });
            _db.SaveChanges();
        }

        public void FeedDataForSectors(string sectorName) {
            _db.Sectors.Add(new Sector{
                Id = Guid.NewGuid().ToString(),
                SectorName = sectorName
            });
            _db.SaveChanges();
        }

        public void FeedDataForMarginLenders(string lenderName) {
            _db.MarginLenders.Add(new MarginLender {
                LenderId = Guid.NewGuid().ToString(),
                LenderName = lenderName
            });
            _db.SaveChanges();
        }

        public List<Bond> GetAllBonds() {
            return _db.Bonds.ToList();
        }

        public List<Property> GetAllPropertyForApi() {
            return _db.Properties.ToList();
        }

        private Account GetAccountByAccountId(string AccountId) {
            return _db.Accounts.Where(a => a.AccountId == AccountId).FirstOrDefault();
        }

        public void InsertCouponDividend(DevidendCreationModel model) {
            var coupon = new CouponPaymentCreation();
            coupon.AccountNumber = GetAccountByAccountId(model.Account.id).AccountNumber;
            coupon.Amount = Convert.ToDouble(model.Amount);
            coupon.PaymentOn = model.PaymentOn;
            coupon.Ticker = model.Ticker;
            RecordIncomeSync(coupon);
            MakeCashTransactions(coupon.AccountNumber, coupon.Amount);
        }

        public void InsertRentalDividend(DevidendCreationModel model) {
            var rental = new RentalPaymentCreation();
            rental.Amount = Convert.ToDouble(model.Amount);
            rental.PropertyId = _db.Properties.Where(p => p.GooglePlaceId == model.AddtionalInfo).FirstOrDefault().PropertyId;
            rental.AccountNumber = GetAccountByAccountId(model.Account.id).AccountNumber;
            rental.PaymentOn = model.PaymentOn;
            RecordIncomeSync(rental);
            MakeCashTransactions(rental.AccountNumber, rental.Amount);
        }

        public void InsertInterestDividend(DevidendCreationModel model) {
            var interest = new InterestPaymentCreation();

            var account = _db.Accounts.Where(a => a.AccountId == model.Account.id).FirstOrDefault();
            var cashAccount = new CashAccount();
            if (model.Account.accountCatagory == "ClientAccount") {
                cashAccount = GetCashAccountByClientAccount(account);
            }
            if (model.Account.accountCatagory == "GroupAccount") {
                cashAccount = GetCashAccountByGroupAccount(account);
            }
            interest.CashAccountId = cashAccount.Id;
            interest.Amount = Convert.ToDouble(model.Amount);
            interest.PaymentOn = model.PaymentOn;
            interest.AccountNumber = account.AccountNumber;
           
            RecordIncomeSync(interest);
            MakeCashTransactions(account.AccountNumber, interest.Amount);
        }

        public void InsertJustDividend(DevidendCreationModel model) {
            var dividend = new DividendPaymentCreation();
            dividend.AccountNumber = GetAccountByAccountId(model.Account.id).AccountNumber;
            dividend.Amount = Convert.ToDouble(model.Amount);
            dividend.PaymentOn = model.PaymentOn;
            dividend.Ticker = model.Ticker;
            RecordIncomeSync(dividend);
            MakeCashTransactions(dividend.AccountNumber, dividend.Amount);
        }

    }
}