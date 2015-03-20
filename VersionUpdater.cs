using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace UpVer
{
    public class VersionUpdater
    {
        private readonly Settings _settings;
        private readonly string _filepath;
        private readonly XDocument _config;

        public VersionUpdater(Settings settings)
        {
            _settings = settings;
            _filepath = settings.File ?? ConfigurationManager.AppSettings["File"];
            _config = XDocument.Load(_filepath);
        }

        public Changes Process()
        {
            var changes = new Changes();
            if (_settings.Major)
            {
                changes.Major = Inc("major");
                changes.Minor = Set("minor", 0);
                changes.Patch = Set("patch", 0);
            } else if (_settings.Minor)
            {
                changes.Major = Changes.NoChange(Get("major"));
                changes.Minor = Inc("minor");
                changes.Patch = Set("patch", 0);
            } else if (_settings.Patch)
            {
                changes.Major = Changes.NoChange(Get("major"));
                changes.Minor = Changes.NoChange(Get("minor"));
                changes.Patch = Inc("patch");
            }
            _config.Save(_filepath);
            return changes;
        }

        private Change Inc(string part)
        {
            var val = Get(part);
            var change = new Change
            {
                From = val,
                To = ++val
            };
            Set(part, change.To);
            return change;
        }

        private int Get(string part)
        {
            var elem = GetProperty("version." + part);
            return Get(elem.Attribute("value"));
        }

        private int Get(XAttribute attr)
        {
            return int.Parse(attr.Value);
        }

        private Change Set(string part, int val)
        {
            var elem = GetProperty("version." + part);
            return Set(elem, val);
        }

        private Change Set(XElement elem, int val)
        {
            var attr = elem.Attribute("value");
            var change = new Change { From = Get(attr), To = val };
            attr.SetValue(val);
            return change;
        }

        private IEnumerable<XElement> _properties = null;
        private XElement GetProperty(string name)
        {
            if (_properties == null) _properties = _config.Element("project").Elements("property");
            return _properties.Single(x => x.Attribute("name").Value == name);
        }
    }
}