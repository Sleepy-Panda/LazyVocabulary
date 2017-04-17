using System.Collections.Generic;

namespace LazyVocabulary.Common.Entities
{
    public class Language
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string FlagImagePath { get; set; }

        public virtual ICollection<Dictionary> SourceDictionaries { get; set; }

        public virtual ICollection<Dictionary> TargetDictionaries { get; set; }

        public Language()
        {
            SourceDictionaries = new List<Dictionary>();
            TargetDictionaries = new List<Dictionary>();
        }
    }
}
