namespace AnswerPhrasesRepository
{
    public static class JokesRepository
    {
        private static int _currentIndex;
        private static string[] Jokes { get; }
        static JokesRepository()
        {
            Jokes = new[] { "Joke1", "Joke2", "Joke3" };
            _currentIndex = 0;
        }
        public static string GetJoke()
        {
            if (_currentIndex >= Jokes.Length)
                _currentIndex = 0;

            return Jokes[_currentIndex++];
        }
    }
}
