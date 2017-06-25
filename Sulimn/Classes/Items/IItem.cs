using Sulimn.Classes.Enums;

namespace Sulimn.Classes.Items
{
    /// <summary>
    /// Represents an Item.
    /// </summary>
    internal interface IItem
    {
        #region Modifying Properties

        string Name { get; set; }
        ItemTypes Type { get; set; }
        string Description { get; set; }
        int Weight { get; set; }
        int Value { get; set; }
        bool CanSell { get; set; }
        bool IsSold { get; set; }

        #endregion Modifying Properties

        #region Helper Properties

        string ValueToString { get; }
        string ValueToStringWithText { get; }
        int SellValue { get; }
        string SellValueToString { get; }
        string SellValueToStringWithText { get; }
        string CanSellToString { get; }

        #endregion Helper Properties
    }
}