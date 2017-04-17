using System;

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

        public string AvatarImagePath { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int? GuiLanguageId { get; set; }
        public virtual GuiLanguage GuiLanguage { get; set; }

        public UserProfile()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            PasswordUpdatedAt = DateTime.Now;
            AvatarImagePath = "default_avatar.png";
        }
    }
}
