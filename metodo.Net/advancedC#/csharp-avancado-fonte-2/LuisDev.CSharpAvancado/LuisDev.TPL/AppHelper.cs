using System.Diagnostics;

namespace LuisDev.TPL
{
    internal static class AppHelper
    {
        public static Stopwatch StartStopwatch()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            return stopwatch;
        }

        public static void ShowFinalizedData(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            Console.WriteLine($"Tempo decorrido: {elapsedTime.TotalSeconds} segundos");
        }

    }
}
