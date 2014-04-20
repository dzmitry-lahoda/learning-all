using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Resources;

public partial class ManageTypesPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e){

    }
    protected void GridViewItemTypes_SelectedIndexChanged(object sender, EventArgs e){

    }

    protected void DetailsViewFieldType_ItemInserting(object sender, DetailsViewInsertEventArgs e){
        e.Values["ItemTypeId"] = (int)GridViewItemTypes.SelectedValue;
        var typeOfField = (DropDownList) DetailsViewFieldType.FindControl("DropDownListFieldType");
        e.Values["TypeOfField"] = typeOfField.SelectedItem.Value;
    }
    protected void ObjectDataSourceFieldTypes_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {

    }
    protected void ObjectDataSourceFieldTypes_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.InputParameters["itemTypeId"] == null) e.Cancel = true;
    }
    protected void GridViewItemTypes_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void GridViewItemTypes_RowCreated(object sender, GridViewRowEventArgs e)
    {
        GridViewItemTypes.SelectedIndex = GridViewItemTypes.Rows.Count-1;
    }
}
