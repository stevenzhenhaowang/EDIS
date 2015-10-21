using Domain.Portfolio.AggregateRoots.Asset;
using Edis.Db.Assets;
using Microsoft.Office.Interop.Excel;
using Shared;
using SqlRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using AssetPrice = Edis.Db.Assets.AssetPrice;

namespace EDISAngular.APIControllers
{
    public class ImportExcelData
    {

        EdisRepository edisRepo = new EdisRepository();

        public void ImportDataFromExcelEquity()
        {

            string excelFilePath = "C:/Users/ahksysuser06/Desktop/EDISData/International_Equity_Daily_Closing_Price_20131211.xlsx";
            // make sure your sheet name is correct, here sheet name is sheet1, so you can change the sheet name if have    different 
            string excelQuery = "select SecId,Name,Symbol from [Sheet1$]";
            try
            {
                string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath +
                ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
                OleDbConnection oleConn = new OleDbConnection(excelConnectionString);
                oleConn.Open();
                OleDbDataReader oleReader = new OleDbCommand(excelQuery, oleConn).ExecuteReader();
                //while (oleReader.Read())
                //{
                //    Domain.Portfolio.AggregateRoots.Asset.Equity equity = new AustralianEquity(edisRepo)
                //    {
                //        Name = oleReader.GetString(1),
                //        Sector = oleReader.GetString(0),
                //        Ticker = oleReader.GetString(2),
                //        EquityType = EquityTypes.AustralianEquity,
                //    };

                //    edisRepo.InsertEquityData(equity);
                //}
                while (oleReader.Read())
                {
                    Domain.Portfolio.AggregateRoots.Asset.Equity equity = new InternationalEquity(edisRepo)
                    {
                        Name = oleReader.GetString(1),
                        Sector = oleReader.GetString(0),
                        Ticker = oleReader.GetString(2),
                        EquityType = EquityTypes.InternationalEquity,
                    };

                    edisRepo.InsertEquityData(equity);
                }
                oleReader.Close();
                oleConn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void ImportDataFromExcelStockPrices()
        {
            Excel.Application MyApp = new Excel.Application();
            Excel.Workbook MyBook = MyApp.Workbooks.Open("C:/Users/ahksysuser06/Desktop/EDISData/Australian Equity Daily Closing Price 20131211.xlsx");
            Excel.Worksheet MySheet = MyBook.Sheets[1];


            DateTime startDate = new DateTime(2003, 01, 01);
            DateTime endDate = new DateTime(2013, 12, 11);

            //120   ARR     AustralianEquity
            for (int i = 120; i <= MySheet.Rows.Count; i++)        //MySheet.Rows.Count
            {

                using (Edis.Db.EdisContext db = new Edis.Db.EdisContext())
                {
                    //List<AssetPrice> assetPrices = new List<AssetPrice>();
                    int j = 8;
                    // Edis.Db.Assets.Equity equity = edisRepo.getEquityByTicker(MySheet.Cells[i, 4].Value.ToString(), EquityTypes.AustralianEquity);
                    string ticker = MySheet.Cells[i, 4].Value.ToString();
                    var equity = db.Equities.SingleOrDefault(e => e.Ticker == ticker && e.EquityType == EquityTypes.AustralianEquity);
                    for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                       var price =new AssetPrice
                        {
                            AssetType = AssetTypes.AustralianEquity,
                            CorrespondingAssetKey = equity.AssetId,
                            CreatedOn = date,
                            Price = MySheet.Cells[i, j].Value == null ? -1 : Convert.ToDouble(MySheet.Cells[i, j++].Value.ToString()),
                        };
                        equity.Prices.Add(new AssetPrice
                        {
                            Id = Guid.NewGuid().ToString(),
                            AssetType = price.AssetType,
                            CreatedOn = price.CreatedOn,
                            CorrespondingAssetKey = price.CorrespondingAssetKey,
                            Price = price.Price
                        });
                    }
                    db.SaveChanges();

                    //edisRepo.InsertStockPricesData(assetPrices);
                }

            }





            //string excelFilePath = "C:/Users/ahksysuser06/Desktop/EDISData/Australian Equity Daily Closing Price 20131211.xlsx";
            //// make sure your sheet name is correct, here sheet name is sheet1, so you can change the sheet name if have    different 

            //List<DateTime> allDates = new List<DateTime>();
            //string allDateString = "";


            ////for (DateTime date = startDate; date <= endDate; date = date.AddDays(1)) {
            ////    allDateString += date.ToString("dd-MM-yyyy") + " ,";
            ////}

            ////allDateString = allDateString.Substring(0, allDateString.Length - 1);


            ////string excelQuery = "select Symbol, " + allDateString + " from [Sheet1$]";

            //string excelQuery = "select * from [Sheet1$]";
            //try
            //{
            //    string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath +
            //    ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
            //    OleDbConnection oleConn = new OleDbConnection(excelConnectionString);
            //    oleConn.Open();
            //    OleDbDataReader oleReader = new OleDbCommand(excelQuery, oleConn).ExecuteReader();

            //    List<AssetPrice> assetPrices = new List<AssetPrice>();

            //    while (oleReader.Read())
            //    {
            //        Edis.Db.Assets.Equity equity = edisRepo.getEquityByTicker(oleReader.GetString(3));

            //        int i = 7;

            //        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            //        {
            //            assetPrices.Add(new AssetPrice
            //            {
            //                AssetType = AssetTypes.AustralianEquity,
            //                CorrespondingAssetKey = equity.AssetId,
            //                CreatedOn = date,
            //                Price = (oleReader[i] == null || oleReader[i].ToString() == "") ? null : (double?)oleReader[i],
            //            });

            //            System.Diagnostics.Debug.WriteLine(AssetTypes.AustralianEquity + ", " + equity.AssetId + ", " + date.ToString("dd-MM-yyyy") + ", " + oleReader[1] + ", " + (oleReader[i] == null || oleReader[i].ToString() == "" ? null : (double?)oleReader[i]));

            //            i++;
            //        }

            //        System.Diagnostics.Debug.WriteLine(AssetTypes.AustralianEquity + ", " + equity.AssetId + ", " + oleReader[1] + ", " + (oleReader[1000] == null || oleReader[1000].ToString() == "" ? null : (double?)oleReader[1000]));

            //        //edisRepo.InsertStockPricesData();

            //    }
            //    oleReader.Close();
            //    oleConn.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }

        public void ImportDataFromExcelStockPricesIE()
        {
            Excel.Application MyApp = new Excel.Application();
            Excel.Workbook MyBook = MyApp.Workbooks.Open("C:/Users/ahksysuser06/Desktop/EDISData/International_Equity_Daily_Closing_Price_20131211.xlsx");
            Excel.Worksheet MySheet = MyBook.Sheets[1];


            DateTime startDate = new DateTime(2003, 01, 01);
            DateTime endDate = new DateTime(2013, 12, 11);

            //120   ARR     AustralianEquity
            for (int i = 2; i < MySheet.Rows.Count; i++)
            {
                List<AssetPrice> assetPrices = new List<AssetPrice>();
                int j = 8;
                Edis.Db.Assets.Equity equity = edisRepo.getEquityByTicker(MySheet.Cells[i, 4].Value.ToString(), EquityTypes.InternationalEquity);

                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    assetPrices.Add(new AssetPrice
                    {
                        AssetType = AssetTypes.InternationalEquity,
                        CorrespondingAssetKey = equity.AssetId,
                        CreatedOn = date,
                        Price = MySheet.Cells[i, j].Value == null ? -1 : Convert.ToDouble(MySheet.Cells[i, j].Value.ToString()),
                    });

                    j++;
                }
                edisRepo.InsertStockPricesData(assetPrices);
            }
        }
        //public void ImportDataFromExcel(string excelFilePath)
        //{
        //    // make sure your sheet name is correct, here sheet name is sheet1, so you can change your sheet name if have    different 
        //    string myexceldataquery = "select SecId,Name,Symbol from [Sheet1$]";
        //    try
        //    {
        //        //create our connection strings 
        //        string sexcelconnectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath +
        //        ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
        //        //string ssqlconnectionstring = "Data Source=AHKSYS06;Integrated Security=True;Initial Catalog=excelData;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        //        //execute a query to erase any previous data from our destination table 
        //        //string sclearsql = "delete from " + ssqltable;
        //        //SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
        //        //SqlCommand sqlcmd = new SqlCommand(sclearsql, sqlconn);
        //        //sqlconn.Open();
        //        //sqlcmd.ExecuteNonQuery();
        //        //sqlconn.Close();
        //        //series of commands to bulk copy data from the excel file into our sql table 
        //        OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionstring);
        //        oledbconn.Open();
        //        OleDbDataReader dr = new OleDbCommand(myexceldataquery, oledbconn).ExecuteReader();
        //        //SqlBulkCopy bulkcopy = new SqlBulkCopy(ssqlconnectionstring);
        //        //bulkcopy.DestinationTableName = table;
        //        while (dr.Read())
        //        {
        //            Equity equity = new InternationalEquity(edisRepo)
        //            {
        //                Name = dr.GetString(1),
        //                Sector = dr.GetString(0),
        //                Ticker = dr.GetString(2),
        //                EquityType = EquityTypes.InternationalEquity,
        //            };

        //            edisRepo.InsertEquityData(equity);
        //            //bulkcopy.WriteToServer(dr);
        //        }
        //        dr.Close();
        //        oledbconn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
}