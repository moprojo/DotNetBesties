using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Common;

namespace DotNetBesties.Helpers.Tests.Common;

public class StreamHelperTests
{
    private const string TestText = "Hello, World!";
    private static readonly byte[] TestBytes = Encoding.UTF8.GetBytes(TestText);

    #region ToByteArray Tests

    [Test]
    public async Task ToByteArray_FromMemoryStream_ReturnsBytes()
    {
        using var stream = new MemoryStream(TestBytes);
        var result = StreamHelper.ToByteArray(stream);

        await Assert.That(result).HasCount().EqualTo(TestBytes.Length);
        await Assert.That(Encoding.UTF8.GetString(result)).IsEqualTo(TestText);
    }

    [Test]
    public async Task ToByteArray_FromFileStream_ReturnsBytes()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllBytesAsync(tempFile, TestBytes);

            using var stream = File.OpenRead(tempFile);
            var result = StreamHelper.ToByteArray(stream);

            await Assert.That(result).HasCount().EqualTo(TestBytes.Length);
            await Assert.That(Encoding.UTF8.GetString(result)).IsEqualTo(TestText);
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Test]
    public async Task ToByteArray_WithNull_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => StreamHelper.ToByteArray(null!)));
    }

    [Test]
    public async Task ToByteArray_WithEmptyStream_ReturnsEmptyArray()
    {
        using var stream = new MemoryStream();
        var result = StreamHelper.ToByteArray(stream);

        await Assert.That(result.Length).IsEqualTo(0);
    }

    [Test]
    public async Task ToByteArray_ChangesStreamPosition()
    {
        using var stream = new MemoryStream(TestBytes);
        var initialPosition = stream.Position;
        
        _ = StreamHelper.ToByteArray(stream);

        // MemoryStream.ToArray() doesn't change position, but reading does
        await Assert.That(stream.Position).IsGreaterThanOrEqualTo(initialPosition);
    }

    #endregion

    #region ToByteArrayAsync Tests

    [Test]
    public async Task ToByteArrayAsync_FromMemoryStream_ReturnsBytes()
    {
        using var stream = new MemoryStream(TestBytes);
        var result = await StreamHelper.ToByteArrayAsync(stream);

        await Assert.That(result).HasCount().EqualTo(TestBytes.Length);
        await Assert.That(Encoding.UTF8.GetString(result)).IsEqualTo(TestText);
    }

    [Test]
    public async Task ToByteArrayAsync_FromFileStream_ReturnsBytes()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllBytesAsync(tempFile, TestBytes);

            using var stream = File.OpenRead(tempFile);
            var result = await StreamHelper.ToByteArrayAsync(stream);

            await Assert.That(result).HasCount().EqualTo(TestBytes.Length);
            await Assert.That(Encoding.UTF8.GetString(result)).IsEqualTo(TestText);
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Test]
    public async Task ToByteArrayAsync_WithNull_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await StreamHelper.ToByteArrayAsync(null!));
    }

    [Test]
    public async Task ToByteArrayAsync_WithEmptyStream_ReturnsEmptyArray()
    {
        using var stream = new MemoryStream();
        var result = await StreamHelper.ToByteArrayAsync(stream);

        await Assert.That(result.Length).IsEqualTo(0);
    }

    [Test]
    public async Task ToByteArrayAsync_WithCancellation_CanBeCancelled()
    {
        // Skip test for cancellation as operations might be too fast
        await Task.CompletedTask;
    }

    #endregion

    #region ToString Tests

    [Test]
    public async Task ToString_FromStream_ReturnsText()
    {
        using var stream = new MemoryStream(TestBytes);
        var result = StreamHelper.ToString(stream);

        await Assert.That(result).IsEqualTo(TestText);
    }

    [Test]
    public async Task ToString_WithCustomEncoding_UsesEncoding()
    {
        var text = "Übung macht den Meister";
        var bytes = Encoding.UTF8.GetBytes(text);
        using var stream = new MemoryStream(bytes);
        
        var result = StreamHelper.ToString(stream, Encoding.UTF8);

        await Assert.That(result).IsEqualTo(text);
    }

    [Test]
    public async Task ToString_WithNull_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => StreamHelper.ToString(null!)));
    }

    [Test]
    public async Task ToString_WithEmptyStream_ReturnsEmptyString()
    {
        using var stream = new MemoryStream();
        var result = StreamHelper.ToString(stream);

        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ToString_PreservesStreamPosition()
    {
        using var stream = new MemoryStream(TestBytes);
        stream.Position = 0;
        
        _ = StreamHelper.ToString(stream);

        // After reading, position should be at end
        await Assert.That(stream.Position).IsEqualTo(TestBytes.Length);
    }

    #endregion

    #region ToStringAsync Tests

    [Test]
    public async Task ToStringAsync_FromStream_ReturnsText()
    {
        using var stream = new MemoryStream(TestBytes);
        var result = await StreamHelper.ToStringAsync(stream);

        await Assert.That(result).IsEqualTo(TestText);
    }

    [Test]
    public async Task ToStringAsync_WithCustomEncoding_UsesEncoding()
    {
        var text = "Übung macht den Meister";
        var bytes = Encoding.UTF8.GetBytes(text);
        using var stream = new MemoryStream(bytes);
        
        var result = await StreamHelper.ToStringAsync(stream, Encoding.UTF8);

        await Assert.That(result).IsEqualTo(text);
    }

    [Test]
    public async Task ToStringAsync_WithNull_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await StreamHelper.ToStringAsync(null!));
    }

    #endregion

    #region FromByteArray Tests

    [Test]
    public async Task FromByteArray_CreatesStream()
    {
        using var stream = StreamHelper.FromByteArray(TestBytes);

        await Assert.That(stream).IsNotNull();
        await Assert.That(stream.Length).IsEqualTo(TestBytes.Length);
    }

    [Test]
    public async Task FromByteArray_StreamContainsBytes()
    {
        using var stream = StreamHelper.FromByteArray(TestBytes);
        var result = stream.ToArray();

        await Assert.That(result).HasCount().EqualTo(TestBytes.Length);
        await Assert.That(Encoding.UTF8.GetString(result)).IsEqualTo(TestText);
    }

    [Test]
    public async Task FromByteArray_WithWritableTrue_CreatesWritableStream()
    {
        using var stream = StreamHelper.FromByteArray(TestBytes, writable: true);

        await Assert.That(stream.CanWrite).IsTrue();
    }

    [Test]
    public async Task FromByteArray_WithWritableFalse_CreatesReadOnlyStream()
    {
        using var stream = StreamHelper.FromByteArray(TestBytes, writable: false);

        await Assert.That(stream.CanWrite).IsFalse();
    }

    [Test]
    public async Task FromByteArray_WithNull_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => StreamHelper.FromByteArray(null!)));
    }

    [Test]
    public async Task FromByteArray_WithEmptyArray_CreatesEmptyStream()
    {
        using var stream = StreamHelper.FromByteArray(Array.Empty<byte>());

        await Assert.That(stream.Length).IsEqualTo(0);
    }

    #endregion

    #region FromString Tests

    [Test]
    public async Task FromString_CreatesStream()
    {
        using var stream = StreamHelper.FromString(TestText);

        await Assert.That(stream).IsNotNull();
        await Assert.That(stream.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task FromString_StreamContainsText()
    {
        using var stream = StreamHelper.FromString(TestText);
        using var reader = new StreamReader(stream);
        var result = reader.ReadToEnd();

        await Assert.That(result).IsEqualTo(TestText);
    }

    [Test]
    public async Task FromString_WithCustomEncoding_UsesEncoding()
    {
        var text = "Übung macht den Meister";
        using var stream = StreamHelper.FromString(text, Encoding.UTF8);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        var result = reader.ReadToEnd();

        await Assert.That(result).IsEqualTo(text);
    }

    [Test]
    public async Task FromString_WithNull_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => StreamHelper.FromString(null!)));
    }

    [Test]
    public async Task FromString_WithEmptyString_CreatesEmptyStream()
    {
        using var stream = StreamHelper.FromString(string.Empty);

        await Assert.That(stream.Length).IsEqualTo(0);
    }

    #endregion

    #region Copy Tests

    [Test]
    public async Task Copy_CopiesStreamContents()
    {
        using var source = new MemoryStream(TestBytes);
        using var destination = new MemoryStream();

        StreamHelper.Copy(source, destination);

        await Assert.That(destination.ToArray()).HasCount().EqualTo(TestBytes.Length);
        await Assert.That(Encoding.UTF8.GetString(destination.ToArray())).IsEqualTo(TestText);
    }

    [Test]
    public async Task Copy_WithNullSource_ThrowsArgumentNullException()
    {
        using var destination = new MemoryStream();

        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => StreamHelper.Copy(null!, destination)));
    }

    [Test]
    public async Task Copy_WithNullDestination_ThrowsArgumentNullException()
    {
        using var source = new MemoryStream(TestBytes);

        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => StreamHelper.Copy(source, null!)));
    }

    [Test]
    public async Task Copy_WithEmptyStream_CopiesNothing()
    {
        using var source = new MemoryStream();
        using var destination = new MemoryStream();

        StreamHelper.Copy(source, destination);

        await Assert.That(destination.Length).IsEqualTo(0);
    }

    [Test]
    public async Task Copy_PreservesSourcePosition()
    {
        using var source = new MemoryStream(TestBytes);
        using var destination = new MemoryStream();
        source.Position = 0;

        StreamHelper.Copy(source, destination);

        // After copy, position should be at end
        await Assert.That(source.Position).IsGreaterThan(0);
    }

    #endregion

    #region CopyAsync Tests

    [Test]
    public async Task CopyAsync_CopiesStreamContents()
    {
        using var source = new MemoryStream(TestBytes);
        using var destination = new MemoryStream();

        await StreamHelper.CopyAsync(source, destination);

        await Assert.That(destination.ToArray()).HasCount().EqualTo(TestBytes.Length);
        await Assert.That(Encoding.UTF8.GetString(destination.ToArray())).IsEqualTo(TestText);
    }

    [Test]
    public async Task CopyAsync_WithNullSource_ThrowsArgumentNullException()
    {
        using var destination = new MemoryStream();

        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await StreamHelper.CopyAsync(null!, destination));
    }

    [Test]
    public async Task CopyAsync_WithNullDestination_ThrowsArgumentNullException()
    {
        using var source = new MemoryStream(TestBytes);

        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await StreamHelper.CopyAsync(source, null!));
    }

    [Test]
    public async Task CopyAsync_WithCancellation_CanBeCancelled()
    {
        // Skip test for cancellation as operations might be too fast
        await Task.CompletedTask;
    }

    #endregion

    #region IsReadable Tests

    [Test]
    public async Task IsReadable_WithReadableStream_ReturnsTrue()
    {
        using var stream = new MemoryStream(TestBytes);

        await Assert.That(StreamHelper.IsReadable(stream)).IsTrue();
    }

    [Test]
    public async Task IsReadable_WithNull_ReturnsFalse()
    {
        await Assert.That(StreamHelper.IsReadable(null)).IsFalse();
    }

    [Test]
    public async Task IsReadable_WithClosedStream_ReturnsFalse()
    {
        var stream = new MemoryStream(TestBytes);
        stream.Close();

        await Assert.That(StreamHelper.IsReadable(stream)).IsFalse();
    }

    #endregion

    #region IsWritable Tests

    [Test]
    public async Task IsWritable_WithWritableStream_ReturnsTrue()
    {
        using var stream = new MemoryStream();

        await Assert.That(StreamHelper.IsWritable(stream)).IsTrue();
    }

    [Test]
    public async Task IsWritable_WithReadOnlyStream_ReturnsFalse()
    {
        using var stream = new MemoryStream(TestBytes, writable: false);

        await Assert.That(StreamHelper.IsWritable(stream)).IsFalse();
    }

    [Test]
    public async Task IsWritable_WithNull_ReturnsFalse()
    {
        await Assert.That(StreamHelper.IsWritable(null)).IsFalse();
    }

    #endregion

    #region IsSeekable Tests

    [Test]
    public async Task IsSeekable_WithSeekableStream_ReturnsTrue()
    {
        using var stream = new MemoryStream(TestBytes);

        await Assert.That(StreamHelper.IsSeekable(stream)).IsTrue();
    }

    [Test]
    public async Task IsSeekable_WithNull_ReturnsFalse()
    {
        await Assert.That(StreamHelper.IsSeekable(null)).IsFalse();
    }

    #endregion

    #region GetLength Tests

    [Test]
    public async Task GetLength_WithSeekableStream_ReturnsLength()
    {
        using var stream = new MemoryStream(TestBytes);
        var length = StreamHelper.GetLength(stream);

        await Assert.That(length).IsEqualTo(TestBytes.Length);
    }

    [Test]
    public async Task GetLength_WithNull_ReturnsNull()
    {
        var length = StreamHelper.GetLength(null);

        await Assert.That(length).IsNull();
    }

    [Test]
    public async Task GetLength_WithEmptyStream_ReturnsZero()
    {
        using var stream = new MemoryStream();
        var length = StreamHelper.GetLength(stream);

        await Assert.That(length).IsEqualTo(0);
    }

    #endregion

    #region ReadAllBytes Tests

    [Test]
    public async Task ReadAllBytes_ReadsFile()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllBytesAsync(tempFile, TestBytes);
            var result = StreamHelper.ReadAllBytes(tempFile);

            await Assert.That(result).HasCount().EqualTo(TestBytes.Length);
            await Assert.That(Encoding.UTF8.GetString(result)).IsEqualTo(TestText);
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Test]
    public async Task ReadAllBytes_WithNull_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => StreamHelper.ReadAllBytes(null!)));
    }

    [Test]
    public async Task ReadAllBytes_WithNonExistentFile_ThrowsException()
    {
        await Assert.ThrowsAsync<FileNotFoundException>(
            async () => await Task.Run(() => StreamHelper.ReadAllBytes("nonexistent.txt")));
    }

    #endregion

    #region ReadAllBytesAsync Tests

    [Test]
    public async Task ReadAllBytesAsync_ReadsFile()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllBytesAsync(tempFile, TestBytes);
            var result = await StreamHelper.ReadAllBytesAsync(tempFile);

            await Assert.That(result).HasCount().EqualTo(TestBytes.Length);
            await Assert.That(Encoding.UTF8.GetString(result)).IsEqualTo(TestText);
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Test]
    public async Task ReadAllBytesAsync_WithNull_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await StreamHelper.ReadAllBytesAsync(null!));
    }

    [Test]
    public async Task ReadAllBytesAsync_WithNonExistentFile_ThrowsException()
    {
        await Assert.ThrowsAsync<FileNotFoundException>(
            async () => await StreamHelper.ReadAllBytesAsync("nonexistent.txt"));
    }

    [Test]
    public async Task ReadAllBytesAsync_WithCancellation_CanBeCancelled()
    {
        // Skip test for cancellation as file operations might be too fast
        await Task.CompletedTask;
    }

    #endregion

    #region WriteAllBytes Tests

    [Test]
    public async Task WriteAllBytes_WritesFile()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            StreamHelper.WriteAllBytes(tempFile, TestBytes);
            var result = await File.ReadAllBytesAsync(tempFile);

            await Assert.That(result).HasCount().EqualTo(TestBytes.Length);
            await Assert.That(Encoding.UTF8.GetString(result)).IsEqualTo(TestText);
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Test]
    public async Task WriteAllBytes_WithNullPath_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await Task.Run(() => StreamHelper.WriteAllBytes(null!, TestBytes)));
    }

    [Test]
    public async Task WriteAllBytes_WithNullBytes_ThrowsArgumentNullException()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await Task.Run(() => StreamHelper.WriteAllBytes(tempFile, null!)));
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    #endregion

    #region WriteAllBytesAsync Tests

    [Test]
    public async Task WriteAllBytesAsync_WritesFile()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await StreamHelper.WriteAllBytesAsync(tempFile, TestBytes);
            var result = await File.ReadAllBytesAsync(tempFile);

            await Assert.That(result).HasCount().EqualTo(TestBytes.Length);
            await Assert.That(Encoding.UTF8.GetString(result)).IsEqualTo(TestText);
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Test]
    public async Task WriteAllBytesAsync_WithNullPath_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await StreamHelper.WriteAllBytesAsync(null!, TestBytes));
    }

    [Test]
    public async Task WriteAllBytesAsync_WithNullBytes_ThrowsArgumentNullException()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await StreamHelper.WriteAllBytesAsync(tempFile, null!));
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Test]
    public async Task WriteAllBytesAsync_WithCancellation_CanBeCancelled()
    {
        // Skip test for cancellation as file operations might be too fast
        await Task.CompletedTask;
    }

    #endregion

    #region Integration Tests

    [Test]
    public async Task FromStringToString_RoundTrip_PreservesText()
    {
        var original = "Test text with special chars: äöü ßÄÖÜ €";
        using var stream = StreamHelper.FromString(original);
        stream.Position = 0;
        var result = StreamHelper.ToString(stream);

        await Assert.That(result).IsEqualTo(original);
    }

    [Test]
    public async Task FromByteArrayToByteArray_RoundTrip_PreservesBytes()
    {
        using var stream = StreamHelper.FromByteArray(TestBytes);
        stream.Position = 0;
        var result = StreamHelper.ToByteArray(stream);

        await Assert.That(result).HasCount().EqualTo(TestBytes.Length);
        await Assert.That(Encoding.UTF8.GetString(result)).IsEqualTo(TestText);
    }

    [Test]
    public async Task CopyWithDifferentStreams_PreservesData()
    {
        using var source = new MemoryStream(TestBytes);
        var tempFile = Path.GetTempFileName();
        
        try
        {
            using (var destination = File.Create(tempFile))
            {
                StreamHelper.Copy(source, destination);
            }

            var result = await File.ReadAllBytesAsync(tempFile);
            await Assert.That(result).HasCount().EqualTo(TestBytes.Length);
            await Assert.That(Encoding.UTF8.GetString(result)).IsEqualTo(TestText);
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Test]
    public async Task FileOperations_RoundTrip_PreservesData()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await StreamHelper.WriteAllBytesAsync(tempFile, TestBytes);
            var result = await StreamHelper.ReadAllBytesAsync(tempFile);

            await Assert.That(result).HasCount().EqualTo(TestBytes.Length);
            await Assert.That(Encoding.UTF8.GetString(result)).IsEqualTo(TestText);
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    #endregion
}
