namespace MergeSorting.Mergers.Interfaces
{
    /// <summary>
    /// Интерфейс слияния двух упорядоченных массивов.
    /// </summary>
    /// <typeparam name="T">Тип данных в массиве.</typeparam>
    internal interface IMerger<T>
    {
        /// <summary>
        /// Слить два упорядоченных массива в один упорядоченный массив.
        /// </summary>
        /// <param name="left">Первый массив для слияния.</param>
        /// <param name="right">Второй массив для слияния.</param>
        /// <returns>Упорядоченный массив, включающий все элементы обоих массивов.</returns>
        T[] Merge(T[] left, T[] right);
    }
}
