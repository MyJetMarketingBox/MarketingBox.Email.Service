using System.Threading.Tasks;
using MarketingBox.Affiliate.Service.Messages.Affiliates;
using MarketingBox.Email.Service.Domain;
using Microsoft.Extensions.Logging;

namespace MarketingBox.Email.Service.Engines
{
    public class PasswordRecoverEngine
    {
        private readonly ILogger<PasswordRecoverEngine> _logger;
        private readonly ISendGridEmailSender _sendGridEmailSender;

        public PasswordRecoverEngine(ILogger<PasswordRecoverEngine> logger,
            ISendGridEmailSender sendGridEmailSender)
        {
            _sendGridEmailSender = sendGridEmailSender;
            _logger = logger;
        }

        public async Task HandleAffiliate(AffiliateUpdated elem)
        {
            await _sendGridEmailSender.SendMailAsync(
                elem.Affiliate.GeneralInfo.Email,
                Program.Settings.ConfirmationEmailHeader,
                Program.Settings.ConfirmationEmailSubject,
                Program.Settings.ConfirmationEmailTemplateId,
                new
                {
                    nickName = elem.Affiliate.GeneralInfo.Username,
                    confirmEmailUrl = string.Empty
                });
        }
    }
}