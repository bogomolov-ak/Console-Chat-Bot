using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatBot
{
    public static class Program
    {
        public static event EventHandler<ReadMessageEventArgs> ReadEvent;
        static void Main(string[] args)
        {
            var botThread = new BotThread();
            botThread.Listen();

            while (true)
            {
                var message = Console.ReadLine();
                if (!string.IsNullOrEmpty(message) && message.Equals("exit"))
                {
                    break;
                }

                try
                {
                    botThread.Dispatch(message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    break;
                }
            }

            botThread.EndListen();
        }
    }    
}
