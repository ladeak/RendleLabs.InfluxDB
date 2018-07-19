using System;

namespace RendleLabs.InfluxDB
{
    public struct WriteRequest
    {
        internal static WriteRequest FlushRequest = new WriteRequest(true);
        public ILineWriter Writer { get; }
        public object Args { get; }
        public long Timestamp { get; }
        internal bool Flush { get; }

        public WriteRequest(ILineWriter writer, object args, DateTimeOffset? timestamp = null)
        {
            Writer = writer;
            Args = args;
            Timestamp = timestamp.GetValueOrDefault(DateTimeOffset.UtcNow).ToUnixTimeMilliseconds();
        }

        private WriteRequest(bool flush)
        {
            Flush = flush;
            Writer = default;
            Args = default;
            Timestamp = default;
        }
    }
}