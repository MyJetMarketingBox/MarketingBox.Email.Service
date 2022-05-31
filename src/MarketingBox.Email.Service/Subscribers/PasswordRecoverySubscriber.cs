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
            ISubscriber<PasswordRecoveryEmailMessage> subscriber,
            PasswordRecoveryEngine passwordRecoveryEngine)
        {
            _logger = logger;
            _passwordRecoveryEngine = passwordRecoveryEngine;

            subscriber.Subscribe(HandleEvent);
        }

        private async ValueTask HandleEvent(PasswordRecoveryEmailMessage message)
        {
            try
            {
                _logger.LogInformation($"{nameof(PasswordRecoverySubscriber)} receive message:{message}.");

                await _passwordRecoveryEngine.HandleAffiliate(message);
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