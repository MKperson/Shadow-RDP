using System.Diagnostics;
using System.Windows.Forms;

namespace ShadowClassLibrary
{
    public class LogOffUser
    {
        static readonly AdminCreds AdminCreds = new AdminCreds();

        public static void LogOff(ListViewItem Item)
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
                    UserName = AdminCreds.Username,
                    Password = AdminCreds.Password,
                    FileName = @"C:\Windows\System32\cmd.exe",
                    WorkingDirectory = @"C:\Windows\System32",
                    Arguments = @"/C logoff " + Item.SubItems[3].Text + @" /server:" + Item.SubItems[0].Text,
                    Verb = "RUNAS",
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                cmd.Start();
                cmd.WaitForExit();
            }
        }
    }
}
