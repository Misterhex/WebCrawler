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
        public static IObservable<Uri> Crawl(Uri uri)
        {
            return new Crawler().StartCrawl(uri);
        }

        private List<Uri> _jobList = new List<Uri>();
        private ReplaySubject<Uri> _subject = new ReplaySubject<Uri>();

        private IObservable<Uri> StartCrawl(Uri uri)
        {
            StartCrawlingAsync(uri);
            return _subject;
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

                jobsToRun.ForEach(i => crawlerTasks.Add(i.Execute()));

                if (crawlerTasks.Count > 0)
                {
                    Task.WaitAll(crawlerTasks.ToArray());
                }

                List<Uri> allResults = crawlerTasks.SelectMany(i => i.Result).ToList();

                // filter out external links from results.
                // filter out links already in cache.
                var filtered = Filter(allResults, new ExcludeRootUriFilter(uri), new ExternalUriFilter(uri), new AlreadyVisitedUriFilter());

                // push to observer.
                filtered.ForEach(_subject.OnNext);

                // put all unfiltered back to job list.
                _jobList.AddRange(filtered.ToArray());

                // repeat.
            }

            _subject.OnCompleted();
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
}
