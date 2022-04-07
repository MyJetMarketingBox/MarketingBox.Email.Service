using System;
using System.Threading.Tasks;
using MarketingBox.Email.Service.Domain;
using MarketingBox.Email.Service.Grpc;
using MarketingBox.Email.Service.Grpc.Models;
using MarketingBox.Sdk.Common.Extensions;
using MarketingBox.Sdk.Common.Models.Grpc;
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

        public async Task<Response<bool>> SendCredentialsEmailAsync(SendCredentialsEmailRequest request)
        {
            try
            {
                _logger.LogInformation("SendCredentialsEmailAsync receive request : {requestJson}", 
                    JsonConvert.SerializeObject(request));

                var (success, error) = await _sendGridEmailSender.SendMailAsync(
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
                _logger.LogInformation($"Sent credentials email to {request.EmailTo}. Success = {success}. ErrorMessage = {error}.");
                return new Response<bool>()
                {
                    Status = ResponseStatus.Ok,
                    Data = success
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return e.FailedResponse<bool>();
            }
        }
    }
}
