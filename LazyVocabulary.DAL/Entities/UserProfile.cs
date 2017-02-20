using System;

namespace LazyVocabulary.DAL.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AvatarImagePath { get; set; }

        public Guid ApplicationUserId { get; set; }
        public int GuiLanguageId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual GuiLanguage GuiLanguage { get; set; }
    }
}
