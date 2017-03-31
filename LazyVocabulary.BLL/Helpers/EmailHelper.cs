using LazyVocabulary.BLL.OperationDetails;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LazyVocabulary.BLL.Helpers
{
    public static class EmailHelper
    {
        public static async Task<Result> SendEmail(string email, string subject, string text)
        {
            var result = new Result();

            try
            {
                MailAddress from = new MailAddress(
                    ConfigurationHelper.CredentialEmail,
                    "Lazy Vocabulary"
                );
                MailAddress to = new MailAddress(email);
                MailMessage message = new MailMessage(from, to);

                message.Subject = subject;
                message.Body = text;
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(
                    ConfigurationHelper.SmtpClientHost,
                    ConfigurationHelper.SmtpClientPort
                );
                smtp.Credentials = new NetworkCredential(
                    ConfigurationHelper.CredentialEmail,
                    ConfigurationHelper.CredentialPassword
                );
                smtp.EnableSsl = true;

                await smtp.SendMailAsync(message);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.StackTrace = ex.StackTrace;
            }

            return result;
        }
    }
}
