using System.Runtime.Serialization;
using Service.MarketingBox.Email.Service.Domain.Models;

namespace Service.MarketingBox.Email.Service.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}