using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Threading;
using System.Timers;

namespace Net
{

    internal class Program
    {
        private static ThreadSafeResult _threadSafeResult = new ThreadSafeResult();
        private const int ThreadCount = 4;

        static void Main(string[] args)
        {
            int start = 0;
            int end = 100;
            int n = 1;
            double eps = 0.000001;
            Console.WriteLine("Using threading");
            ResultData res = Calc(end, start, n, eps, ThreadCount, true);
            Console.WriteLine($"Total elapsed time: {res.Elapsed} ms");
            Console.WriteLine($"Square: {Math.Abs(res.Result)}");
            Console.WriteLine("Not using threading");
            ResultData res2 = Calc(end, start, n, eps, ThreadCount, false);
            Console.WriteLine($"Total elapsed time: {res2.Elapsed} ms");
            Console.WriteLine($"Square: {Math.Abs(res2.Result)}");
            Console.ReadKey();
        }
        private static double function(double x)
        {
            return Math.Exp(Math.Cos(x)) * Math.Sin(x);
        }

        private static double Integral(double start, double end, int n)
        {
            double x, step;
            double sum = 0.0;
            double fx;
            step = (end - start) / n;

            for (int i = 1; i <= n; i++)
            {
                x = start + i * step;
                fx = function(x);
                sum += fx;
            }
            return (sum * step);
        }

        private static double CalculateIntegralSingle(double start, double end, int n)
        {
            return Integral(start, end, n);
        }

        private static double CalculateIntegralWithThreads(double start, double end, double step, int n, int threadCount)
        {
            Parallel.For(0, threadCount, (i) =>
            {
                var threadStart = start + step * i;
                var threadEnd = start + step * (i + 1);
                _threadSafeResult.Push(Integral(threadStart, threadEnd, n));
            });

            return _threadSafeResult.GetAndClear();
        }

        private static ResultData Calc(double end, double start, int n, double eps, int threadCount, bool useThreads)
        {
            double step = (end - start) / threadCount;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            double previousResult, currentResult;
            currentResult = useThreads ? CalculateIntegralWithThreads(start, end, step, n, threadCount) : CalculateIntegralSingle(start, end, n);
            double loss;
            do
            {
                previousResult = currentResult;
                n = 2 * n;
                currentResult = useThreads ? CalculateIntegralWithThreads(start, end, step, n, threadCount) : CalculateIntegralSingle(start, end, n);
                loss = Math.Abs(previousResult - currentResult);
            } while (loss > eps);

            return new ResultData { Elapsed = stopwatch.ElapsedMilliseconds, Result = currentResult };
        }
    }
}