using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

/// <summary>
/// Contains information about type of instance.
/// </summary>
[Serializable]
public class ItemType
{
    private int id;
    private String name;
    private List<FieldType> fieldTypes = new List<FieldType>();
    private List<Item> items = new List<Item>();

    public ItemType()
    {
    }

    public ItemType(String name)
    {
        this.name = name;
    }

    public ItemType(int id, String name)
    {
        this.id = id;
        this.name = name;
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public List<FieldType> FieldTypes
    {
        get { return fieldTypes; }
        set { fieldTypes = value; }
    }

    public List<Item> Items
    {
        get { return items; }
        set { items = value; }
    }

}
