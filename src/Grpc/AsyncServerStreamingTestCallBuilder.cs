using System;
using Grpc.Core;
using Grpc.Core.Testing;

namespace InsightArchitectures.Testing
{
    /// <summary>
    /// A builder of <see cref="AsyncServerStreamingCall{TResponse}"/>.
    /// </summary>
    /// <typeparam name="TResponse">Response message type for the call.</typeparam>
    public class AsyncServerStreamingTestCallBuilder<TResponse> : TestCallBuilder<AsyncServerStreamingCall<TResponse>>
    {
        private readonly IAsyncStreamReader<TResponse> _responses;

        /// <summary>
        /// Creates a <see cref="AsyncServerStreamingTestCallBuilder{TResponse}" />.
        /// </summary>
        /// <param name="responses">The responses to be returned.</param>
        public AsyncServerStreamingTestCallBuilder(IAsyncStreamReader<TResponse> responses)
        {
            _responses = responses ?? throw new ArgumentNullException(nameof(responses));
        }

        /// <inheritdoc />
        public override AsyncServerStreamingCall<TResponse> Build() => TestCalls.AsyncServerStreamingCall(_responses, ResponseHeaders, StatusFunc, TrailersFunc, DisposeAction);
    }
}
