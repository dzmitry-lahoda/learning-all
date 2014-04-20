using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Represents name and type of field wich item can contain.
/// </summary>
[Serializable]
public sealed class FieldType {
    private int id;
    private String name;
    private String typeOfField;
    private int itemTypeId;

    public FieldType() {}

    public FieldType(int id, String name, String typeOfField, int itemTypeId) {
        this.id = id;
        this.name = name;
        this.typeOfField = typeOfField;
        this.itemTypeId = itemTypeId;
    }

    public FieldType(String name, String typeOfField, int itemTypeId) {
        this.name = name;
        this.typeOfField = typeOfField;
        this.itemTypeId = itemTypeId;
    }

    public int Id {
        get { return id; }
        set { id = value; }
    }

    public int ItemTypeId {
        get { return itemTypeId; }
        set { itemTypeId = value; }
    }

    public string Name {
        get { return name; }
        set { name = value; }
    }

    public string TypeOfField {
        get { return typeOfField; }
        set { typeOfField = value; }
    }
}