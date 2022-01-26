using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MarketingBox.Email.Service.Domain;
using Service.MarketingBox.Email.Service.Grpc;
using Service.MarketingBox.Email.Service.Grpc.Models;

namespace Service.MarketingBox.Email.Service.Services
{
    public class EmailSenderService: IEmailSenderService
    {
        private readonly ILogger<EmailSenderService> _logger;
        private readonly ISendGridEmailSender _sendGridEmailSender;

        public EmailSenderService(ILogger<EmailSenderService> logger, 
            ISendGridEmailSender sendGridEmailSender)
        {
            _logger = logger;
            _sendGridEmailSender = sendGridEmailSender;
        }

        public async Task<SendCredentialsEmailResponse> SendCredentialsEmailAsync(SendCredentialsEmailRequest request)
        {
            _logger.LogInformation("SendCredentialsEmailAsync receive request : {requestJson}", 
                JsonConvert.SerializeObject(request));

            var response = await _sendGridEmailSender.SendMailAsync(
                request.EmailTo,
                Program.Settings.CredentialsEmailHeader,
                Program.Settings.CredentialsEmailSubject,
                Program.Settings.CredentialsEmailTemplateId,
                new {
                    login = request.Login,
                    password = request.Password,
                    loginLink = Program.Settings.CredentialsEmailLoginLink
                });
            _logger.LogInformation($"Sent credentials email to {request.EmailTo}. Receive response = {response}");
            
            return new SendCredentialsEmailResponse()
            {
                Success = response
            };
        }
    }
}
