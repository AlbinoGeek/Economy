// <copyright file="ItemTemplate.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc. All rights reserved.
// </copyright>
/// <summary>
/// 
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.  We want to have public methods.")]
public class ItemTemplate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Weight { get; set; }
    public float Value { get; set; }
}
