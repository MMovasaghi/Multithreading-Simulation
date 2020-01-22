using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSproject.Classes
{
    class Sample
    {
        public static int number { get; set; }
        public IntPtr ProcessorAffinity { get; set; }
        public Sample()
        {
            number = -1;
        }
        public static void DefaultJob()
        {
            Thread t1 = new Thread(new ThreadStart(Sample.Job1));
            Thread t2 = new Thread(new ThreadStart(Sample.Job2));
            t1.Start();
            t2.Start();

            Process Proc = Process.GetCurrentProcess();
            Proc.ProcessorAffinity = (IntPtr)(0x0001);

            t1.Join();
            t2.Join();
            Console.WriteLine("Main thread: Call Join(), to wait until ThreadProc ends.");

            Console.WriteLine("Main thread: ThreadProc.Join has returned.  Press Enter to end program.");
            Console.ReadLine();
        }
        public static void Job1()
        {
            for (long i = 0; i < 200000; i++)
            {
                Console.WriteLine("JOB[1]: {0}" , i);                
                // Yield the rest of the time slice.
                Thread.Sleep(100);
            }
        }
        public static void Job2()
        {
            for (long i = 0; i < 200000; i++)
            {
                Console.WriteLine("JOB[2]: {0}", i);
                // Yield the rest of the time slice.
                Thread.Sleep(100);
            }
        }
        public static void Job3()
        {
            for (long i = 0; i < 200000; i++)
            {
                Console.WriteLine("JOB[2]: {0}", i);
                // Yield the rest of the time slice.
                Thread.Sleep(100);
            }
        }
        public static void ClearScreen()
        {
            for (long i = 0; i < 200 ; i++)
            {
                Console.Clear();
                // Yield the rest of the time slice.
                Thread.Sleep(1000);
            }
        }
    }
}
