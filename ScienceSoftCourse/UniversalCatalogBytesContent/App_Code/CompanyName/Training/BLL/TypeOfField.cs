using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Available types of fields for items.
/// </summary>
public static  class TypeOfField
{

    public const string Image = "System.Drawing.Image"; 

    public static string[] supportedTypes = new string[] { "System.String", "System.Int32", "System.Double", "System.Drawing.Image" };

    public static string[] GetSupportedTypes()
    {
        return supportedTypes;
    }
}
