using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.AggregateRoots.Accounts;
using Domain.Portfolio.DataFeed;
using EDISAngular.Models.ServiceModels;
using EDISAngular.Models.ServiceModels.DataFeedModels;
using EDISAngular.Models.ViewModels;
using Shared;
using SqlRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Http;
using Excel = Microsoft.Office.Interop.Excel;

namespace EDISAngular.APIControllers {
    public class DataFeedController : ApiController {

        private EdisRepository edisRepo = new EdisRepository();

        [HttpGet, Route("api/Admin/DataFeed/GetSetctorDetails")]
        public List<ListView> GetSetctorDetails() {
            List<ListView> views = new List<ListView>();
            edisRepo.GetAllSectorsSync().ForEach(s => views.Add(new ListView { name = s }));
            return views;
        }

        [HttpGet, Route("api/Admin/DataFeed/GetEquityTypesDetails")]
        public List<ListView> GetEquityTypesDetails() {
            List<ListView> views = new List<ListView>();
            var types = Enum.GetValues(typeof(EquityTypes)).Cast<EquityTypes>();

            foreach(var type in types){
                views.Add(new ListView { 
                    id = ((int)type).ToString(),
                    name = edisRepo.GetEnumDescription(type)
                });
            }
            return views;
        }

        [HttpPost, Route("api/Admin/DataFeed/InsertEquityBasicDetails")]
        public IHttpActionResult InsertEquityBasicDetails(EquityBasicModel model) {
            EquityFeed equity = new EquityFeed { 
                CompanyName = model.CompanyName,
                EquityType = model.EquityType,
                Sector = model.Sector,
                Ticker = model.Ticker
            };
            edisRepo.FeedDataForEquities(equity);
            return Ok();
        }

        [HttpGet, Route("api/Admin/DataFeed/GetMarginLenderDetails")]
        public List<ClientView> getAllMarginLenders() {

            var lenders = edisRepo.GetAllMarginLenders();
            List<ClientView> lenderView = new List<ClientView>();
            foreach (var lender in lenders) {
                lenderView.Add(new ClientView {
                    id = lender.LenderId,
                    name = lender.LenderName
                });
            }
            return lenderView;
        }

        [HttpGet, Route("api/Admin/DataFeed/GetBondTypes")]
        public List<ListView> GetBondTypes() {
            List<ListView> views = new List<ListView>();
            edisRepo.GetAllBondTypesSync().ForEach(b => views.Add(new ListView { name = b }));
            return views;
        }

        [HttpGet, Route("api/Admin/DataFeed/GetFrequenciesDetails")]
        public List<ListView> GetFrequenciesDetails() {
            List<ListView> views = new List<ListView>();

            var frequencies = Enum.GetValues(typeof(Frequency)).Cast<Frequency>();

            foreach (var frequency in frequencies) {
                views.Add(new ListView {
                    id = ((int)frequency).ToString(),
                    name = edisRepo.GetEnumDescription(frequency)
                });
            }
            return views;
        }

        [HttpPost, Route("api/Admin/DataFeed/InsertBondBasicDetails")]
        public IHttpActionResult InsertBondBasicDetails(BondBasicModel model) {
            BondFeed bond = new BondFeed { 
                BondType = model.BondType,
                CompanyName = model.CompanyName,
                Frequency = model.Frequency,
                Issuer = model.Issuer,
                Ticker = model.Ticker
            };
            edisRepo.FeedDataForBonds(bond);
            return Ok();
        }

        [HttpGet, Route("api/Admin/DataFeed/GetPropertyAddresses")]
        public List<ListView> GetPropertyAddresses() {
            return edisRepo.GetAllProperties().Select(p => new ListView { 
                id = p.id,
                name = p.value
            }).ToList();
        }

        [HttpGet, Route("api/Admin/DataFeed/GetEquityTickers")]
        public List<ListView> GetEquityTickers() {
            return edisRepo.GetAllEquities().Select(e => new ListView { 
                id = e.Ticker,
                name = e.Name
            }).OrderBy(l => l.id).ToList();
        }

