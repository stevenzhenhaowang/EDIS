using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlRepository;
using Shared;
using Domain.Portfolio.Entities.CreationModels.Transaction;
using Domain.Portfolio.Entities.CreationModels.Cost;
using System.Collections.Generic;
using Domain.Portfolio.Services;

namespace testForTrans
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            EdisRepository repo = new EdisRepository();

            var account = repo.CreateNewClientGroupAccountSync("70b77f8c-9385-427d-adbf-59c224d6d9d8", "", AccountType.GenenralPurpose);
            var equity = repo.getEquityByTicker("PHAR");
            account.MakeTransactionSync(new EquityTransactionCreation() {
                FeesRecords = new List<TransactionFeeRecordCreation>(),
                Name = equity.Name,
                NumberOfUnits = 100,
                Price = 10,
                Sector = equity.Sector,
                Ticker = equity.Ticker,
                TransactionDate = DateTime.Now.AddDays(-10)
            });
            account.MakeTransactionSync(new EquityTransactionCreation()
            {
                FeesRecords = new List<TransactionFeeRecordCreation>(),
                Name = equity.Name,
                NumberOfUnits = -50,
                Price = 5,
                Sector = equity.Sector,
                Ticker = equity.Ticker,
                TransactionDate = DateTime.Now.AddDays(-9)
            });
           
            var asset = account.GetAssets()[0];
         
            var assets = account.GetAssets();
            var marketvalue = assets.GetTotalMarketValue();
            var cost = assets.GetTotalCost();
            Assert.IsTrue(marketvalue == 0);
            //Assert.IsTrue(cost.)
            Assert.IsTrue(cost.AssetCost == 1000);
            Assert.IsTrue(cost.Total == 1000);
            Assert.IsTrue(cost.Expense == 1000);
            repo.Dispose();

        }
    }
}
