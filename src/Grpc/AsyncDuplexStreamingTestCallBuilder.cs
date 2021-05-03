using System;
using Grpc.Core;
using Grpc.Core.Testing;

namespace InsightArchitectures.Testing
{
    /// <summary>
    /// A builder of <see cref="AsyncDuplexStreamingCall{TRequest,TResponse}"/>.
    /// </summary>
    /// <typeparam name="TRequest">Request message type for this call.</typeparam>
    /// <typeparam name="TResponse">Response message type for this call.</typeparam>
    public class AsyncDuplexStreamingTestCallBuilder<TRequest, TResponse> : TestCallBuilder<AsyncDuplexStreamingCall<TRequest, TResponse>>
    {
        private readonly IClientStreamWriter<TRequest> _requests;
        private readonly IAsyncStreamReader<TResponse> _responses;

        /// <summary>
        /// Creates a <see cref="AsyncDuplexStreamingTestCallBuilder{TRequest, TResponse}" />.
        /// </summary>
        /// <param name="requests">Request values.</param>
        /// <param name="responses">The set of responses to return.</param>
        public AsyncDuplexStreamingTestCallBuilder(IClientStreamWriter<TRequest> requests, IAsyncStreamReader<TResponse> responses)
        {
            _requests = requests ?? throw new ArgumentNullException(nameof(requests));
            _responses = responses ?? throw new ArgumentNullException(nameof(responses));
        }

        /// <inheritdoc />
        public override AsyncDuplexStreamingCall<TRequest, TResponse> Build() => TestCalls.AsyncDuplexStreamingCall(_requests, _responses, ResponseHeaders, StatusFunc, TrailersFunc, DisposeAction);
    }
}
