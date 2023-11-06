using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XFin.API.Core.Enums;

namespace XFin.API.Core.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SourceBankAccountId")]
        public int? SourceBankAccountId { get; set; }
        public BankAccount SourceBankAccount { get; set; }

        [ForeignKey("TargetBankAccountId")]
        public int? TargetBankAccountId { get; set; }
        public BankAccount TargetBankAccount { get; set; }

        //only needed for TransactionType.Expense
        [ForeignKey("CostCenterId")]
        public int? CostCenterId { get; set; }
        public CostCenter CostCenter { get; set; }

        //only needed for TransactionType.Expense
        [ForeignKey("CostCenterAssetId")]
        public int? CostCenterAssetId { get; set; }
        public CostCenterAsset CostCenterAsset { get; set; }

        [ForeignKey("LoanId")]
        public int? LoanId { get; set; }
        public Loan Loan { get; set; }

        [ForeignKey("RecurringTransactionId")]
        public int? RecurringTransactionId { get; set; }
        public RecurringTransaction RecurringTransaction { get; set; }

        [MaxLength(100)]
        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        //equals property 'Date' on non-recurring transactions - does NOT ALWAYS equal property 'Date' on transactions, that are linked to a recurringTransaction!
        public DateTime DueDate { get; set; }

        //always true on non-recurring regular transactions - can be false on recurringTransactions, that have been cancelled by the user 
        public bool Executed { get; set; }

        public bool IsCashTransaction { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
