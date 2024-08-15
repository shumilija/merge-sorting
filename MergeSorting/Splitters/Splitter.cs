using MergeSorting.Splitters.Interfaces;

namespace MergeSorting.Splitters
{
    /// <summary>
    /// Реализация <see cref="ISplitter{T}"/> выполняющая разделение массива на подмассивы, содержащие один единственный элемент.
    /// </summary>
    /// <typeparam name="T">Тип данных в массиве.</typeparam>
    internal class Splitter<T> : ISplitter<T>
    {
        /// <inheritdoc/>
        public Queue<T[]> Split(T[] source)
        {
            var result = new Queue<T[]>();

            foreach (var item in source)
            {
                result.Enqueue(new T[] { item });
            }

            return result;
        }
    }
}
