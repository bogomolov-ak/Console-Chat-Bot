using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Repositories.AnswerRepositories.Interfaces;

namespace Repositories.AnswerRepositories
{
    public class MyNameRepository : IMyNameRepository
    {
        private int _currentIndex;
        private readonly List<string> _myNamePhrases;
        public MyNameRepository(string myNameRepositoryFilePath)
        {
            if (new FileInfo(myNameRepositoryFilePath).Exists)
            {
                var xmlDocument = new XmlDocument();
                _myNamePhrases = new List<string>();

                try
                {
                    xmlDocument.Load(myNameRepositoryFilePath);

                    var xRoot = xmlDocument.DocumentElement;
                    foreach (XmlNode xNode in xRoot)
                    {
                        this._myNamePhrases.Add(xNode.InnerText);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
            {
                _myNamePhrases = new List<string>();
                this._myNamePhrases.Add("MyName1");
                this._myNamePhrases.Add("MyName2");
                this._myNamePhrases.Add("MyName3");
            }
        }
        public string GetMyNamePhrase()
        {
            if (this._currentIndex >= this._myNamePhrases.Count)
                this._currentIndex = 0;

            return this._myNamePhrases[this._currentIndex++];
        }
    }
}