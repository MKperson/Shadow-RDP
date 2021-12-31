using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ShadowClassLibrary
{
    public class Qwinsta
    {
        private string output;
        private List<string[]> Loutput;


        public Qwinsta(string computerName)
        {
            Run(@"/server:" + computerName);
            RawtoList();
        }

        private void Run(string cstr)
        {
            using (Process cmd = new Process())
            {
                // set start info
                cmd.StartInfo = new ProcessStartInfo()
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = @"C:\Windows\System32\qwinsta.exe",
                    WorkingDirectory = @"C:\Windows\System32",
                    Arguments = cstr,
                    Verb = "RUNAS",
                    WindowStyle = ProcessWindowStyle.Hidden

                };

                cmd.Start();
                cmd.WaitForExit();
                output = cmd.StandardOutput.ReadToEnd();
            }

        }

        private void RawtoList()
        {
            Loutput = new List<string[]>();

            if (!string.IsNullOrEmpty(output))
            {
                var op = output.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var cmdline in op)
                {
                    if (cmdline.ToUpper().Contains("ACTIVE"))
                    {
                        string line = cmdline.Trim();
                        while (line.Contains("  ")) { line = line.Replace("  ", " "); }
                        string[] items = line.Split();
                        items[0] = items[0].ToUpper();
                        Loutput.Add(items);

                    }

                }

            }
        }
        public string Output { get { return output; } }
        public List<string[]> Active { get { return Loutput; } }
    }
}
