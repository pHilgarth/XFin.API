using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.Core.Models
{
    public class BankAccountModel
    {
        public int Id { get; set; }

        public int AccountHolderId { get; set; }

        public string AccountHolderName { get; set; }

        public string Iban { get; set; }

        public string AccountNumber { get; set; }

        public string Bic { get; set; }

        public string Bank { get; set; }

        public string Description { get; set; }

        public bool External { get; set; }

        public decimal Balance { get; set; }

        public List<LoanModel> Loans { get; set; }
            = new List<LoanModel>();

        public List<RecurringTransactionModel> RecurringTransactions { get; set; }
            = new List<RecurringTransactionModel>();

        public List<ReserveModel> Reserves { get; set; }
            = new List<ReserveModel>();

        public List<TransactionModel> Revenues { get; set; }
            = new List<TransactionModel>();

        public List<TransactionModel> Expenses { get; set; }
            = new List<TransactionModel>();
    }
}
