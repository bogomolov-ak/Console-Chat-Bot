using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Repositories.AnswerRepositories.Interfaces;

namespace Repositories.AnswerRepositories
{
    public class HelloRepository : IHelloRepository
    {
        private int _currentIndex;
        private readonly List<string> _helloPhrases;
        public HelloRepository(string helloRepositoryFilePath)
        {
            if (new FileInfo(helloRepositoryFilePath).Exists)
            {
                var xmlDocument = new XmlDocument();
                _helloPhrases = new List<string>();

                try
                {
                    xmlDocument.Load(helloRepositoryFilePath);

                    var xRoot = xmlDocument.DocumentElement;
                    foreach (XmlNode xNode in xRoot)
                    {
                        this._helloPhrases.Add(xNode.InnerText);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
            {
                _helloPhrases = new List<string>();
                this._helloPhrases.Add("Hello1");
                this._helloPhrases.Add("Hello2");
                this._helloPhrases.Add("Hello3");
            }
        }
        public string GetHelloPhrase()
        {
            if (this._currentIndex >= this._helloPhrases.Count)
                this._currentIndex = 0;

            return this._helloPhrases[this._currentIndex++];
        }
    }
}