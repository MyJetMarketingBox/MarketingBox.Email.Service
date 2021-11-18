using System.ServiceModel;
using System.Threading.Tasks;
using Service.MarketingBox.Email.Service.Grpc.Models;

namespace Service.MarketingBox.Email.Service.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}