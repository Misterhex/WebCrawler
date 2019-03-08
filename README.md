WebCrawler
=====================
[![Build status](https://ci.appveyor.com/api/projects/status/2wjwe5e2ug5siarr?svg=true)](https://ci.appveyor.com/project/Misterhex/webcrawler)
[![NuGet version](https://badge.fury.io/nu/Misterhex.WebCrawling.svg)](https://badge.fury.io/nu/Misterhex.WebCrawling)

Just a simple web crawler which return crawled links as IObservable<Uri> using reactive extension and async await.

<code>Install-Package MisterHex.WebCrawling</code>
<br /> 
<br /> 
Usage
<br /> 

<code>Crawler crawler = new Crawler();</code><br/>
<code>IObservable<Uri> observable = crawler.Crawl(new Uri("http://www.codinghorror.com/"));</code>
<code>observable.Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));</code>

