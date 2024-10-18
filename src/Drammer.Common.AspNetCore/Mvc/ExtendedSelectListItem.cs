using Microsoft.AspNetCore.Mvc.Rendering;

namespace Drammer.Common.AspNetCore.Mvc;

/// <summary>
/// Extended SelectListItem.
/// </summary>
public class ExtendedSelectListItem : SelectListItem
{
    /// <summary>
    /// Gets or sets the integer value.
    /// </summary>
    public int? IntValue { get; set; }
}