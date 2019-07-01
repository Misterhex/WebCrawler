WebCrawler
=====================
[![Build Status](https://misterhex.visualstudio.com/WebCrawler/_apis/build/status/Misterhex.WebCrawler?branchName=master)](https://misterhex.visualstudio.com/WebCrawler/_build/latest?definitionId=6&branchName=master)
[![NuGet version](https://badge.fury.io/nu/Misterhex.WebCrawling.svg)](https://badge.fury.io/nu/Misterhex.WebCrawling)

Just a simple web crawler which return crawled links as IObservable<Uri> using reactive extension and async await.

```
dotnet add package MisterHex.WebCrawling --version 2.0.1
```

## Usage

```cs
Crawler crawler = new Crawler();
IObservable<Uri> observable = crawler.Crawl(new Uri("http://www.codinghorror.com/"));
observable.Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));
```
