namespace LazyVocabulary.Common.Entities
{
    public class TranslatedPhrase
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public int SourcePhraseId { get; set; }
        public SourcePhrase SourcePhrase { get; set; }
    }
}
