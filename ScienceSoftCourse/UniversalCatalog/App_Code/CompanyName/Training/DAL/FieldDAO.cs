using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Drawing;

/// <summary>
/// Summary description for FieldTemplate
/// </summary>
[DataObject]
public static class FieldDAO
{
    [DataObjectMethod(DataObjectMethodType.Insert, true)]
    public static int AddField(Field field)
    {
        var command = Factory.CreateCommandWithConnection("AddField", CommandType.StoredProcedure);
        command.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.Int, 4));
        command.Parameters["@ItemId"].Value = field.ItemId;
        command.Parameters.Add(new SqlParameter("@FieldTypeId", SqlDbType.VarChar, 50));
        command.Parameters["@FieldTypeId"].Value = field.FieldTypeId;
        command.Parameters.Add(new SqlParameter("@Content", SqlDbType.Xml));
        command.Parameters["@Content"].Value = field.Content;
        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
        command.Parameters["@Id"].Direction = ParameterDirection.Output;
        try
        {
            command.Connection.Open();
            command.ExecuteNonQuery();
            return (int)command.Parameters["@Id"].Value;
        }
        finally
        {
            command.Dispose();
        }
    }



    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public static List<Field> GetFieldsForItemId(int itemId)
    {
        var command =
            Factory.CreateCommandWithConnection("GetFieldsForItemById", CommandType.StoredProcedure);
        command.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.Int, 4));
        command.Parameters["@ItemId"].Value = itemId;
        var fields = new List<Field>();
        try
        {
            command.Connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var field = new Field((int)reader["Id"], (int)reader["ItemId"], (int)reader["FieldTypeId"], (string)reader["Content"]);
                fields.Add(field);
            }
            reader.Close();
            return fields;
        }
        finally
        {
            command.Dispose();
        }
    }


}
