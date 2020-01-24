using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using OSproject.Classes;

namespace OSproject
{
    class Program
    {
        public static string version = "3.1";
        public static void JobSelect(int Select)
        {
            switch (Select)
            {
                case 11:
                    MultiThreadingAndCoreAssigning.DefaultJob();
                    break;
                case 12:
                    MultiThreadingAndCoreAssigning.CustomJob();
                    break;
                case 21:
                    ConcurrencyAndParallelism.DoConcurrency();
                    break;
                case 22:
                    ConcurrencyAndParallelism.DoParallel();
                    break;
                default:
                    break;
            }
        }
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            int choise = menu.ShowMenu();
            JobSelect(choise);
        }
    }
}
