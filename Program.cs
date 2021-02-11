using System;
using System.Collections.Generic;
using System.IO;
using NUnitLite;

namespace TextAnalysis
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var text = File.ReadAllText("HarryPotterText.txt");
            var sentences = SentencesParser.ParseSentences(text);
            var frequency = FrequencyAnalysis.GetMostFrequentNextWords(sentences);

            while (true)
            {
                var rnd = new Random();
                var count = rnd.Next(0, 40);
                Console.Write("Введите первое слово (например, harry): ");
                var beginning = Console.ReadLine();
                if (string.IsNullOrEmpty(beginning)) return;
                var phrase = TextGenerator.ContinuePhrase(frequency, beginning.ToLower(), count);
                Console.WriteLine(phrase);
            }
        }
    }
}