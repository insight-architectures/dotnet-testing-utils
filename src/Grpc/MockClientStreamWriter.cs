using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;

namespace InsightArchitectures.Testing
{
    /// <summary>
    /// An implementation of <see cref="IClientStreamWriter{T}" /> suitable for tests.
    /// </summary>
    /// <typeparam name="T">The type of the message contained in the stream.</typeparam>
    public class MockClientStreamWriter<T> : IClientStreamWriter<T>, IEnumerable<T>
    {
        private readonly IList<T> _items = new List<T>();

        /// <inheritdoc/>
        public Task WriteAsync(T message)
        {
            if (_isComplete)
            {
                throw new Exception();
            }

            _items.Add(message);

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public WriteOptions WriteOptions { get; set; } = WriteOptions.Default;

        private bool _isComplete;

        /// <inheritdoc/>
        public Task CompleteAsync()
        {
            if (_isComplete)
            {
                throw new Exception();
            }

            _isComplete = true;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
