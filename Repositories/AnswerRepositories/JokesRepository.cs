using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Repositories.AnswerRepositories.Interfaces;

namespace Repositories.AnswerRepositories
{
    public class JokesRepository : IJokesRepository
    {
        private int _currentIndex;
        private readonly List<string> _jokes;
        public JokesRepository(string jokesRepositoryFilePath)
        {
            if (new FileInfo(jokesRepositoryFilePath).Exists)
            {
                var xmlDocument = new XmlDocument();
                _jokes = new List<string>();

                try
                {
                    xmlDocument.Load(jokesRepositoryFilePath);

                    var xRoot = xmlDocument.DocumentElement;
                    foreach (XmlNode xNode in xRoot)
                    {
                        this._jokes.Add(xNode.InnerText);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
            {
                _jokes = new List<string>();
                this._jokes.Add("Joke1");
                this._jokes.Add("Joke2");
                this._jokes.Add("Joke3");
            }
        }
        public string GetJoke()
        {
            if (this._currentIndex >= this._jokes.Count)
                this._currentIndex = 0;

            return this._jokes[this._currentIndex++];
        }
    }
}
