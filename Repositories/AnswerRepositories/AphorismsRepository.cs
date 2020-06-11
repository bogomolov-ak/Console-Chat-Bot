using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Repositories.AnswerRepositories.Interfaces;

namespace Repositories.AnswerRepositories
{
    public class AphorismsRepository : IAphorismRepository
    {
        private int _currentIndex;
        private readonly List<string> _aphorisms;
        
        public AphorismsRepository(string aphorismRepositoryFilePath)
        {
            if (new FileInfo(aphorismRepositoryFilePath).Exists)
            {
                var xmlDocument = new XmlDocument();
                _aphorisms = new List<string>();

                try
                {
                    xmlDocument.Load(aphorismRepositoryFilePath);

                    var xRoot = xmlDocument.DocumentElement;
                    foreach (XmlNode xNode in xRoot)
                    {
                        this._aphorisms.Add(xNode.InnerText);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
            {
                _aphorisms = new List<string>();
                this._aphorisms.Add("Aphorism1");
                this._aphorisms.Add("Aphorism2");
                this._aphorisms.Add("Aphorism3");
            }
        }

        public string GetAphorism()
        {
            if (this._currentIndex >= this._aphorisms.Count)
                this._currentIndex = 0;

            return this._aphorisms[_currentIndex++];
        }
    }
}
