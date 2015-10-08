(function () {    
    app.factory("clientTransactionsDBService", function (AppStrings, $resource) {
        var DBContext = {
            GetBuyAnalysis: function () {
                var result = [{ Date: "17/Nov/2010", ContractNo: "12346495", Units: "182", Price_Dollars: "15.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", NetValue_exBrokerage_GST: "490.56", AvailableUnits: "32", SoldUnits: "150", RecommendationComment: "BUY" },
                { Date: "23/May/2011", ContractNo: "12350131", Units: "50", Price_Dollars: "15.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", NetValue_exBrokerage_GST: "490.56", AvailableUnits: "32", SoldUnits: "150", RecommendationComment: "BUY" },
                { Date: "24/Jul/2012", ContractNo: "12353766", Units: "100", Price_Dollars: "14.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", NetValue_exBrokerage_GST: "490.56", AvailableUnits: "32", SoldUnits: "150", RecommendationComment: "BUY" },
                { Date: "2/Dec/2013", ContractNo: "12358795", Units: "200", Price_Dollars: "17.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", NetValue_exBrokerage_GST: "490.56", AvailableUnits: "32", SoldUnits: "150", RecommendationComment: "BUY" }
                ]

                return result;
            },
            GetSellAnalysis: function () {
                var result = [{ Date: "17/Nov/2010", ContractNo: "12346495", Units: "182", Price_Dollars: "15.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", NetValue_exBrokerage_GST: "490.56", AvailableUnits: "32", SoldUnits: "150", RecommendationComment: "BUY" },
                { Date: "23/May/2011", ContractNo: "12350131", Units: "50", Price_Dollars: "15.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", NetValue_exBrokerage_GST: "490.56", AvailableUnits: "32", SoldUnits: "150", RecommendationComment: "BUY" },
                { Date: "24/Jul/2012", ContractNo: "12353766", Units: "100", Price_Dollars: "14.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", NetValue_exBrokerage_GST: "490.56", AvailableUnits: "32", SoldUnits: "150", RecommendationComment: "BUY" },
                { Date: "2/Dec/2013", ContractNo: "12358795", Units: "200", Price_Dollars: "17.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", NetValue_exBrokerage_GST: "490.56", AvailableUnits: "32", SoldUnits: "150", RecommendationComment: "BUY" }
                ]

                return result;
            },
            GetDistributionInfo: function () {
                var result = [{ Date: "17/Nov/2010", Type: "12346495", Amount: "182", Franking: "15.33", NumOfUnits: "100", Total_Dist: "30", Total_Franking_Credit: "600.56", Ex_Dates: "30/06/2012", BookDate: "30/06/2012", PaymentDate: "30/06/2012", Total_Dist_Income: "600.56" },
                { Date: "23/May/2011", Type: "12346495", Amount: "182", Franking: "15.33", NumOfUnits: "100", Total_Dist: "30", Total_Franking_Credit: "600.56", Ex_Dates: "30/06/2012", BookDate: "30/06/2012", PaymentDate: "30/06/2012", Total_Dist_Income: "600.56" },
                { Date: "24/Jul/2012", Type: "12346495", Amount: "182", Franking: "15.33", NumOfUnits: "100", Total_Dist: "30", Total_Franking_Credit: "600.56", Ex_Dates: "30/06/2012", BookDate: "30/06/2012", PaymentDate: "30/06/2012", Total_Dist_Income: "600.56" },
                { Date: "2/Dec/2013", Type: "12346495", Amount: "182", Franking: "15.33", NumOfUnits: "100", Total_Dist: "30", Total_Franking_Credit: "600.56", Ex_Dates: "30/06/2012", BookDate: "30/06/2012", PaymentDate: "30/06/2012", Total_Dist_Income: "600.56" }
                ]

                return result;
            },
            GetTransactionHistory: function () {
                var result = [{ Date: "17/Nov/2010", ContractNo: "12346495", Units: "182", Price_Dollars: "15.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", BuySell: "B", RecommendationComment: "BUY" },
                    { Date: "17/Nov/2010", ContractNo: "12346495", Units: "182", Price_Dollars: "15.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", BuySell: "B", RecommendationComment: "SELL" },
                    { Date: "123/May/2011", ContractNo: "12346495", Units: "182", Price_Dollars: "15.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", BuySell: "B", RecommendationComment: "BUY" },
                    { Date: "23/May/2011", ContractNo: "12346495", Units: "182", Price_Dollars: "15.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", BuySell: "B", RecommendationComment: "SELL" },
                    { Date: "24/Jul/2012", ContractNo: "12346495", Units: "182", Price_Dollars: "15.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", BuySell: "B", RecommendationComment: "BUY" },
                    { Date: "24/Jul/2012", ContractNo: "12346495", Units: "182", Price_Dollars: "15.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", BuySell: "B", RecommendationComment: "SELL" },
                    { Date: "17/Nov/2010", ContractNo: "12346495", Units: "182", Price_Dollars: "15.33", Brokerage_exGST: "100", GST_Value: "30", NetValue_incBrokerage_GST: "600.56", BuySell: "B", RecommendationComment: "BUY" }
                ]

                return result;
            },
            GetManagementExpenses: function () {
                var result = [{ Date: "17/Nov/2010", Description: "Setup Fee", Frequency: "Annually", Amount: "1000", DateOfPayment: "30/06/2013" },
                { Date: "17/Nov/2010", Description: "Management Fee", Frequency: "Annually", Amount: "1000", DateOfPayment: "30/06/2013" },
                { Date: "17/Nov/2010", Description: "Consulting Fee", Frequency: "Annually", Amount: "1000", DateOfPayment: "30/06/2013" },
                { Date: "17/Nov/2010", Description: "Ongoing Fee", Frequency: "Annually", Amount: "1000", DateOfPayment: "30/06/2013" }
                ]
                return result;
            },
            GetNotesOnAsset: function () {
                var result = [{ NoteSerialNum: "3216548", Date: "17/Nov/2010", Time: "12:01:05", TimeSpent: "3 Hours", SubjectHeading: "Update Transaction", UserID: "32158", UserName: "John Smith", UserTitle: "Mr" },
                    { NoteSerialNum: "3216548", Date: "17/Nov/2010", Time: "12:01:05", TimeSpent: "3 Hours", SubjectHeading: "Execute Transaction", UserID: "32158", UserName: "John Smith", UserTitle: "Mr" },
                    { NoteSerialNum: "3216548", Date: "17/Nov/2010", Time: "12:01:05", TimeSpent: "3 Hours", SubjectHeading: "Calculate Transaction", UserID: "32158", UserName: "John Smith", UserTitle: "Mr" },
                    { NoteSerialNum: "3216548", Date: "17/Nov/2010", Time: "12:01:05", TimeSpent: "3 Hours", SubjectHeading: "Update Transaction", UserID: "32158", UserName: "John Smith", UserTitle: "Mr" }
                ]

                return result;
            },
            GetAssetTransactionInfo: function () {
                var result = { TotalUnits: "1000", TotalUnitsTransacted: "1000", TotalCostValue_incBrokerage: "10000", TotalCostValue_exBrokerage: "9000", TotalBrokerage_Buy: "500", TotalBrokerage_Sell: "500", CostPerUnit_incBrokerage: "100", CostPerUnit_exBrokerage: "90", RealisedProfit_Loss: "28500", HoldingValue: "9000", UnrealisedProfit_Loss: "28500", }
                return result;
            },
            GetClientTransactionAccount: function () {
                var result = { SRN_HIN: "H-1234587", DataFeed: "Manual", OngoingFee: "1000", MinBrokerage: "50" }
                return result;
            },
            GetCompanyData: function () {
                var result = { CompanyName: "Australia & NZ Bank", Ticker: "ANZ", MarketPrice: "5.30", Currency: "AUD" }
                return result;
            }

        }

        return DBContext;
    });
})();