using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Email.Service.Grpc.Models;

namespace MarketingBox.Email.Service.Grpc
{
    [ServiceContract]
    public interface IEmailSenderService
    {
        [OperationContract]
        Task<SendCredentialsEmailResponse> SendCredentialsEmailAsync(SendCredentialsEmailRequest request);
    }
}