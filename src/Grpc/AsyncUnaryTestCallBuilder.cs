using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Testing;

namespace InsightArchitectures.Testing
{
    /// <summary>
    /// A builder of <see cref="AsyncUnaryCall{TResponse}"/>.
    /// </summary>
    /// <typeparam name="TResponse">Response message type for the call.</typeparam>
    public class AsyncUnaryTestCallBuilder<TResponse> : TestCallBuilder<AsyncUnaryCall<TResponse>>
    {
        private readonly Task<TResponse> _response;

        /// <summary>
        /// Creates a <see cref="AsyncUnaryTestCallBuilder{TResponse}" />.
        /// </summary>
        /// <param name="response">The response to be returned.</param>
        public AsyncUnaryTestCallBuilder(Task<TResponse> response)
        {
            _response = response ?? throw new ArgumentNullException(nameof(response));
        }

        /// <inheritdoc />
        public override AsyncUnaryCall<TResponse> Build() => TestCalls.AsyncUnaryCall(_response, ResponseHeaders, StatusFunc, TrailersFunc, DisposeAction);
    }
}
