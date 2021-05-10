using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;

namespace InsightArchitectures.Testing
{
    /// <summary>
    /// A static accessor for creating GRPC test calls.
    /// </summary>
    public static class TestCallBuilder
    {
        /// <summary>
        /// Creates a builder for testing single request - single response calls.
        /// </summary>
        /// <typeparam name="TResponse">Response message type for this call.</typeparam>
        /// <param name="response">The object to be returned as response of the call.</param>
        /// <returns>A builder that can be customized and used to create a test call.</returns>
        public static TestCallBuilder<AsyncUnaryCall<TResponse>> AsyncUnaryCall<TResponse>(TResponse response) => AsyncUnaryCall(Task.FromResult(response));

        /// <summary>
        /// Creates a builder for testing single request - single response calls.
        /// </summary>
        /// <typeparam name="TResponse">Response message type for this call.</typeparam>
        /// <param name="response">A task producing the object to be returned as response of the call.</param>
        /// <returns>A builder that can be customized and used to create a test call.</returns>
        public static TestCallBuilder<AsyncUnaryCall<TResponse>> AsyncUnaryCall<TResponse>(Task<TResponse> response) => new AsyncUnaryTestCallBuilder<TResponse>(response);

        /// <summary>
        /// Creates a builder for testing server streaming calls.
        /// </summary>
        /// <typeparam name="TResponse">Response message type for this call.</typeparam>
        /// <param name="responses">The set of responses to return.</param>
        /// <returns>A builder that can be customized and used to create a test call.</returns>
        public static TestCallBuilder<AsyncServerStreamingCall<TResponse>> AsyncServerStreamingCall<TResponse>(IAsyncStreamReader<TResponse> responses) => new AsyncServerStreamingTestCallBuilder<TResponse>(responses);

        /// <summary>
        /// Creates a builder for testing server streaming calls.
        /// </summary>
        /// <typeparam name="TResponse">Response message type for this call.</typeparam>
        /// <param name="items">The set of responses to return.</param>
        /// <returns>A builder that can be customized and used to create a test call.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposed by the container.")]
        public static TestCallBuilder<AsyncServerStreamingCall<TResponse>> AsyncServerStreamingCall<TResponse>(IEnumerable<TResponse> items) => new AsyncServerStreamingTestCallBuilder<TResponse>(new MockAsyncStreamReader<TResponse>(items));

        /// <summary>
        /// Creates a builder for testing client streaming calls.
        /// </summary>
        /// <typeparam name="TRequest">Request message type for this call.</typeparam>
        /// <typeparam name="TResponse">Response message type for this call.</typeparam>
        /// <param name="requests">Request values.</param>
        /// <param name="response">A task producing the object to be returned as response of the call.</param>
        /// <returns>A builder that can be customized and used to create a test call.</returns>
        public static TestCallBuilder<AsyncClientStreamingCall<TRequest, TResponse>> AsyncClientStreamingCall<TRequest, TResponse>(IClientStreamWriter<TRequest> requests, Task<TResponse> response) => new AsyncClientStreamingTestCallBuilder<TRequest, TResponse>(requests, response);

        /// <summary>
        /// Creates a builder for testing client streaming calls.
        /// </summary>
        /// <typeparam name="TRequest">Request message type for this call.</typeparam>
        /// <typeparam name="TResponse">Response message type for this call.</typeparam>
        /// <param name="requests">Request values.</param>
        /// <param name="response">The object to be returned as response of the call.</param>
        /// <returns>A builder that can be customized and used to create a test call.</returns>
        public static TestCallBuilder<AsyncClientStreamingCall<TRequest, TResponse>> AsyncClientStreamingCall<TRequest, TResponse>(IClientStreamWriter<TRequest> requests, TResponse response) => AsyncClientStreamingCall(requests, Task.FromResult(response));

