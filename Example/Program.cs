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
            IObservable<Uri> observable1 = Crawler.Crawl(new Uri("http://www.codinghorror.com/"));

            observable1.Subscribe(onNext: uri =>
            {
                Console.WriteLine(uri);
            }
            , onCompleted: () => Console.WriteLine("Crawling completed")
            );

            Console.ReadLine();
        }

    }
}
