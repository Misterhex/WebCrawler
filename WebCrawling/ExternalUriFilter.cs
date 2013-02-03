using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisterHex.WebCrawling
{
    internal class ExternalUriFilter : IUriFilter
    {
        private Uri _root;
        public ExternalUriFilter(Uri root)
        { _root = root; }

        public List<Uri> Filter(IEnumerable<Uri> input)
        {
            var result = input.Where(i => _root.Host == i.Host).ToList();
            return result;
        }
    }
}
