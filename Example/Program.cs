using MisterHex.WebCrawling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            IObservable<Uri> observable = Crawler.Crawl(new Uri("http://www.codinghorror.com/"));
            observable.Subscribe(onNext: uri =>
            {
                Console.WriteLine(uri);
            }
            , onCompleted: () => Console.WriteLine("Crawling completed")
            );

            Console.ReadLine();

        }

    }
}
