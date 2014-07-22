using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace UpVer
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new Settings(args);
            var filepath = settings.File ?? ConfigurationManager.AppSettings["File"];
            var config = XDocument.Load(filepath);
            if (settings.Major)
            {
                Inc(config, "major");
                Set(config, "minor", 0);
                Set(config, "patch", 0);
            } else if (settings.Minor)
            {
                Inc(config, "minor");
                Set(config, "patch", 0);
            } else if (settings.Patch)
            {
                Inc(config, "patch");
            }
            config.Save(filepath);
        }

        private static void Inc(XDocument config, string part)
        {
            var elem = GetProperty(config, "version." + part);
            var val = int.Parse(elem.Attribute("value").Value);
            Set(elem, ++val);
        }

        private static void Set(XDocument config, string part, int val)
        {
            var elem = GetProperty(config, "version." + part);
            Set(elem, val);
        }

        private static void Set(XElement elem, int val)
        {
            elem.Attribute("value").SetValue(val);
        }

        private static XElement GetProperty(XDocument config, string name)
        {
            return config.Element("project").Elements("property").Single(x => x.Attribute("name").Value == name);
        }
    }
}
