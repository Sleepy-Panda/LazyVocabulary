using System.Collections.Generic;

namespace LazyVocabulary.Common.Entities
{
    public class SourcePhrase
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public int DictionaryId { get; set; }
        public Dictionary Dictionary { get; set; }

        public virtual ICollection<TranslatedPhrase> TranslatedPhrases { get; set; }

        public SourcePhrase()
        {
            TranslatedPhrases = new List<TranslatedPhrase>();
        }
    }
}
