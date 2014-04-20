using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.XPath;
using System.Xml;

namespace Itransition.Training.Task06
{
    /// <summary>
    /// Static class which provide a possibility to change language
    /// </summary>
    public static class Language
    {
        private static String currentName="language";
 
        private class NameText
        {
            public String Name;
            public String Text;
        }

        /// <summary>
        /// Gets current language name
        /// </summary>
        public static string CurrentName
        {
            get { return currentName; }
        }

        #region Methods which provide possibility to save current state of texts on the form
        private static void AddNameText(List<NameText> nametextList, String Name,String Text)
        {
            NameText nt = new NameText();
            nt.Name = Name;
            nt.Text = Text;
            nametextList.Add(nt);
        }
        private static void AddControls(Control parent, List<NameText> nametextList)
        {

            if (parent != null)
            {
                Control.ControlCollection Controls = parent.Controls;
                foreach (Control c in Controls)
                {
                    AddNameText(nametextList, c.Name, c.Text);
                    if (c is ToolStrip)
                    {
                        AddToolStripItems((ToolStrip)c, nametextList);
                    }
                    AddControls(c, nametextList);
                }
            }
        }
        private static void AddToolStripItems(ToolStrip current, List<NameText> nametextList)
        {
            for (int i = 0; i < current.Items.Count; i++)
            {
                AddNameText(nametextList, current.Items[i].Name, current.Items[i].Text);
            }

        }

        /// <summary>
        /// Saves all Controls and ToolStripMenuItems Name and Text properties to XML file
        /// </summary>
        /// <param name="name">Language name</param>
        /// <param name="parent">Parent Control(Form)</param>
        /// <exception cref="System.Xml.XmlException"></exception>
        public static void Serialize(String name,Control parent)
        {
            List<NameText> NameTextList = new List<NameText>();
            AddNameText(NameTextList, parent.Name, parent.Text);
            AddControls(parent, NameTextList);

            XmlDocument document = new XmlDocument();
            XmlProcessingInstruction instruction =
                document.CreateProcessingInstruction("xml", "version=\"1.0\"  encoding=\"UTF-8\"");
            document.AppendChild(instruction);

            XmlElement elementLanguage = document.CreateElement("Language");
            document.AppendChild(elementLanguage);

            XmlElement elementLanguageName = document.CreateElement("LanguageName");
            elementLanguage.AppendChild(elementLanguageName);
            XmlText textLanguageName = document.CreateTextNode(name);
            elementLanguageName.AppendChild(textLanguageName);

            XmlElement elementNameTextList = document.CreateElement("NameTextList");
            elementLanguage.AppendChild(elementNameTextList);

            for (int i = 0; i < NameTextList.Count; i++)
            {
                XmlElement elementNameText = document.CreateElement("NameText");
                elementNameTextList.AppendChild(elementNameText);

                XmlElement elementName = document.CreateElement("Name");
                elementNameText.AppendChild(elementName);
                XmlText textName = document.CreateTextNode(NameTextList[i].Name);
                elementName.AppendChild(textName);

                XmlElement elementText = document.CreateElement("Text");
                elementNameText.AppendChild(elementText);
                XmlText textText = document.CreateTextNode(NameTextList[i].Text);
                elementText.AppendChild(textText);
            }
            document.Save(name + ".xml");
        }
        #endregion

        #region Methods which provide possibility to change language
        private static String SetNameText(List<NameText> nametextList, String Name,  String Text)
        {
            foreach (NameText nt in nametextList)
            {
                if (Name == nt.Name)
                {
                    return nt.Text;
                }
            }
            return Text;
        }        
        private static void SetControls(Control parent, List<NameText> nametextList)
        {
            if (parent != null)
            {
                Control.ControlCollection Controls = parent.Controls;
                foreach (Control c in Controls)
                {
                    c.Text = SetNameText(nametextList, c.Name, c.Text);
                    if (c is ToolStrip)
                    {
                        SetToolStripItems((ToolStrip)c, nametextList);
                    }
                    SetControls(c, nametextList);
                }
            }
        }
        private static void SetToolStripItems(ToolStrip current, List<NameText> nametextList)
        {
            for (int i = 0; i < current.Items.Count; i++)
            {
                current.Items[i].Text=SetNameText(nametextList, current.Items[i].Name, current.Items[i].Text);
            }

        }

        /// <summary>
        /// Restore all Controls and ToolStripMenuItems Text properties from XML file
        /// </summary>
        /// <param name="name">Language name</param>
        /// <param name="parent">Parent Control(Form)</param>
        /// <exception cref="System.Xml.XPath.XPathException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        public static void Deserialize(String name, Control parent)
        {
            List<NameText> NameTextList = new List<NameText>();
            XPathDocument document = new XPathDocument(name + ".xml");
            XPathNavigator navigator = document.CreateNavigator();
            navigator.SelectSingleNode(@"/Language/LanguageName").MoveToFirst();
            currentName = navigator.SelectSingleNode(@"/Language/LanguageName").Value;
            String xpath = String.Format(@"/Language/NameTextList/NameText");
            XPathNodeIterator nodes = navigator.Select(xpath);
            if (nodes != null && nodes.Count != 0)
            {
                while (nodes.MoveNext())
                {
                    NameText nt = new NameText();
                    XPathNodeIterator node = nodes.Current.Select("Name");
                    node.MoveNext();
                    nt.Name = node.Current.Value;
                    node = nodes.Current.Select("Text");
                    node.MoveNext();
                    nt.Text = node.Current.Value;
                    NameTextList.Add(nt);
                }
            }
            parent.Text = SetNameText(NameTextList, parent.Name, parent.Text);
            SetControls(parent, NameTextList);
        }
        #endregion

    }
}
