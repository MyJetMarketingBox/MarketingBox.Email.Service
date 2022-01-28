using System.Threading.Tasks;

namespace MarketingBox.Email.Service.Domain
{
    public interface ISendGridEmailSender
    {
        Task<(bool, string)> SendMailAsync(string to, string header,
            string subject, string templateId, object data, string from = "");
    }
}