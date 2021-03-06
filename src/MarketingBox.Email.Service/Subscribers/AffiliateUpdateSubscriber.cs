using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using MarketingBox.Affiliate.Service.Messages.Affiliates;
using MarketingBox.Email.Service.Engines;
using Microsoft.Extensions.Logging;

namespace MarketingBox.Email.Service.Subscribers
{
    public class AffiliateUpdateSubscriber : IStartable
    {
        private readonly ILogger<AffiliateUpdateSubscriber> _logger;
        private readonly AffiliateConfirmationEngine _affiliateConfirmationEngine;

        public AffiliateUpdateSubscriber(ILogger<AffiliateUpdateSubscriber> logger,
            ISubscriber<IReadOnlyList<AffiliateUpdated>> subscriber, 
            AffiliateConfirmationEngine affiliateConfirmationEngine)
        {
            _logger = logger;
            _affiliateConfirmationEngine = affiliateConfirmationEngine;

            subscriber.Subscribe(HandleEvents);
        }

        private async ValueTask HandleEvents(IReadOnlyList<AffiliateUpdated> events)
        {
            try
            {
                _logger.LogInformation($"{nameof(AffiliateUpdateSubscriber)} receive {events.Count} events.");

                foreach (var elem in events)
                {
                    if (elem.EventType != AffiliateUpdatedEventType.CreatedSub)
                    {
                        _logger.LogInformation(
                            $"Skip affiliate with id = {elem.Affiliate.AffiliateId}, because EventType is {elem.EventType}.");
                        continue;
                    }
                    await _affiliateConfirmationEngine.HandleAffiliate(elem);
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