using System;
using System.Threading.Tasks;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using Grpc.Core;
using Grpc.Core.Utils;
using InsightArchitectures.Testing;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    [TestOf(typeof(AsyncDuplexStreamingTestCallBuilder<,>))]
    public class AsyncDuplexStreamingTestCallBuilderTests
    {
        [Test, CustomAutoData]
        public void Constructor_does_not_accept_nulls(GuardClauseAssertion assertion) => assertion.Verify(typeof(AsyncDuplexStreamingTestCallBuilder<,>).GetConstructors());

        [Test, CustomAutoData]
        public async Task Build_returns_call_with_response([Frozen] HelloReply result, AsyncDuplexStreamingTestCallBuilder<HelloRequest, HelloReply> sut)
        {
            var call = sut.Build();

            var items = await call.ResponseStream.ToListAsync();

            Assert.That(items[0], Is.SameAs(result));
        }

        [Test, CustomAutoData]
        public void Build_returns_call_with_response(AsyncDuplexStreamingTestCallBuilder<HelloRequest, HelloReply> sut)
        {
            var call = sut.Build();

            Assert.That(call, Is.InstanceOf<AsyncDuplexStreamingCall<HelloRequest, HelloReply>>());
        }

        [Test, CustomAutoData]
        public async Task WithResponseHeaders_enriches_produced_call(AsyncDuplexStreamingTestCallBuilder<HelloRequest, HelloReply> sut, Metadata responseHeaders)
        {
            var call = sut.WithResponseHeaders(responseHeaders).Build();

            Assert.That(await call.ResponseHeadersAsync, Is.SameAs(responseHeaders));
        }

        [Test, CustomAutoData]
        public void WithResponseHeaders_enriches_produced_call(AsyncDuplexStreamingTestCallBuilder<HelloRequest, HelloReply> sut, Task<Metadata> responseHeaders)
        {
            var call = sut.WithResponseHeaders(responseHeaders).Build();

            Assert.That(call.ResponseHeadersAsync, Is.SameAs(responseHeaders));
        }

        [Test, CustomAutoData]
        public void WithStatus_enriches_produced_call(AsyncDuplexStreamingTestCallBuilder<HelloRequest, HelloReply> sut, Status status)
        {
            var call = sut.WithStatus(status).Build();

            Assert.That(call.GetStatus(), Is.EqualTo(status));
        }

        [Test, CustomAutoData]
        public void WithStatus_enriches_produced_call(AsyncDuplexStreamingTestCallBuilder<HelloRequest, HelloReply> sut, Func<Status> statusFunc, Status status)
        {
            Mock.Get(statusFunc).Setup(p => p()).Returns(status);

            var call = sut.WithStatus(statusFunc).Build();

            Assert.That(call.GetStatus(), Is.EqualTo(status));
        }

        [Test, CustomAutoData]
        public void WithTrailers_enriches_produced_call(AsyncDuplexStreamingTestCallBuilder<HelloRequest, HelloReply> sut, Metadata trailers)
        {
            var call = sut.WithTrailers(trailers).Build();

            Assert.That(call.GetTrailers(), Is.EqualTo(trailers));
        }

        [Test, CustomAutoData]
        public void WithTrailers_enriches_produced_call(AsyncDuplexStreamingTestCallBuilder<HelloRequest, HelloReply> sut, Func<Metadata> trailersFunc, Metadata trailers)
        {
            Mock.Get(trailersFunc).Setup(p => p()).Returns(trailers);

            var call = sut.WithTrailers(trailersFunc).Build();

            Assert.That(call.GetTrailers(), Is.EqualTo(trailers));
        }

        [Test, CustomAutoData]
        public void Call_customizations_can_be_chained(AsyncDuplexStreamingTestCallBuilder<HelloRequest, HelloReply> sut, Metadata responseHeaders, Status status, Metadata trailers, Action disposableAction)
        {
            var call = sut.WithResponseHeaders(responseHeaders).WithStatus(status).WithTrailers(trailers).WithDisposeAction(disposableAction).Build();

            Assert.Multiple(async () =>
            {
                Assert.That(await call.ResponseHeadersAsync, Is.SameAs(responseHeaders));
                Assert.That(call.GetStatus(), Is.EqualTo(status));
                Assert.That(call.GetTrailers(), Is.EqualTo(trailers));
            });
        }
    }
}
