namespace Sulimn.Classes.Enums
{
    /// <summary>Represents an action performed at the Bank.</summary>
    internal enum BankAction
    {
        /// <summary>Deposit gold into an account</summary>
        Deposit,

        /// <summary>Withdrawal gold from an account</summary>
        Withdrawal,

        /// <summary>Borrow gold from the bank</summary>
        Borrow,

        /// <summary>Repay gold owed on a loan</summary>
        Repay
    }
}