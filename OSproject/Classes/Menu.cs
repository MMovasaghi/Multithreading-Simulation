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
        public static void ShowTitle()
        {
            Console.WriteLine("MultiThreading & MultiProcessing Simulation [Version {0}]\n" +
                "2020 Shahed University.\n" +
                "(c) MH.Movasaghinia\n\n", Program.version);            
        }
        public int ShowMenu()
        {
            int ch = 0;
            int final = 0;
            do
            {
                Console.Clear();
                Menu.ShowTitle();

                Console.Write("Choices :                        \n" +
                            "1. Multi-Threading Core Assigning  \n" +
                            "2. Concurrency vs. Parallelism     \n" +                            
                            "0. Exit                            \n\n" +
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

            if(ch == 1)
            {
                final = 10;
                this.OpenTaskManager();
                do
                {
                    Console.Clear();
                    Menu.ShowTitle();
                    Console.WriteLine(ConcurrencyAndParallelism.Title + ConcurrencyAndParallelism.Discription);
                    Console.Write("Choices :        \n" +
                                "1. Default Input   \n" +
                                "2. Custom Input    \n" +
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
                final += ch;
            }
            else if(ch == 2)
            {
                final = 20;
                do
                {
                    Console.Clear();
                    Menu.ShowTitle();
                    Console.WriteLine(MultiThreadingAndCoreAssigning.Title + MultiThreadingAndCoreAssigning.Discription);
                    Console.Write("Choices :        \n" +
                                "1. Example for Concurrency\n" +
                                "2. Example for Parallelism\n" +
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
                final += ch;
            }
            return final;
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
