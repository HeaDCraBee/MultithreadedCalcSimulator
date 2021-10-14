using System;
using System.Collections.Generic;
using System.Threading;

namespace MultithreadedCalcSimulator
{
    class Program
    {
        private readonly static int s_threadNumber = 10;
        private static long s_calculationLength = 10;     

        static void Main(string[] args)
        {
            Console.WindowWidth = 150;
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Start");

            var calculatorsList = new List<Calculator>();
            var threadsList = new List<Thread>();

            for (int i = 0; i < s_threadNumber; i++){
                var calc = new Calculator(s_calculationLength, i);
                calculatorsList.Add(calc);

                var thread = new Thread(new ThreadStart(calc.Calculate));

                Console.SetCursorPosition(0, i + 1);
                Console.Write($"{i} ({thread.ManagedThreadId})");


                thread.Start();
                threadsList.Add(thread);
            }

            foreach(var thread in threadsList)
            {
                thread.Join();
            }

            Console.SetCursorPosition(0, s_threadNumber + 1);
            Console.WriteLine("END");
            Console.WriteLine("Press ENTER to close");
            Console.ReadLine();
        }
    }
}
