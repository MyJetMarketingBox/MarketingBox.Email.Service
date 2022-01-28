using Autofac;
using MarketingBox.Email.Service.Grpc;

// ReSharper disable UnusedMember.Global

namespace MarketingBox.Email.Service.Client
{
    public static class AutofacHelper
    {
        public static void RegisterEmailServiceClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new ServiceClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetEmailSenderService()).As<IEmailSenderService>().SingleInstance();
        }
    }
}
