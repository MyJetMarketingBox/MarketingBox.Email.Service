using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using MarketingBox.Email.Service.Engines;
using MarketingBox.PasswordApi.Domain.Models;
using Microsoft.Extensions.Logging;

namespace MarketingBox.Email.Service.Subscribers
{
    public class PasswordRecoverySubscriber : IStartable
    {
        private readonly ILogger<PasswordRecoverySubscriber> _logger;
        private readonly PasswordRecoveryEngine _passwordRecoveryEngine;

        public PasswordRecoverySubscriber(ILogger<PasswordRecoverySubscriber> logger,
            ISubscriber<IReadOnlyList<PasswordRecoveryEmailMessage>> subscriber,
            PasswordRecoveryEngine passwordRecoveryEngine)
        {
            _logger = logger;
            _passwordRecoveryEngine = passwordRecoveryEngine;

            subscriber.Subscribe(HandleEvents);
        }

        private async ValueTask HandleEvents(IReadOnlyList<PasswordRecoveryEmailMessage> events)
        {
            try
            {
                _logger.LogInformation($"{nameof(PasswordRecoverySubscriber)} receive {events.Count} events.");

                foreach (var elem in events)
                {
                    await _passwordRecoveryEngine.HandleAffiliate(elem);
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