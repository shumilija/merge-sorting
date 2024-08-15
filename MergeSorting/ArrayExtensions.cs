namespace MergeSorting
{
    internal static class ArrayExtensions
    {
        /// <summary>
        /// Преобразовать массив к очереди.
        /// </summary>
        /// <typeparam name="T">Типа данных в массиве/очереди.</typeparam>
        /// <param name="source">Исходны массив.</param>
        /// <returns>Очередь с элементами массива.</returns>
        internal static Queue<T> ToQueue<T>(this T[] source)
        {
            var result = new Queue<T>();

            foreach (var item in source)
            {
                result.Enqueue(item);
            }

            return result;
        }
    }
}
