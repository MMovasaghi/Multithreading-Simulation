using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OSproject.Classes
{
    class Menu
    {
        public Menu()
        {
            
        }
        private void ShowTitle()
        {
            Console.WriteLine("MultiThreading & MultiProcessing Simulation [Version {0}]\n" +
                "2020 Shahed University.\n" +
                "(c) MH.Movasaghinia\n" +
                "---------------------------------------------------------\n", Program.version);            
        }
        public int ShowMenu()
        {
            int ch = 0;
            this.OpenTaskManager();
            do
            {
                Console.Clear();
                this.ShowTitle();

                Console.Write("Choices :        \n" +
                            "1. Custom Input    \n" +
                            "2. Default Input   \n" +
                            "0. Exit            \n\n" +
                            "Your choice : ");                
                try
                {
                    ch = Int16.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    ch = 0;
                }
            } while (ch < 0 || ch > 3);

            
            return ch;
        }
        private void OpenTaskManager()
        {
            //Open TaskManager
            Process cmd = new Process();

            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;

            cmd.Start();

            /* execute "dir" */

            cmd.StandardInput.WriteLine("taskmgr");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
        }
    }
}
