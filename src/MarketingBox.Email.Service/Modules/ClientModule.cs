using Autofac;
using MarketingBox.Affiliate.Service.Messages;
using MarketingBox.Affiliate.Service.Messages.Affiliates;
using MarketingBox.Auth.Service.Messages;
using MarketingBox.PasswordApi.Domain.Models;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;

namespace MarketingBox.Email.Service.Modules
{
    public class ClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusClient = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.MarketingBoxServiceBusHostPort), Program.LogFactory);
            
            builder.RegisterMyServiceBusSubscriberSingle<PasswordRecoveryEmailMessage>(
                serviceBusClient, PasswordRecoveryEmailMessage.Topic, 
                "MarketingBox-Email-Service-password-recovery", TopicQueueType.PermanentWithSingleConnection);
            builder.RegisterMyServiceBusSubscriberBatch<AffiliateUpdated>(
                serviceBusClient, Topics.AffiliateUpdatedTopic, 
                "MarketingBox-Email-Service", TopicQueueType.PermanentWithSingleConnection);
            builder.RegisterMyServiceBusSubscriberBatch<UserPasswordChangedMessage>(
                serviceBusClient, UserPasswordChangedMessage.Topic, 
                "MarketingBox-Email-Service-password-changed", TopicQueueType.PermanentWithSingleConnection);
        }
    }
}