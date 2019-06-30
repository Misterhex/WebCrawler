using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive;
using System.Threading;

namespace WebCrawling
{
    public class Crawler
    {
        class ReceivingCrawledUri : ObservableBase<Uri>
        {
            public int _numberOfLinksLeft = 0;

            private ReplaySubject<Uri> _subject = new ReplaySubject<Uri>();

            private readonly Uri _uri;

            private IEnumerable<IUriFilter> _filters;

            public ReceivingCrawledUri(Uri uri)
                : this(uri, Enumerable.Empty<IUriFilter>().ToArray())
            { }

            public ReceivingCrawledUri(Uri uri, params IUriFilter[] filters)
            {
                _uri = uri;
                _filters = filters;

            }

            protected override IDisposable SubscribeCore(IObserver<Uri> observer)
            {
                var _ = CrawlAsync(_uri);
                return _subject.Subscribe(observer);
            }

            private async Task CrawlAsync(Uri uri)
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(1) })
                {
                    IEnumerable<Uri> result = new List<Uri>();

                    try
                    {
                        string html = await client.GetStringAsync(uri);
                        result = CQ.Create(html)["a"].Select(i => i.Attributes["href"]).SafeSelect(i => new Uri(i));
                        result = Filter(result, _filters.ToArray());

                        result.ToList().ForEach(async i =>
                        {
                            Interlocked.Increment(ref _numberOfLinksLeft);
                            _subject.OnNext(i);
                            await CrawlAsync(i);
                        });
                    }
                    catch
                    { }

                    if (Interlocked.Decrement(ref _numberOfLinksLeft) == 0)
                        _subject.OnCompleted();
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
            return new ReceivingCrawledUri(uri, new ExcludeRootUriFilter(uri), new ExternalUriFilter(uri), new AlreadyVisitedUriFilter());
        }

        public IObservable<Uri> Crawl(Uri uri, params IUriFilter[] filters)
        {
            return new ReceivingCrawledUri(uri, filters);
        }
    }
}
