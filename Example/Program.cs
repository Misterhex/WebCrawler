using System;
using MisterHex.WebCrawling;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Crawler crawler = new Crawler();
            IObservable<Uri> observable = crawler.Crawl(new Uri("https://dotnet.microsoft.com"));

            observable.Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));

            Console.ReadLine();
        }
    }
}
