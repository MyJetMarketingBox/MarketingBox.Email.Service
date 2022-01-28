using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace MarketingBox.Email.Service.Settings
{
    public class SettingsModel
    {
        [YamlProperty("MarketingBoxEmailService.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("MarketingBoxEmailService.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("MarketingBoxEmailService.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }

        [YamlProperty("MarketingBoxEmailService.SendGridSettingsFrom")]
        public string SendGridSettingsFrom { get; set; }
        
        [YamlProperty("MarketingBoxEmailService.SendGridSettingsApiKey")]
        public string SendGridSettingsApiKey { get; set; }
        
        [YamlProperty("MarketingBoxEmailService.IgnoreEmailsDomains")]
        public string IgnoreEmailsDomains { get; set; }

        [YamlProperty("MarketingBoxEmailService.MarketingBoxServiceBusHostPort")]
        public string MarketingBoxServiceBusHostPort { get; set; }

        [YamlProperty("MarketingBoxEmailService.MyNoSqlWriterUrl")]
        public string MyNoSqlWriterUrl { get; set; }

        [YamlProperty("MarketingBoxEmailService.ConfirmationTokenLifetimeInHours")]
        public int ConfirmationTokenLifetimeInHours { get; set; }

        [YamlProperty("MarketingBoxEmailService.ConfirmationCacheLength")]
        public int ConfirmationCacheLength { get; set; }

        [YamlProperty("MarketingBoxEmailService.ConfirmationEmailHeader")]
        public string ConfirmationEmailHeader { get; set; }
        
        [YamlProperty("MarketingBoxEmailService.ConfirmationEmailTemplateId")]
        public string ConfirmationEmailTemplateId { get; set; }
        
        [YamlProperty("MarketingBoxEmailService.ConfirmationEmailSubject")]
        public string ConfirmationEmailSubject { get; set; }
        
        [YamlProperty("MarketingBoxEmailService.RegistrationAffiliateApiUrl")]
        public string RegistrationAffiliateApiUrl { get; set; }

        [YamlProperty("MarketingBoxEmailService.CredentialsEmailHeader")]
        public string CredentialsEmailHeader { get; set; }
        
        [YamlProperty("MarketingBoxEmailService.CredentialsEmailSubject")]
        public string CredentialsEmailSubject { get; set; }
        
        [YamlProperty("MarketingBoxEmailService.CredentialsEmailTemplateId")]
        public string CredentialsEmailTemplateId { get; set; }
        
        [YamlProperty("MarketingBoxEmailService.CredentialsEmailLoginLink")]
        public object CredentialsEmailLoginLink { get; set; }
    }
}
