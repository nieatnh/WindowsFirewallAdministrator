using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FirewallEngine
{
    public class CommandLineFirewall
    {
        public CommandLineFirewall()
        { 
        
        }

        public CommandResult GetCommandLineRules(string name = "all")
        {
            string command = "netsh advfirewall firewall show rule name={0}";
            var result = CommandLine.RunCommand(command, name);            
            return result;
        }

        public List<FirewallRule> GetRules(string name = "all")
        {
            var commandResult = this.GetCommandLineRules(name);
            return _ParseRules(commandResult.Output);
        }

        private List<FirewallRule> _ParseRules(string result)
        {
            var lines = result.Split('\n');
            lines = lines.Select(x => x.Replace("\r", "")).ToArray();
            Dictionary<string, string> values = null;
            List<FirewallRule> rules = new List<FirewallRule>();
            string lastKey = "";
            string pattern = "([A-Za-z0-9\\s]+):(.+)";
            Regex matcher = new Regex(pattern);
            string currentLine = "";

            for (int i = 0; i < lines.Length; i++)
            {
                currentLine = lines[i];
                if (currentLine.StartsWith("---")) continue;

                if (currentLine == "Ok." || currentLine == "")
                {
                    if (values != null)
                        rules.Add(new FirewallRule(values));
                    values = new Dictionary<string, string>();
                }
                else
                {
                    var matched = matcher.Match(currentLine);
                    if (matched.Success && matched.Index == 0)
                        lastKey = _AddItem(values, currentLine);
                    else
                        values[lastKey] += currentLine;
                }
            }
            return rules;
        }

        private string _AddItem(Dictionary<string, string> values, string line)
        {
            string[] items = line.Split(':');
            values.Add(items[0].Trim(), items[1].Trim());
            return items[0];
        }
    }
}
