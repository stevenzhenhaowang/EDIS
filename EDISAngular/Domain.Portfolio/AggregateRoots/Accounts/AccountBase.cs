﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Portfolio.AggregateRoots.Asset;
using Domain.Portfolio.Base;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Entities.CreationModels;
using Domain.Portfolio.Entities.CreationModels.Cost;
using Domain.Portfolio.Entities.CreationModels.Income;
using Domain.Portfolio.Entities.CreationModels.Transaction;
using Domain.Portfolio.Entities.Transactions;
using Domain.Portfolio.Interfaces;
using Domain.Portfolio.Services;
using Domain.Portfolio.Values;
using Domain.Portfolio.Values.ManagedInvestmentValues;
using Shared;

namespace Domain.Portfolio.AggregateRoots.Accounts
{
    public abstract class AccountBase : AggregateRootBase
    {
        public string AccountNumber { get; set; }
        public AccountType AccountType { get; set; }

        public string AccountNameOrInfo { get; set; }

        public string MarginLenderId { get; set; }

        public List<AssetBase> GetAssets(DateTime? beforeDate = null)
        {
            return this._repository.GetAssetsForAccount(this.AccountNumber, beforeDate ?? DateTime.Now).Result;
        }

        public List<AssetBase> GetAssetsSync(DateTime? beforeDate = null)
        {
            return this._repository.GetAssetsForAccountSync(this.AccountNumber, beforeDate ?? DateTime.Now);
        }

        public List<AssetBase> GetEquities(DateTime? beforeDate = null) {
            return this._repository.GetEquityAssetsForAccount(this.AccountNumber, beforeDate ?? DateTime.Now);
        }

        public List<LiabilityBase> GetLiabilities(DateTime? beforeDate = null)
        {
            return this._repository.GetLiabilitiesForAccount(this.AccountNumber, beforeDate ?? DateTime.Now).Result;
        }

        public List<LiabilityBase> GetLiabilitiesSync(DateTime? beforeDate = null)
        {
            return this._repository.GetLiabilitiesForAccountSync(this.AccountNumber, beforeDate ?? DateTime.Now);
        } 
        /// <summary>
        /// All activities that do not influence portfolio positions.
        /// </summary>
        ///todo need to populate or support runtime retrieval
        public List<ConsultancyActivity> ConsultancyActivities { get; set; }
        public Cost GetTotalCost(DateTime? beforeDate = null)
        {
            return GetAssets(beforeDate).GetTotalCost();
        }
        /// <summary>
        ///     Return an aggregated total income value from all different types of incomes generated by this account
        /// </summary>
        /// <returns></returns>
        public double GetTotalIncomeValue(DateTime? beforeDate = null)
        {
            return GetAssets(beforeDate).GetTotalIncome();
        }
        public double GetTotalMarketValue(DateTime? beforeDate = null)
        {
            return GetAssets(beforeDate).GetTotalMarketValue();
        }
        public double GetTotalMarketValue_ByEquityType<TEquity>(DateTime? beforeDate = null) where TEquity : Equity
        {
            return GetAssets(beforeDate).GetTotalMarketValue_ByEquityType<TEquity>();
        }
        public double GetTotalMarketValue_ByAssetType<TAssetType>(DateTime? beforeDate = null) where TAssetType : AssetBase
        {
            return GetAssets(beforeDate).GetTotalMarketValue_ByAssetType<TAssetType>();
        }
        public double GetProfitAndLoss(DateTime? beforeDate = null)
        {
            return GetAssets(beforeDate).GetProfitAndLoss();
        }
        public async Task MakeTransaction(TransactionCreationBase transaction)
        {
            transaction.AccountNumber = this.AccountNumber;
            await this._repository.RecordTransaction(transaction);
        }
        public void MakeTransactionSync(TransactionCreationBase transaction)          //added
        {
            transaction.AccountNumber = this.AccountNumber;
            this._repository.RecordTransactionSync(transaction);
        }
        public async Task RecordConsultancyFee(ConsultancyFeeRecordCreation fee)
        {
            fee.AccountNumber = this.AccountNumber;
            await this._repository.RecordConsultancyFee(fee);
        }
        public async Task RecordIncome(IncomeCreationBase income)
        {
            income.AccountNumber = this.AccountNumber;
            await this._repository.RecordIncome(income);
        }


        public void RecordIncomeSync(IncomeCreationBase income)
        {
            income.AccountNumber = this.AccountNumber;
            this._repository.RecordIncomeSync(income);
        }

        public async Task RecordRepayment(RepaymentCreation record)
        {
            record.AccountNumber = this.AccountNumber;
            await this._repository.RecordRepayment(record);

        }


