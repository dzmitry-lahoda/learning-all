using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FieldTypeDAO
/// </summary>
[DataObject]
public static class FieldTypeDAO
{
    [DataObjectMethod(DataObjectMethodType.Insert, true)]
    public static int AddFieldType(FieldType type)
    {
        var command = Factory.CreateCommandWithConnection("AddFieldType", CommandType.StoredProcedure);
        command.Parameters.Add(new SqlParameter("@ItemTypeId", SqlDbType.Int, 4));
        command.Parameters["@ItemTypeId"].Value = type.ItemTypeId;
        command.Parameters.Add(new SqlParameter("@TypeOfField", SqlDbType.VarChar, 50));
        command.Parameters["@TypeOfField"].Value = type.TypeOfField;
        command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50));
        command.Parameters["@Name"].Value = type.Name;
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
    public static List<FieldType> GetFieldTypesForItemTypeById(int itemTypeId)
    {
        var command =
   Factory.CreateCommandWithConnection("GetFieldTypesForItemTypeById", CommandType.StoredProcedure);
        command.Parameters.Add(new SqlParameter("@ItemTypeId", SqlDbType.Int, 4));
        command.Parameters["@ItemTypeId"].Value = itemTypeId;
        var fieldTypes = new List<FieldType>();
        try
        {
            command.Connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var fieldType = new FieldType((int)reader["Id"], (String)reader["Name"], (String)reader["TypeOfField"], (int)reader["ItemTypeId"]);
                fieldTypes.Add(fieldType);
            }
            reader.Close();
            return fieldTypes;
        }
        finally
        {
            command.Dispose();
        }
    }

    [DataObjectMethod(DataObjectMethodType.Select, false)]
    public static FieldType GetFieldTypeById(int id)
    {
        var command =
   Factory.CreateCommandWithConnection("GetFieldTypeById", CommandType.StoredProcedure);
        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
        command.Parameters["@Id"].Value = id;
        try
        {
            command.Connection.Open();
            var fieldType = (FieldType)command.ExecuteScalar();
            return fieldType;
        }
        finally
        {
            command.Dispose();
        }
    }
}
