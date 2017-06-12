using LazyVocabulary.Logic.OperationDetails;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LazyVocabulary.Logic.Helpers
{
    public static class EmailHelper
    {
        public static async Task<Result> SendEmailToAsync(string email, string subject, string text)
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

                await Send(message);
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

        public static async Task<Result> SendEmailFromAsync(string email, string subject, string text)
        {
            var result = new Result();

            text += $"\n\nОтправитель: { email }";

            try
            {
                MailAddress from = new MailAddress(ConfigurationHelper.CredentialEmail);
                MailAddress to = new MailAddress(ConfigurationHelper.FeedbackReceiverEmail);
                MailMessage message = new MailMessage(from, to);

                message.Subject = subject;
                message.Body = text;
                message.IsBodyHtml = false;

                await Send(message);
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

        private static async Task Send(MailMessage message)
        {
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
        }
    }
}
