using System.Runtime.Serialization;

namespace MarketingBox.Email.Service.Grpc.Models
{
    [DataContract]
    public class SendCredentialsEmailResponse
    {
        [DataMember(Order = 1)]
        public bool Success { get; set; }
        
        [DataMember(Order = 2)]
        public string ErrorMessage { get; set; }
    }
}