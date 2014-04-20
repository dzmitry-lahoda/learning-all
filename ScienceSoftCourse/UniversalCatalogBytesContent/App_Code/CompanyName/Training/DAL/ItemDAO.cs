using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ItemData
/// </summary>
[DataObject]
public static class ItemDAO
{

    [DataObjectMethod(DataObjectMethodType.Delete, true)]
    public static void DeleteItem(Item item)
    {
        var command =
      Factory.CreateCommandWithConnection("DeleteItem", CommandType.StoredProcedure);
        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
        command.Parameters["@Id"].Value = item.Id;
        try
        {
            command.Connection.Open();
            command.ExecuteNonQuery();
        }
        finally
        {
            command.Dispose();
        }
    }

    [DataObjectMethod(DataObjectMethodType.Insert, true)]
    public static int AddItem(Item item)
    {
        var command = Factory.CreateCommandWithConnection("AddItem", CommandType.StoredProcedure);
        command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50));
        command.Parameters["@Name"].Value = item.Name;
        command.Parameters.Add(new SqlParameter("@ItemTypeId", SqlDbType.Int, 4));
        command.Parameters["@ItemTypeId"].Value = item.ItemTypeId;
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

    [DataObjectMethod(DataObjectMethodType.Insert, false)]
    public static void AddItemWithFields(Item item)
    {
        var connection = Factory.CreateConnection();
        var addItemCommand = Factory.CreateCommand("AddItem", CommandType.StoredProcedure, connection);
        addItemCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50));
        addItemCommand.Parameters["@Name"].Value = item.Name;
        addItemCommand.Parameters.Add(new SqlParameter("@ItemTypeId", SqlDbType.Int, 4));
        addItemCommand.Parameters["@ItemTypeId"].Value = item.ItemTypeId;
        addItemCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
        addItemCommand.Parameters["@Id"].Direction = ParameterDirection.Output;
        SqlTransaction transaction = null;
        try
        {
            connection.Open();
            transaction = connection.BeginTransaction();
            addItemCommand.Transaction = transaction;
            addItemCommand.ExecuteNonQuery();
            var itemId = (int)addItemCommand.Parameters["@Id"].Value;
            var commands = new List<SqlCommand>(item.Fields.Count + 1);
            foreach (var field in item.Fields)
            {
                var command = Factory.CreateCommand("AddField", CommandType.StoredProcedure, connection);
                command.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.Int, 4));
                command.Parameters["@ItemId"].Value = itemId;
                command.Parameters.Add(new SqlParameter("@FieldTypeId", SqlDbType.VarChar, 50));
                command.Parameters["@FieldTypeId"].Value = field.FieldTypeId;
                command.Parameters.Add(new SqlParameter("@Content", SqlDbType.Image));
                command.Parameters["@Content"].Value = field.Content;
                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
                command.Parameters["@Id"].Direction = ParameterDirection.Output;
                commands.Add(command);
            }
            foreach (var cmd in commands) {
                cmd.Transaction = transaction;
            }
            foreach (var cmd in commands)
            {
                cmd.ExecuteNonQuery();
            }
            transaction.Commit();
        }
        catch {
            if (transaction != null) {transaction.Rollback();}
            throw;
        }
        finally
        {
            connection.Dispose();
        }
    }

    public static List<Item> GetItemWithFieldsByItemTypeId(int itemTypeId) {
        var connection = Factory.CreateConnection();
        var command =
Factory.CreateCommand("GetItemsOfItemTypeById", CommandType.StoredProcedure, connection);
        command.Parameters.Add(new SqlParameter("@ItemTypeId", SqlDbType.Int, 4));
        command.Parameters["@ItemTypeId"].Value = itemTypeId;
        var items = new List<Item>();
        try
        {
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var item = new Item((int)reader["Id"], (string)reader["Name"], (int)reader["ItemTypeId"]);
                items.Add(item);
            }
            reader.Close();
            foreach (var item in items) {
                command =
Factory.CreateCommand("GetFieldsForItemById", CommandType.StoredProcedure,connection);
                command.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.Int, 4));
                command.Parameters["@ItemId"].Value = item.Id;
                var fields = new List<Field>();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var field = new Field((int)reader["Id"], (int)reader["ItemId"], (int)reader["FieldTypeId"], (byte[])reader["Content"]);
                    fields.Add(field);
                }
                reader.Close();
                item.Fields=fields;   
            }
            return items;
        }
        finally
        {
            connection.Dispose();
        }
    }

    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public static List<Item> GetItemByItemTypeId(int itemTypeId)
    {
        var command =
   Factory.CreateCommandWithConnection("GetItemsOfItemTypeById", CommandType.StoredProcedure);
        command.Parameters.Add(new SqlParameter("@ItemTypeId", SqlDbType.Int, 4));
        command.Parameters["@ItemTypeId"].Value = itemTypeId;
        var items = new List<Item>();
        try
        {
            command.Connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read()){
                var item = new Item((int)reader["Id"], (string)reader["Name"], (int)reader["ItemTypeId"]);
                items.Add(item);
            }
            reader.Close();
            return items;
        }
        finally
        {
            command.Dispose();
        }
    }
}
