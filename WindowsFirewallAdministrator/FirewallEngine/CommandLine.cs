using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirewallEngine
{
    class CommandLine
    {        
        public static CommandResult RunCommand(string command, params object[] values)
        {
            StreamWriter inputStream;
            StreamReader errorStream, outputStream;

            Process p = new Process();
            string finalCommand = String.Format(command, values);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            DateTime start;
            Console.WriteLine(finalCommand);
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + finalCommand;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;

            p.StartInfo = startInfo;

            string output = "";

            start = DateTime.Now;
            p.Start();

            inputStream = p.StandardInput;
            outputStream = p.StandardOutput;
            errorStream = p.StandardError;

            while (!outputStream.EndOfStream)
            {
                output += outputStream.ReadToEnd();
            }

            p.WaitForExit();

            
            CommandResult result = new CommandResult()
            {
                EllapsedMilliseconds = (DateTime.Now- start).TotalMilliseconds,
                Error = errorStream.ReadToEnd(),
                ExitCode = p.ExitCode,
                Output = output
            };

            inputStream.Dispose();
            outputStream.Dispose();
            errorStream.Dispose();
            p.Dispose();

            return result;
        }

    }
}
