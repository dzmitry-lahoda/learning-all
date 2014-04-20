using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Itransition.Training.Task02
{
    /// <summary>
    /// Represents a class which provides method of phonesearching
    /// </summary>
    public static class PhoneSearcher
    {
        /// <summary>
        /// Searches phone in phonebook by surname
        /// </summary>
        /// <param name="filename">Path to the phonebook</param>
        /// <param name="surname">Surname of a person which phone should be found</param>
        /// <returns>String array of phones</returns>
        public static String[] SearchPhonebySurname(String filename, String surname)
        {
            String xpath = String.Format(@"/phonebook/entry[translate(surname, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='{0}']", surname.ToLower());
            XPathDocument document;
            XPathNodeIterator nodes;
            document = new XPathDocument(filename);
            XPathNavigator navigator = document.CreateNavigator();
            nodes = navigator.Select(xpath);
            List<String> numbers = new List<String>();
            if (nodes != null && nodes.Count != 0)
            {

                while (nodes.MoveNext())
                {
                    XPathNodeIterator numbernode = nodes.Current.Select("number");
                    numbernode.MoveNext();
                    numbers.Add(numbernode.Current.Value);
                }
            }
            if (numbers.Count == 0)
            {
                numbers.Add("Phone not found");
            }
            return numbers.ToArray();
        }
    }
}
