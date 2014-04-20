using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var itemTypes = ItemTypeDAO.GetAllItemTypes();
        foreach (var itemType in itemTypes) {
            var items = ItemDAO.GetItemWithFieldsByItemTypeId(itemType.Id);
            var fieldTypes = FieldTypeDAO.GetFieldTypesForItemTypeById(itemType.Id);
            foreach (var item in items)
            {
                PlaceHolderItems.Controls.Add(new Literal { Text = "<hr/>" });
                PlaceHolderItems.Controls.Add(new Label { Text = item.Name});
                PlaceHolderItems.Controls.Add(new Literal { Text = "<br/>" });
                foreach (var field in item.Fields)
                {
                   // var content=Utility.BinaryToContent(field.Content);
                    //if (content is System.Drawing.Image) {
                        var directory=Server.MapPath("./Images");
                        var path = Path.Combine(directory, Guid.NewGuid().ToString());
                       // (content as System.Drawing.Image).Save(path);
                        var image = new Image { ImageUrl = path };
                        PlaceHolderItems.Controls.Add(image);
                    //}
                    //else {
                       // PlaceHolderItems.Controls.Add(new Label { Text = content.ToString() });
                    //}
                }
                PlaceHolderItems.Controls.Add(new Literal { Text = "<hr/>" });
            }  
        }

    }
}
