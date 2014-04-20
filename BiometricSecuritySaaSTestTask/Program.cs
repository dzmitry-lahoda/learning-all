using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace photoindex
{
    class Program
    {
        static void Main(string[] args)
        {
            var namesfile = "names.txt";
            var photofile = "photos.txt";
            if (!File.Exists(namesfile) || !File.Exists(photofile))
                return;

            var names = File.ReadAllLines(namesfile);
            var validNames = new Dictionary<int,string>();
			var pattern = @"^([1-9]+)\.\t([a-zA-Z0-9_\\s]*)$"; 
            
            foreach (var name in names)
            {
			    var match = Regex.Match(name, pattern);//compile above
                if (match.Success && match.Groups.Count == 3)
                {
                    validNames[int.Parse(match.Groups[1].Value)] = match.Groups[2].Value;
                }
            }

            var photos = File.ReadAllLines(photofile);
            var validPhotos = new List<string[]>();
            foreach (var photo in photos)
            {
                var items = photo.Split('\t');
                if (items.Length < 2 || !items[0].Contains('.') )
                    continue;
                for (int i = 1; i < items.Length; i++)
                {
                    int value;
                    bool parsed = int.TryParse(items[i],out  value);
                    if (parsed)
                    {
                        string name;
                        bool has = validNames.TryGetValue(value, out name);
                        if (has)
                        {
                            items[i] = name;
                        }
                    }
                }
                validPhotos.Add(items);
            }

            using (var result = File.CreateText("photos-final.txt"))
			{
                foreach (var photo in validPhotos)
                {
                    for (int i = 0; i < photo.Length-1; i++)
                    {
                        result.Write(photo[i]);result.Write('\t');
                    }
                    result.WriteLine(photo.Last());
                }
                result.Flush();
			}
            Console.WriteLine("photos-final.txt");
            Console.ReadKey();
        
            
        }
    }
}
