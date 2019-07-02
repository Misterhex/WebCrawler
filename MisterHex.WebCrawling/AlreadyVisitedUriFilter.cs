using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MisterHex.WebCrawling
{
    internal class AlreadyVisitedUriFilter : IUriFilter
    {
        private ObjectCache _cache = MemoryCache.Default;

        public List<Uri> Filter(IEnumerable<Uri> input)
        {
            var result = input.Where(i => hasNotVisitedLink(i.ToString(), i))
                .ToList();
            return result;
        }

        private bool hasNotVisitedLink(string key, Uri value)
        {
            bool hasNotVisited = _cache.AddOrGetExisting(key, value,
                policy: new CacheItemPolicy() { Priority = CacheItemPriority.NotRemovable })
                == null;
            return hasNotVisited;
        }
    }
}
