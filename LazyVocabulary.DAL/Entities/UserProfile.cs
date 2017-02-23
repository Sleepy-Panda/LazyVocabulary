using System;

namespace LazyVocabulary.DAL.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public DateTime PasswordUpdatedAt { get; set; } = DateTime.Now;
        public string AvatarImagePath { get; set; }

        public int GuiLanguageId { get; set; }
        public virtual GuiLanguage GuiLanguage { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
