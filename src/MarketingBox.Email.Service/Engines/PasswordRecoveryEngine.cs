// using System.Threading.Tasks;
// using MarketingBox.Email.Service.Domain;
// using MarketingBox.PasswordApi.Domain.Models;
// using Microsoft.Extensions.Logging;
//
// namespace MarketingBox.Email.Service.Engines
// {
//     public class PasswordRecoveryEngine
//     {
//         private readonly ILogger<PasswordRecoveryEngine> _logger;
//         private readonly ISendGridEmailSender _sendGridEmailSender;
//
//         public PasswordRecoveryEngine(ILogger<PasswordRecoveryEngine> logger,
//             ISendGridEmailSender sendGridEmailSender)
//         {
//             _sendGridEmailSender = sendGridEmailSender;
//             _logger = logger;
//         }
//
//         public async Task HandleAffiliate(PasswordRecoveryEmailMessage message)
//         {
//             var (success, error) = await _sendGridEmailSender.SendMailAsync(
//                 message.Email,
//                 Program.Settings.RecoverPasswordEmailHeader,
//                 Program.Settings.RecoverPasswordEmailSubject,
//                 Program.Settings.RecoverPasswordEmailTemplateId,
//                 new
//                 {
//                     changePasswordUrl = message.Url,
//                     nickName = message.UserName
//                 });
//             if (!success)
//             {
//                 _logger.LogError(error);
//             }
//         }
//     }
// }