using System;
using System.Configuration;

namespace LazyVocabulary.Logic.Helpers
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

        public static string AvatarFolder
        {
            get
            {
                return ConfigurationManager.AppSettings["AvatarFolder"];
            }
        }

        public static string DefaultAvatar
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultAvatar"];
            }
        }

        public static string DictionaryNameForPluginRuEn
        {
            get
            {
                return ConfigurationManager.AppSettings["DictionaryNameForPluginRuEn"];
            }
        }

        public static string DictionaryDescriptionForPluginRuEn
        {
            get
            {
                return ConfigurationManager.AppSettings["DictionaryDescriptionForPluginRuEn"];
            }
        }
    }
}
