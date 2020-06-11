using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Repositories.AnswerRepositories.Interfaces;

namespace Repositories.AnswerRepositories
{
    public class ByeRepository : IByeRepository
    {
        private int _currentIndex;
        private readonly List<string> _byePhrases;
        public ByeRepository(string byeRepositoryFilePath)
        {
            if (new FileInfo(byeRepositoryFilePath).Exists)
            {
                var xmlDocument = new XmlDocument();
                _byePhrases = new List<string>();

                try
                {
                    xmlDocument.Load(byeRepositoryFilePath);

                    var xRoot = xmlDocument.DocumentElement;
                    foreach (XmlNode xNode in xRoot)
                    {
                        this._byePhrases.Add(xNode.InnerText);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
            {
                _byePhrases = new List<string>();
                this._byePhrases.Add("Bye1");
                this._byePhrases.Add("Bye2");
                this._byePhrases.Add("Bye3");
            }
        }
        public string GetByePhrase()
        {
            if (this._currentIndex >= this._byePhrases.Count)
                this._currentIndex = 0;

            return this._byePhrases[_currentIndex++];
        }
    }
}