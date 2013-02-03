MisterHex.WebCrawling
=====================

Lightweight web crawler which expose result as IObservable so that you can chain it with other Rx (Reactive Extension) operators.

Usage
<br /> 

<code>IObservable<Uri> observable1 = Crawler.Crawl(new Uri("http://www.codinghorror.com/"));</code>
<code>observable1.Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));
</code>
