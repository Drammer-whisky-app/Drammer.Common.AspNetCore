using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Drammer.Common.AspNetCore.Mvc;

public static class HtmlEditorExtensions
{
    public static IHtmlContent DropDownListEnumFor<TModel, TProperty>(
        this IHtmlHelper<TModel> h,
        Expression<Func<TModel, TProperty>> expression,
        Type enumType,
        bool addEmptyEntry = false,
        object? htmlAttributes = null,
        bool orderByDisplayName = true,
        bool sortAsc = true,
        string notApplicableText = "- N/A -")
    {
        var list = new List<ExtendedSelectListItem>();
        var hasIntValues = false;
        foreach (var obj in Enum.GetValues(enumType))
        {
            try
            {
                var intVal = (int) obj;
                list.Add(
                    new ExtendedSelectListItem
                        {Text = (obj as Enum)?.ToString(), Value = obj.ToString(), IntValue = intVal});
                hasIntValues = true;
            }
            catch (InvalidCastException)
            {
                list.Add(
                    new ExtendedSelectListItem
                        {Text = (obj as Enum)?.ToString(), Value = obj.ToString(), IntValue = null});
            }
        }

        if (orderByDisplayName)
        {
            list = sortAsc ? list.OrderBy(x => x.Text).ToList() : list.OrderByDescending(x => x.Text).ToList();
        }
        else
        {
            if (hasIntValues)
            {
                list = sortAsc
                    ? list.OrderBy(x => x.IntValue).ToList()
                    : list.OrderByDescending(x => x.IntValue).ToList();
            }
            else
            {
                list = sortAsc ? list.OrderBy(x => x.Value).ToList() : list.OrderByDescending(x => x.Value).ToList();
            }
        }

        if (addEmptyEntry)
        {
            list.Insert(0, new ExtendedSelectListItem {Value = string.Empty, Text = notApplicableText, IntValue = 0});
        }

        return h.DropDownListFor(expression, list, htmlAttributes);
    }
}