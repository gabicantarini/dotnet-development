using System;
using System.Diagnostics;

namespace AdvancedCsharp.DelegatesEvents
{
	public class InstrumentationService
	{
        public int Measure(Func<int> method)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var result = method();
            stopwatch.Stop();

            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

            return result;
        }

        public T Measure<T>(Func<int, T> method, int parameter)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var result = method(parameter);
            stopwatch.Stop();

            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

            return result;
        }

        public TResult Measure<TInput, TResult>(Func<TInput, TResult> method, TInput parameter)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var result = method(parameter);
            stopwatch.Stop();

            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

            return result;
        }

        public void Measure(Action method)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            method();
            stopwatch.Stop();

            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
        }

        public void Measure<T>(Action<T> method, T parameter)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            method(parameter);
            stopwatch.Stop();

            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}

