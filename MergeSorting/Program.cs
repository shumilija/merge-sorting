using MergeSorting.Mergers;
using MergeSorting.Sorters;
using MergeSorting.Splitters;

namespace MergeSorting
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var countOfElements = 100000;

			var random = new Random(2024);
			var source = Enumerable.Range(0, countOfElements).Select(_ => random.Next(100)).ToArray();
			//Console.WriteLine("Исходная коллекция: {0}\n", string.Join(" ", source));

			var splitter = new Splitter<int>();
			var merger = new Merger<int>(Comparer<int>.Default);

			var syncSorter = new SyncSorter<int>(splitter, merger);
			var asyncSorter = new AsyncSorter<int>(splitter, merger);
			var awaitSorter = new AwaitSorter<int>(splitter, merger);

			var start = DateTime.UtcNow;
			var syncSorted = syncSorter.Sort(source);
			Console.WriteLine("Время выполнения синхронной сортировки: {0:s\\.fff}", DateTime.UtcNow - start);
			//Console.WriteLine("Результат синхронной сортировки: {0}\n", string.Join(" ", syncSorted));

			start = DateTime.UtcNow;
			var asyncSorted = asyncSorter.SortAsync(source).Result;
			Console.WriteLine("Время выполнения асинхронной сортировки: {0:s\\.fff}", DateTime.UtcNow - start);
			//Console.WriteLine("Результат асинхронной сортировки: {0}\n", string.Join(" ", asyncSorted));

			start = DateTime.UtcNow;
			var awaitSorted = awaitSorter.SortAsync(source).Result;
			Console.WriteLine("Время выполнения асинхронной (через await) сортировки: {0:s\\.fff}", DateTime.UtcNow - start);
			//Console.WriteLine("Результат асинхронной (через await) сортировки: {0}", string.Join(" ", awaitSorted));
		}
	}
}
