using System.Diagnostics;

namespace ShadowClassLibrary
{
    public class PopRDP
    {

        static AdminCreds AdminCreds = new AdminCreds();

        public static void Run(string sessionID, string computerName)
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
                    FileName = @"C:\Windows\System32\cmd.exe",
                    WorkingDirectory = @"C:\Windows\System32",
                    Arguments = @"/C Mstsc /shadow:" + sessionID + @" /v:" + computerName + @" /Control /noConsentPrompt",
                    Verb = "RUNAS",
                    WindowStyle = ProcessWindowStyle.Hidden

                };

                cmd.Start();
                cmd.WaitForExit(10000);
            }
        }

    }
}
