using System.Threading.Tasks;
using MarketingBox.Auth.Service.Messages;
using MarketingBox.Email.Service.Domain;
using Microsoft.Extensions.Logging;

namespace MarketingBox.Email.Service.Engines
{
    public class PasswordChangedEngine
    {
        private readonly ILogger<PasswordChangedEngine> _logger;
        private readonly ISendGridEmailSender _sendGridEmailSender;

        public PasswordChangedEngine(ILogger<PasswordChangedEngine> logger,
            ISendGridEmailSender sendGridEmailSender)
        {
            _sendGridEmailSender = sendGridEmailSender;
            _logger = logger;
        }

        public async Task HandleMessage(UserPasswordChangedMessage message)
        {
            var (success, error) = await _sendGridEmailSender.SendMailAsync(
                message.Email,
                Program.Settings.ChangePasswordEmailHeader,
                Program.Settings.ChangePasswordEmailSubject,
                Program.Settings.ChangePasswordEmailTemplateId,
                new
                {
                    nickName = message.UserName
                });
            if (!success)
            {
                _logger.LogError(error);
            }
        }
    }
}