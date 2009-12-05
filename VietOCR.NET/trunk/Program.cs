using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VietOCR.NET
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                ConsoleApp.PerformOCR(args);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new GUIWithTools());
            }
        }
    }
}