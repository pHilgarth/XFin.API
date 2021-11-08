namespace XFin.API.Core.Models
{
    public class InternalBankAccountSettingsModel
    {
        public int Id { get; set; }

        public int InternalBankAccountId { get; set; }

        public bool EffectsExpenses { get; set; }

        public bool ReceivesRevenues { get; set; }

        public bool AllowsOverdraft { get; set; }

        public decimal BalanceThreshold { get; set; }

        public decimal ExpensesThreshold { get; set; }
    }
}
