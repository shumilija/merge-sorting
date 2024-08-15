namespace MergeSorting.Splitters.Interfaces
{
    /// <summary>
    /// Интерфейс представления массива в виде очереди упорядоченных подмассивов.
    /// </summary>
    /// <typeparam name="T">Тип данных в массиве.</typeparam>
    internal interface ISplitter<T>
    {
        /// <summary>
        /// Разделить массив на очередь упорядоченных подмассивов.
        /// </summary>
        /// <param name="source">Исходный массив.</param>
        /// <returns>Очередь упорядоченных подмассивов исходного массива.</returns>
        Queue<T[]> Split(T[] source);
    }
}
