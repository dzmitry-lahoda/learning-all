using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Utility
/// </summary>
public static class Utility
{
    public static string ContentToXml(object content)
    {
        var formatter = new SoapFormatter();
        using (var stream = new MemoryStream())
        {
            var writer = new StreamWriter(stream);
            formatter.Serialize(stream, content);
            var reader = new StreamReader(stream);
            var xml = reader.ReadToEnd();
            return xml;
        }
    }

    public static object XmlToContent(string xml)
    {
        var formatter = new SoapFormatter();
        using (var stream = new MemoryStream())
        {
            var writer = new StreamWriter(stream);
            writer.Write(xml);
            var content = formatter.Deserialize(stream);
            return content;
        }
    }
}
