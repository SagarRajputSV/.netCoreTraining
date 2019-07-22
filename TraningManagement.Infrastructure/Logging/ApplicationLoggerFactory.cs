using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace TraningManagement.Infrastructure.Logging
{
    public class ApplicationLoggerFactory
    {
        private static ILoggerFactory _loggerFactory;

        private static ILoggerFactory LoggerFactory
        {
            get => _loggerFactory ?? (_loggerFactory = new LoggerFactory());
            set => _loggerFactory = value;
        }

        public static void AddLoggerFactory(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
        }

        public static ILogger CreateLogger<T>() =>
            LoggerFactory.CreateLogger<T>();

        public static ILogger CreateLogger(string name) => LoggerFactory.CreateLogger(name);
    }
}
