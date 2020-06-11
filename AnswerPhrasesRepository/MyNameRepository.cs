namespace AnswerPhrasesRepository
{
    public class MyNameRepository
    {
        private static int _currentIndex;
        private static string[] MyNamePhrases { get; }
        static MyNameRepository()
        {
            MyNamePhrases = new[] { "Меня зовут шарпик!", "Я Шарпик!", "Моё имя - Шарпик!" };
            _currentIndex = 0;
        }
        public static string GetMyNamePhrase()
        {
            if (_currentIndex >= MyNamePhrases.Length)
                _currentIndex = 0;

            return MyNamePhrases[_currentIndex++];
        }
    }
}