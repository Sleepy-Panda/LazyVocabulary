using System.Collections.Generic;

namespace LazyVocabulary.DAL.Entities
{
    public class GuiLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string FlagImagePath { get; set; }
        public bool IsDefault { get; set; } = false;

        public virtual ICollection<UserProfile> UserProfiles { get; set; }

        public GuiLanguage()
        {
            UserProfiles = new List<UserProfile>();
        }
    }
}
