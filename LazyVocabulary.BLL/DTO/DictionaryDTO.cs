using System;

namespace LazyVocabulary.BLL.DTO
{
    public class DictionaryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ViewsCount { get; set; }

        public string ApplicationUserId { get; set; }

        public int SourceLanguageId { get; set; }

        public string SourceLanguageImagePath { get; set; }

        public int TargetLanguageId { get; set; }

        public string TargetLanguageImagePath { get; set; }

        public int PhrasesCount { get; set; }
    }
}
