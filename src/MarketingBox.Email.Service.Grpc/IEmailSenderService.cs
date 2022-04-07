using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Email.Service.Grpc.Models;
using MarketingBox.Sdk.Common.Models.Grpc;

namespace MarketingBox.Email.Service.Grpc
{
    [ServiceContract]
    public interface IEmailSenderService
    {
        [OperationContract]
        Task<Response<bool>> SendCredentialsEmailAsync(SendCredentialsEmailRequest request);
    }
}