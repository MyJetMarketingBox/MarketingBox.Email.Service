using Autofac;
using MyJetWallet.Sdk.NoSql;
using Service.MarketingBox.Email.Service.Domain;
using Service.MarketingBox.Email.Service.Domain.Models;
using Service.MarketingBox.Email.Service.Engines;
using Service.MarketingBox.Email.Service.Services;
using Service.MarketingBox.Email.Service.Subscribers;

namespace Service.MarketingBox.Email.Service.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<SendGridEmailSender>()
                .As<ISendGridEmailSender>()
                .SingleInstance();

            builder
                .RegisterType<AffiliateConfirmationEngine>()
                .AsSelf()
                .SingleInstance();
            
            builder
                .RegisterType<AffiliateUpdateSubscriber>()
                .As<IStartable>()
                .AutoActivate()
                .SingleInstance();
            
            builder.RegisterMyNoSqlWriter<AffiliateConfirmationNoSql>(
                Program.ReloadedSettings(e => e.MyNoSqlWriterUrl),
                AffiliateConfirmationNoSql.TableName);
        }
    }
}