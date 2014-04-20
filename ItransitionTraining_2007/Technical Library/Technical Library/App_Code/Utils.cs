using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using Itransition.Training.Data;

/// <summary>
/// Static class with some functionality
/// </summary>
public static class Utils
{
    /// <summary>
    /// Splits string with spaces.
    /// </summary>
    /// <param name="tagString"></param>
    /// <returns>Collection of tags' titles.</returns>
    public static IList<String> ParseTagString(String tagsString)
    {
        String pattern=@"\s+";
        Regex regex=new Regex(pattern);
        IList<String> list = new List<string>();
        foreach (Match match in regex.Matches(tagsString))
        {
            if (match.ToString() != " ")
            {
                list.Add(match.ToString());
            }
        }
        return list;
    }

    /// <summary>
    /// Transforms list of strings to list of tags.
    /// </summary>
    /// <param name="tagString"></param>
    /// <returns>Collection of tags with specified titles.</returns>
    public static IList<Tag> GetTagsFromStringList(IList<String> list)
    {
        IList<Tag> tags = new List<Tag>();
        foreach (String tagTitle in list)
        {
            tags.Add(new Tag(tagTitle));
        }
        return tags;
    }
}
