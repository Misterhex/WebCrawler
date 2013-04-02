WebCrawler
=====================

Just a simple web crawler which return crawled links as IObservable<Uri> using reactive extension and async await.

<code>Install-Package MisterHex.WebCrawling</code>
<br /> 
<br /> 
Usage
<br /> 

<code>Crawler crawler = new Crawler();</code><br/>
<code>IObservable<Uri> observable = crawler.Crawl(new Uri("http://www.codinghorror.com/"));</code>
<code>observable.Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));</code>

