// <copyright file="Item.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc.  No Rights Reserved.
//     Licensed under the "Do What the Fuck You Want To Public License"
// </copyright>
namespace Economy
{
    // TODO(Albino) Extend to an ItemTemplate struct and Item class

    /// <summary>
    /// temporary enum defining items
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:EnumerationItemsMustBeDocumented", Justification = "Reviewed.")]
    public enum Item
    {
        Currency,
        Bread,
        Flour,
        Wheat,
        Grain,
        Water,

        Liquor,
        
        /// <summary>
        /// completely useless to have
        /// </summary>
        Meme,
        
        MagicMushroom,

        Crystal,
    }
}
