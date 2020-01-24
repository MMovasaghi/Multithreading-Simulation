using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSproject.Classes
{
    class MultiThreadingAndCoreAssigning
    {
        public static string Title = "\t\t******************************************\n" +
                                     "\t\t***   Multi-Threading Core Assigning   ***\n" +
                                     "\t\t******************************************\n";

        public static string Discription = " >> Discription :\n" +
                                           " ---------------------------------------------------------------------------------\n" +
                                           " |   In computer architecture, multithreading is the ability                     |\n" +
                                           " |   of a central processing unit (CPU) (or a single core                        |\n" +
                                           " |   in a multi-core processor) to provide multiple threads of                   |\n" +
                                           " |   execution concurrently, supported by the operating system.                  |\n" +
                                           " |   >> This approach differs from multiprocessing.                              |\n" +
                                           " ---------------------------------------------------------------------------------\n";

        public static long loop_number = 99999999;
        public static Process process { get; set; }
        public static int offset { get; set; }
        public static int cpuCount { get; set; }
        public static int core_number { get; set; }
        public static int thread_number { get; set; }
                
        public static void DefaultJob()
        {
            Console.Clear();
            Menu.ShowTitle();
            Console.WriteLine(MultiThreadingAndCoreAssigning.Title + MultiThreadingAndCoreAssigning.Discription);
            //Get the our application's process.
            process = Process.GetCurrentProcess();

            //Get the number of cpu cores number
            cpuCount = Environment.ProcessorCount;

            //Get the current threads number in the cpu
            offset = process.Threads.Count;
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            Console.WriteLine("Your CPU Cores Count : {0}", cpuCount);
            Console.WriteLine("There is [{0}] threads are handled in the cpu.", offset);

            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            core_number = 1;
            Console.WriteLine(">> Core Count : {0}", core_number);

            thread_number = 4;
            Console.WriteLine(">> Thread(s) Count : {0}", thread_number);
            Thread[] threads = new Thread[thread_number];

            Create_Thread();

            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            //Refresh the process information in order to get the newest
            //thread list.
            process.Refresh();
            Console.WriteLine("Number of Threads : {0}\n", process.Threads.Count);
            LogThreadIds(process);

            Core_Assign();

            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.ReadLine();
        }
        public static void CustomJob()
        {
            //Get the our application's process.
            process = Process.GetCurrentProcess();

            //Get the number of cpu cores number
            cpuCount = Environment.ProcessorCount;

            //Get the current threads number in the cpu
            offset = process.Threads.Count;
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            Console.WriteLine("Your CPU Cores Count : {0}", cpuCount);
            Console.WriteLine("There is [{0}] threads are handled in the cpu.", offset);

            Console.WriteLine(process.Threads.Count);
            LogThreadIds(process);

            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            Console.Write(">> Core Count [must be less than {0}]: ", cpuCount);
            core_number = Int32.Parse(Console.ReadLine());

            Console.Write(">> Thread(s) Count [1,2,...,n] : ");
            thread_number = Int32.Parse(Console.ReadLine());
            

            Create_Thread();

            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            //Refresh the process information in order to get the newest
            //thread list.
            process.Refresh();
            Console.WriteLine("Number of Threads : {0}\n", process.Threads.Count);
            LogThreadIds(process);

            Core_Assign();


            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.ReadLine();
        }
        public static void Create_Thread()
        {
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("Threads are starting ...");
            for (long i = 0; i < thread_number; i++)
            {
                Thread t = new Thread(new ThreadStart(Job))
                { 
                    IsBackground = true 
                };
                t.Start();
                Console.WriteLine("Thread [{0}] created.",i);
            }           
        }
        public static void Core_Assign()
        {
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("Core assigning ...");
            for (int i = 0; i < thread_number; ++i)
            {
                if (core_number == 1)
                {
                    process.Threads[i + offset].ProcessorAffinity = (IntPtr)(1L << 0);
                    Console.WriteLine("Thread [{0}] assign to core [{1}].", process.Threads[i + offset].Id, 1);
                }
                else
                {
                    process.Threads[i + offset].ProcessorAffinity = (IntPtr)(1L << (i % core_number));
                    Console.WriteLine("Thread [{0}] assign to core [{1}].", process.Threads[i + offset].Id, i % core_number);
                }
            }
        }
        public static void Job()
        {
            //some extreme loads.
            for (long i = 0; i < loop_number; i++)
            {
                Random rand = new Random();
                double a = rand.NextDouble();
                a = Math.Sin(Math.Sin(a));
            }
        }
        static void LogThreadIds(Process proc)
        {
            //This will log out all the thread id binded to the process.
            //It is used to test whether newly added threads are the latest elements
            //in the collection.
            Console.WriteLine("===Thread Ids===");
            for (int i = 0; i < proc.Threads.Count; ++i)
            {
                Console.WriteLine(proc.Threads[i].Id);
            }
            Console.WriteLine("===End of Thread Ids===");
        }
    }
}
