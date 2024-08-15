using MergeSorting.Mergers.Interfaces;
using MergeSorting.Splitters.Interfaces;

namespace MergeSorting.Sorters
{
	/// <summary>
	/// Средство асинхронной сортировки массива (через async/await).
	/// </summary>
	/// <typeparam name="T">Тип данных в массиве.</typeparam>
	internal class AwaitSorter<T>
	{
		private readonly ISplitter<T> _splitter;
		private readonly IMerger<T> _merger;

		internal AwaitSorter(ISplitter<T> splitter, IMerger<T> merger)
		{
			_splitter = splitter;
			_merger = merger;
		}

		/// <summary>
		/// Асинхронно отстортировать массив.
		/// </summary>
		/// <param name="source">Исходный массив.</param>
		/// <returns>Задача, содержащая массив после сортировки.</returns>
		internal async Task<T[]> SortAsync(T[] source)
		{
			var splitted = await Task.Run(() => _splitter.Split(source));

			var merged = await MergeAsync(splitted);

			return merged.Dequeue();
		}

		/// <summary>
		/// Асинхронно и рекурсивно слить очередь упорядоченных массивов.
		/// </summary>
		/// <param name="queues">Очередь упорядоченных массивов.</param>
		/// <returns>Задача, содержащая очередь упорядоченных массивов с одним единственным элементом.</returns>
		private async Task<Queue<T[]>> MergeAsync(Queue<T[]> queues)
		{
			if (queues.Count == 1)
			{
				return queues;
			}

			var tasks = new List<Task<T[]>>();

			while (queues.TryDequeue(out var left))
			{
				if (queues.TryDequeue(out var right))
				{
					tasks.Add(Task.Run(() => _merger.Merge(left, right)));
				}
				else
				{
					tasks.Add(Task.Run(() => left));
				}
			}

			var subarrays = await Task.WhenAll(tasks);

			var result = await MergeAsync(subarrays.ToQueue());

			return result;
		}
	}
}
