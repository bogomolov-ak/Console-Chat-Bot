using System;
using System.Text;
using System.Linq;
using System.Threading;
using Repositories.AnswerRepositories;
using Repositories.AnswerRepositories.Interfaces;
using Repositories.QuestionRepositories;
using Repositories.QuestionRepositories.Interfaces;

namespace ChatBot
{
    public class BotAnswerer : IDisposable
    {
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

        public BotAnswerer()
        {
            _questionRepository = new QuestionRepository(QuestionRepositoryFilePath);
            _aphorismRepository = new AphorismsRepository(AphorismRepositoryFilePath);
            _byeRepository = new ByeRepository(ByeRepositoryFilePath);
            _helloRepository = new HelloRepository(HelloRepositoryFilePath);
            _jokesRepository = new JokesRepository(JokesRepositoryFilePath);
            _myNameRepository = new MyNameRepository(MyNameRepositoryFilePath);
        }
        
        public void Listen()
        {
            Program.ReadEvent += ReadEventHandler;
        }

        public void EndListen()
        {
            Program.ReadEvent -= ReadEventHandler;
        }
        
        public void Dispose()
        {
            Program.ReadEvent -= ReadEventHandler;
        }

        public void ReadEventHandler(object sender, EventArgs args)
        {
            var message = (args as ReadMessageEventArgs)?.Message;
            if (string.IsNullOrEmpty(message))
            {
                Console.WriteLine($"Шарпик: {_aphorismRepository.GetAphorism()}");
                return;
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
                this.EndListen();
            }

            Thread.Sleep(5000);

            Console.WriteLine(answerBuilder.Length == 0 ? $"Шарпик: {_aphorismRepository.GetAphorism()}" : $"Шарпик: {answerBuilder}");
        }
    }
}
