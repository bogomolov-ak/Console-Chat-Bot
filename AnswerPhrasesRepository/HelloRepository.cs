namespace AnswerPhrasesRepository
{
    public class HelloRepository
    {
        private static int _currentIndex;
        private static string[] HelloPhrases { get; }
        static HelloRepository()
        {
            HelloPhrases = new[] { "Привет!", "Добрейший вечерочек!", "Доброе утро!" };
            _currentIndex = 0;
        }
        public static string GetHelloPhrase()
        {
            if (_currentIndex >= HelloPhrases.Length)
                _currentIndex = 0;

            return HelloPhrases[_currentIndex++];
        }
    }
}