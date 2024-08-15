using MergeSorting;
using MergeSorting.Mergers.Interfaces;
using MergeSorting.Splitters.Interfaces;

namespace MergeSorting.Sorters
{
    /// <summary>
    /// Средство асинхронной сортировки массива.
    /// </summary>
    /// <typeparam name="T">Тип данных в массиве.</typeparam>
    internal class AsyncSorter<T>
    {
        private readonly ISplitter<T> _splitter;
        private readonly IMerger<T> _merger;

        internal AsyncSorter(ISplitter<T> splitter, IMerger<T> merger)
        {
            _splitter = splitter;
            _merger = merger;
        }

        /// <summary>
        /// Асинхронно отстортировать массив.
        /// </summary>
        /// <param name="source">Исходный массив.</param>
        /// <returns>Задача, содержащая массив после сортировки.</returns>
        internal Task<T[]> SortAsync(T[] source)
        {
            var splitting = Task.Run(() => _splitter.Split(source));

            return splitting
                .ContinueWith(t => MergeAsync(t.Result))
                .Unwrap()
                .ContinueWith(t => t.Result.Dequeue());
        }

        /// <summary>
        /// Асинхронно и рекурсивно слить очередь упорядоченных массивов.
        /// </summary>
        /// <param name="queues">Очередь упорядоченных массивов.</param>
        /// <returns>Задача, содержащая очередь упорядоченных массивов с одним единственным элементом.</returns>
        private Task<Queue<T[]>> MergeAsync(Queue<T[]> queues)
        {
            if (queues.Count == 1)
            {
                return Task.Run(() => queues);
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

            return Task
                .WhenAll(tasks)
                .ContinueWith(t => MergeAsync(t.Result.ToQueue()))
                .Unwrap();
        }
    }
}
