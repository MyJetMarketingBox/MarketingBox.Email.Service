using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.MarketingBox.Email.Service.Settings
{
    public class SettingsModel
    {
        [YamlProperty("MarketingBox.Email.Service.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("MarketingBox.Email.Service.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("MarketingBox.Email.Service.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }
    }
}
