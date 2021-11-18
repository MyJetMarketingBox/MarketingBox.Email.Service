using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.MarketingBox.Email.Service.Settings
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

        [YamlProperty("MarketingBoxEmailService.SpotServiceBusHostPort")]
        public string SpotServiceBusHostPort { get; set; }
    }
}
