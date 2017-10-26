namespace Sulimn.Classes.Enums
{
    /// <summary>Represents an action performed at the Bank.</summary>
    internal enum BankAction
    {
        /// <summary>Deposit Gold into an account</summary>
        Deposit,

        /// <summary>Withdraw Gold from an account</summary>
        Withdrawal,

        /// <summary>Borrow Gold from the bank</summary>
        Borrow,

        /// <summary>Repay Gold owed on a loan</summary>
        Repay
    }
}