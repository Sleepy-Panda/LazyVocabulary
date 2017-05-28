using LazyVocabulary.Common.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LazyVocabulary.Common.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime PasswordUpdatedAt { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public LocaleLanguage Locale { get; set; }

        private static readonly Dictionary<LocaleLanguage, CultureInfo> _cultures;
        private static readonly LocaleLanguage _defaultLocale;

        public UserProfile()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            PasswordUpdatedAt = DateTime.Now;
            Locale = _defaultLocale;
        }

        public UserProfile(string locale) : this()
        {
            try
            {
                Locale = (LocaleLanguage)Enum.Parse(typeof(LocaleLanguage), locale, true);
            }
            catch (Exception)
            {
                Locale = _defaultLocale;
            }
        }

        static UserProfile()
        {
            _defaultLocale = LocaleLanguage.Ru;
            _cultures = new Dictionary<LocaleLanguage, CultureInfo>();

            foreach (LocaleLanguage language in Enum.GetValues(typeof(LocaleLanguage)))
            {
                _cultures.Add(language, new CultureInfo(language.ToString()));
            }
        }

        public CultureInfo UserCulture
        {
            get
            {
                return _cultures[Locale];
            }
        }

        public static CultureInfo DefaultCulture
        {
            get
            {
                return _cultures[_defaultLocale];
            }
        }
    }
}
