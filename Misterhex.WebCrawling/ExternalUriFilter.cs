using System;
using System.Collections.Generic;
using System.Linq;

namespace WebCrawling
{
    internal class ExternalUriFilter : IUriFilter
    {
        private Uri _root;
        public ExternalUriFilter(Uri root)
        {
            _root = root;
        }

        public List<Uri> Filter(IEnumerable<Uri> input)
        {
            return input.Where(i => getBaseDomain(_root).Equals(getBaseDomain(i))).ToList();
        }

        public string getBaseDomain(Uri uri)
        {
            var tokens = uri.Host.Split('.').Reverse().Take(2).Reverse();
            return String.Join(".", tokens);
        }
    }
}
