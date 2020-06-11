using System;

namespace ChatBot
{
    public class ReadMessageEventArgs : EventArgs
    {
        public string Message { get; }
        public ReadMessageEventArgs(string message)
        {
            Message = message;
        }
    }
}