using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirewallEngine
{
    public class CommandResult
    {
        public string Output { get; set; }
        public int ExitCode { get; set; }
        public string Error { get; set; }
        public double EllapsedMilliseconds { get; set; }
    }
}
