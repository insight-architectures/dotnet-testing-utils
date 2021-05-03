using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace InsightArchitectures.Testing
{
    /// <summary>
    /// An implementation of <see cref="IAsyncStreamReader{T}" /> suitable for tests.
    /// </summary>
    /// <typeparam name="T">The type of the message contained in the stream.</typeparam>
    public sealed class MockAsyncStreamReader<T> : IAsyncStreamReader<T>, IDisposable
    {
        private readonly IEnumerator<T> _enumerator;

        /// <summary>
        /// Creates an instance of <see cref="MockAsyncStreamReader{T}" /> with a predefined set of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="items">The items contained in the fake stream.</param>
        public MockAsyncStreamReader(IEnumerable<T> items)
        {
            _enumerator = items?.GetEnumerator() ?? throw new ArgumentNullException(nameof(items));
        }

        /// <inheritdoc />
        public Task<bool> MoveNext(CancellationToken cancellationToken) => Task.FromResult(_enumerator.MoveNext());

        /// <inheritdoc />
        public T Current => _enumerator.Current;

        /// <inheritdoc />
        public void Dispose() => _enumerator?.Dispose();
    }
}