        [HttpGet, Route("api/Admin/DataFeed/GetEquityTickersByType")]
        public List<ListView> GetEquityTickers(string assetType) {
            if(assetType == ((int)AssetTypes.FixedIncomeInvestments).ToString()){
                return edisRepo.GetBondTickers().Select(b => new ListView { 
                    id = b.Ticker,
                    name = b.CompanyName
                }).ToList();
            }
            EquityTypes type = (EquityTypes)(Int32.Parse(assetType));

            return edisRepo.GetEquityTickersByType(type).Select(e => new ListView { 
                id = e.Ticker,
                name = e.Name
            }).OrderBy(l => l.id).ToList();


        }

        [HttpGet, Route("api/Admin/DataFeed/GetAssetPriceTypes")]
        public List<ListView> GetAssetPriceTypes() {
            List<ListView> views = new List<ListView>();
            var types = Enum.GetValues(typeof(AssetPriceTypes)).Cast<AssetPriceTypes>();
            foreach (var type in types) {
                views.Add(new ListView {
                    id = ((int)type).ToString(),
                    name = edisRepo.GetEnumDescription(type)
                });
            }
            return views;
        }

        [HttpGet, Route("api/Admin/DataFeed/GetUploadDataTypes")]
        public List<ListView> GetUploadDataTypes() {
            List<ListView> views = new List<ListView>();
            var types = Enum.GetValues(typeof(UploadDataTypes)).Cast<UploadDataTypes>();
            foreach (var type in types) {
                views.Add(new ListView {
                    id = ((int)type).ToString(),
                    name = edisRepo.GetEnumDescription(type)
                });
            }
            return views;
        }

        [HttpPost, Route("api/Admin/DataFeed/InsertAssetPriceDetails")]
        public IHttpActionResult InsertAssetPriceDetails(AssetPriceModel model) {
            AssetPriceFeed assetPrice = new AssetPriceFeed { 
                Address = model.Address,
                Ticker = model.Ticker,
                AssetPrice = model.AssetPrice,
                TransactionDate = model.TransactionDate,
                AssetType = model.AssetType
            };
            edisRepo.FeedDataForAssetPrices(assetPrice, (AssetTypes)Int32.Parse(model.AssetType));
            return Ok();
        }

        [HttpGet, Route("api/Admin/DataFeed/GetResearchValueKeys")]
        public List<ListView> GetResearchValueKeys() {
            List<ListView> views = new List<ListView>();
            
            ResearchValueKeys keys = new ResearchValueKeys();
            foreach(var a in keys.GetType().GetFields()){
                views.Add(new ListView { 
                    id = a.Name,
                    name = GetConstFieldAttributeValue<ResearchValueKeys, string, DescriptionAttribute>(a.GetValue(keys).ToString(), y => y.Description)
                });
            }
            return views.OrderBy(v => v.name).ToList();
        }


        [HttpPost, Route("api/Admin/DataFeed/InsertResearchValue")]
        public IHttpActionResult InsertResearchValue(ResearchValueModel model) {
            ResearchValueFeed researchValue = new ResearchValueFeed {
                Address = model.Address,
                AssetType = model.AssetType,
                CompanyName = model.CompanyName,
                CreateDate = model.CreateDate,
                Issuer = model.Issuer,
                Key = model.Key,
                StringValue = model.StringValue,
                Ticker = model.Ticker,
                Value = model.Value,
                ValueType = model.ValueType
            };
            edisRepo.FeedDataForResearchValues(researchValue, (AssetTypes)Int32.Parse(model.AssetType));
            return Ok();
        }

        [HttpPost, Route("api/Admin/DataFeed/InsertBondType")]
        public IHttpActionResult InsertBondType(OneValueModel model) {
            edisRepo.FeedDataForBondTypes(model.Value);
            return Ok();
        }

