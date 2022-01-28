using System.Threading.Tasks;
using MarketingBox.Email.Service.Domain;
using MarketingBox.Email.Service.Grpc;
using MarketingBox.Email.Service.Grpc.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MarketingBox.Email.Service.Services
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
                    nickName = request.Username,
                    login = request.Login,
                    password = request.Password,
                    loginUrl = Program.Settings.CredentialsEmailLoginLink
                });
            _logger.LogInformation($"Sent credentials email to {request.EmailTo}. Success = {response.Item1}. ErrorMessage = {response.Item2}.");
            
            return new SendCredentialsEmailResponse()
            {
                Success = response.Item1,
                ErrorMessage = response.Item2
            };
        }
    }
}
