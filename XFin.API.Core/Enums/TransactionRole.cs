//TODO - I might not need this enum

namespace XFin.API.Core.Enums
{
    /*
     * Clearance    => the source of this transaction redeems a loan from the target    => transaction connected to a loan instance
     * Loan         => the source of this transaction lends money to the target         => transaction connected to a loan instance
     * Neutral      => a simple payment or donation                                     => transaction not connected to a loan instance
     *
    */
    public enum TransactionRole { Clearance, Loan, Neutral }
}
