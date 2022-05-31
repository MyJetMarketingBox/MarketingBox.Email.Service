using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using MarketingBox.Auth.Service.Messages;
using MarketingBox.Email.Service.Engines;
using Microsoft.Extensions.Logging;

namespace MarketingBox.Email.Service.Subscribers
{
    public class PasswordChangedSubscriber : IStartable
    {
        private readonly ILogger<PasswordChangedSubscriber> _logger;
        private readonly PasswordChangedEngine _passwordChangedEngine;

        public PasswordChangedSubscriber(ILogger<PasswordChangedSubscriber> logger,
            ISubscriber<IReadOnlyList<UserPasswordChangedMessage>> subscriber, 
            PasswordChangedEngine passwordChangedEngine)
        {
            _logger = logger;
            _passwordChangedEngine = passwordChangedEngine;

            subscriber.Subscribe(HandleEvents);
        }

        private async ValueTask HandleEvents(IReadOnlyList<UserPasswordChangedMessage> events)
        {
            try
            {
                _logger.LogInformation($"{nameof(PasswordChangedSubscriber)} recieved {events.Count} events.");

                foreach (var elem in events)
                {
                    await _passwordChangedEngine.HandleMessage(elem);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
        public void Start()
        {
        }
    }
}