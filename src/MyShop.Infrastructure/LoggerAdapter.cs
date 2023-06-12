using Microsoft.Extensions.Logging;
using MyShop.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Infrastructure
{
    public sealed class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;



        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger= loggerFactory.CreateLogger<T>();
        }

        public void LogError(Exception exception, string? messege, params object[] args)
        {
            _logger.LogError(exception, messege, args);
        }

        public void LogInformation(string messege, params object[] args)
        {
            _logger.LogInformation(messege, args);
        }

        public void LogWarning(string messege, params object[] args)
        {
            _logger.LogWarning(messege, args);
        }
    }
}
