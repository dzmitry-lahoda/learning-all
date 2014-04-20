using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using Common;

namespace Client
{
    public class PluginManager:MarshalByRefObject
    {
        private Dictionary<String, Type> plugins = new Dictionary<String,Type>();

        private bool IsClrImage(String fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var dat = new byte[300];
                stream.Read(dat, 0, 128);
                if ((dat[0] != 0x4d) || (dat[1] != 0x5a)) // "MZ" DOS header 
                {
                    return false;
                }
                var lfanew = BitConverter.ToInt32(dat, 0x3c);
                stream.Seek(lfanew, SeekOrigin.Begin);
                stream.Read(dat, 0, 24); // read signature & PE file header 
                if ((dat[0] != 0x50) || (dat[1] != 0x45)) // "PE" signature 
                {
                    return false;
                }
                stream.Read(dat, 0, 96 + 128); // read PE optional header 
                if ((dat[0] != 0x0b) || (dat[1] != 0x01)) // magic 
                {
                    return false;
                }
                var clihd = BitConverter.ToInt32(dat, 208); // get IMAGE_COR20_HEADER rva-address 
                return clihd != 0;
            }
        }

        public List<String> GetPlugins(String path)
        {
            var files = Directory.GetFiles(path, "*.dll");
            foreach (var file in files)
            {
                if (IsClrImage(file))
                {
                    var assembly = Assembly.LoadFile(file);
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        if (type.FindInterfaces(Filter, "Common.IPlugin").Length > 0)
                        {
                            plugins.Add(type.FullName, type);
                        }
                    }
                }
            }
            var keys = new List<String>();
            foreach (var pair in plugins)
            {
                keys.Add(pair.Key);
            }
            return keys;
        }

        public static bool Filter(Type m, Object filterCriteria)
        {
            return (m.ToString() == filterCriteria.ToString());
        }


        public void Execute(String source,String destination,String pluginName)
        {
            IPlugin plugin;
            try
            {
                plugin = (IPlugin) Activator.CreateInstance(plugins[pluginName]);
            }
            catch(KeyNotFoundException ex)
            {
                throw new ArgumentException(String.Format("Plugin with pluginName=\'{0}\' not found. Use \'PluginManager.GetPlugins\' to get available plugins.", pluginName), ex);
            }
            plugin.Move(source, destination);
        }
    }
}
