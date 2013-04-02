using MisterHex.WebCrawling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Crawler crawler = new Crawler();
            IObservable<Uri> observable1 = crawler.Crawl(new Uri("http://www.codinghorror.com/"));

            observable1.Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));

            Console.ReadLine();
        }

    }
}
