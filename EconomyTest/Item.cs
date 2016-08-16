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
    public static string FilePath
    {
        get
        {
            return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Items.db");
        }
    }

    public static SQLiteConnection Connection()
    {
        return new SQLiteConnection("Data Source=" + FilePath);
    }

    public int Quantity;

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
}

/*
Currency,
Bread,
Flour,
Wheat,
Grain,
Water,
Liquor,
Meme,
MagicMushroom,
Crystal,
*/
