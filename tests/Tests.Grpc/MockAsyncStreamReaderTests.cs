using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Idioms;
using Grpc.Core.Utils;
using InsightArchitectures.Testing;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    [TestOf(typeof(MockAsyncStreamReader<>))]
    public class MockAsyncStreamReaderTests
    {
        [Test, CustomAutoData]
        public void Constructor_does_not_accept_nulls(GuardClauseAssertion assertion) => assertion.Verify(typeof(MockAsyncStreamReader<>).GetConstructors());

        [Test, CustomAutoData]
        public async Task Instance_contains_data_passed_via_constructor(IList<HelloReply> items)
        {
            var sut = new MockAsyncStreamReader<HelloReply>(items);

            Assert.That(await sut.ToListAsync(), Is.EquivalentTo(items));
        }
    }
}
