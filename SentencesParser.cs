using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParser
    {
        public static List<string> AddWords(string snt)
        {
            var word = new StringBuilder();
            var sentence = new List<string>();

            for (var i = 0; i < snt.Length; i++)
            {
                if (Char.IsLetter(snt[i]) || snt[i] == '\'')
                    word.Append(snt[i]);
                else if (word.ToString() != "")
                {
                    sentence.Add(word.ToString().ToLower());
                    word = new StringBuilder();
                }
            }
            if (word.ToString() != "")
                sentence.Add(word.ToString().ToLower());

            return sentence;
        }

        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            char[] sentenceSeparators = {'.', '!', '?', ';', ':', '(', ')'};

            var sentences = text.Split(sentenceSeparators);
            foreach (var snt in sentences)
                if (snt != "")
                {
                    var sentence = AddWords(snt);
                    if(sentence.Count != 0)
                        sentencesList.Add(sentence);
                }
            return sentencesList;
        }
    }
}