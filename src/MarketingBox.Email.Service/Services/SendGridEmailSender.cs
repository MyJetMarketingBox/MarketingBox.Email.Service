using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MarketingBox.Email.Service.Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MarketingBox.Email.Service.Services
{
    public class SendGridEmailSender : ISendGridEmailSender
    {
        private readonly ILogger<SendGridEmailSender> _logger;
        private readonly SendGridClient _client;

        public SendGridEmailSender(ILogger<SendGridEmailSender> logger)
        {
            _logger = logger;
            
            var apiKey = Program.Settings.SendGridSettingsApiKey;
            _client = new SendGridClient(apiKey);
        }

        public async Task<bool> SendMailAsync(string to, string header, string subject, string templateId, object data, string from = "")
        {
            if (Regex.IsMatch(to, Program.Settings.IgnoreEmailsDomains))
            {
                _logger.LogError($"Email in ignored list: {to}");
                return false;
            }
            if (string.IsNullOrWhiteSpace(from))
            {
                from = Program.Settings.SendGridSettingsFrom;
            }
            try
            {
                var msg = new SendGridMessage
                {
                    From = new EmailAddress(from, header),
                    Subject = subject,
                    TemplateId = templateId
                };
                msg.AddTo(to);
                msg.SetTemplateData(data);
                var response = await _client.SendEmailAsync(msg);
                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    _logger.LogError("Can't send message, response: {resp}", JsonConvert.SerializeObject(response));
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return false;
            }
            return true;
        }
    }
}