using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Helper methods for collection operations.
/// </summary>
public static class CollectionHelper
{
    #region Null/Empty Checks

    /// <summary>
    /// Determines whether a collection is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The collection to check.</param>
    /// <returns><c>true</c> if the collection is null or empty; otherwise, <c>false</c>.</returns>
    public static bool IsNullOrEmpty<T>(IEnumerable<T>? source)
        => source == null || !source.Any();

    /// <summary>
    /// Determines whether a collection has any elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The collection to check.</param>
    /// <returns><c>true</c> if the collection has elements; otherwise, <c>false</c>.</returns>
    public static bool HasAny<T>(IEnumerable<T>? source)
        => source != null && source.Any();

    /// <summary>
    /// Returns the collection if it has elements, otherwise returns the default collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The collection to check.</param>
    /// <param name="defaultValue">The default collection to return if source is null or empty.</param>
    /// <returns>The original collection or the default collection.</returns>
    public static IEnumerable<T> DefaultIfNullOrEmpty<T>(IEnumerable<T>? source, IEnumerable<T> defaultValue)
    {
        ArgumentNullException.ThrowIfNull(defaultValue);
        return IsNullOrEmpty(source) ? defaultValue : source!;
    }

    #endregion

    #region List Operations

    /// <summary>
    /// Adds a range of items to a collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The target collection.</param>
    /// <param name="items">The items to add.</param>
    public static void AddRange<T>(ICollection<T> source, IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(items);

        foreach (var item in items)
        {
            source.Add(item);
        }
    }

    /// <summary>
    /// Removes all items from the collection that match the predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="predicate">The predicate to match items for removal.</param>
    /// <returns>The number of items removed.</returns>
    public static int RemoveWhere<T>(ICollection<T> source, Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);

        var itemsToRemove = source.Where(predicate).ToList();
        foreach (var item in itemsToRemove)
        {
            source.Remove(item);
        }

