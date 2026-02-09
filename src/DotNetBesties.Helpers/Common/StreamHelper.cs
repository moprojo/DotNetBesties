using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetBesties.Helpers.Common;

/// <summary>
/// Helper methods for working with streams.
/// </summary>
public static class StreamHelper
{
    private const int DefaultBufferSize = 81920; // 80 KB

    #region Stream to Byte Array

    /// <summary>
    /// Reads all bytes from a stream.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <returns>A byte array containing the stream contents.</returns>
    /// <exception cref="ArgumentNullException">Thrown when stream is null.</exception>
    public static byte[] ToByteArray(Stream stream)
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

        if (stream is MemoryStream ms)
            return ms.ToArray();

        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream, DefaultBufferSize);
        return memoryStream.ToArray();
    }

    /// <summary>
    /// Asynchronously reads all bytes from a stream.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A byte array containing the stream contents.</returns>
    /// <exception cref="ArgumentNullException">Thrown when stream is null.</exception>
    public static async Task<byte[]> ToByteArrayAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

        if (stream is MemoryStream ms)
            return ms.ToArray();

        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream, DefaultBufferSize, cancellationToken);
        return memoryStream.ToArray();
    }

    #endregion

    #region Stream to String

    /// <summary>
    /// Reads all text from a stream using the specified encoding.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <param name="encoding">The encoding to use (default: UTF-8).</param>
    /// <returns>The string contents of the stream.</returns>
    /// <exception cref="ArgumentNullException">Thrown when stream is null.</exception>
    public static string ToString(Stream stream, Encoding? encoding = null)
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

        encoding ??= Encoding.UTF8;

        using var reader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks: true, bufferSize: DefaultBufferSize, leaveOpen: true);
        return reader.ReadToEnd();
    }

    /// <summary>
    /// Asynchronously reads all text from a stream using the specified encoding.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <param name="encoding">The encoding to use (default: UTF-8).</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The string contents of the stream.</returns>
    /// <exception cref="ArgumentNullException">Thrown when stream is null.</exception>
    public static async Task<string> ToStringAsync(Stream stream, Encoding? encoding = null, CancellationToken cancellationToken = default)
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

        encoding ??= Encoding.UTF8;

        using var reader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks: true, bufferSize: DefaultBufferSize, leaveOpen: true);
        return await reader.ReadToEndAsync(cancellationToken);
    }

    #endregion

    #region Byte Array to Stream

    /// <summary>
    /// Creates a MemoryStream from a byte array.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="writable">Whether the stream should be writable.</param>
    /// <returns>A MemoryStream containing the bytes.</returns>
    /// <exception cref="ArgumentNullException">Thrown when bytes is null.</exception>
    public static MemoryStream FromByteArray(byte[] bytes, bool writable = false)
    {
        if (bytes == null)
            throw new ArgumentNullException(nameof(bytes));

        return new MemoryStream(bytes, writable);
    }

    #endregion

    #region String to Stream

    /// <summary>
    /// Creates a MemoryStream from a string using the specified encoding.
    /// </summary>
    /// <param name="text">The string to convert.</param>
    /// <param name="encoding">The encoding to use (default: UTF-8).</param>
    /// <returns>A MemoryStream containing the string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when text is null.</exception>
    public static MemoryStream FromString(string text, Encoding? encoding = null)
    {
        if (text == null)
            throw new ArgumentNullException(nameof(text));

        encoding ??= Encoding.UTF8;
        var bytes = encoding.GetBytes(text);
        return new MemoryStream(bytes);
    }

    #endregion

    #region Copy Operations

    /// <summary>
    /// Copies the contents of one stream to another.
    /// </summary>
    /// <param name="source">The source stream.</param>
    /// <param name="destination">The destination stream.</param>
    /// <param name="bufferSize">The buffer size (default: 80 KB).</param>
    /// <exception cref="ArgumentNullException">Thrown when source or destination is null.</exception>
    public static void Copy(Stream source, Stream destination, int bufferSize = DefaultBufferSize)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (destination == null)
            throw new ArgumentNullException(nameof(destination));

        source.CopyTo(destination, bufferSize);
    }

    /// <summary>
    /// Asynchronously copies the contents of one stream to another.
    /// </summary>
    /// <param name="source">The source stream.</param>
    /// <param name="destination">The destination stream.</param>
    /// <param name="bufferSize">The buffer size (default: 80 KB).</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="ArgumentNullException">Thrown when source or destination is null.</exception>
    public static async Task CopyAsync(Stream source, Stream destination, int bufferSize = DefaultBufferSize, CancellationToken cancellationToken = default)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (destination == null)
            throw new ArgumentNullException(nameof(destination));

        await source.CopyToAsync(destination, bufferSize, cancellationToken);
    }

    #endregion

    #region Stream Properties

    /// <summary>
    /// Checks if a stream is readable.
    /// </summary>
    /// <param name="stream">The stream to check.</param>
    /// <returns><c>true</c> if the stream can be read; otherwise, <c>false</c>.</returns>
    public static bool IsReadable(Stream? stream)
        => stream?.CanRead ?? false;

    /// <summary>
    /// Checks if a stream is writable.
    /// </summary>
    /// <param name="stream">The stream to check.</param>
    /// <returns><c>true</c> if the stream can be written to; otherwise, <c>false</c>.</returns>
    public static bool IsWritable(Stream? stream)
        => stream?.CanWrite ?? false;

    /// <summary>
    /// Checks if a stream is seekable.
    /// </summary>
    /// <param name="stream">The stream to check.</param>
    /// <returns><c>true</c> if the stream supports seeking; otherwise, <c>false</c>.</returns>
    public static bool IsSeekable(Stream? stream)
        => stream?.CanSeek ?? false;

    /// <summary>
    /// Gets the length of a stream, or null if the stream doesn't support seeking.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>The length of the stream, or null.</returns>
    public static long? GetLength(Stream? stream)
    {
        if (stream == null || !stream.CanSeek)
            return null;

        try
        {
            return stream.Length;
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region File Operations

    /// <summary>
    /// Reads all bytes from a file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>A byte array containing the file contents.</returns>
    /// <exception cref="ArgumentNullException">Thrown when filePath is null.</exception>
    public static byte[] ReadAllBytes(string filePath)
    {
        if (filePath == null)
            throw new ArgumentNullException(nameof(filePath));

        return File.ReadAllBytes(filePath);
    }

    /// <summary>
    /// Asynchronously reads all bytes from a file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A byte array containing the file contents.</returns>
    /// <exception cref="ArgumentNullException">Thrown when filePath is null.</exception>
    public static async Task<byte[]> ReadAllBytesAsync(string filePath, CancellationToken cancellationToken = default)
    {
        if (filePath == null)
            throw new ArgumentNullException(nameof(filePath));

        return await File.ReadAllBytesAsync(filePath, cancellationToken);
    }

    /// <summary>
    /// Writes all bytes to a file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="bytes">The bytes to write.</param>
    /// <exception cref="ArgumentNullException">Thrown when filePath or bytes is null.</exception>
    public static void WriteAllBytes(string filePath, byte[] bytes)
    {
        if (filePath == null)
            throw new ArgumentNullException(nameof(filePath));

        if (bytes == null)
            throw new ArgumentNullException(nameof(bytes));

        File.WriteAllBytes(filePath, bytes);
    }

    /// <summary>
    /// Asynchronously writes all bytes to a file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="bytes">The bytes to write.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="ArgumentNullException">Thrown when filePath or bytes is null.</exception>
    public static async Task WriteAllBytesAsync(string filePath, byte[] bytes, CancellationToken cancellationToken = default)
    {
        if (filePath == null)
            throw new ArgumentNullException(nameof(filePath));

        if (bytes == null)
            throw new ArgumentNullException(nameof(bytes));

        await File.WriteAllBytesAsync(filePath, bytes, cancellationToken);
    }

    #endregion
}
