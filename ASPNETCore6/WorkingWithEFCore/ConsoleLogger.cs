using Microsoft.Extensions.Logging;
using System;
using static System.Console;

namespace Packt.Shared
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new ConsoleLogger();
        }

        // Используется при необходимости
        public void Dispose() { }
    }
    public class ConsoleLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // фильтрация по уровню логированмя чтобы не писать лишних логов.
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Information:
                case LogLevel.None:
                    return false;
                case LogLevel.Debug:
                case LogLevel.Warning:
                case LogLevel.Error:
                case LogLevel.Critical:
                default:
                    return true;
            }
        }

        // логика логирования
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, 
            Exception? exception, Func<TState, Exception?, string> formatter)
        {
            // идентификаторы события разные в зависимости от провайдеров дынных
            // здесь код события преобразования LINQ в SQL
            if (eventId.Id == 20100)
            {
                // регистрация идентификатора уровня и события
                Write($"Level: {logLevel}, Event ID: {eventId.Id}");

                // вывод только состояния или исключения при наличии
                if (state != null)
                {
                    Write($", State: {state}");
                }
                if (exception != null)
                {
                    Write($", Exception: {exception.Message}");
                }
                WriteLine();
            }
        }
    }
}
