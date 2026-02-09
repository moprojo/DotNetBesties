using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for collection types.
/// </summary>
public static class CollectionExtensions
{
    #region Null/Empty Checks

    /// <summary>
    /// Determines whether a collection is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The collection to check.</param>
    /// <returns><c>true</c> if the collection is null or empty; otherwise, <c>false</c>.</returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
        => CollectionHelper.IsNullOrEmpty(source);

    /// <summary>
    /// Determines whether a collection has any elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The collection to check.</param>
    /// <returns><c>true</c> if the collection has elements; otherwise, <c>false</c>.</returns>
    public static bool HasAny<T>(this IEnumerable<T>? source)
        => CollectionHelper.HasAny(source);

    /// <summary>
    /// Returns the collection if it has elements, otherwise returns the default collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The collection to check.</param>
    /// <param name="defaultValue">The default collection to return if source is null or empty.</param>
    /// <returns>The original collection or the default collection.</returns>
    public static IEnumerable<T> DefaultIfNullOrEmpty<T>(this IEnumerable<T>? source, IEnumerable<T> defaultValue)
        => CollectionHelper.DefaultIfNullOrEmpty(source, defaultValue);

    #endregion

    #region List Operations

    /// <summary>
    /// Adds a range of items to a collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The target collection.</param>
    /// <param name="items">The items to add.</param>
    public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> items)
        => CollectionHelper.AddRange(source, items);

    /// <summary>
    /// Removes all items from the collection that match the predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="predicate">The predicate to match items for removal.</param>
    /// <returns>The number of items removed.</returns>
    public static int RemoveWhere<T>(this ICollection<T> source, Func<T, bool> predicate)
        => CollectionHelper.RemoveWhere(source, predicate);

    #endregion

    #region Shuffle

    /// <summary>
    /// Shuffles the elements in the collection using the Fisher-Yates algorithm.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The collection to shuffle.</param>
    /// <param name="random">Optional random number generator.</param>
    /// <returns>A shuffled collection.</returns>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, System.Random? random = null)
        => CollectionHelper.Shuffle(source, random);

    #endregion

    #region Chunk

    /// <summary>
    /// Splits the collection into chunks of the specified size.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="size">The size of each chunk.</param>
    /// <returns>A collection of chunks.</returns>
    public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, int size)
        => CollectionHelper.ChunkBy(source, size);

    /// <summary>
    /// Splits the collection into batches of the specified size. Useful for processing items in groups.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="batchSize">The size of each batch.</param>
    /// <returns>A collection of batches.</returns>
    /// <exception cref="ArgumentNullException">Thrown when source is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when batchSize is less than or equal to zero.</exception>
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        => CollectionHelper.Batch(source, batchSize);

    #endregion

    #region ForEach

    /// <summary>
    /// Performs the specified action on each element of the collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="action">The action to perform on each element.</param>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        => CollectionHelper.ForEach(source, action);

    /// <summary>
    /// Performs the specified action on each element of the collection with its index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="action">The action to perform on each element (element, index).</param>
    public static void ForEachWithIndex<T>(this IEnumerable<T> source, Action<T, int> action)
        => CollectionHelper.ForEachWithIndex(source, action);

    #endregion

    #region Partition

    /// <summary>
    /// Splits the collection into two groups: elements that match the predicate and elements that don't.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="predicate">The predicate to test each element.</param>
    /// <returns>A tuple containing matching and non-matching elements.</returns>
    /// <exception cref="ArgumentNullException">Thrown when source or predicate is null.</exception>
    public static (IEnumerable<T> Matching, IEnumerable<T> NotMatching) Partition<T>(
        this IEnumerable<T> source, 
        Func<T, bool> predicate)
        => CollectionHelper.Partition(source, predicate);

    #endregion

    #region Async Operations

    /// <summary>
    /// Performs the specified asynchronous action on each element of the collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="asyncAction">The asynchronous action to perform on each element.</param>
    /// <param name="maxDegreeOfParallelism">The maximum number of concurrent operations. Default is 1 (sequential).</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when source or asyncAction is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when maxDegreeOfParallelism is less than 1.</exception>
    public static Task ForEachAsync<T>(
        this IEnumerable<T> source,
        Func<T, Task> asyncAction,
        int maxDegreeOfParallelism = 1)
        => CollectionHelper.ForEachAsync(source, asyncAction, maxDegreeOfParallelism);

    /// <summary>
    /// Performs the specified asynchronous action on each element of the collection with its index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="asyncAction">The asynchronous action to perform on each element (element, index).</param>
    /// <param name="maxDegreeOfParallelism">The maximum number of concurrent operations. Default is 1 (sequential).</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when source or asyncAction is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when maxDegreeOfParallelism is less than 1.</exception>
    public static Task ForEachAsync<T>(
        this IEnumerable<T> source,
        Func<T, int, Task> asyncAction,
        int maxDegreeOfParallelism = 1)
        => CollectionHelper.ForEachWithIndexAsync(source, asyncAction, maxDegreeOfParallelism);

    #endregion

    #region Other

    /// <summary>
    /// Returns a collection containing only the non-null elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <returns>A collection without null elements.</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class
        => CollectionHelper.WhereNotNull(source);

    /// <summary>
    /// Returns a random element from the collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="random">Optional random number generator.</param>
    /// <returns>A random element from the collection.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the collection is empty.</exception>
    public static T GetRandom<T>(this IEnumerable<T> source, System.Random? random = null)
        => CollectionHelper.GetRandom(source, random);

    /// <summary>
    /// Returns a random element from the collection or a default value if empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="random">Optional random number generator.</param>
    /// <returns>A random element from the collection or default if empty.</returns>
    public static T? GetRandomOrDefault<T>(this IEnumerable<T> source, System.Random? random = null)
        => CollectionHelper.GetRandomOrDefault(source, random);

    #endregion
}
