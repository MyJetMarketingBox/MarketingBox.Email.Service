using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.MarketingBox.Email.Service.Grpc;

namespace Service.MarketingBox.Email.Service.Client
{
    [UsedImplicitly]
    public class ServiceClientFactory : MyGrpcClientFactory
    {
        public ServiceClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IHelloService GetHelloService() => CreateGrpcService<IHelloService>();
    }
}
