using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Configuration;

[DataObject]
public static class ItemTypeDAO
{

    [DataObjectMethod(DataObjectMethodType.Delete, true)]
    public static void DeleteItemType(ItemType type)
    {
        var command =
      Factory.CreateCommandWithConnection("DeleteItemType", CommandType.StoredProcedure);

        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
        command.Parameters["@Id"].Value = type.Id;

        try{
            command.Connection.Open();
            command.ExecuteNonQuery();
        }
        finally
        {
            command.Dispose();
        }
    }

    [DataObjectMethod(DataObjectMethodType.Insert, true)]
    public static int AddItemType(ItemType type)
    {
        var command = Factory.CreateCommandWithConnection("AddItemType", CommandType.StoredProcedure);
        command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50));
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
    public static List<ItemType> GetAllItemTypes(){
        var command =
           Factory.CreateCommandWithConnection("GetAllItemTypes", CommandType.StoredProcedure);
        var itemTypes = new List<ItemType>();
        try{
            command.Connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read()){
                var emp = new ItemType((int)reader["Id"], (String)reader["Name"]);
                itemTypes.Add(emp);
            }
            reader.Close();
            return itemTypes;
        }
        finally{
            command.Dispose();
        }
    }

    public static ItemType GetItemTypeById(int id)
    {
        throw new System.NotImplementedException();
    }
}

