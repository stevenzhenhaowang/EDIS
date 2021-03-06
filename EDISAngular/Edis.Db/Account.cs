﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Edis.Db.IncomeRecords;
using Edis.Db.Liabilities;
using Edis.Db.Transactions;
using Shared;



//I want to merge the conflict 
//this is the output if you don't agree 
//make you changes later

namespace Edis.Db
{
    public class Account
    {
        [Key]
        public string AccountId { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public AccountType AccountType { get; set; }
        public string AccountInfo { get; set; }
        public string MarginLenderId { get; set; }

        public virtual ICollection<MarginLendingTransaction> MarginLendings  { get; set; }
        public virtual ICollection<MortgageHomeLoanTransaction> MortgageHomeLoans { get; set; }
        public virtual ICollection<InsuranceTransaction> Insurances{ get; set; }
        public virtual ICollection<RepaymentRecord> RepaymentRecords { get; set; }

        public virtual ICollection<BondTransaction> BondTransactions { get; set; }
        public virtual ICollection<CashTransaction> CashTransactions { get; set; }
        public virtual ICollection<EquityTransaction> EquityTransactions { get; set; }
        public virtual ICollection<PropertyTransaction> PropertyTransactions { get; set; }
        public virtual ICollection<CouponPayment> FixedIncomePayments { get; set; }
        public virtual ICollection<Interest> CashAndTermDepositPayments { get; set; }
        public virtual ICollection<Dividend> EquityPayments { get; set; }
        public virtual ICollection<Rental> DirectPropertyPayments { get; set; }

        public Account()
        {
            this.MarginLendings = new HashSet<MarginLendingTransaction>();
            this.MortgageHomeLoans = new HashSet<MortgageHomeLoanTransaction>();
            this.Insurances = new HashSet<InsuranceTransaction>();
            this.RepaymentRecords = new HashSet<RepaymentRecord>();
            this.BondTransactions = new HashSet<BondTransaction>();
            this.CashTransactions = new HashSet<CashTransaction>();
            this.EquityTransactions = new HashSet<EquityTransaction>();
            this.PropertyTransactions = new HashSet<PropertyTransaction>();
            this.FixedIncomePayments = new HashSet<CouponPayment>();
            this.CashAndTermDepositPayments = new HashSet<Interest>();
            this.EquityPayments = new HashSet<Dividend>();
            this.DirectPropertyPayments = new HashSet<Rental>();
        }
    }
}