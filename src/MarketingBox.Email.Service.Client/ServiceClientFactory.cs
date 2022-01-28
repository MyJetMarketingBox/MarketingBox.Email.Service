using JetBrains.Annotations;
using MarketingBox.Email.Service.Grpc;
using MyJetWallet.Sdk.Grpc;

namespace MarketingBox.Email.Service.Client
{
    [UsedImplicitly]
    public class ServiceClientFactory : MyGrpcClientFactory
    {
        public ServiceClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IEmailSenderService GetEmailSenderService() => CreateGrpcService<IEmailSenderService>();
    }
}
