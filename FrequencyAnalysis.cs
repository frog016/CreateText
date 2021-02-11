using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysis
    {
        public static void IncreaseValue(Dictionary<string, Dictionary<string, int>> dictionary, string key, string value)
        {
            if (!dictionary.ContainsKey(key))
                dictionary[key] = new Dictionary<string, int>();
            if (!dictionary[key].ContainsKey(value))
                dictionary[key][value] = 0;
            dictionary[key][value]++;
        }

        public static Dictionary<string, Dictionary<string, int>> 
            CountRepetitionNGram(List<List<string>> text, int numberCharAtBeginNgram)
        {
            var repetitionRate = new Dictionary<string, Dictionary<string, int>>();
            for (var i = 0; i < text.Count; i++)
                for (var j = 0; j < text[i].Count - numberCharAtBeginNgram; j++)
                {
                    var keyNgram = text[i][j];
                    var valueNgram = text[i][j + numberCharAtBeginNgram];

                    for (var ngram = j + 1; ngram < j + numberCharAtBeginNgram; ngram++)
                        keyNgram += " " + text[i][ngram];

                    IncreaseValue(repetitionRate, keyNgram, valueNgram);
                }

            return repetitionRate;
        }

        public static Dictionary<string, Dictionary<string, int>> CombiningTwoDict(
            Dictionary<string, Dictionary<string, int>> firtDictionary,
            Dictionary<string, Dictionary<string, int>> secondDictionary)
        {
            var result = firtDictionary;
            foreach (var pair in secondDictionary)
                result.Add(pair.Key, pair.Value);
            return result;
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();

            var countValuesByKey = CombiningTwoDict(CountRepetitionNGram(text, 1), CountRepetitionNGram(text, 2));

            foreach (var value in countValuesByKey)
            {
                var maxValue = int.MinValue;
                string keyMaxValue = null;
                foreach (var pair in value.Value)
                {
                    if (pair.Value > maxValue)
                    {
                        maxValue = pair.Value;
                        keyMaxValue = pair.Key;
                    }
                    else if (pair.Value == maxValue && string.CompareOrdinal(pair.Key, keyMaxValue) < 0)
                        keyMaxValue = pair.Key;
                }
                result.Add(value.Key, keyMaxValue);
            }

            return result;
        }
    }
}