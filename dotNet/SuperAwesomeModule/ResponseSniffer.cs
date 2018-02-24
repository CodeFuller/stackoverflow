using System.IO;

namespace SuperAwesomeModule
{
    public class ResponseSniffer : Stream
    {
        private readonly Stream _responseStream;

        public MemoryStream RecordStream { get; set; }

        #region Implements of Stream
        public override bool CanRead => _responseStream.CanRead;

        public override bool CanSeek => _responseStream.CanSeek;

        public override bool CanWrite => _responseStream.CanWrite;

        public override void Flush()
        {
            _responseStream.Flush();
        }

        public override long Length => _responseStream.Length;

        public override long Position
        {
            get => _responseStream.Position;
            set => _responseStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _responseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin) => _responseStream.Seek(offset, origin);

        public override void SetLength(long value)
        {
            _responseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            RecordStream.Write(buffer, offset, count);
            _responseStream.Write(buffer, offset, count);
        }
        #endregion

        public ResponseSniffer(Stream stream)
        {
            RecordStream = new MemoryStream();
            _responseStream = stream;
        }
    }
}