using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using Repositories.QuestionRepositories.Interfaces;

namespace Repositories.QuestionRepositories
{
    public class QuestionRepository : IQuestionRepository
    {
        public QuestionRepository(string questionRepositoryFilesPath)
        {
            InitializeHelloRepository(questionRepositoryFilesPath);
            InitializeMyNameRepository(questionRepositoryFilesPath);
            InitializeJokesRepository(questionRepositoryFilesPath);
            InitializeDateRepository(questionRepositoryFilesPath);
            InitializeByeRepository(questionRepositoryFilesPath);
        }

        public List<Regex> HelloPhrases { get; private set; }
        public List<Regex> MyNamePhrases { get; private set; }
        public List<Regex> JokePhrases { get; private set; }
        public List<Regex> DatePhrases { get; private set; }
        public List<Regex> ByePhrases { get; private set; } 

        private void InitializeHelloRepository(string questionRepositoryFilesPath)
        {
            var helloPhrasesRepositoryFilePath = $@"{questionRepositoryFilesPath}\HelloPhrases.xml";
            if (new FileInfo(helloPhrasesRepositoryFilePath).Exists)
            {
                this.HelloPhrases = new List<Regex>();
                RepositoryLoad(helloPhrasesRepositoryFilePath, HelloPhrases);
            }
            else
            {
                this.HelloPhrases = new List<Regex>
                {
                    new Regex(@".*привет.*", RegexOptions.IgnoreCase),
                    new Regex(@".*здравствуй.*", RegexOptions.IgnoreCase),
                    new Regex(@".*добрый день.*", RegexOptions.IgnoreCase),
                    new Regex(@".*добрый вечер.*", RegexOptions.IgnoreCase),
                    new Regex(@".*доброе утро.*", RegexOptions.IgnoreCase),
                    new Regex(@".*доброй ночи.*", RegexOptions.IgnoreCase)
                };
            }
        }

        private void InitializeMyNameRepository(string questionRepositoryFilesPath)
        {
            var myNamePhrasesRepositoryFilePath = $@"{questionRepositoryFilesPath}\MyNamePhrases.xml";
            if (new FileInfo(myNamePhrasesRepositoryFilePath).Exists)
            {
                this.MyNamePhrases = new List<Regex>();
                RepositoryLoad(myNamePhrasesRepositoryFilePath, MyNamePhrases);
            }
            else
            {
                this.MyNamePhrases = new List<Regex>
                {
                    new Regex(@".*как тебя зовут.*", RegexOptions.IgnoreCase)
                };
            }
        }

        private void InitializeJokesRepository(string questionRepositoryFilesPath)
        {
            var jokesRepositoryFilePath = $@"{questionRepositoryFilesPath}\JokesPhrases.xml";
            if (new FileInfo(jokesRepositoryFilePath).Exists)
            {
                this.JokePhrases = new List<Regex>();
                RepositoryLoad(jokesRepositoryFilePath, JokePhrases);
            }
            else
            {
                this.JokePhrases = new List<Regex>
                {
                    new Regex(@".*анекдот.*", RegexOptions.IgnoreCase)
                };
            }
        }

        private void InitializeDateRepository(string questionRepositoryFilesPath)
        {
            var dateRepositoryFilePath = $@"{questionRepositoryFilesPath}\DatePhrases.xml";
            if (new FileInfo(dateRepositoryFilePath).Exists)
            {
                this.DatePhrases = new List<Regex>();
                RepositoryLoad(dateRepositoryFilePath, DatePhrases);
            }
            else
            {
                this.DatePhrases = new List<Regex>
                {
                    new Regex(@".*сколько времени.*", RegexOptions.IgnoreCase),
                    new Regex(@".*который час.*", RegexOptions.IgnoreCase)
                };
            }
        }

        private void InitializeByeRepository(string questionRepositoryFilesPath)
        {
            var byeRepositoryFilePath = $@"{questionRepositoryFilesPath}\ByePhrases.xml";
            if (new FileInfo(byeRepositoryFilePath).Exists)
            {
                this.ByePhrases = new List<Regex>();
                RepositoryLoad(byeRepositoryFilePath, ByePhrases);
            }
            else
            {
                this.ByePhrases = new List<Regex>
                {
                    new Regex(@".*пока.*", RegexOptions.IgnoreCase),
                    new Regex(@".*до свидания.*", RegexOptions.IgnoreCase)
                };
            }
        }

        private static void RepositoryLoad(string filePath, ICollection<Regex> repository)
        {
            var xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.Load(filePath);

                var xRoot = xmlDocument.DocumentElement;
                foreach (XmlNode xmlNode in xRoot)
                    repository.Add(new Regex($@".*{xmlNode.InnerText}.*", RegexOptions.IgnoreCase));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}