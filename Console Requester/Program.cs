using Lomtseu;
using Lomtseu.Logger;
using System;
using System.Net.Http;

namespace Lomtseu.ConsoleRequester
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.UserInterface();
        }

        static void DebugInterface()
        {
            var exampleUrl = new Uri($"http://google.com/");
            Requester req = new Requester(new LoggingHandler(new FileLogger(true)));

            var res = req.GetResponseString(exampleUrl);
        }

        static void UserInterface()
        {
            Requester requester = new Requester(new LoggingHandler(new ConsoleLogger(true)));
            var isDone = false;

            while (!isDone)
            {
                String uriString = null;
                String responseString = null;

                Console.Clear();
                Console.WriteLine(
                    String.Format(
                        "\r\n\t{0}\r\n",
                        "Введите URI web-страницы:"
                    )
                );
                uriString = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(uriString))
                {
                    Console.Clear();
                    Console.WriteLine(
                        String.Format(
                            "\r\n\t{0}\r\n",
                            "Ожидание ответа от web-страницы ..."
                        )
                    );

                    var uri = new Uri(uriString);
                    responseString = requester.GetResponseString(uri);


                    Console.Clear();
                    Console.WriteLine(
                        responseString
                    );

                    Console.ReadLine();
                }
                else
                {
                    isDone = true;
                }
            }

            Console.Clear();
            Console.WriteLine(String.Format(
                "\r\n\t{0}\r\n",
                "Program is finished! Press any key to exit ..."
            ));
            Console.ReadLine();
        }
    }
}