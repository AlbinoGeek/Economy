// <copyright file="Item.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc. All rights reserved.
// </copyright>
using System.Data.SQLite;
using System.Linq;
using System.IO;

using Dapper;
/// <summary>
/// 
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.  We want to have public methods.")]
public class Item : ItemTemplate
{
    /// <summary>
    /// number of items represented
    /// </summary>
    public int Quantity;

    /// <summary>
    /// Initializes a new instance of the <see cref="Item" /> class from a database template
    /// </summary>
    /// <param name="itemName">name of ItemTemplate in database</param>
    public Item(string itemName)
    {
        using (SQLiteConnection conn = Connection())
        {
            conn.Open();

            var result = conn.Query<ItemTemplate>(
                @"select * from ItemTemplates
                where Name = @Name",
            new { Name = itemName }
            ).FirstOrDefault();
            
            Name = result.Name;
            Value = result.Value;
            Weight = result.Weight;
        }
    }

    /// <summary>
    /// Gets the path to our SQLite database
    /// </summary>
    public static string FilePath
    {
        get
        {
            return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Items.db");
        }
    }

    /// <summary>
    /// Gets a connection to the SQLite database
    /// </summary>
    /// <returns>unopened connection to database</returns>
    public static SQLiteConnection Connection()
    {
        return new SQLiteConnection("Data Source=" + FilePath);
    }
}
