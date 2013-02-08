using CsQuery;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive;
using System.Reactive.Disposables;

namespace MisterHex.WebCrawling
{
    public class Crawler
    {
        private class CrawledLinksObservable : ObservableBase<Uri>
        {
            private List<Uri> _jobList = new List<Uri>();
            private ReplaySubject<Uri> _subject = new ReplaySubject<Uri>();
            private Uri _rootUri;
            private IEnumerable<IUriFilter> _filters = Enumerable.Empty<IUriFilter>();

            public CrawledLinksObservable(Uri uri)
                : this(uri, new IUriFilter[0])
            { }

            public CrawledLinksObservable(Uri uri, params IUriFilter[] filters)
            {
                _rootUri = uri;
                _filters = filters;
            }

            protected override IDisposable SubscribeCore(IObserver<Uri> observer)
            {
                StartCrawlingAsync(_rootUri);
                return _subject.Subscribe(observer);
            }

            private Task StartCrawlingAsync(Uri uri)
            {
                return Task.Factory.StartNew(() => StartCrawling(uri));
            }

            private void StartCrawling(Uri uri)
            {
                _jobList.Add(uri);

                while (_jobList.Count != 0)
                {
                    List<Task<IEnumerable<Uri>>> crawlerTasks = new List<Task<IEnumerable<Uri>>>();

                    var jobsToRun = _jobList.ToList();
                    _jobList.Clear();

                    jobsToRun.ForEach(i =>
                    {
                        var task = CrawlSingle(i)
                            .ContinueWith(t =>
                            {
                                var filtered = Filter(t.Result, _filters.ToArray()).AsEnumerable();
                                filtered.ToList().ForEach(_subject.OnNext);
                                return filtered;
                            }, TaskContinuationOptions.OnlyOnRanToCompletion);
                        crawlerTasks.Add(task);
                    });

                    Task.WaitAll(crawlerTasks.ToArray());

                    List<Uri> newLinks = crawlerTasks.SelectMany(i => i.Result).ToList();

                    _jobList.AddRange(newLinks.ToArray());
                }

                _subject.OnCompleted();
            }

            private async Task<IEnumerable<Uri>> CrawlSingle(Uri uri)
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(1) })
                {
                    IEnumerable<Uri> result = new List<Uri>();

                    try
                    {
                        string html = await client.GetStringAsync(uri).ContinueWith(t => t.Result, TaskContinuationOptions.OnlyOnRanToCompletion);
                        result = CQ.Create(html)["a"].Select(i => i.Attributes["href"]).SafeSelect(i => new Uri(i));
                        return result;
                    }
                    catch
                    { }
                    return result;
                }
            }

            private static List<Uri> Filter(IEnumerable<Uri> uris, params IUriFilter[] filters)
            {
                var filtered = uris.ToList();
                foreach (var filter in filters.ToList())
                {
                    filtered = filter.Filter(filtered);
                }
                return filtered;
            }
        }

        public IObservable<Uri> Crawl(Uri uri)
        {
            return new CrawledLinksObservable(uri, new ExcludeRootUriFilter(uri), new ExternalUriFilter(uri), new AlreadyVisitedUriFilter());
        }

    }
}
