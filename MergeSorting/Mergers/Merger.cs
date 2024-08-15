using MergeSorting.Mergers.Interfaces;

namespace MergeSorting.Mergers
{
    /// <summary>
    /// Реализация <see cref="IMerger{T}"/> с использованием <see cref="IComparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">Тип данных в массиве.</typeparam>
    internal class Merger<T> : IMerger<T>
    {
        private readonly IComparer<T> _comparer;

        internal Merger(IComparer<T> comparer)
        {
            _comparer = comparer;
        }

        /// <inheritdoc/>
        public T[] Merge(T[] left, T[] right)
        {
            var result = new T[left.Length + right.Length];

            int i = 0;
            int j = 0;
            while (i < left.Length && j < right.Length)
            {
                if (_comparer.Compare(left[i], right[j]) < 0)
                {
                    result[i + j] = left[i];
                    i++;
                }
                else
                {
                    result[i + j] = right[j];
                    j++;
                }
            }

            while (i < left.Length)
            {
                result[i + j] = left[i];
                i++;
            }

            while (j < right.Length)
            {
                result[i + j] = right[j];
                j++;
            }

            return result;
        }
    }
}
