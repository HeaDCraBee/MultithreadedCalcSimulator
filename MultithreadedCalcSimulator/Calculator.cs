using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultithreadedCalcSimulator
{
    public class Calculator
    {
        public long Result { get; private set; }

        private List<long> _unSuccessNumbers;
        private int _threadIndex;
        private long _calcLength;
        private Queue<long> _dataToDisplay = new Queue<long>();
        private ProgressInterpreter _progressInterpreter;


        public Calculator(long calcLength, int threadIndex)
        {
            _calcLength = calcLength;
            _threadIndex = threadIndex;
            _unSuccessNumbers = new List<long>();
        }

        public void Calculate()
        {
            _progressInterpreter = new ProgressInterpreter(_calcLength, _threadIndex, DateTime.Now);

            var tasks = new Task[1];
            for (long i = 0; i < _calcLength; i++)
            {
                var isSuccess = true;
                try
                {
                    Delay(i);
                }
                catch
                {
                    isSuccess = false;
                }

                var task = ShowProgress(i, isSuccess);

                _dataToDisplay.Enqueue(i);
                tasks[0] = task;
            }

            DateTime endTime = DateTime.Now;
            Task.WaitAll(new Task[] { tasks[0] });

            ShowResult(_calcLength, endTime);
        }


        private async Task ShowProgress(long i, bool isSuccsses)
        {
            await _progressInterpreter.ShowProgress(i, isSuccsses);
        }

        private void ShowResult(long i, DateTime endTime)
        {
            _progressInterpreter.ShowResult(i, endTime);
        }

        private void Delay(long i)
        {
            Random random = new Random();
   
            
            if (random.Next(0, 15) == _threadIndex)
            {
                Thread.Sleep(10);
                throw new ArgumentException();
            }

            Thread.Sleep(random.Next(500, 2000));
        }
    }
}
