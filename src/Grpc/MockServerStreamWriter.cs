using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;

namespace InsightArchitectures.Testing
{
    /// <summary>
    /// An implementation of <see cref="IServerStreamWriter{T}" /> suitable for tests.
    /// </summary>
    /// <typeparam name="T">The type of the message contained in the stream.</typeparam>
    public class MockServerStreamWriter<T> : IServerStreamWriter<T>, IEnumerable<T>
    {
        private readonly IList<T> _items = new List<T>();

        /// <inheritdoc/>
        public Task WriteAsync(T message)
        {
            _items.Add(message);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public WriteOptions WriteOptions { get; set; } = WriteOptions.Default;

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
