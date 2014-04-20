using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Available types of fields for items.
/// </summary>
public static  class TypeOfField
{

    public const string Uri = "System.Uri";

    public static string[] supportedTypes = new string[] { "System.String", "System.Int32", "System.Double", "System.Uri" };

    public static string[] GetSupportedTypes()
    {
        return supportedTypes;
    }
}
