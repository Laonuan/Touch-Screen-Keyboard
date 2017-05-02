using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Num
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Mutex mutex = new Mutex(false, Application.ProductName);
            
            if (mutex.WaitOne(0, false))
            {
                if (args.Length > 1)
                {
                    Application.Run(new Form1(args));
                }
                else
                {
                    Application.Run(new Form1());
                }
            }

        }
    }
}
