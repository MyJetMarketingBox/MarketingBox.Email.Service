using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Service.MarketingBox.Email.Service.Domain;
using Service.MarketingBox.Email.Service.Services;

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
        }
    }
}