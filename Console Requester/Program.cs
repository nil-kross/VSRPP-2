using Lomtseu;
using System;

namespace Lomtseu.ConsoleRequester
{
    class Program
    {
        static void Main(string[] args)
        {
            Requester requester = new Requester();
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
                } else
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