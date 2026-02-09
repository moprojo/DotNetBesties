using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format;

public class CollectionHelperTests
{
    #region IsNullOrEmpty Tests

    [Test]
    public async Task IsNullOrEmpty_WithNull_ReturnsTrue()
    {
        IEnumerable<int>? list = null;
        var result = CollectionHelper.IsNullOrEmpty(list);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNullOrEmpty_WithEmpty_ReturnsTrue()
    {
        IEnumerable<int> list = new List<int>();
        var result = CollectionHelper.IsNullOrEmpty(list);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IsNullOrEmpty_WithItems_ReturnsFalse()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        var result = CollectionHelper.IsNullOrEmpty(list);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region HasAny Tests

    [Test]
    public async Task HasAny_WithItems_ReturnsTrue()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        var result = CollectionHelper.HasAny(list);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task HasAny_WithEmpty_ReturnsFalse()
    {
        IEnumerable<int> list = new List<int>();
        var result = CollectionHelper.HasAny(list);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task HasAny_WithNull_ReturnsFalse()
    {
        IEnumerable<int>? list = null;
        var result = CollectionHelper.HasAny(list);
        await Assert.That(result).IsFalse();
    }

    #endregion

    #region AddRange Tests

    [Test]
    public async Task AddRange_AddsAllItems()
    {
        var list = new List<int> { 1, 2 };
        CollectionHelper.AddRange(list, new[] { 3, 4, 5 });

        await Assert.That(list.Count).IsEqualTo(5);
        await Assert.That(list).Contains(3);
        await Assert.That(list).Contains(4);
        await Assert.That(list).Contains(5);
    }

    [Test]
    public async Task AddRange_WithNullSource_ThrowsArgumentNullException()
    {
        ICollection<int>? list = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.AddRange(list!, new[] { 1, 2, 3 })));
    }

    [Test]
    public async Task AddRange_WithNullItems_ThrowsArgumentNullException()
    {
        var list = new List<int>();
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.AddRange(list, null!)));
    }

    [Test]
    public async Task AddRange_WithEmptyItems_DoesNotAddAnything()
    {
        var list = new List<int> { 1, 2 };
        CollectionHelper.AddRange(list, Array.Empty<int>());

        await Assert.That(list.Count).IsEqualTo(2);
    }

    #endregion

    #region RemoveWhere Tests

    [Test]
    public async Task RemoveWhere_RemovesMatchingItems()
    {
        var list = new List<int> { 1, 2, 3, 4, 5 };
        var removed = CollectionHelper.RemoveWhere(list, x => x % 2 == 0);

        await Assert.That(removed).IsEqualTo(2);
        await Assert.That(list.Count).IsEqualTo(3);
        await Assert.That(list).DoesNotContain(2);
        await Assert.That(list).DoesNotContain(4);
    }

    [Test]
    public async Task RemoveWhere_WithNullSource_ThrowsArgumentNullException()
    {
        ICollection<int>? list = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.RemoveWhere(list!, x => true)));
    }

    [Test]
    public async Task RemoveWhere_WithNullPredicate_ThrowsArgumentNullException()
    {
        var list = new List<int> { 1, 2, 3 };
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.RemoveWhere(list, null!)));
    }

    [Test]
    public async Task RemoveWhere_WithNoMatches_ReturnsZero()
    {
        var list = new List<int> { 1, 3, 5 };
        var removed = CollectionHelper.RemoveWhere(list, x => x % 2 == 0);

        await Assert.That(removed).IsEqualTo(0);
        await Assert.That(list.Count).IsEqualTo(3);
    }

    #endregion

    #region Shuffle Tests

    [Test]
    public async Task Shuffle_ShufflesElements()
    {
        IEnumerable<int> list = Enumerable.Range(1, 100).ToList();
        var shuffled = CollectionHelper.Shuffle(list).ToList();

        await Assert.That(shuffled.Count).IsEqualTo(100);
        // Very unlikely to be in the same order
        var isSame = list.SequenceEqual(shuffled);
        await Assert.That(isSame).IsFalse();
    }

    [Test]
    public async Task Shuffle_WithNullSource_ThrowsArgumentNullException()
    {
        IEnumerable<int>? list = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.Shuffle(list!).ToList()));
    }

    [Test]
    public async Task Shuffle_WithEmptyCollection_ReturnsEmpty()
    {
        IEnumerable<int> list = new List<int>();
        var shuffled = CollectionHelper.Shuffle(list).ToList();
        await Assert.That(shuffled.Count).IsEqualTo(0);
    }

    [Test]
    public async Task Shuffle_WithSingleElement_ReturnsSameElement()
    {
        IEnumerable<int> list = new List<int> { 42 };
        var shuffled = CollectionHelper.Shuffle(list).ToList();
        await Assert.That(shuffled.Count).IsEqualTo(1);
        await Assert.That(shuffled[0]).IsEqualTo(42);
    }

    [Test]
    public async Task Shuffle_WithCustomRandom_UsesProvidedRandom()
    {
        var random = new Random(12345); // Fixed seed
        IEnumerable<int> list = Enumerable.Range(1, 10).ToList();
        var shuffled1 = CollectionHelper.Shuffle(list, new Random(12345)).ToList();
        var shuffled2 = CollectionHelper.Shuffle(list, new Random(12345)).ToList();

        await Assert.That(shuffled1).IsEquivalentTo(shuffled2);
    }

    #endregion

    #region Partition Tests

    [Test]
    public async Task Partition_SplitsCollectionCorrectly()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3, 4, 5, 6 };
        var (matching, notMatching) = CollectionHelper.Partition(list, x => x % 2 == 0);

        var matchingList = matching.ToList();
        var notMatchingList = notMatching.ToList();

        await Assert.That(matchingList.Count).IsEqualTo(3);
        await Assert.That(matchingList).Contains(2);
        await Assert.That(matchingList).Contains(4);
        await Assert.That(matchingList).Contains(6);

        await Assert.That(notMatchingList.Count).IsEqualTo(3);
        await Assert.That(notMatchingList).Contains(1);
        await Assert.That(notMatchingList).Contains(3);
        await Assert.That(notMatchingList).Contains(5);
    }

    [Test]
    public async Task Partition_WithAllMatching_ReturnsEmptyNotMatching()
    {
        IEnumerable<int> list = new List<int> { 2, 4, 6 };
        var (matching, notMatching) = CollectionHelper.Partition(list, x => x % 2 == 0);

        await Assert.That(matching.ToList().Count).IsEqualTo(3);
        await Assert.That(notMatching.ToList().Count).IsEqualTo(0);
    }

    [Test]
    public async Task Partition_WithNoneMatching_ReturnsEmptyMatching()
    {
        IEnumerable<int> list = new List<int> { 1, 3, 5 };
        var (matching, notMatching) = CollectionHelper.Partition(list, x => x % 2 == 0);

        await Assert.That(matching.ToList().Count).IsEqualTo(0);
        await Assert.That(notMatching.ToList().Count).IsEqualTo(3);
    }

    [Test]
    public async Task Partition_WithEmptyCollection_ReturnsBothEmpty()
    {
        IEnumerable<int> list = new List<int>();
        var (matching, notMatching) = CollectionHelper.Partition(list, x => x % 2 == 0);

        await Assert.That(matching.ToList().Count).IsEqualTo(0);
        await Assert.That(notMatching.ToList().Count).IsEqualTo(0);
    }

    [Test]
    public async Task Partition_WithNullSource_ThrowsArgumentNullException()
    {
        IEnumerable<int>? list = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.Partition(list!, x => true)));
    }

    [Test]
    public async Task Partition_WithNullPredicate_ThrowsArgumentNullException()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.Partition(list, null!)));
    }

    #endregion

    #region ForEachAsync Tests

    [Test]
    public async Task ForEachAsync_ExecutesActionForEachSequentially()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        var sum = 0;

        await CollectionHelper.ForEachAsync(list, async x =>
        {
            await Task.Delay(1);
            sum += x;
        });

        await Assert.That(sum).IsEqualTo(6);
    }

    [Test]
    public async Task ForEachAsync_WithParallelism_ExecutesActionsInParallel()
    {
        IEnumerable<int> list = Enumerable.Range(1, 10);
        var processedItems = new System.Collections.Concurrent.ConcurrentBag<int>();

        await CollectionHelper.ForEachAsync(list, async x =>
        {
            await Task.Delay(10);
            processedItems.Add(x);
        }, maxDegreeOfParallelism: 5);

        await Assert.That(processedItems.Count).IsEqualTo(10);
    }

    [Test]
    public async Task ForEachAsync_WithNullSource_ThrowsArgumentNullException()
    {
        IEnumerable<int>? list = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await CollectionHelper.ForEachAsync(list!, async x => await Task.Delay(1)));
    }

    [Test]
    public async Task ForEachAsync_WithNullAction_ThrowsArgumentNullException()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        Func<int, Task>? action = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await CollectionHelper.ForEachAsync(list, action!));
    }

    [Test]
    public async Task ForEachAsync_WithZeroParallelism_ThrowsArgumentOutOfRangeException()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await CollectionHelper.ForEachAsync(list, async x => await Task.Delay(1), maxDegreeOfParallelism: 0));
    }

    [Test]
    public async Task ForEachAsync_WithNegativeParallelism_ThrowsArgumentOutOfRangeException()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await CollectionHelper.ForEachAsync(list, async x => await Task.Delay(1), maxDegreeOfParallelism: -1));
    }

    [Test]
    public async Task ForEachAsync_WithEmptyCollection_DoesNotExecuteAction()
    {
        IEnumerable<int> list = new List<int>();
        var executed = false;

        await CollectionHelper.ForEachAsync(list, async x =>
        {
            await Task.Delay(1);
            executed = true;
        });

        await Assert.That(executed).IsFalse();
    }

    #endregion

    #region ForEachWithIndexAsync Tests

    [Test]
    public async Task ForEachWithIndexAsync_ExecutesActionForEachSequentially()
    {
        IEnumerable<string> list = new List<string> { "a", "b", "c" };
        var result = new System.Collections.Concurrent.ConcurrentBag<string>();

        await CollectionHelper.ForEachWithIndexAsync(list, async (item, index) =>
        {
            await Task.Delay(1);
            result.Add($"{index}:{item}");
        });

        await Assert.That(result.Count).IsEqualTo(3);
        await Assert.That(result).Contains("0:a");
        await Assert.That(result).Contains("1:b");
        await Assert.That(result).Contains("2:c");
    }

    [Test]
    public async Task ForEachWithIndexAsync_WithParallelism_ExecutesActionsInParallel()
    {
        IEnumerable<int> list = Enumerable.Range(1, 10);
        var processedItems = new System.Collections.Concurrent.ConcurrentBag<(int value, int index)>();

        await CollectionHelper.ForEachWithIndexAsync(list, async (x, index) =>
        {
            await Task.Delay(10);
            processedItems.Add((x, index));
        }, maxDegreeOfParallelism: 5);

        await Assert.That(processedItems.Count).IsEqualTo(10);
        // Verify all indices are present
        var indices = processedItems.Select(p => p.index).OrderBy(i => i).ToList();
        await Assert.That(indices).IsEquivalentTo(Enumerable.Range(0, 10));
    }

    [Test]
    public async Task ForEachWithIndexAsync_WithNullSource_ThrowsArgumentNullException()
    {
        IEnumerable<int>? list = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await CollectionHelper.ForEachWithIndexAsync(list!, async (x, i) => await Task.Delay(1)));
    }

    [Test]
    public async Task ForEachWithIndexAsync_WithNullAction_ThrowsArgumentNullException()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        Func<int, int, Task>? action = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await CollectionHelper.ForEachWithIndexAsync(list, action!));
    }

    [Test]
    public async Task ForEachWithIndexAsync_WithZeroParallelism_ThrowsArgumentOutOfRangeException()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await CollectionHelper.ForEachWithIndexAsync(list, async (x, i) => await Task.Delay(1), maxDegreeOfParallelism: 0));
    }

    [Test]
    public async Task ForEachWithIndexAsync_WithEmptyCollection_DoesNotExecuteAction()
    {
        IEnumerable<int> list = new List<int>();
        var executed = false;

        await CollectionHelper.ForEachWithIndexAsync(list, async (x, i) =>
        {
            await Task.Delay(1);
            executed = true;
        });

        await Assert.That(executed).IsFalse();
    }

    [Test]
    public async Task ForEachWithIndexAsync_MaintainsOrderInSequentialMode()
    {
        IEnumerable<int> list = Enumerable.Range(1, 5);
        var result = new List<int>();
        var lockObj = new object();

        await CollectionHelper.ForEachWithIndexAsync(list, async (x, i) =>
        {
            await Task.Delay(10);
            lock (lockObj)
            {
                result.Add(x);
            }
        }, maxDegreeOfParallelism: 1);

        await Assert.That(result).IsEquivalentTo(new[] { 1, 2, 3, 4, 5 });
    }

    #endregion

    #region GetRandom Tests

    [Test]
    public async Task GetRandom_ReturnsElementFromCollection()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3, 4, 5 };
        var random = CollectionHelper.GetRandom(list);

        await Assert.That(list).Contains(random);
    }

    [Test]
    public async Task GetRandom_WithEmptyCollection_ThrowsException()
    {
        IEnumerable<int> list = new List<int>();
        await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await Task.Run(() => CollectionHelper.GetRandom(list)));
    }

    [Test]
    public async Task GetRandom_WithNullSource_ThrowsArgumentNullException()
    {
        IEnumerable<int>? list = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.GetRandom(list!)));
    }

    [Test]
    public async Task GetRandom_WithSingleElement_ReturnsThatElement()
    {
        IEnumerable<int> list = new List<int> { 42 };
        var random = CollectionHelper.GetRandom(list);

        await Assert.That(random).IsEqualTo(42);
    }

    [Test]
    public async Task GetRandom_WithCustomRandom_UsesProvidedRandom()
    {
        var fixedRandom = new Random(12345);
        IEnumerable<int> list = new List<int> { 1, 2, 3, 4, 5 };
        var random = CollectionHelper.GetRandom(list, fixedRandom);

        await Assert.That(list).Contains(random);
    }

    #endregion

    #region GetRandomOrDefault Tests

    [Test]
    public async Task GetRandomOrDefault_WithEmptyCollection_ReturnsDefault()
    {
        IEnumerable<int> list = new List<int>();
        var random = CollectionHelper.GetRandomOrDefault(list);

        await Assert.That(random).IsEqualTo(0);
    }

    [Test]
    public async Task GetRandomOrDefault_WithItems_ReturnsElement()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3, 4, 5 };
        var random = CollectionHelper.GetRandomOrDefault(list);

        await Assert.That(list).Contains(random);
    }

    [Test]
    public async Task GetRandomOrDefault_WithNullSource_ThrowsArgumentNullException()
    {
        IEnumerable<int>? list = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.GetRandomOrDefault(list!)));
    }

    [Test]
    public async Task GetRandomOrDefault_WithReferenceType_ReturnsNull()
    {
        IEnumerable<string> list = new List<string>();
        var random = CollectionHelper.GetRandomOrDefault(list);

        await Assert.That(random).IsNull();
    }

    #endregion

    #region DefaultIfNullOrEmpty Tests

    [Test]
    public async Task DefaultIfNullOrEmpty_WithNull_ReturnsDefault()
    {
        IEnumerable<int>? list = null;
        var defaultList = new List<int> { 1, 2, 3 };
        var result = CollectionHelper.DefaultIfNullOrEmpty(list, defaultList);

        await Assert.That(result).IsEquivalentTo(defaultList);
    }

    [Test]
    public async Task DefaultIfNullOrEmpty_WithEmpty_ReturnsDefault()
    {
        IEnumerable<int> list = new List<int>();
        var defaultList = new List<int> { 1, 2, 3 };
        var result = CollectionHelper.DefaultIfNullOrEmpty(list, defaultList);

        await Assert.That(result).IsEquivalentTo(defaultList);
    }

    [Test]
    public async Task DefaultIfNullOrEmpty_WithItems_ReturnsOriginal()
    {
        IEnumerable<int> list = new List<int> { 4, 5, 6 };
        var defaultList = new List<int> { 1, 2, 3 };
        var result = CollectionHelper.DefaultIfNullOrEmpty(list, defaultList);

        await Assert.That(result).IsEquivalentTo(list);
    }

    [Test]
    public async Task DefaultIfNullOrEmpty_WithNullDefault_ThrowsArgumentNullException()
    {
        IEnumerable<int>? list = null;
        IEnumerable<int>? defaultList = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.DefaultIfNullOrEmpty(list, defaultList!)));
    }

    #endregion

    #region ChunkBy Tests

    [Test]
    public async Task ChunkBy_SplitsIntoChunks()
    {
        IEnumerable<int> list = Enumerable.Range(1, 10);
        var chunks = CollectionHelper.ChunkBy(list, 3).ToList();

        await Assert.That(chunks.Count).IsEqualTo(4);
        await Assert.That(chunks[0]).IsEquivalentTo(new[] { 1, 2, 3 });
        await Assert.That(chunks[1]).IsEquivalentTo(new[] { 4, 5, 6 });
        await Assert.That(chunks[2]).IsEquivalentTo(new[] { 7, 8, 9 });
        await Assert.That(chunks[3]).IsEquivalentTo(new[] { 10 });
    }

    [Test]
    public async Task ChunkBy_WithNullSource_ThrowsArgumentNullException()
    {
        IEnumerable<int>? list = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.ChunkBy(list!, 2).ToList()));
    }

    [Test]
    public async Task ChunkBy_WithZeroSize_ThrowsArgumentOutOfRangeException()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => CollectionHelper.ChunkBy(list, 0).ToList()));
    }

    [Test]
    public async Task ChunkBy_WithNegativeSize_ThrowsArgumentOutOfRangeException()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => CollectionHelper.ChunkBy(list, -1).ToList()));
    }

    #endregion

    #region Batch Tests

    [Test]
    public async Task Batch_SplitsIntoBatches()
    {
        IEnumerable<int> list = Enumerable.Range(1, 10);
        var batches = CollectionHelper.Batch(list, 4).ToList();

        await Assert.That(batches.Count).IsEqualTo(3);
        await Assert.That(batches[0]).IsEquivalentTo(new[] { 1, 2, 3, 4 });
        await Assert.That(batches[1]).IsEquivalentTo(new[] { 5, 6, 7, 8 });
        await Assert.That(batches[2]).IsEquivalentTo(new[] { 9, 10 });
    }

    [Test]
    public async Task Batch_WithNullSource_ThrowsArgumentNullException()
    {
        IEnumerable<int>? list = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.Batch(list!, 2).ToList()));
    }

    [Test]
    public async Task Batch_WithZeroSize_ThrowsArgumentOutOfRangeException()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await Task.Run(() => CollectionHelper.Batch(list, 0).ToList()));
    }

    #endregion

    #region ForEach Tests

    [Test]
    public async Task ForEach_ExecutesActionForEachElement()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        var sum = 0;

        CollectionHelper.ForEach(list, x => sum += x);

        await Assert.That(sum).IsEqualTo(6);
    }

    [Test]
    public async Task ForEach_WithNullSource_ThrowsArgumentNullException()
    {
        IEnumerable<int>? list = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.ForEach(list!, x => { })));
    }

    [Test]
    public async Task ForEach_WithNullAction_ThrowsArgumentNullException()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.ForEach(list, null!)));
    }

    #endregion

    #region ForEachWithIndex Tests

    [Test]
    public async Task ForEachWithIndex_ExecutesActionWithIndex()
    {
        IEnumerable<string> list = new List<string> { "a", "b", "c" };
        var result = new List<string>();

        CollectionHelper.ForEachWithIndex(list, (item, index) => result.Add($"{index}:{item}"));

        await Assert.That(result.Count).IsEqualTo(3);
        await Assert.That(result[0]).IsEqualTo("0:a");
        await Assert.That(result[1]).IsEqualTo("1:b");
        await Assert.That(result[2]).IsEqualTo("2:c");
    }

    [Test]
    public async Task ForEachWithIndex_WithNullSource_ThrowsArgumentNullException()
    {
        IEnumerable<int>? list = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.ForEachWithIndex(list!, (x, i) => { })));
    }

    [Test]
    public async Task ForEachWithIndex_WithNullAction_ThrowsArgumentNullException()
    {
        IEnumerable<int> list = new List<int> { 1, 2, 3 };
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.ForEachWithIndex(list, null!)));
    }

    #endregion

    #region WhereNotNull Tests

    [Test]
    public async Task WhereNotNull_FiltersOutNulls()
    {
        IEnumerable<string?> list = new List<string?> { "a", null, "b", null, "c" };
        var result = CollectionHelper.WhereNotNull(list).ToList();

        await Assert.That(result.Count).IsEqualTo(3);
        await Assert.That(result).Contains("a");
        await Assert.That(result).Contains("b");
        await Assert.That(result).Contains("c");
    }

    [Test]
    public async Task WhereNotNull_WithAllNulls_ReturnsEmpty()
    {
        IEnumerable<string?> list = new List<string?> { null, null, null };
        var result = CollectionHelper.WhereNotNull(list).ToList();

        await Assert.That(result.Count).IsEqualTo(0);
    }

    [Test]
    public async Task WhereNotNull_WithNoNulls_ReturnsAll()
    {
        IEnumerable<string?> list = new List<string?> { "a", "b", "c" };
        var result = CollectionHelper.WhereNotNull(list).ToList();

        await Assert.That(result.Count).IsEqualTo(3);
    }

    [Test]
    public async Task WhereNotNull_WithNullSource_ThrowsArgumentNullException()
    {
        IEnumerable<string?>? list = null;
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => CollectionHelper.WhereNotNull(list!).ToList()));
    }

    #endregion
}
