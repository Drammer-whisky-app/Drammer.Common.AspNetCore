using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
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
        var list = CreateOptionList(enumType);
        var hasIntValues = list.Any(x => x.IntValue.HasValue);

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

    internal static List<ExtendedSelectListItem> CreateOptionList(Type enumType)
    {
        var list = new List<ExtendedSelectListItem>();
        foreach (var obj in Enum.GetValues(enumType))
        {
            var text = (obj as Enum)?.ToString();

            var fieldInfo = enumType.GetField(obj.ToString() ?? string.Empty);
            if (fieldInfo != null)
            {
                var attribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
                if (attribute is {Name: not null})
                {
                    text = attribute.GetName();
                }
            }

            try
            {
                var intVal = (int) obj;
                list.Add(
                    new ExtendedSelectListItem
                        {Text = text, Value = obj.ToString(), IntValue = intVal});
            }
            catch (InvalidCastException)
            {
                list.Add(
                    new ExtendedSelectListItem
                        {Text = text, Value = obj.ToString(), IntValue = null});
            }
        }

        return list;
    }
}