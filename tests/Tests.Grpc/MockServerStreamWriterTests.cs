using System.Threading.Tasks;
using AutoFixture.Idioms;
using InsightArchitectures.Testing;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    [TestOf(typeof(MockServerStreamWriter<>))]
    public class MockServerStreamWriterTests
    {
        [Test, CustomAutoData]
        public void Constructor_does_not_accept_nulls(GuardClauseAssertion assertion) => assertion.Verify(typeof(MockServerStreamWriter<>).GetConstructors());

        [Test, CustomAutoData]
        public async Task Instance_contains_items_passed_via_WriteAsync(MockServerStreamWriter<HelloReply> sut, HelloReply item)
        {
            await sut.WriteAsync(item);

            Assert.That(sut, Contains.Item(item));
        }
    }
}
