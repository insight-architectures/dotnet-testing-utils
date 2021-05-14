using System;
using InsightArchitectures.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class TestContextLoggerAdapterTests
    {
        [Test, CustomAutoData]
        public void BeginScope_returns_self(TestContextLoggerAdapter<TestService> sut, object state)
        {
            Assert.That(sut.BeginScope(state), Is.SameAs(sut));
        }

        [Test]
        [CustomInlineAutoData(LogLevel.Critical)]
        [CustomInlineAutoData(LogLevel.Debug)]
        [CustomInlineAutoData(LogLevel.Error)]
        [CustomInlineAutoData(LogLevel.Information)]
        [CustomInlineAutoData(LogLevel.Trace)]
        [CustomInlineAutoData(LogLevel.Warning)]
        public void IsEnabled_returns_true(LogLevel level, TestContextLoggerAdapter<TestService> sut)
        {
            Assert.That(sut.IsEnabled(level), Is.True);
        }

        [Test]
        [CustomInlineAutoData(LogLevel.Critical)]
        [CustomInlineAutoData(LogLevel.Debug)]
        [CustomInlineAutoData(LogLevel.Error)]
        [CustomInlineAutoData(LogLevel.Information)]
        [CustomInlineAutoData(LogLevel.Trace)]
        [CustomInlineAutoData(LogLevel.Warning)]
        public void Log_uses_formatter(LogLevel level, TestContextLoggerAdapter<TestService> sut, EventId eventId, object state, Exception exception, string message, Func<object, Exception, string> formatter)
        {
            Mock.Get(formatter).Setup(p => p(state, exception)).Returns(message).Verifiable();

            sut.Log(level, eventId, state, exception, formatter);

            Mock.VerifyAll();
        }
    }

    public class TestService
    {
        private readonly ILogger<TestService> _logger;

        public TestService(ILogger<TestService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string Hello(string name)
        {
            _logger.LogInformation("Received {NAME}", name);
            return $"Hello {name}";
        }
    }
}
