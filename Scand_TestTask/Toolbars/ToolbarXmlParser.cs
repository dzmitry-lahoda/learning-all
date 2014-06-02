using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Scand.Toolbars
{
    /// <summary>
    /// Provides static methods for parsing Xml file and and generating toolbear buttons.
    /// </summary>
    internal static class ToolbarXmlParser
    {
        /// <summary>
        ///  Gets btoolbar buttons by parsing Xml file, getting it using Uri
        /// </summary>
        /// <param name="xmlUri">Uri to Xml with description of toolbar.</param>
        /// <returns></returns>
        public static List<ToolbarButton> GetToolbarButtons(Uri xmlUri)
        {
            var resolver = new XmlUrlResolver();
            using (var stream = (Stream)resolver.GetEntity(xmlUri, null, typeof(Stream)))
            {
                var document = new XPathDocument(stream);
                var nav = document.CreateNavigator();
                var iter = nav.Select("/toolbar/button");
                var buttons=Iterate(iter, xmlUri);
                return buttons;
            }
        }

        /// <summary>
        /// Generates list of ToolbarButton iterating through Xml file using <see cref="XPathNodeIterator"/>.
        /// </summary>
        /// <param name="iterator"></param>
        /// <param name="uri">Uri to Xml file. Used for throwing exceptions only.</param>
        /// <returns></returns>
        public static List<ToolbarButton> Iterate(XPathNodeIterator iterator, Uri uri)
        {
            var buttons = new List<ToolbarButton>();
            while (iterator.MoveNext())
            {
                var id = iterator.Current.GetAttribute("id", "");
                if (!IsValidId(id))
                {
                    throw new WrongXmlDataException("Id is not valid.", uri.AbsoluteUri, iterator.CurrentPosition, "id", id);
                }
                var hasid = ContainsId(buttons, id);
                if (hasid)
                {
                    throw new WrongXmlDataException("Dublicate button's id.", uri.AbsoluteUri, iterator.CurrentPosition, "id", id);
                }
                var imgString = iterator.Current.GetAttribute("img", "");
                var text = iterator.Current.GetAttribute("text", "");
                if (String.IsNullOrEmpty(imgString) && String.IsNullOrEmpty(text))
                {
                    throw new WrongXmlDataException("Image URI and text for button can't be absent or empty simultaniously.", uri.AbsoluteUri, iterator.CurrentPosition);
                }
                Uri imgUri = null;
                if (!String.IsNullOrEmpty(imgString))
                {
                    if (!Uri.IsWellFormedUriString(imgString, UriKind.RelativeOrAbsolute))
                    {
                        throw new WrongXmlDataException("Image URI for button is not well formed.", uri.AbsoluteUri, iterator.CurrentPosition, "img", imgString);
                    }
                    imgUri = new Uri(imgString);
                }
                var button = new ToolbarButton { Name = id, Text = text, ImageUri = imgUri };
                buttons.Add(button);
            }
            return buttons;
        }

        /// <summary>
        /// Returns true if id can be used as button id(is not empty string or null).
        /// </summary>
        /// <param name="buttonId"></param>
        /// <returns></returns>
        public static bool IsValidId(string buttonId)
        {
            var isvalid = !String.IsNullOrEmpty(buttonId);
            return isvalid;
        }

        /// <summary>
        /// Returns true, if collection of buttons contains button with specified id.
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="buttonId"></param>
        /// <returns></returns>
        public static bool ContainsId(IEnumerable<ToolbarButton> buttons, string buttonId)
        {
            foreach (var button in buttons)
            {
                if (button.Name == buttonId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
