﻿using Dapper;
using Storage.Repositories.Providers;

namespace Storage
{
    public class DatabaseCleaner : IHostedService
    {
        private readonly ILogger<DatabaseCleaner> _logger;
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private Timer? _timer;

        public DatabaseCleaner(ILogger<DatabaseCleaner> logger, IDatabaseConnectionFactory connectionFactory)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoCleaning, null, TimeSpan.Zero, TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        private async void DoCleaning(object state)
        {
            var now = DateTime.Now;
            _logger.LogInformation("DoCleaning {0}", now);
            if(now.Hour == 1)
            {
                _logger.LogInformation("Removing test data from database.");
                var sqlQuery = "DELETE FROM meetings WHERE name LIKE '%TESTIKOKOUS%'";

                try
                {
                    using var connection = await _connectionFactory.CreateOpenConnection();
                    await connection.ExecuteAsync(sqlQuery);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "DoCleaning failed");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
