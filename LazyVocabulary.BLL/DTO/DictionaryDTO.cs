using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int TargetLanguageId { get; set; }
    }
}
