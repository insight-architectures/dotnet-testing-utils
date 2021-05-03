using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Testing;

namespace InsightArchitectures.Testing
{
    /// <summary>
    /// A builder of <see cref="AsyncClientStreamingCall{TRequest,TResponse}"/>.
    /// </summary>
    /// <typeparam name="TRequest">Request message type for this call.</typeparam>
    /// <typeparam name="TResponse">Response message type for this call.</typeparam>
    public class AsyncClientStreamingTestCallBuilder<TRequest, TResponse> : TestCallBuilder<AsyncClientStreamingCall<TRequest, TResponse>>
    {
        private readonly IClientStreamWriter<TRequest> _requests;
        private readonly Task<TResponse> _response;

        /// <summary>
        /// Creates a <see cref="AsyncClientStreamingTestCallBuilder{TRequest, TResponse}" />.
        /// </summary>
        /// <param name="requests">Request values.</param>
        /// <param name="response">The object to be returned as response of the call.</param>
        public AsyncClientStreamingTestCallBuilder(IClientStreamWriter<TRequest> requests, Task<TResponse> response)
        {
            _requests = requests ?? throw new ArgumentNullException(nameof(requests));
            _response = response ?? throw new ArgumentNullException(nameof(response));
        }

        /// <inheritdoc />
        public override AsyncClientStreamingCall<TRequest, TResponse> Build() => TestCalls.AsyncClientStreamingCall(_requests, _response, ResponseHeaders, StatusFunc, TrailersFunc, DisposeAction);
    }
}