        [HttpPost, Route("api/Admin/DataFeed/InsertSector")]
        public IHttpActionResult InsertSector(OneValueModel model) {
            edisRepo.FeedDataForSectors(model.Value);
            return Ok();
        }

        [HttpPost, Route("api/Admin/DataFeed/InsertMarginLender")]
        public IHttpActionResult InsertMarinLender(OneValueModel model) {
            edisRepo.FeedDataForMarginLenders(model.Value);
            return Ok();
        }

        [HttpPost, Route("api/Admin/DataFeed/InsertLoanValueRatio")]
        public IHttpActionResult InsertLoanValueRatio(LoanValueRatioModel model) {
            AssetTypes type = (AssetTypes)Int32.Parse(model.AssetType);

            edisRepo.FeedDataForLoanValueRatios(new LoanValueRatioFeed {
                AssetType = type,
                CreateOn = model.CreateOn,
                Lender = model.Lender,
                Ratio = model.Ratio,
                Ticker = model.Ticker
            });

            return Ok();
        }

        [HttpPost, Route("api/Admin/DataFeed/UploadDataFile")]
        public IHttpActionResult UploadDataFile(string dataType) {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count == 1) {
                foreach (string file in httpRequest.Files) {


                    var postedFile = httpRequest.Files[file];
                    UploadResourceFile(postedFile, dataType);
                }
                return Ok();
            } else {
                return BadRequest();
            }
        }

        public void UploadResourceFile(HttpPostedFile postedFile, string dataType) {
            var fileName = BusinessLayerParameters.UserDocumentFolderPrefix + postedFile.FileName;
            var filePath = HttpContext.Current.Server.MapPath(fileName);
            postedFile.SaveAs(filePath);

            string excelFilePath = filePath;
            string excelQuery = "select * from [Sheet1$]";

            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath +
                ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
            OleDbConnection oleConn = new OleDbConnection(excelConnectionString);
            oleConn.Open();
            OleDbDataReader oleReader = new OleDbCommand(excelQuery, oleConn).ExecuteReader();

            try {

                UploadDataTypes type = (UploadDataTypes)Int32.Parse(dataType);
                switch (type) {
                    case UploadDataTypes.AssetPrice:
                        FeedExcelDataForAssetPrice(oleReader);
                        break;
                    case UploadDataTypes.BondInfo:
                        FeedExcelDataForBonds(oleReader);
                        break;
                    case UploadDataTypes.EquityInfo:
                        FeedExcelDataForEquities(oleReader);
                        break;
                    case UploadDataTypes.ResearchValue:
                        FeedExcelDataForResearchValue(oleReader);
                        break;
                }
            }catch(Exception e){
                throw e;
            } finally {
                oleReader.Close();
                oleConn.Close();
                File.Delete(HttpContext.Current.Server.MapPath(fileName));
            }
        }


        public void FeedExcelDataForEquities(OleDbDataReader oleReader) {
            
            using (Edis.Db.EdisContext db = new Edis.Db.EdisContext()) {
                while (oleReader.Read()) {
                    db.Equities.Add(new Edis.Db.Assets.Equity {
                        AssetId = Guid.NewGuid().ToString(),
                        Ticker = oleReader.GetString(0),
                        Name = oleReader.GetString(1),
                        Sector = oleReader.GetString(2),
                        EquityType = GetValueFromDescription<EquityTypes>(oleReader.GetString(3))
                    });
                }
                db.SaveChanges();
            }
        }

        public void FeedExcelDataForBonds(OleDbDataReader oleReader) {

            using (Edis.Db.EdisContext db = new Edis.Db.EdisContext()) {
                while (oleReader.Read()) {
                    string bondTypeName = oleReader.GetString(3);

                    Edis.Db.BondType bondType = db.BondTypes.FirstOrDefault(t => t.TypeName == bondTypeName);

                    if (bondType == null) {
                        bondType = new Edis.Db.BondType {
                            Id = Guid.NewGuid().ToString(),
                            TypeName = bondTypeName
                        };
                        db.BondTypes.Add(bondType);
                    }

                    db.Bonds.Add(new Edis.Db.Assets.Bond {
                        BondId = Guid.NewGuid().ToString(),
                        Ticker = oleReader.GetString(0),
                        Name = oleReader.GetString(1),
                        Frequency = GetValueFromDescription<Frequency>(oleReader.GetString(2)),
                        BondType = bondType.Id,
                        Issuer = oleReader.GetString(4)
                    });
                }
                db.SaveChanges();
            }
        }

