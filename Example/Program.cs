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
            IObservable<Uri> observable1 = Crawler.Crawl(new Uri("http://www.fairytail.tv"));
            IObservable<Uri> observable2 = Crawler.Crawl(new Uri("http://www.narutoget.com/"));
            IObservable<Uri> observable3 = Crawler.Crawl(new Uri("http://www.bleachget.com/"));
            IObservable<Uri> observable4 = Crawler.Crawl(new Uri("http://www1.watchop.com/"));


            observable1.Merge(observable2).Merge(observable3).Merge(observable4)
                .Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));

            Console.ReadLine();
        }

    }
}
