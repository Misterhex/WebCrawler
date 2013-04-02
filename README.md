WebCrawler
=====================

Just a simple web crawler which return crawled links as IObservable<Uri> using reactive extension and async await.

Install-Package MisterHex.WebCrawling
<br /> 
<br /> 
Usage
<br /> 

<code>Crawler crawler = new Crawler();</code><br/>
<code>var observable = crawler.Crawl(new Uri("http://www.codinghorror.com/"));</code>
<code>observable.Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));</code>

