using Autofac;
using MarketingBox.Affiliate.Service.Messages;
using MarketingBox.Affiliate.Service.Messages.Affiliates;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;

namespace Service.MarketingBox.Email.Service.Modules
{
    public class ClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusClient = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.MarketingBoxServiceBusHostPort), Program.LogFactory);
            builder.RegisterMyServiceBusSubscriberBatch<AffiliateUpdated>(serviceBusClient, Topics.AffiliateUpdatedTopic, 
                "MarketingBox-Email-Service", TopicQueueType.PermanentWithSingleConnection);
        }
    }
}