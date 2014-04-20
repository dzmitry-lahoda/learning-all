using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Field
/// </summary>
[Serializable]
public class Field{

    private int id;
    private int itemId;
    private int fieldTypeId;
    private byte[] content;


    public Field(int id,int itemId, int fieldTypeId, byte[] content)
    {
        this.id = id;
        this.itemId = itemId;
        this.fieldTypeId = fieldTypeId;
        this.content = content;
    }

    public Field(int itemId, int fieldTypeId, byte[] content)
    {
        this.itemId = itemId;
        this.fieldTypeId = fieldTypeId;
        this.content = content;
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public int ItemId
    {
        get { return itemId; }
        set { itemId = value; }
    }

    public int FieldTypeId
    {
        get { return fieldTypeId; }
        set { fieldTypeId = value; }
    }

    public byte[] Content
    {
        get { return content; }
        set { content = value; }
    }


}
