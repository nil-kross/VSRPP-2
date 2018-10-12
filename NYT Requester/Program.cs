using System;

// http://developer.nytimes.com/books_api.json#/Console/GET/lists/best-sellers/history.json

namespace Lomtseu
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = new Uri($"http://developer.nytimes.com/proxy/https/api.nytimes.com/svc/books/v3/lists/best-sellers/history.json?api-key={ApiKey.ToString()}");
            IRequester requester = new Requester(new LoggingHandler(new ConsoleLogger(false)));

            var res = requester.GetResponseItem<BestSellersResponse>(url);
            Console.WriteLine(" Press any key to continue ...");
            Console.ReadKey();

            Console.WriteLine("There are New York Times bestsellers:");
            foreach (var book in res.Results)
            {
                Console.WriteLine(String.Format(
                    "{0} = <<{1}>> {2}",
                    book.Author,
                    book.Title,
                    !String.IsNullOrWhiteSpace(book.Publisher) ? $"({book.Publisher})" : ""
                ));
            }

            Console.WriteLine(" Press any key to finish program ...");
            Console.ReadKey();
        }
    }
}