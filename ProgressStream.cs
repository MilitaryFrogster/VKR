using System;
using System.IO;
using System.Threading.Tasks;

public class ProgressStream : Stream
{
    private readonly Stream _baseStream;
    private readonly Action<long> _progressAction;
    private long _totalBytesRead;

    public ProgressStream(Stream baseStream, Action<long> progressAction)
    {
        _baseStream = baseStream ?? throw new ArgumentNullException(nameof(baseStream));
        _progressAction = progressAction ?? throw new ArgumentNullException(nameof(progressAction));
        _totalBytesRead = 0;
    }

    public override bool CanRead => _baseStream.CanRead;
    public override bool CanSeek => _baseStream.CanSeek;
    public override bool CanWrite => false;
    public override long Length => _baseStream.Length;
    public override long Position
    {
        get => _baseStream.Position;
        set => _baseStream.Position = value;
    }

    public override void Flush() => _baseStream.Flush();

    public override int Read(byte[] buffer, int offset, int count)
    {
        int bytesRead = _baseStream.Read(buffer, offset, count);
        _totalBytesRead += bytesRead;
        _progressAction(_totalBytesRead);
        return bytesRead;
    }

    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken)
    {
        int bytesRead = await _baseStream.ReadAsync(buffer, offset, count, cancellationToken);
        _totalBytesRead += bytesRead;
        _progressAction(_totalBytesRead);
        Console.WriteLine($"[DEBUG] Прочитано {bytesRead} байт, всего: {_totalBytesRead}");
        return bytesRead;
    }

    public override long Seek(long offset, SeekOrigin origin) => _baseStream.Seek(offset, origin);
    public override void SetLength(long value) => _baseStream.SetLength(value);
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
}
