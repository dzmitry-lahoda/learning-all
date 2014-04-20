using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

/// <summary>
/// Summary description for Utility
/// </summary>
public static class Utility
{
    public static byte[] ContentToBinary(object content)
    {
        var formatter = new BinaryFormatter();
        using (var stream = new MemoryStream())
        {
            formatter.Serialize(stream, content);
            var binaryContent = stream.ToArray();
            return binaryContent;
        }
    }

    public static object BinaryToContent(byte[] binary)
    {
        var formatter = new BinaryFormatter();
        using (var stream = new MemoryStream(binary))
        {
            var content=formatter.Deserialize(stream);
            return content;
        }
    }
}
