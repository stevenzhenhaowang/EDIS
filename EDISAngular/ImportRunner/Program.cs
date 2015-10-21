using Domain.Portfolio.AggregateRoots.Asset;
using Edis.Db.Assets;
using Shared;
using SqlRepository;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using AssetPrice = Edis.Db.Assets.AssetPrice;

namespace ImportRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            ImportDataFromExcelStockPrices();
            //ImportDataFromExcelEquity();
        }
        static void ImportDataFromExcelStockPrices()
        {
            Console.WriteLine("Starting...");
            Excel.Application MyApp = new Excel.Application();
            Excel.Workbook MyBook = MyApp.Workbooks.Open("C:/Users/ahksysuser06/Desktop/EDISData/Australian Equity Daily Closing Price 20131211.xlsx");
            Excel.Worksheet MySheet = MyBook.Sheets[1];


            DateTime startDate = new DateTime(2003, 01, 01);
            DateTime endDate = new DateTime(2013, 12, 11);

            // lost 182,1247, 1922, 2015, 2043   processing 2044   ARR     AustralianEquity   
            for (int i = 2044; i <= MySheet.Rows.Count; i++)        //MySheet.Rows.Count
            {
                Console.WriteLine("Processing row " + i.ToString());
                using (Edis.Db.EdisContext db = new Edis.Db.EdisContext())
                {
                    //List<AssetPrice> assetPrices = new List<AssetPrice>();
                    int j = 8;
                    // Edis.Db.Assets.Equity equity = edisRepo.getEquityByTicker(MySheet.Cells[i, 4].Value.ToString(), EquityTypes.AustralianEquity);
                    string ticker = MySheet.Cells[i, 4].Value.ToString();
                    var equity = db.Equities.FirstOrDefault(e => e.Ticker == ticker && e.EquityType == EquityTypes.AustralianEquity);
                    for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                        var price = new AssetPrice
                        {
                            AssetType = AssetTypes.AustralianEquity,
                            CorrespondingAssetKey = equity.AssetId,
                            CreatedOn = date,
                            Price = MySheet.Cells[i, j].Value == null ? 0 : Convert.ToDouble(MySheet.Cells[i, j].Value.ToString()),
                        };
                        equity.Prices.Add(new AssetPrice
                        {
                            Id = Guid.NewGuid().ToString(),
                            AssetType = price.AssetType,
                            CreatedOn = price.CreatedOn,
                            CorrespondingAssetKey = price.CorrespondingAssetKey,
                            Price = price.Price
                        });
                        j++;
                    }
                    db.SaveChanges();

                    //edisRepo.InsertStockPricesData(assetPrices);
                }

            }

        }

        static void ImportDataFromExcelEquity()
        {
            using (EdisRepository edisRepo = new EdisRepository()) {
                Console.WriteLine("Program start......");
                //string excelFilePath = "C:/Users/ahksysuser06/Desktop/EDISData/Australian Equity Daily Closing Price 20131211.xlsx";
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
                    //    Console.WriteLine("inserting");
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
                        Console.WriteLine("inserting");
                        edisRepo.InsertEquityData(equity);
                    }
                    oleReader.Close();
                    oleConn.Close();
                    Console.WriteLine("Finished");
                }
                    
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            
        }
    }
}
