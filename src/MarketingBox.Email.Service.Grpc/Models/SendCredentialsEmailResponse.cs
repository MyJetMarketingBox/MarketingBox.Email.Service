using System.Runtime.Serialization;
using Service.MarketingBox.Email.Service.Domain.Models;

namespace Service.MarketingBox.Email.Service.Grpc.Models
{
    [DataContract]
    public class SendCredentialsEmailResponse
    {
        [DataMember(Order = 1)]
        public bool Success { get; set; }
    }
}