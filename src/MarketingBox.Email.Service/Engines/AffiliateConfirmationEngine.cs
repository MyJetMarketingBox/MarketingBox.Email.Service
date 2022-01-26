using System;
using System.Threading.Tasks;
using MarketingBox.Affiliate.Service.Messages.Affiliates;
using Microsoft.Extensions.Logging;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;
using MarketingBox.Email.Service.Domain;
using Service.MarketingBox.Email.Service.Domain.Models;

namespace Service.MarketingBox.Email.Service.Engines
{
    public class AffiliateConfirmationEngine
    {
        private readonly ILogger<AffiliateConfirmationEngine> _logger;
        private readonly IMyNoSqlServerDataWriter<AffiliateConfirmationNoSql> _dataWriter;
        private readonly ISendGridEmailSender _sendGridEmailSender;

        public AffiliateConfirmationEngine(ILogger<AffiliateConfirmationEngine> logger, 
            IMyNoSqlServerDataWriter<AffiliateConfirmationNoSql> dataWriter, 
            ISendGridEmailSender sendGridEmailSender)
        {
            _dataWriter = dataWriter;
            _sendGridEmailSender = sendGridEmailSender;
            _logger = logger;
        }

        public async Task HandleAffiliate(AffiliateUpdated elem)
        {
            var token = Guid.NewGuid().ToString("N");

            var noSqlEntity = AffiliateConfirmationNoSql.Create(new AffiliateConfirmation()
            {
                CreatedDate = DateTime.UtcNow,
                ExpiredDate = DateTime.UtcNow.AddHours(Program.Settings.ConfirmationTokenLifetimeInHours),
                AffiliateId = elem.AffiliateId,
                Token = token
            });
            _logger.LogInformation($"Saving to noSql entity : {JsonConvert.SerializeObject(noSqlEntity)}");
            await _dataWriter.InsertOrReplaceAsync(noSqlEntity);
            
            await _dataWriter.CleanAndKeepMaxPartitions(Program.Settings.ConfirmationCacheLength);

            var confirmationLink = GetConfirmationLink(token);
            var response = await _sendGridEmailSender.SendMailAsync(
                elem.GeneralInfo.Email,
                Program.Settings.ConfirmationEmailHeader,
                Program.Settings.ConfirmationEmailSubject,
                Program.Settings.ConfirmationEmailTemplateId,
                new {
                    link = confirmationLink
                });
            _logger.LogInformation($"Sent email to {elem.GeneralInfo.Email} with confirmation link: {confirmationLink}. Receive response = {response}");
        }

        private static string GetConfirmationLink(string token)
        {
            return Program.Settings.RegistrationAffiliateApiUrl + "/api/affiliates/confirmation/" + token;
        }
    }
}