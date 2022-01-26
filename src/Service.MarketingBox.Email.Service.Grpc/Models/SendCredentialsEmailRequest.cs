using System.Runtime.Serialization;

namespace Service.MarketingBox.Email.Service.Grpc.Models
{
    [DataContract]
    public class SendCredentialsEmailRequest
    {
        [DataMember(Order = 1)]
        public string EmailTo { get; set; }
        
        [DataMember(Order = 2)]
        public string Login { get; set; }
        
        [DataMember(Order = 3)]
        public string Password { get; set; }
    }
}