        public void FeedExcelDataForAssetPrice(OleDbDataReader oleReader) {

            using (Edis.Db.EdisContext db = new Edis.Db.EdisContext()) {
                while (oleReader.Read()) {
                    AssetTypes assetType = GetValueFromDescription<AssetTypes>(oleReader.GetString(3));
                    var price = oleReader[1].ToString();

                    string ticker = oleReader.GetString(0);

                    var equity = db.Equities.FirstOrDefault(e => e.Ticker == ticker);
                    var bond = db.Bonds.FirstOrDefault(e => e.Ticker == ticker);

                    if (equity != null) {
                        equity.Prices.Add(new Edis.Db.Assets.AssetPrice {
                            Id = Guid.NewGuid().ToString(),
                            Price = price == "" ? null : (double?)double.Parse(price),
                            CorrespondingAssetKey = equity.AssetId,
                            CreatedOn = oleReader.GetDateTime(2),
                            AssetType = assetType
                        });
                    } else if (bond != null) {
                        bond.Prices.Add(new Edis.Db.Assets.AssetPrice {
                            Id = Guid.NewGuid().ToString(),
                            Price = price == "" ? null : (double?)double.Parse(price),
                            CorrespondingAssetKey = bond.BondId,
                            CreatedOn = oleReader.GetDateTime(2),
                            AssetType = assetType
                        });
                    }
                }
                db.SaveChanges();
            }
        }

        public void FeedExcelDataForResearchValue(OleDbDataReader oleReader) {

            using (Edis.Db.EdisContext db = new Edis.Db.EdisContext()) {

                while (oleReader.Read()) {
                    var value = oleReader[2].ToString();

                    string ticker = oleReader.GetString(0);
                    var equity = db.Equities.FirstOrDefault(e => e.Ticker == ticker);
                    var bond = db.Bonds.FirstOrDefault(e => e.Ticker == ticker);

                    if (equity != null) {
                        equity.ResearchValues.Add(new Edis.Db.ResearchValue {
                            Id = Guid.NewGuid().ToString(),
                            Key = oleReader.GetString(1),
                            Value = value == "" ? 0 : (double?)double.Parse(value),
                            StringValue = oleReader[3].ToString(),
                            Issuer = oleReader.GetString(4),
                            CreatedOn = oleReader.GetDateTime(5),
                        });
                    } else if (bond != null) {
                        bond.ResearchValues.Add(new Edis.Db.ResearchValue {
                            Id = Guid.NewGuid().ToString(),
                            Key = oleReader.GetString(1),
                            Value = value == "" ? 0 : (double?)double.Parse(value),
                            StringValue = oleReader[3].ToString(),
                            Issuer = oleReader.GetString(4),
                            //CreatedOn = DateTime.Parse(createOn.ToString(), new CultureInfo("en-AU")).Date,
                            CreatedOn = oleReader.GetDateTime(5),
                        });
                    }
                }
                db.SaveChanges();
            }
        }

        public T GetValueFromDescription<T>(string description) {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new InvalidOperationException();
            foreach (var field in type.GetFields()) {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null) {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                } else {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }

        
        public TOut GetConstFieldAttributeValue<T, TOut, TAttribute>(string fieldName, Func<TAttribute, TOut> valueSelector) where TAttribute : Attribute {
            var fieldInfo = typeof(T).GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
            if (fieldInfo == null) {
                return default(TOut);
            }
            var att = fieldInfo.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            return att != null ? valueSelector(att) : default(TOut);
        }
    }
}