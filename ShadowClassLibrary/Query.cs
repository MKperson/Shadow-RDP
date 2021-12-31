using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ShadowClassLibrary
{
    public class Query
    {
        AdminCreds AdminCreds = new AdminCreds();
        private string output;
        private List<string[]> Loutput;
        private string compName;

        public Query(string computerName)
        {
            compName = computerName;
            Run(@" /server:" + computerName);
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
                    UserName = AdminCreds.Username,
                    Password = AdminCreds.Password,
                    CreateNoWindow = true,
                    FileName = @"C:\Windows\System32\quser.exe",
                    WorkingDirectory = @"C:\Windows\System32",
                    Arguments = cstr,
                    Verb = "RUNAS",
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                cmd.Start();
                cmd.WaitForExit(10000);
                if (!cmd.HasExited)
                {
                    cmd.Kill();
                    cmd.Dispose();

                    return;

                }
                if (cmd.StandardOutput != null)
                {
                    output = cmd.StandardOutput.ReadToEnd();
                }

            }

        }

        private void RawtoList()
        {
            Loutput = new List<string[]>();

            if (!string.IsNullOrEmpty(output))
            {
                using (TextFieldParser parser =
                    new TextFieldParser(new StringReader(output)))
                {
                    parser.TextFieldType = FieldType.FixedWidth;
                    //USERNAME, SESSIONNAME, ID, STATE, IDLE TIME, LOGON TIME
                    parser.SetFieldWidths(23, 19, 4, 8, 11, -1);
                    parser.ReadLine();
                    while (!parser.EndOfData)
                    {

                        string[] row = new string[] { compName }.Concat(parser.ReadFields()).ToArray();


                        Loutput.Add(row);
                    }

                }
            }
        }
        public string Output { get { return output; } }
        public List<string[]> ListUsers { get { return Loutput; } }
    }
}