        return itemsToRemove.Count;
    }

    #endregion

    #region Shuffle

    /// <summary>
    /// Shuffles the elements in the collection using the Fisher-Yates algorithm.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The collection to shuffle.</param>
    /// <param name="random">Optional random number generator.</param>
    /// <returns>A shuffled collection.</returns>
    public static IEnumerable<T> Shuffle<T>(IEnumerable<T> source, Random? random = null)
    {
        ArgumentNullException.ThrowIfNull(source);

        var rng = random ?? Random.Shared;
        var list = source.ToList();
        var n = list.Count;

        while (n > 1)
        {
            n--;
            var k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }

    #endregion

    #region Chunk

    /// <summary>
    /// Splits the collection into chunks of the specified size.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="size">The size of each chunk.</param>
    /// <returns>A collection of chunks.</returns>
    public static IEnumerable<IEnumerable<T>> ChunkBy<T>(IEnumerable<T> source, int size)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (size <= 0)
            throw new ArgumentOutOfRangeException(nameof(size), "Chunk size must be greater than zero.");

        return source.Chunk(size);
    }

    /// <summary>
    /// Splits the collection into batches of the specified size. Useful for processing items in groups.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="batchSize">The size of each batch.</param>
    /// <returns>A collection of batches.</returns>
    /// <exception cref="ArgumentNullException">Thrown when source is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when batchSize is less than or equal to zero.</exception>
    public static IEnumerable<IEnumerable<T>> Batch<T>(IEnumerable<T> source, int batchSize)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (batchSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(batchSize), "Batch size must be greater than zero.");

        return source.Chunk(batchSize);
    }

    #endregion

    #region ForEach

    /// <summary>
    /// Performs the specified action on each element of the collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="action">The action to perform on each element.</param>
    public static void ForEach<T>(IEnumerable<T> source, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);

        foreach (var item in source)
        {
            action(item);
        }
    }

    /// <summary>
    /// Performs the specified action on each element of the collection with its index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="action">The action to perform on each element (element, index).</param>
    public static void ForEachWithIndex<T>(IEnumerable<T> source, Action<T, int> action)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);

        int index = 0;
        foreach (var item in source)
        {
            action(item, index++);
        }
    }

    #endregion

    #region Partition

    /// <summary>
    /// Splits the collection into two groups: elements that match the predicate and elements that don't.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="predicate">The predicate to test each element.</param>
    /// <returns>A tuple containing matching and non-matching elements.</returns>
    public static (IEnumerable<T> Matching, IEnumerable<T> NotMatching) Partition<T>(
        IEnumerable<T> source,
        Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);

        var list = source.ToList();
        var matching = list.Where(predicate).ToList();
        var notMatching = list.Where(x => !predicate(x)).ToList();

        return (matching, notMatching);
    }

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
    public static async Task ForEachAsync<T>(
        IEnumerable<T> source,
        Func<T, Task> asyncAction,
        int maxDegreeOfParallelism = 1)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(asyncAction);

        if (maxDegreeOfParallelism < 1)
            throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), "Max degree of parallelism must be at least 1.");

        if (maxDegreeOfParallelism == 1)
        {
            // Sequential processing
            foreach (var item in source)
            {
                await asyncAction(item);
            }
        }
        else
        {
            // Parallel processing with semaphore
            using var semaphore = new System.Threading.SemaphoreSlim(maxDegreeOfParallelism);
            var tasks = source.Select(async item =>
            {
                await semaphore.WaitAsync();
                try
                {
                    await asyncAction(item);
                }
                finally
                {
                    semaphore.Release();
                }
            });

            await Task.WhenAll(tasks);
        }
    }

    /// <summary>
    /// Performs the specified asynchronous action on each element of the collection with its index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="asyncAction">The asynchronous action to perform on each element (element, index).</param>
    /// <param name="maxDegreeOfParallelism">The maximum number of concurrent operations. Default is 1 (sequential).</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task ForEachWithIndexAsync<T>(
        IEnumerable<T> source,
        Func<T, int, Task> asyncAction,
        int maxDegreeOfParallelism = 1)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(asyncAction);

        if (maxDegreeOfParallelism < 1)
            throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), "Max degree of parallelism must be at least 1.");

        if (maxDegreeOfParallelism == 1)
        {
            // Sequential processing
            int index = 0;
            foreach (var item in source)
            {
                await asyncAction(item, index++);
            }
        }
        else
        {
            // Parallel processing with semaphore
            using var semaphore = new System.Threading.SemaphoreSlim(maxDegreeOfParallelism);
            var indexed = source.Select((item, index) => (item, index));
            var tasks = indexed.Select(async pair =>
            {
                await semaphore.WaitAsync();
                try
                {
                    await asyncAction(pair.item, pair.index);
                }
                finally
                {
                    semaphore.Release();
                }
            });

            await Task.WhenAll(tasks);
        }
    }

    #endregion

    #region Random Selection

    /// <summary>
    /// Returns a random element from the collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="random">Optional random number generator.</param>
    /// <returns>A random element from the collection.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the collection is empty.</exception>
    public static T GetRandom<T>(IEnumerable<T> source, Random? random = null)
    {
        ArgumentNullException.ThrowIfNull(source);

        var list = source as IList<T> ?? source.ToList();
        if (list.Count == 0)
            throw new InvalidOperationException("Cannot select a random element from an empty collection.");

        var rng = random ?? Random.Shared;
        return list[rng.Next(list.Count)];
    }

    /// <summary>
    /// Returns a random element from the collection or a default value if empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="random">Optional random number generator.</param>
    /// <returns>A random element from the collection or default if empty.</returns>
    public static T? GetRandomOrDefault<T>(IEnumerable<T> source, Random? random = null)
    {
        ArgumentNullException.ThrowIfNull(source);

        var list = source as IList<T> ?? source.ToList();
        if (list.Count == 0)
            return default;

        var rng = random ?? Random.Shared;
        return list[rng.Next(list.Count)];
    }

    #endregion

    #region Other

    /// <summary>
    /// Returns a collection containing only the non-null elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <returns>A collection without null elements.</returns>
    public static IEnumerable<T> WhereNotNull<T>(IEnumerable<T?> source) where T : class
    {
        ArgumentNullException.ThrowIfNull(source);
        return source.Where(x => x != null)!;
    }

    #endregion
}
