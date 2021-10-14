using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultithreadedCalcSimulator
{
    public class ProgressInterpreter
    {
        private long _calcLength;
        private int _threadIndex;
        private DateTime _startTime;
        private List<char> _stringToShow = new List<char>();
        private int _lastProgress = 0;

        public ProgressInterpreter(long calcLength, int threadIndex, DateTime startTime)
        {
            _calcLength = calcLength;
            _threadIndex = threadIndex;
            _startTime = startTime;
        }

        public async Task ShowProgress(long value, bool isSuccess)
        {
            int progress = (int)((value + 1) / ((double)_calcLength / 100));

            if (progress > _lastProgress)
            {
                for (int i = _lastProgress; i < progress; i++)
                {
                    if (isSuccess)
                    {
                        _stringToShow.Add('G');
                    }
                    else
                    {
                        _stringToShow.Add('R');
                    }
                }

                _lastProgress = progress;

                if (progress != 100)
                {
                    await Task.Factory.StartNew(() =>
                    {
                        ProgressIndicator.ShowProgress(_lastProgress, value, _threadIndex, _stringToShow);
                    });
                }
            }
        }

        public void ShowResult(long value, DateTime endTime)
        {
            ProgressIndicator.ShowProgress(_lastProgress, value, _threadIndex, _stringToShow, endTime - _startTime);
        }
    }

}