        /// <summary>
        /// Creates a builder for testing bidirectional streaming calls.
        /// </summary>
        /// <typeparam name="TRequest">Request message type for this call.</typeparam>
        /// <typeparam name="TResponse">Response message type for this call.</typeparam>
        /// <param name="requests">Request values.</param>
        /// <param name="responses">The set of responses to return.</param>
        /// <returns>A builder that can be customized and used to create a test call.</returns>
        public static TestCallBuilder<AsyncDuplexStreamingCall<TRequest, TResponse>> AsyncDuplexStreamingCall<TRequest, TResponse>(IClientStreamWriter<TRequest> requests, IAsyncStreamReader<TResponse> responses) => new AsyncDuplexStreamingTestCallBuilder<TRequest, TResponse>(requests, responses);
    }

    /// <summary>
    /// Base class for the concrete test call builders.
    /// </summary>
    /// <typeparam name="TCall">The type of call to build.</typeparam>
    public abstract class TestCallBuilder<TCall>
    {
        /// <summary>
        /// Sets the default for different options.
        /// </summary>
        protected TestCallBuilder()
        {
            ResponseHeaders = Task.FromResult(new Metadata());
            StatusFunc = () => Status.DefaultSuccess;
            TrailersFunc = () => new Metadata();
            DisposeAction = () => { };
        }

        /// <summary>
        /// Gets the task producing the metadata included in the headers of the call.
        /// </summary>
        public Task<Metadata> ResponseHeaders { get; protected set; }

        /// <summary>
        /// Gets the function producing the status of the call.
        /// </summary>
        public Func<Status> StatusFunc { get; protected set; }

        /// <summary>
        /// Gets the function producing the metadata included in the trailers of the call.
        /// </summary>
        public Func<Metadata> TrailersFunc { get; protected set; }

        /// <summary>
        /// Gets the function used to dispose the call.
        /// </summary>
        public Action DisposeAction { get; protected set; }

        /// <summary>
        /// Specifies the metadata to be included in the header of the call.
        /// </summary>
        /// <returns>The builder.</returns>
        public TestCallBuilder<TCall> WithResponseHeaders(Metadata metadata) => WithResponseHeaders(Task.FromResult(metadata));

        /// <summary>
        /// Specifies a task producing the metadata to be included in the header of the call.
        /// </summary>
        /// <returns>The builder.</returns>
        public TestCallBuilder<TCall> WithResponseHeaders(Task<Metadata> metadata)
        {
            ResponseHeaders = metadata ?? throw new ArgumentNullException(nameof(metadata));

            return this;
        }

        /// <summary>
        /// Specifies the status of the call.
        /// </summary>
        /// <returns>The builder.</returns>
        public TestCallBuilder<TCall> WithStatus(Status status) => WithStatus(() => status);

        /// <summary>
        /// Specifies a function producing the status of the call.
        /// </summary>
        /// <returns>The builder.</returns>
        public TestCallBuilder<TCall> WithStatus(Func<Status> statusFunc)
        {
            StatusFunc = statusFunc ?? throw new ArgumentNullException(nameof(statusFunc));

            return this;
        }

        /// <summary>
        /// Specifies the metadata to be included in the trailer of the call.
        /// </summary>
        /// <returns>The builder.</returns>
        public TestCallBuilder<TCall> WithTrailers(Metadata metadata) => WithTrailers(() => metadata);

        /// <summary>
        /// Specifies a function producing the metadata to be included in the trailer of the call.
        /// </summary>
        /// <returns>The builder.</returns>
        public TestCallBuilder<TCall> WithTrailers(Func<Metadata> trailersFunc)
        {
            TrailersFunc = trailersFunc ?? throw new ArgumentNullException(nameof(trailersFunc));

            return this;
        }

        /// <summary>
        /// Specifies an action to be used to dispose the call.
        /// </summary>
        /// <returns>The builder.</returns>
        public TestCallBuilder<TCall> WithDisposeAction(Action disposeAction)
        {
            DisposeAction = disposeAction ?? throw new ArgumentNullException(nameof(disposeAction));

            return this;
        }

        /// <summary>
        /// Produces the <typeparamref name="TCall"/>.
        /// </summary>
        public abstract TCall Build();
    }
}
