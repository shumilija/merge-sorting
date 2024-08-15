using MergeSorting.Mergers.Interfaces;
using MergeSorting.Splitters.Interfaces;

namespace MergeSorting.Sorters
{
    /// <summary>
    /// Средство синхронной сортировки масства.
    /// </summary>
    /// <typeparam name="T">Тип данных в массиве.</typeparam>
	internal class SyncSorter<T>
    {
        private readonly ISplitter<T> _splitter;
        private readonly IMerger<T> _merger;

        internal SyncSorter(ISplitter<T> splitter, IMerger<T> merger)
        {
            _splitter = splitter;
            _merger = merger;
        }

        /// <summary>
        /// Синхронно отстортировать массив.
        /// </summary>
        /// <param name="source">Исходный массив.</param>
        /// <returns>Отсортированный массив.</returns>
        internal T[] Sort(T[] source)
        {
            var splitted = _splitter.Split(source);

            T[] left;
            while (splitted.TryDequeue(out left) && splitted.TryDequeue(out var right))
            {
                var merged = _merger.Merge(left, right);
                splitted.Enqueue(merged);
            }

            return left;
        }
    }
}
