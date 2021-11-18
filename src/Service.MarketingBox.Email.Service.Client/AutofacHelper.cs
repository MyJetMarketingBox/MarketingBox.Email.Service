using Autofac;
using Service.MarketingBox.Email.Service.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.MarketingBox.Email.Service.Client
{
    public static class AutofacHelper
    {
        public static void ServiceClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new ServiceClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetHelloService()).As<IHelloService>().SingleInstance();
        }
    }
}
