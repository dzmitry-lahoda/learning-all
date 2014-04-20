using System;
using System.Xml;

public partial class Task01_Default : System.Web.UI.Page
{

    protected void Page_PreLoad(object sender, EventArgs e)
	{
        Page.Form.DefaultButton = ButtonSaveToFile.ID;
	}

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.RegisterRequiresViewStateEncryption();
    }
    protected void ButtonSaveToFile_Click(object sender, EventArgs e)
    {
        XmlTextWriter writer = new XmlTextWriter(Request.PhysicalApplicationPath + @"Task01\userinfo.xml", System.Text.Encoding.UTF8);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartDocument(true);
        writer.WriteStartElement("user");
        writer.WriteAttributeString("login",LoginTextBox.Text);
        writer.WriteElementString("email", EmailTextBox1.Text);
        writer.WriteElementString("password", PasswordTextBox1.Text);
        writer.WriteElementString("text", TextTextBox.Text);

        writer.WriteEndElement();
        writer.WriteEndDocument();

        writer.Flush();
        writer.Close();


    }
}
