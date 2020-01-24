using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace OSproject.Classes
{
    class ConcurrencyAndParallelism
    {
        public static string Title = "\t\t***************************************\n" +
                                     "\t\t***   Concurrency vs. Parallelism   ***\n" +
                                     "\t\t***************************************\n";

        public static string Discription = " >> Discription :\n" +
                                           " -------------------------------------------------------------------------------------\n" +
                                           " |   Concurrency means multiple tasks which start, run,                              |\n" +
                                           " |   and complete in overlapping time periods,                                       |\n" +
                                           " |   in no specific order. Parallelism is when multiple                              |\n" +
                                           " |   tasks OR several part of a unique task literally                                |\n" +
                                           " |   run at the same time, e.g. on a multi-core processor.                           |\n" +
                                           " |   >> Remember that Concurrency and parallelism are NOT the same thing.            |\n" +
                                           " -------------------------------------------------------------------------------------\n";
        
        public static string ParallelismDiscription = " >> Parallelism Discription :\n" +
                                           " --------------------------------------------------------------------------------------\n" +
                                           " |   Parallelism does not require two tasks to exist.                                 |\n" +
                                           " |   It literally physically run parts of tasks OR multiple tasks,                    |\n" +
                                           " |   at the same time using multi-core infrastructure of CPU,                         |\n" +
                                           " |   by assigning one core to each task or sub-task.                                  |\n" +
                                           " |   Parallelism requires hardware with multiple processing units,                    |\n" +
                                           " |   essentially.In single core CPU, you may get concurrency but NOT parallelism.     |\n" +
                                           " --------------------------------------------------------------------------------------\n";

        public static string ConcurrencyDiscription = " >> Concurrency Discription :\n" +
                                           " ----------------------------------------------------------------------------------------------------------------\n" +
                                           " |   Concurrency is essentially applicable when we talk about minimum two tasks or more.                        |\n" +
                                           " |   When an application is capable of executing two tasks virtually at same time,                              |\n" +
                                           " |   we call it concurrent application. Though here tasks run looks like simultaneously,                        |\n" +
                                           " |   but essentially they MAY not. They take advantage of CPU time-slicing feature of operating                 |\n" +
                                           " |   system where each task run part of its task and then go to waiting state.                                  |\n" +
                                           " |   When first task is in waiting state, CPU is assigned to second task to complete                            |\n" +
                                           " |   it’s part of task. Operating system based on priority of tasks, thus, assigns CPU                          |\n" +
                                           " |   and other computing resources e.g.memory; turn by turn to all tasks and  give them chance to complete.     |\n" +
                                           " |   To end user, it seems that all tasks are running in parallel.This is called concurrency.                   |\n" +
                                           " ----------------------------------------------------------------------------------------------------------------\n";

        public static long loop_number = 99999999;
        public static Process process { get; set; }
        public static int offset { get; set; }
        public static int cpuCount { get; set; }
        public static int core_number { get; set; }
        public static int thread_number { get; set; }

        public static bool exact { get; set; }

        public static void DoConcurrency()
        {
            Console.Clear();
            Menu.ShowTitle();
            Console.WriteLine(ConcurrencyAndParallelism.Title + ConcurrencyAndParallelism.ConcurrencyDiscription);

            //Get the our application's process.
            process = Process.GetCurrentProcess();

            //Get the number of cpu cores number
            cpuCount = Environment.ProcessorCount;

            //Get the current threads number in the cpu
            offset = process.Threads.Count;
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            Console.WriteLine("Your CPU Cores Count : {0}", cpuCount);

            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            Console.Write(">> Core exact Number [must be less than {0}]: ", cpuCount);
            core_number = Int32.Parse(Console.ReadLine()) - 1;

            exact = true;//not exactlly one core

            Console.Write(">> Thread(s) Count [less than core count] : ");
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
        public static void DoParallel()
        {
            Console.Clear();
            Menu.ShowTitle();
            Console.WriteLine(ConcurrencyAndParallelism.Title + ConcurrencyAndParallelism.ParallelismDiscription);

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

            Console.WriteLine("The Core count and Thread count must be the same.\n" +
                              "Example :\n" +
                              "## Core count = 2 and Thread count = 2\n" +
                              "## Core count = 3 and Thread count = 3\n");

            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            Console.Write(">> Core Count [must be less than {0}]: ", cpuCount);
            core_number = Int32.Parse(Console.ReadLine());

            exact = false;//not exactlly one core

            Console.Write(">> Thread(s) Count [less than core count] : ");
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
                Console.WriteLine("Thread [{0}] created.", i);
            }
        }
        public static void Core_Assign()
        {
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("Core assigning ...");
            if(exact)
            {
                for (int i = 0; i < thread_number; ++i)
                {
                    process.Threads[i + offset].ProcessorAffinity = (IntPtr)(1L << core_number);
                    Console.WriteLine("Thread [{0}] assign to core [{1}].", process.Threads[i + offset].Id, core_number);
                }
            }
            else
            {
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
