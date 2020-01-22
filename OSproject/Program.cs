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
        public static string version = "1.1";
               
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            int choise = menu.ShowMenu();

        }
    }
}
