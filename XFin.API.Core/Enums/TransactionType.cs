namespace XFin.API.Core.Enums
{
    /*
     * Revenue          => the money comes from an external source
     * Transfer         => the transaction is an interal transfer
     * Expense          => the money goes to an external source
     * Initialization   => this transaction represents the initialization of the bankAccount
     *
    */
    public enum TransactionType { Revenue, Transfer, Expense, Initialization }}