        public FundAllocation GetManagedFundAllocation(DateTime? beforeDate = null)
        {
            var managedFunds = GetAssets(beforeDate).OfType<ManagedInvestment>().ToList();
            var totalFund = managedFunds.Sum(f => f.GetTotalMarketValue());

            #region default when no managed fund

            if (totalFund <= 0)
            {
                return new FundAllocation
                {
                    Total = 0,
                    PropertyAllocation = new PropertyAllocation
                    {
                        PropertyDirect = 0
                    },
                    AlternativeAllocation = new AlternativeAllocation
                    {
                        HedgeFund = 0,
                        OtherFund = 0
                    },
                    FixedIncomeAllocation = new FixedIncomeAllocation
                    {
                        AustraliaFixedIncome = 0,
                        GlobalFixedIncome = 0,
                        HighYieldFixedIncome = 0
                    },
                    EquityAllocation = new EquityAllocation
                    {
                        Asia = 0,
                        AustraliaEquity = 0,
                        EmergingMarketsEquity = 0,
                        EuropeEquity = 0,
                        GlobalEquity = 0,
                        GlobalEquityLargeCap = 0,
                        GlobalEquityMidSmallCap = 0,
                        OtherSectorEquity = 0,
                        RealEstateSectorEquity = 0,
                        TechnologySectorEquity = 0,
                        UsEquityLargeCapBlend = 0
                    }
                };
            }

            #endregion

            return new FundAllocation
            {
                FixedIncomeAllocation = new FixedIncomeAllocation
                {
                    GlobalFixedIncome =
                        managedFunds.Sum(m => m.FundAllocation.FixedIncomeAllocation.GlobalFixedIncome) / totalFund,
                    HighYieldFixedIncome =
                        managedFunds.Sum(m => m.FundAllocation.FixedIncomeAllocation.HighYieldFixedIncome) / totalFund,
                    AustraliaFixedIncome =
                        managedFunds.Sum(m => m.FundAllocation.FixedIncomeAllocation.AustraliaFixedIncome) / totalFund
                },
                PropertyAllocation = new PropertyAllocation
                {
                    PropertyDirect = managedFunds.Sum(m => m.FundAllocation.PropertyAllocation.PropertyDirect) / totalFund
                },
                Total = totalFund,
                AlternativeAllocation = new AlternativeAllocation
                {
                    OtherFund = managedFunds.Sum(m => m.FundAllocation.AlternativeAllocation.OtherFund) / totalFund,
                    HedgeFund = managedFunds.Sum(m => m.FundAllocation.AlternativeAllocation.HedgeFund) / totalFund
                },
                EquityAllocation = new EquityAllocation
                {
                    OtherSectorEquity =
                        managedFunds.Sum(m => m.FundAllocation.EquityAllocation.OtherSectorEquity) / totalFund,
                    RealEstateSectorEquity =
                        managedFunds.Sum(m => m.FundAllocation.EquityAllocation.RealEstateSectorEquity) / totalFund,
                    GlobalEquityLargeCap =
                        managedFunds.Sum(m => m.FundAllocation.EquityAllocation.GlobalEquityLargeCap) / totalFund,
                    TechnologySectorEquity =
                        managedFunds.Sum(m => m.FundAllocation.EquityAllocation.TechnologySectorEquity) / totalFund,
                    AustraliaEquity = managedFunds.Sum(m => m.FundAllocation.EquityAllocation.AustraliaEquity) / totalFund,
                    GlobalEquityMidSmallCap =
                        managedFunds.Sum(m => m.FundAllocation.EquityAllocation.GlobalEquityMidSmallCap) / totalFund,
                    EuropeEquity = managedFunds.Sum(m => m.FundAllocation.EquityAllocation.EuropeEquity) / totalFund,
                    GlobalEquity = managedFunds.Sum(m => m.FundAllocation.EquityAllocation.GlobalEquity) / totalFund,
                    EmergingMarketsEquity =
                        managedFunds.Sum(m => m.FundAllocation.EquityAllocation.EmergingMarketsEquity) / totalFund,
                    UsEquityLargeCapBlend =
                        managedFunds.Sum(m => m.FundAllocation.EquityAllocation.UsEquityLargeCapBlend) / totalFund,
                    Asia = managedFunds.Sum(m => m.FundAllocation.EquityAllocation.Asia) / totalFund
                },
                SuitabilityAllocation = new SuitabilityAllocation
                {
                    AggressiveAllocation =
                        managedFunds.Sum(m => m.FundAllocation.SuitabilityAllocation.AggressiveAllocation) / totalFund,
                    BalancedAllocation =
                        managedFunds.Sum(m => m.FundAllocation.SuitabilityAllocation.BalancedAllocation) / totalFund,
                    ConservativeAllocation =
                        managedFunds.Sum(m => m.FundAllocation.SuitabilityAllocation.ConservativeAllocation) / totalFund,
                    ModerateAllocation =
                        managedFunds.Sum(m => m.FundAllocation.SuitabilityAllocation.ModerateAllocation) / totalFund
                }
            };
        }
        protected AccountBase(IRepository repo) : base(repo)
        {
        }
    }
}