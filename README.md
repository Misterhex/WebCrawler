MisterHex.WebCrawling
=====================

Lightweight web crawler which return result as IObservable<Uri> so it can be chained with other Rx (Reactive Extension) operators.
<br /> 
<br /> 
Usage
<br /> 

<code>IObservable<Uri> observable = Crawler.Crawl(new Uri("http://www.codinghorror.com/"));</code>
<code>observable.Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));
</code>
