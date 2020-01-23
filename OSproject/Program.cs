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
        public static string version = "2.1";
        public static void JobSelect(int Select)
        {
            switch (Select)
            {
                case 1:
                    Sample.CustomJob();
                    break;
                case 2:
                    Sample.DefaultJob();
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
