using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Policy;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Image = System.Web.UI.WebControls.Image;

public partial class ManageItemsPage : System.Web.UI.Page
{

    protected List<FieldType> FieldTypes
    {
        get
        {
            return (List<FieldType>)ViewState["FieldTypes"];
        }
        set
        {
            ViewState.Add("FieldTypes", value);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //var item = CreateItem();
        var control = (FileUpload)PlaceHolderFields.FindControl("Control12");
        var value = ViewState["GridViewItemTypes.SelectedIndex"];
        if (value != null)
        {
            PanelEdit.Visible = true;
            FieldTypes = FieldTypeDAO.GetFieldTypesForItemTypeById((int)ViewState["GridViewItemTypes.SelectedValue"]);

            FieldTypes = FieldTypeDAO.GetFieldTypesForItemTypeById((int)GridViewItemTypes.SelectedValue);
            AddControls(false);
        }
    }


    private Item CreateItem()
    {
        var item = new Item(TextBoxName.Text, (int)GridViewItemTypes.SelectedValue);
        for (var i = 0; i < FieldTypes.Count; i++)
        {
            Field field = null;
            if (FieldTypes[i].TypeOfField == TypeOfField.Uri)
            {
                var control = (FileUpload)PlaceHolderFields.FindControl("Control" + FieldTypes[i].Id);
                var image = System.Drawing.Image.FromStream(control.PostedFile.InputStream);
                //field = new Field(item.Id, FieldTypes[i].Id, Utility.ContentToBinary(image));
            }
            else
            {
                var control = (TextBox)PlaceHolderFields.FindControl("Control" + FieldTypes[i].Id);
                //field = new Field(item.Id, FieldTypes[i].Id, Utility.ContentToBinary(control.Text));
            }
            item.AddField(field);
        }
        return item;
    }

    protected void GridViewItemTypes_SelectedIndexChanged(object sender, EventArgs e) {
        var value = GridViewItemTypes.SelectedIndex;
        if (value > -1) {
            if (ViewState["GridViewItemTypes.SelectedIndex"] != null &&
                GridViewItemTypes.SelectedIndex == (int) ViewState["GridViewItemTypes.SelectedIndex"]) {
                return;
            }
            else {
                PanelEdit.Visible = true;
                ViewState.Add("GridViewItemTypes.SelectedIndex", GridViewItemTypes.SelectedIndex);
                ViewState.Add("GridViewItemTypes.SelectedValue", GridViewItemTypes.SelectedValue);
                FieldTypes = FieldTypeDAO.GetFieldTypesForItemTypeById((int) GridViewItemTypes.SelectedValue);
                AddControls(true);
            }
        }
    }

    private void AddControls(bool clear)
    {
        if (clear)
        {
            PlaceHolderFields.Controls.Clear();
        }
        foreach (var fieldType in FieldTypes)
        {
            var name = new Label { Text = String.Format("{0}({1})", fieldType.Name, fieldType.TypeOfField) };
            var editControl = new TextBox { ID = "Control" + fieldType.Id };
            PlaceHolderFields.Controls.Add(new Literal { Text = "<br/>" });
            PlaceHolderFields.Controls.Add(name);
            PlaceHolderFields.Controls.Add(editControl);
        }
        PlaceHolderFields.Controls.Add(new Literal { Text = "<br/>" });
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        var item = new Item(TextBoxName.Text, (int)GridViewItemTypes.SelectedValue);
        for (var i = 0; i < FieldTypes.Count; i++)
        {
            Field field = null;
            if (FieldTypes[i].TypeOfField == TypeOfField.Uri)
            {
                var control = (TextBox)PlaceHolderFields.FindControl("Control" + FieldTypes[i].Id);
                Uri uri;
                if (!Uri.TryCreate(control.Text, UriKind.Absolute,out uri))
                {
                    throw new ArgumentException(String.Format("Wrong uri string \"{0}\"", control.Text));
                }
                field = new Field(item.Id, FieldTypes[i].Id, Utility.ContentToXml(uri));
            }
            else
            {
                var control = (TextBox)PlaceHolderFields.FindControl("Control" + FieldTypes[i].Id);
                field = new Field(item.Id, FieldTypes[i].Id, Utility.ContentToXml(control.Text));
            }
            item.AddField(field);
        }
        ItemDAO.AddItemWithFields(item);
        PlaceHolderFields.Controls.Clear();
        PanelEdit.Visible = false;
        ViewState.Remove("GridViewItemTypes.SelectedIndex");
        ViewState.Remove("GridViewItemTypes.SelectedValue");
    }

    protected void ButtonUpload_Click(object sender, EventArgs e)
    {
        var destDir = Server.MapPath("./Images");
        var fileName = Path.GetFileName(FileUpload.PostedFile.FileName);
        var destPath = Path.Combine(destDir, fileName);
        FileUpload.PostedFile.SaveAs(destPath);
        LabelUri.Text = destPath;
    }
}
