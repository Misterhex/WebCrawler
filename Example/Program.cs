using System;
using WebCrawling;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Crawler crawler = new Crawler();
            IObservable<Uri> observable = crawler.Crawl(new Uri("http://www.codinghorror.com/"));

            observable.Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));

            Console.ReadLine();
        }
    }
}
