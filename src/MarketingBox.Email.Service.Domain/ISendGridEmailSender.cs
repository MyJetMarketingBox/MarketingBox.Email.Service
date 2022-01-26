using System.Threading.Tasks;

namespace MarketingBox.Email.Service.Domain
{
    public interface ISendGridEmailSender
    {
        Task<bool> SendMailAsync(string to, string header,
            string subject, string templateId, object data, string from = "");
    }
}