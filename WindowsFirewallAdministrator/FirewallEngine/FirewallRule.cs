using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace FirewallEngine
{
    public class FirewallRule
    {
        public enum EDirection { In, Out };
        public enum EProfiles { Domain, Private, Public }
        public enum EProtocol { TCP, UDP }
        public enum EAction { Allow, Block}

        [DescriptionAttribute("Rule Name")]
        public string RuleName { get; set; }
        [DescriptionAttribute("Enabled")]
        public bool Enabled { get; set; }
        [DescriptionAttribute("Direction")]
        public EDirection Direction { get; set; }
        [DescriptionAttribute("Profiles")]
        public EProfiles[] Profiles { get; set; }
        [DescriptionAttribute("Grouping")]
        public string Grouping { get; set; }
        [DescriptionAttribute("LocalIP")]
        public string LocalIp { get; set; }
        [DescriptionAttribute("RemoteIP")]
        public string RemoteIp { get; set; }
        [DescriptionAttribute("Protocol")]
        public string Protocol { get; set; }
        [DescriptionAttribute("LocalPort")]
        public int LocalPort { get; set; }
        [DescriptionAttribute("RemotePort")]
        public int RemotePort { get; set; }
        [DescriptionAttribute("Edge traversal")]
        public bool EdgeTransversal { get; set; }
        [DescriptionAttribute("Action")]
        public EAction Action { get; set; }

        public FirewallRule(Dictionary<string, string> values)
        {
            Type type = typeof(FirewallRule);
            var properties = type.GetProperties();
            DescriptionAttribute descriptionAttribute;
            string[] arrayItems;
            foreach (var property in properties)
            {
                descriptionAttribute = (DescriptionAttribute)property.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
                if (values.ContainsKey(descriptionAttribute.DescriptionName))
                {
                    if (property.PropertyType.IsEnum)
                    {
                        property.SetValue(this, Tools.ParseType(property.PropertyType)[values[descriptionAttribute.DescriptionName]]);
                    }
                    else if (property.PropertyType.IsArray)
                    {
                        arrayItems = this._CleanString(values[descriptionAttribute.DescriptionName]);
                        Array items = Array.CreateInstance(property.PropertyType.GetElementType(), arrayItems.Length);
                        for(int i=0;i<arrayItems.Length;i++)
                        {
                            if (property.PropertyType.GetElementType().IsEnum)
                            {
                                items.SetValue(Tools.ParseType(property.PropertyType.GetElementType())[arrayItems[i]], i);
                            }
                            else
                            {
                                items.SetValue(arrayItems[i], i);
                            } 
                        }

                    }
                    else if (property.PropertyType.Equals(typeof(bool)))
                    {
                        property.SetValue(this, values[descriptionAttribute.DescriptionName] == "Yes");
                    }
                    else if (property.PropertyType.Equals(typeof(int)))
                    {
                        int convert;
                        if (int.TryParse(values[descriptionAttribute.DescriptionName], out convert))
                            property.SetValue(this, convert);
                    }
                    else
                    {
                        property.SetValue(this, values[descriptionAttribute.DescriptionName]);
                    }
                }
            }
        }

        private string[] _CleanString(string line)
        {
            line = line.Trim();
            var lines = line.Split(',');
            for (int i = 0; i < lines.Length; i++)
                lines[i] = lines[i].Trim();
            return lines;
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class DescriptionAttribute : Attribute
        {
            string descriptionName;
            public string DescriptionName { get { return descriptionName; } }
            public DescriptionAttribute(string descriptionName)
            {
                this.descriptionName = descriptionName;
            }
        }
    }
}
