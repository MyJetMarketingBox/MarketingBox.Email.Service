using System;
using System.Threading.Tasks;
using MarketingBox.Affiliate.Service.Messages.Affiliates;
using MarketingBox.Email.Service.Domain;
using MarketingBox.Email.Service.Domain.Models;
using Microsoft.Extensions.Logging;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;

namespace MarketingBox.Email.Service.Engines
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
                AffiliateId = elem.Affiliate.AffiliateId,
                Token = token
            });
            _logger.LogInformation($"Saving to noSql entity : {JsonConvert.SerializeObject(noSqlEntity)}");
            await _dataWriter.InsertOrReplaceAsync(noSqlEntity);

            await _dataWriter.CleanAndKeepMaxPartitions(Program.Settings.ConfirmationCacheLength);

            await _sendGridEmailSender.SendMailAsync(
                elem.Affiliate.GeneralInfo.Email,
                Program.Settings.ConfirmationEmailHeader,
                Program.Settings.ConfirmationEmailSubject,
                Program.Settings.ConfirmationEmailTemplateId,
                new
                {
                    nickName = elem.Affiliate.GeneralInfo.Username,
                    confirmEmailUrl = GetConfirmationLink(token)
                });
        }

        private static string GetConfirmationLink(string token)
        {
            return Program.Settings.RegistrationAffiliateApiUrl + "/api/affiliates/confirmation/" + token;
        }
    }
}