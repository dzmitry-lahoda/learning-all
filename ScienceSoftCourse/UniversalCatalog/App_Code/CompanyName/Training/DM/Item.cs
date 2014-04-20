using System;
using System.Collections.Generic;
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

/// <summary>
/// 
/// </summary>
[Serializable]
public class Item
{

    private int id;
    private String name;
    private List<Field> fields=new List<Field>();
    private int itemTypeId;

    public Item(int id, string name, int itemTypeId)
    {
        this.id = id;
        this.name = name;
        this.itemTypeId = itemTypeId;
    }

    public Item(string name, int itemTypeId)
    {
        this.name = name;
        this.itemTypeId = itemTypeId;
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

    public int ItemTypeId
    {
        get { return itemTypeId; }
        set { itemTypeId = value; }
    }

    public List<Field> Fields
    {
        get { return fields; }
        set { fields = value; }
    }

    public void AddField(Field field) {
        Fields.Add(field);
    }

}
