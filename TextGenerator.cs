using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGenerator
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var result = new StringBuilder(phraseBeginning);
            
            for (var word = wordsCount; word > 0; word--)
            {
                var phrase = result.ToString().Split(' ');
                
                var bigram = phrase[phrase.Length - 1];
                var trigram = "";
                if (phrase.Length > 1)
                    trigram = phrase[phrase.Length - 2] + " " + bigram;

                if (phrase.Length > 1 && nextWords.ContainsKey(trigram))
                    result.Append(" " + nextWords[trigram]);
                else if (nextWords.ContainsKey(bigram)) 
                    result.Append(" " + nextWords[bigram]);
                else return result.ToString();
            }

            return result.ToString();
        }
    }
}