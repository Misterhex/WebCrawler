WebCrawler
=====================

Lightweight web crawler which return result as IObservable<Uri>.
<br /> 
<br /> 
Usage
<br /> 

<code>Crawler crawler = new Crawler();</code>
<code>IObservable<Uri> observable1 = crawler.Crawl(new Uri("http://www.codinghorror.com/"));</code>

