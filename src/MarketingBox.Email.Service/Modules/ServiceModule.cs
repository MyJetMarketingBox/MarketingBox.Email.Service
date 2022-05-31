using Autofac;
using MarketingBox.Email.Service.Domain;
using MarketingBox.Email.Service.Domain.Models;
using MarketingBox.Email.Service.Engines;
using MarketingBox.Email.Service.Services;
using MarketingBox.Email.Service.Subscribers;
using MyJetWallet.Sdk.NoSql;

namespace MarketingBox.Email.Service.Modules
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
                .RegisterType<PasswordRecoveryEngine>()
                .AsSelf()
                .SingleInstance();
            // builder
            //     .RegisterType<PasswordChangedEngine>()
            //     .AsSelf()
            //     .SingleInstance();
            
            // builder
            //     .RegisterType<PasswordChangedSubscriber>()
            //     .As<IStartable>()
            //     .AutoActivate()
            //     .SingleInstance();
            builder
                .RegisterType<AffiliateUpdateSubscriber>()
                .As<IStartable>()
                .AutoActivate()
                .SingleInstance();
            builder
                .RegisterType<PasswordRecoverySubscriber>()
                .As<IStartable>()
                .AutoActivate()
                .SingleInstance();
            
            builder.RegisterMyNoSqlWriter<AffiliateConfirmationNoSql>(
                Program.ReloadedSettings(e => e.MyNoSqlWriterUrl),
                AffiliateConfirmationNoSql.TableName);
        }
    }
}