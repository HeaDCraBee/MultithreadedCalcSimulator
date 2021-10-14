using System;
using System.Collections.Generic;

namespace MultithreadedCalcSimulator
{
    public static class ProgressIndicator
    {
        private static char s_square = (char)9632;
        private static object locker = new object();
        private static int s_queuePosition = 1;

        public static void ShowProgress(int progress, long currentValue, int threadIndex, List<char> progressBar)
        {
            lock (locker)
            {
                Show(progress, currentValue, threadIndex, progressBar);
            }
        }

        public static void ShowProgress(int progress, long currentValue, int threadIndex, List<char> progressBar, TimeSpan inWorkTime)
        {
            lock (locker)
            {
                Show(progress, currentValue, threadIndex, progressBar);
                ShowEnd(inWorkTime);
            }
        }

        private static void Show(int progress, long currentValue, int threadIndex, List<char> progressBar)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(9, threadIndex + 1);
            Console.Write($"{currentValue}  ");

            for (int i = 0; i < progressBar.Count; i++)
            {
                if (progressBar[i] == 'R')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.Write(s_square);
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.Write($" {progress}");
        }

        private static void ShowEnd(TimeSpan inWorkTime)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"  DONE! {s_queuePosition} {inWorkTime.TotalSeconds}");
            s_queuePosition++;
        }
    }
}
