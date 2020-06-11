using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Repositories.AnswerRepositories;
using Repositories.AnswerRepositories.Interfaces;
using Repositories.QuestionRepositories;
using Repositories.QuestionRepositories.Interfaces;

namespace ChatBot
{
    public class BotThread : IDisposable
    {
        private Queue<string> Questions { get; }
        private readonly CancellationTokenSource _cancellationTokenSource;

        private const string QuestionRepositoryFilePath = @"C:\ChatBot\Repositories\QuestionRepository";
        private const string AphorismRepositoryFilePath = @"C:\ChatBot\Repositories\AnswerRepository\AphorismRepository.xml";
        private const string ByeRepositoryFilePath = @"C:\ChatBot\Repositories\AnswerRepository\ByeRepository.xml";
        private const string HelloRepositoryFilePath = @"C:\ChatBot\Repositories\AnswerRepository\HelloRepository.xml";
        private const string JokesRepositoryFilePath = @"C:\ChatBot\Repositories\AnswerRepository\JokesmRepository.xml";
        private const string MyNameRepositoryFilePath = @"C:\ChatBot\Repositories\AnswerRepository\MyNameRepository.xml";

        private readonly IQuestionRepository _questionRepository;
        private readonly IAphorismRepository _aphorismRepository;
        private readonly IByeRepository _byeRepository;
        private readonly IHelloRepository _helloRepository;
        private readonly IJokesRepository _jokesRepository;
        private readonly IMyNameRepository _myNameRepository;

        public BotThread()
        {
            Questions = new Queue<string>();
            _cancellationTokenSource = new CancellationTokenSource();

            _questionRepository = new QuestionRepository(QuestionRepositoryFilePath);
            _aphorismRepository = new AphorismsRepository(AphorismRepositoryFilePath);
            _byeRepository = new ByeRepository(ByeRepositoryFilePath);
            _helloRepository = new HelloRepository(HelloRepositoryFilePath);
            _jokesRepository = new JokesRepository(JokesRepositoryFilePath);
            _myNameRepository = new MyNameRepository(MyNameRepositoryFilePath);
        }

        public void Dispatch(string message)
        {
            Questions.Enqueue(message);
        }

        public void Listen()
        {
            Task.Run(Answer, _cancellationTokenSource.Token);
        }

        public void EndListen()
        {
            _cancellationTokenSource.Cancel();
        }

        private void Answer()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                Thread.Sleep(5000);

                if (Questions.Count == 0)
                    continue;

                var message = Questions.Dequeue();
                if (string.IsNullOrEmpty(message))
                {
                    Console.WriteLine($"Шарпик: {_aphorismRepository.GetAphorism()}");
                    continue;
                }

                var answerBuilder = new StringBuilder();

                if (_questionRepository.HelloPhrases.Any(phrase => phrase.IsMatch(message)))
                    answerBuilder.AppendLine($"{_helloRepository.GetHelloPhrase()}");

                if (_questionRepository.MyNamePhrases.Any(phrase => phrase.IsMatch(message)))
                    answerBuilder.AppendLine($"{_myNameRepository.GetMyNamePhrase()}");

                if (_questionRepository.JokePhrases.Any(phrase => phrase.IsMatch(message)))
                    answerBuilder.AppendLine($"{_jokesRepository.GetJoke()}");

                if (_questionRepository.DatePhrases.Any(phrase => phrase.IsMatch(message)))
                    answerBuilder.AppendLine($"{DateTime.Now}");

                if (_questionRepository.ByePhrases.Any(phrase => phrase.IsMatch(message)))
                {
                    answerBuilder.AppendLine($"{_byeRepository.GetByePhrase()}");
                    _cancellationTokenSource.Cancel();
                }

                Console.WriteLine(answerBuilder.Length == 0 ? $"Шарпик: {_aphorismRepository.GetAphorism()}" : $"Шарпик: {answerBuilder}");
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}