using System;

namespace Itransition.Training.Task02
{
    class Program
    {
        static void Main()
        {
            String filename = "phonebook.xml";
            String surname = "Dirty";
            Console.WriteLine(@"Type phonebook filename (default ""{0}""):", filename);
            String tmp = Console.ReadLine();
            if (tmp.Length>0)
            {
                filename = tmp;
            }
            Console.WriteLine(@"Type surname (default ""{0}""):", surname);
            tmp=Console.ReadLine();
            if (tmp.Length>0)
            {
                surname = tmp;
            }
            String[] foundnumbers = new String[] { };
            try
            {
                foundnumbers = PhoneSearcher.SearchPhonebySurname(filename, surname);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            foreach (String s in foundnumbers)
            {
                Console.WriteLine(s);
            }
            Console.ReadLine();
        }
 
    }
  
}
