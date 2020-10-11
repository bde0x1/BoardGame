using BoardGame.BusinessLogics;
using System;
using System.IO;
using System.Windows.Forms;

namespace BoardGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //if (File.Exists(Path.GetFullPath("keys.xml")))
            //{
            //    if (File.Exists(Path.GetFullPath("run.bat")))
            //    {
            //        MessageBox.Show("Key is Ready!");
            //    }
            //    else
            //    {
            //        StreamWriter BatWriter = new StreamWriter("run.bat");
            //        BatWriter.WriteLine("c:");
            //        BatWriter.WriteLine(@"cd C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319");
            //        BatWriter.WriteLine("aspnet_regiis.exe -pi \"MyKeys\" " + Path.GetFullPath("keys.xml"));
            //        BatWriter.WriteLine("aspnet_regiis.exe -pa \"MyKeys\" \"NT AUTHORITY\\NETWORK\"");
            //        BatWriter.Flush();
            //        BatWriter.Close();

            //        System.Diagnostics.Process process = new System.Diagnostics.Process();
            //        process.EnableRaisingEvents = false;
            //        process.StartInfo.FileName = @"run.bat";
            //        process.Start();
            //        MessageBox.Show("Key was imported.");
            //    }                
            //}
            //else
            //{
            //    MessageBox.Show("keys.xml does not exist in this path: " + Path.GetFullPath("keys.xml"));
            //}
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NavigationMenu());          
        }
    }
}
