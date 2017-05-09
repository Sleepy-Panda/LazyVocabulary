using System;

namespace LazyVocabulary.Logic.Helpers
{
    public class TokenGeneratorHelper
    {
        public static string GetToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
