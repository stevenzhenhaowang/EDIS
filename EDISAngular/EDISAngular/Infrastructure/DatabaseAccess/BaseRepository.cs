using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDISAngular.Infrastructure.DbFirst;
using EDISAngular.Models.ViewModels;
using EDISAngular.Services;
using System.Globalization;

namespace EDISAngular.Infrastructure.DatabaseAccess
{
    public abstract class BaseRepository : IDisposable
    {
        internal edisDbEntities db;

        public BaseRepository(edisDbEntities database)
        {
            db = database;
        }

        public BaseRepository()
        {
            db = new edisDbEntities();
        }


        internal double? getDoubleFromProperty(string property)
        {
            if (string.IsNullOrEmpty(property))
            {
                return null;
            }
            else
            {
                try
                {
                    return Convert.ToDouble(property);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }


        #region adviser related
        public IQueryable<Client> GetAllClientsForAdviser(string adviserId)
        {
            var clientGroups = GetAllClientGroupsForAdviser(adviserId);
            return db.Clients.Where(c => clientGroups.Any(g => g.ClientGroupId == c.ClientGroupId));
        }
        public IQueryable<ClientGroup> GetAllClientGroupsForAdviser(string adviserId)
        {
            return db.ClientGroups.Where(g => g.Adviser_AdviserId == adviserId);
        }
        public IQueryable<Account> GetAllAccountsForAdviser(string adviserId)
        {
            var clientGroups = GetAllClientGroupsForAdviser(adviserId);
            return db.Accounts.Where(a =>
                db.Clients.Any(c => c.ClientId == a.Client_ClientId)
                || db.ClientGroups.Any(g => g.ClientGroupId == a.ClientGroup_ClientGroupId))
                .Distinct();
        }

        //public IQueryable<TransactionExpens> GetAllTransactionsForAdviser(string adviserId)
        //{
        //    var accounts = GetAllAccountsForAdviser(adviserId);
        //    return db.TransactionExpenses.Where(t => accounts.Any(a => a.AccountId == t.AdviserId));//t.AccountID ----> t.AdviserId ------------------------
        //}
        //public IQueryable<TransactionExpens> GetAllTransactionsForAdviserWithAssetType(string adviserId, int balanceTypeID)
        //{
        //    return FilterTransactionsBasedOnBalanceEntityType(GetAllTransactionsForAdviser(adviserId), balanceTypeID);
        //}
        //public IQueryable<TransactionExpens> GetAllTransactionsForAdviserWithBalanceEntityID(string adviserId, string balanceEntityId)
        //{
        //    return GetAllTransactionsForAdviser(adviserId).Where(t => t.BalanceID == balanceEntityId);
        //}

        //public IQueryable<Income> GetAllExpenseAndRevenuesForAdviser_ClientsSpecific(string adviserId)
        //{
        //    var clients = GetAllClientsForAdviser(adviserId);
        //    IQueryable<Income> result = Enumerable.Empty<Income>().AsQueryable();
        //    foreach (var client in clients)
        //    {
        //        result.Concat(GetAllExpenseAndRevenuesForClient(client.ClientUserID));
        //    }
        //    return result.Distinct();
        //}
        //public IQueryable<Income> GetAllExpenseAndRevenuesForAdviser_AdviserSpecific(string adviserId)
        //{
        //    return db.Incomes.Where(i => i.AdviserUserID == adviserId).Distinct();
        //}
        //public IQueryable<Income> GetAllExpenseAndRevenuesForAdviser_ClientsSpecific_WithBalanceType(string adviserId, int balanceTypeID)
        //{
        //    return FilterIncomeBasedOnBalanceEntityType(GetAllExpenseAndRevenuesForAdviser_ClientsSpecific(adviserId), balanceTypeID);
        //}
        //public IQueryable<Income> GetAllExpenseAndRevenuesForAdviser_AdviserSpecific_WithBalanceType(string adviserId, int balanceTypeID)
        //{
        //    return FilterIncomeBasedOnBalanceEntityType(GetAllExpenseAndRevenuesForAdviser_AdviserSpecific(adviserId), balanceTypeID);
        //}
        //#endregion


        //#region account related
        //public IQueryable<Transaction> GetAllTransactionsForAccount(string accountId)
        //{
        //    return db.Transactions.Where(t => t.AccountID == accountId);
        //}
        //public IQueryable<Transaction> GetAllTransactionsForAccountWithAssetType(string accountId, int balanceTypeId)
        //{
        //    return FilterTransactionsBasedOnBalanceEntityType(GetAllTransactionsForAccount(accountId), balanceTypeId);
        //}
        //public IQueryable<Income> GetAllExpenseAndRevenuesForAccount(string accountId)
        //{
        //    var account = db.Accounts.SingleOrDefault(a => a.AccountID == accountId);
        //    var transactions = GetAllTransactionsForAccount(accountId);
        //    return db.Incomes.Where(i => i.ClientUserID == account.ClientUserID
        //        || db.Transaction_IncomeExpense.Any(t => t.IncomeID == i.Id
        //            && transactions.Any(ts => t.TransactionID == ts.TransactionID)));
        //}
        //public IQueryable<Income> GetAllExpensesAndRevenuesForAccountWithBalanceEntityType
        //    (string accountId, int balanceTypeId)
        //{
        //    return FilterIncomeBasedOnBalanceEntityType(GetAllExpenseAndRevenuesForAccount(accountId), balanceTypeId);
        //}
        //#endregion


        //#region client related
        //public IQueryable<Account> GetAllAccountsForClient(string clientUserId)
        //{
        //    return db.Accounts.Where(a => a.ClientUserID == clientUserId);
        //}
        //public IQueryable<Income> GetAllExpenseAndRevenuesForTransaction(string transactionId)
        //{
        //    return db.Incomes.Where(i => db.Transaction_IncomeExpense
        //        .Any(t => t.TransactionID == transactionId && t.IncomeID == i.Id));
        //}
        //public IQueryable<Transaction> GetAllTransactionsForClient(string clientUserId)
        //{
        //    var accounts = GetAllAccountsForClient(clientUserId);
        //    return db.Transactions.Where(t => accounts.Any(a => a.AccountID == t.AccountID));
        //}
        //public IQueryable<Transaction> GetAllTransactionsForClientWithBalanceEntityType(string clientUserId, int balanceTypeID)
        //{
        //    return FilterTransactionsBasedOnBalanceEntityType(GetAllTransactionsForClient(clientUserId), balanceTypeID);
        //}
        //public IQueryable<Income> GetAllExpenseAndRevenuesForClient(string clientUserId)
        //{
        //    var transactions = GetAllTransactionsForClient(clientUserId);
        //    return db.Incomes.Where(i => (db.Transaction_IncomeExpense.Any(f => f.IncomeID == i.Id
        //        && transactions.Any(t => t.TransactionID == f.TransactionID)))
        //        || i.ClientUserID == clientUserId).Distinct();
        //}
        //public IQueryable<Income> GetAllExpenseAndRevenuesForClientWithBalanceEntityType(string clientUserId, int balanceTypeId)
        //{
        //    return FilterIncomeBasedOnBalanceEntityType(GetAllExpenseAndRevenuesForClient(clientUserId), balanceTypeId);
        //}
        //#endregion






        //#region client group related
        //public IQueryable<Client> GetAllClientsForClientGroup(string clientGroupId)
        //{
        //    return db.Clients.Where(c => c.ClientGroupID == clientGroupId);
        //}
        //public IQueryable<Account> GetAllAccountsForClientGroup(string clientGroupId)
        //{
        //    var clients = GetAllClientsForClientGroup(clientGroupId);
        //    return db.Accounts.Where(a => clients.Any(c => c.ClientUserID == a.ClientUserID)
        //        || a.ClientGroupID == clientGroupId);
        //}
        //public IQueryable<Transaction> GetAllTransactionsForClientGroup(string clientGroupId)
        //{
        //    var account = GetAllAccountsForClientGroup(clientGroupId);
        //    return db.Transactions.Where(t => account.Any(a => a.AccountID == t.AccountID));
        //}
        //public IQueryable<Transaction> GetAllTransactionsForClientGroupWithBalanceEntityType(string clientGroupId, int balanceTypeID)
        //{
        //    return FilterTransactionsBasedOnBalanceEntityType(GetAllTransactionsForClientGroup(clientGroupId), balanceTypeID);
        //}
        //public IQueryable<Income> GetAllExpenseAndRevenuesForClientGroup(string clientGroupId)
        //{
        //    var clients = GetAllClientsForClientGroup(clientGroupId);
        //    IQueryable<Income> result = Enumerable.Empty<Income>().AsQueryable();
        //    foreach (var client in clients)
        //    {
        //        result.Concat(GetAllExpenseAndRevenuesForClient(client.ClientUserID));
        //    }
        //    return result;
        //}
        //public IQueryable<Income> GetAllExpenseAndRevenuesForClientGroupWithBalanceEntityType(string clientGroupId, int balanceType)
        //{
        //    return FilterIncomeBasedOnBalanceEntityType(GetAllExpenseAndRevenuesForClientGroup(clientGroupId), balanceType);
        //}
        //#endregion





        //public IQueryable<Adviser_ProfessionalType> GetAdviserProfessionalType()
        //{
        //    return db.Adviser_ProfessionalType;
        //}
        //public IQueryable<Adviser_Details> GetAdviserDetails()
        //{
        //    return db.Adviser_Details;
        //}
        //public IQueryable<Adviser_Education> GetAdviserEducations()
        //{
        //    return db.Adviser_Education;
        //}
        //public IQueryable<User_NewsletterServices> GetUserNewsletterSubscriptions()
        //{
        //    return db.User_NewsletterServices;
        //}
        //public IQueryable<Adviser_CommissionsAndFees> GetAdviserCommissionAndFees()
        //{
        //    return db.Adviser_CommissionsAndFees;
        //}
        //public string GetAdviserAccountNumber(string adviserUserId)
        //{
        //    var adviser = db.Adviser_Details.FirstOrDefault(ad => ad.AdvisorUserId == adviserUserId);
        //    return adviser.AccountNumber.ToString();
        //}

        
        //#region calculation helpers

        //internal decimal GetAssetUnitPriceForDate(string tickerNumber, DateTime date)
        //{
        //    var price = db.Prices.Where(p => p.Ticker == tickerNumber && p.DateCreated <= date).OrderByDescending(p => p.DateCreated)
        //        .FirstOrDefault();

        //    if (price != null)
        //    {
        //        return price.UnitPrice;
        //    }
        //    else
        //    {
        //        throw new Exception("Cannot find asset price for "+tickerNumber);
        //    }

        //}
        //internal List<MonthlyTransactions> GroupingTransactionWithTransactionDate(IQueryable<Transaction> list, DateTime from, DateTime to)
        //{
        //    var transactions = FilterTransactionBasedOnTransactionDate(list, from, to);
        //    List<MonthlyTransactions> result = new List<MonthlyTransactions>();
        //    foreach (var group in transactions.ToList()
        //        .GroupBy(t => t.DateCreated.Year + "," + t.DateCreated.ToString("MMM", CultureInfo.InvariantCulture)))
        //    {
        //        var monthName = group.Key.Split(',')[1];
        //        var item = new MonthlyTransactions()
        //        {
        //            MonthName = monthName,
        //            Transactions = new List<Transaction>()
        //        };
        //        foreach (var transaction in group)
        //        {
        //            item.Transactions.Add(transaction);
        //        }
        //        result.Add(item);
        //    }
        //    return result;
        //}
        //internal List<MonthlyIncomeExpenses> GroupingIncomeWithCreationDate(IQueryable<Income> list, DateTime from, DateTime to)
        //{
        //    var incomes = FilterIncomeBasedOnIncomeCreationDate(list, from, to);
        //    List<MonthlyIncomeExpenses> result = new List<MonthlyIncomeExpenses>();
        //    foreach (var group in incomes.ToList()
        //        .GroupBy(i => i.CreatedOn.Year + "," + i.CreatedOn.ToString("MMM", CultureInfo.InvariantCulture)))
        //    {
        //        var monthName = group.Key.Split(',')[1];
        //        var item = new MonthlyIncomeExpenses
        //        {
        //            IncomeExpenses = new List<Income>(),
        //            MonthName = monthName
        //        };
        //        foreach (var income in group)
        //        {
        //            item.IncomeExpenses.Add(income);
        //        }
        //        result.Add(item);
        //    }
        //    return result;
        //}

        



        //internal double TotalMarketValueFromTransactions_WithAssetTypeOnDate(IQueryable<Transaction> transactions, int balanceType, DateTime before)
        //{
        //    //var transactions = FilterTransactionBasedOnTransactionDate(transactions, DateTime.MinValue,)
        //    //Need to get current price of asset
        //    throw new NotImplementedException();
        //}
        //internal double TotalMarketValueFromTransactions_OnDate(IQueryable<Transaction> transactions, DateTime date)
        //{
        //    //iterate through all asset types and get total


        //    throw new NotImplementedException();
        //}
        //internal double WeightingOfMarketValueFromTransactions_WithAssetTypeOnDate(IQueryable<Transaction> transactions, int balanceType, DateTime date)
        //{
        //    //need to get current price of asset


        //    throw new NotImplementedException();
        //}








        //internal double TotalCostOfBalanceEntitiesForAssetTypeOnDate(IQueryable<Transaction> transactions, int balanceType, DateTime date)
        //{
        //    //sum all cost without price reference table 



        //    throw new NotImplementedException();
        //}
        //internal double TotalCostOfBalanceEntitiesFromTransactionsOnDate(IQueryable<Transaction> transactions, DateTime date)
        //{
        //    //sum all transaction cost without price reference table


        //    throw new NotImplementedException();
        //}
        //internal double CostWeightingOfBalanceEntityFromTransactionForAssetTypeOnDate(IQueryable<Transaction> transactions, DateTime date)
        //{

        //    //get sum and use for percentage calculation
        //    throw new NotImplementedException();

        //}





        //#endregion



        //#region helpers
        //internal IQueryable<TransactionExpens> FilterTransactionsBasedOnBalanceEntityType(IQueryable<TransactionExpens> list, int BalanceEntityType)
        //{
        //    return list.Where(t => db.BalanceEntities.Any(b => b.ID == t.BalanceID && b.TypeID == BalanceEntityType));
        //}
        //internal IQueryable<Income> FilterIncomeBasedOnBalanceEntityType(IQueryable<Income> list, int balanceEntityType)
        //{
        //    return list.Where(i => db.Transaction_IncomeExpense.Any(t => t.IncomeID == i.Id
        //        && db.Transactions
        //        .Any(s => s.TransactionID == t.TransactionID && s.TransactionType == balanceEntityType)));

        //}
        //internal IQueryable<Transaction> FilterTransactionBasedOnTransactionDate(IQueryable<Transaction> list, DateTime from, DateTime to)
        //{
        //    return list.Where(l => l.DateCreated >= from && l.DateCreated <= to);
        //}
        //internal IQueryable<Income> FilterIncomeBasedOnCorrespondingTransactionDate(IQueryable<Income> list, DateTime from, DateTime to)
        //{
        //    return list.Where(l => db.Transaction_IncomeExpense
        //        .Any(ex => ex.IncomeID == l.Id && db.Transactions
        //            .Any(t => t.TransactionID == ex.TransactionID
        //        && t.DateCreated >= from && t.DateCreated <= to)));
        //}
        //internal IQueryable<Income> FilterIncomeBasedOnIncomeCreationDate(IQueryable<Income> list, DateTime from, DateTime to)
        //{
        //    return list.Where(l => l.CreatedOn >= from && l.CreatedOn <= to);
        //}






        #endregion





        public void Dispose()
        {
            db.Dispose();
        }
        public void Save()
        {
            db.SaveChanges();
        }

        //internal class MonthlyTransactions
        //{
        //    public string MonthName { get; set; }
        //    public List<Transaction> Transactions { get; set; }
        //}
        //internal class MonthlyIncomeExpenses
        //{
        //    public string MonthName { get; set; }
        //    public List<Income> IncomeExpenses { get; set; }
        //}


    }
}