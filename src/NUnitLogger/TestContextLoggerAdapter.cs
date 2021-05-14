using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace InsightArchitectures.Testing
{
    /// <summary>
    /// An implementation of <see cref="ILogger{T}" /> that forwards log entries to <see cref="TestContext" />.
    /// </summary>
    /// <typeparam name="T">The type who's name is used for the logger category name.</typeparam>
    [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "Nothing to dispose")]
    public class TestContextLoggerAdapter<T> : ILogger<T>, IDisposable
    {
        /// <inheritdoc />
        public IDisposable BeginScope<TState>(TState state) => this;

        /// <inheritdoc />
        [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "Nothing to dispose")]
        [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "Nothing to dispose")]
        public void Dispose()
        {
        }

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel) => true;

        /// <inheritdoc />
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var builder = new StringBuilder($"{typeof(T).Name}: [{logLevel}]");

            if (eventId.Id != 0)
            {
                builder.Append($" ({eventId})");
            }

            if (formatter != null)
            {
                builder.Append($" {formatter(state, exception)}");
            }

            TestContext.WriteLine(builder.ToString());
        }
    }
}
