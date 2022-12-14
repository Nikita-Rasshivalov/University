using Common.SharedMemory;
using System.Diagnostics;

namespace WorkerProcessing
{
    internal class Program
    {
        private const string FigureIdFileName = "FIGURE_ID_FILE_NAME10";
        private const string FigureFileName = "FIGURE_FILE_NAME";
        static async Task Main(string[] args)
        {
            var worker = new Worker(FigureIdFileName, FigureFileName, new FiguresDataReader(), new IntReader());
            var sw = new Stopwatch();
            sw.Start();
            await worker.ExecuteAsSingleAsync("E:/results");
            Console.WriteLine($"Elapsed for single thread: {sw.ElapsedMilliseconds}");
            sw.Restart();
            await worker.ExecuteAsync("E:/results");
            Console.WriteLine($"Elapsed for multiple threads: {sw.ElapsedMilliseconds}");
            Console.ReadKey();
        }
    }
}