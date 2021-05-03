using System.Threading.Tasks;
using AutoFixture.Idioms;
using Grpc.Core;
using InsightArchitectures.Testing;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    [TestOf(typeof(TestCallBuilder))]
    public class TestCallBuilderTests
    {
        [Test, CustomAutoData]
        public void Methods_are_guarded_against_nulls(GuardClauseAssertion assertion) => assertion.Verify(typeof(TestCallBuilder));

        [Test, CustomAutoData]
        public void AsyncUnaryCall_returns_builder(HelloReply response)
        {
            var call = TestCallBuilder.AsyncUnaryCall(response);

            Assert.That(call, Is.InstanceOf<AsyncUnaryTestCallBuilder<HelloReply>>());
        }

        [Test, CustomAutoData]
        public void AsyncUnaryCall_returns_builder(Task<HelloReply> response)
        {
            var call = TestCallBuilder.AsyncUnaryCall(response);

            Assert.That(call, Is.InstanceOf<AsyncUnaryTestCallBuilder<HelloReply>>());
        }

        [Test, CustomAutoData]
        public void AsyncServerStreamingCall_returns_builder(IAsyncStreamReader<HelloReply> responses)
        {
            var call = TestCallBuilder.AsyncServerStreamingCall(responses);

            Assert.That(call, Is.InstanceOf<AsyncServerStreamingTestCallBuilder<HelloReply>>());
        }

        [Test, CustomAutoData]
        public void AsyncClientStreamingCall_returns_builder(IClientStreamWriter<HelloRequest> requests, HelloReply response)
        {
            var call = TestCallBuilder.AsyncClientStreamingCall(requests, response);

            Assert.That(call, Is.InstanceOf<AsyncClientStreamingTestCallBuilder<HelloRequest, HelloReply>>());
        }

        [Test, CustomAutoData]
        public void AsyncClientStreamingCall_returns_builder(IClientStreamWriter<HelloRequest> requests, Task<HelloReply> response)
        {
            var call = TestCallBuilder.AsyncClientStreamingCall(requests, response);

            Assert.That(call, Is.InstanceOf<AsyncClientStreamingTestCallBuilder<HelloRequest, HelloReply>>());
        }

        [Test, CustomAutoData]
        public void AsyncDuplexStreamingCall_returns_builder(IClientStreamWriter<HelloRequest> requests, IAsyncStreamReader<HelloReply> responses)
        {
            var call = TestCallBuilder.AsyncDuplexStreamingCall(requests, responses);

            Assert.That(call, Is.InstanceOf<AsyncDuplexStreamingTestCallBuilder<HelloRequest, HelloReply>>());
        }
    }
}
