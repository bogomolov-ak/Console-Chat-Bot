using System;

namespace Repositories
{
    public class QuestionPhrasesRepository
    {
        public readonly Regex[] HelloPhrases =
        {
            new Regex(@".*привет.*", RegexOptions.IgnoreCase),
            new Regex(@".*здравствуй.*", RegexOptions.IgnoreCase),
            new Regex(@".*добрый день.*", RegexOptions.IgnoreCase),
            new Regex(@".*добрый вечер.*", RegexOptions.IgnoreCase),
            new Regex(@".*доброе утро.*", RegexOptions.IgnoreCase),
            new Regex(@".*доброй ночи.*", RegexOptions.IgnoreCase)
        };

        public readonly Regex[] MyNamePhrases =
        {
            new Regex(@".*как тебя зовут.*", RegexOptions.IgnoreCase)
        };

        public readonly Regex[] JokePhrases =
        {
            new Regex(@".*анекдот.*", RegexOptions.IgnoreCase)
        };

        public readonly Regex[] DatePhrases =
        {
            new Regex(@".*сколько времени.*", RegexOptions.IgnoreCase),
            new Regex(@".*который час.*", RegexOptions.IgnoreCase),
        };

        public readonly Regex[] ByePhrases =
        {
            new Regex(@".*пока.*", RegexOptions.IgnoreCase),
            new Regex(@".*до свидания.*", RegexOptions.IgnoreCase)
        };
    }
}