using System.Threading.Tasks;
using AutoFixture.Idioms;
using InsightArchitectures.Testing;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    [TestOf(typeof(MockClientStreamWriter<>))]
    public class MockClientStreamWriterTests
    {
        [Test, CustomAutoData]
        public void Constructor_does_not_accept_nulls(GuardClauseAssertion assertion) => assertion.Verify(typeof(MockClientStreamWriter<>).GetConstructors());

        [Test, CustomAutoData]
        public async Task Instance_contains_items_passed_via_WriteAsync(MockClientStreamWriter<HelloReply> sut, HelloReply item)
        {
            await sut.WriteAsync(item);

            Assert.That(sut, Contains.Item(item));
        }

        [Test, CustomAutoData]
        public async Task WriteAsync_throws_if_called_after_CompleteAsync(MockClientStreamWriter<HelloReply> sut, HelloReply item)
        {
            await sut.CompleteAsync();

            Assert.That(() => sut.WriteAsync(item), Throws.Exception);
        }

        [Test, CustomAutoData]
        public async Task CompleteAsync_throws_if_called_twice(MockClientStreamWriter<HelloReply> sut)
        {
            await sut.CompleteAsync();

            Assert.That(() => sut.CompleteAsync(), Throws.Exception);
        }
    }
}
