WebCrawler
=====================
[![Build Status](https://misterhex.visualstudio.com/misterhex-web-crawler/_apis/build/status/misterhex-web-crawler-.NET%20Desktop-CI)](https://misterhex.visualstudio.com/misterhex-web-crawler/_build/latest?definitionId=1)
[![Build status](https://ci.appveyor.com/api/projects/status/2wjwe5e2ug5siarr?svg=true)](https://ci.appveyor.com/project/Misterhex/webcrawler)
[![NuGet version](https://badge.fury.io/nu/Misterhex.WebCrawling.svg)](https://badge.fury.io/nu/Misterhex.WebCrawling)

Just a simple web crawler which return crawled links as IObservable<Uri> using reactive extension and async await.

```
Install-Package MisterHex.WebCrawling
```

## Usage

```cs
Crawler crawler = new Crawler();
IObservable<Uri> observable = crawler.Crawl(new Uri("http://www.codinghorror.com/"));
observable.Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));
```
