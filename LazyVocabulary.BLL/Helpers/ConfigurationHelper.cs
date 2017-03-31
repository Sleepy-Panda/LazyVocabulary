using System;
using System.Configuration;

namespace LazyVocabulary.BLL.Helpers
{
    public static class ConfigurationHelper
    {
        public static string CredentialEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["CredentialEmail"];
            }
        }

        public static string CredentialPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["CredentialPassword"];
            }
        }

        public static string SmtpClientHost
        {
            get
            {
                return ConfigurationManager.AppSettings["SmtpClientHost"];
            }
        }

        public static int SmtpClientPort
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings["SmtpClientPort"]);
            }
        }
    }
}
