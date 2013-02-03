using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisterHex.WebCrawling
{
    internal class ExcludeRootUriFilter : IUriFilter
    {
        private Uri _root;
        public ExcludeRootUriFilter(Uri root)
        {
            _root = root;
        }

        public List<Uri> Filter(IEnumerable<Uri> input)
        {
            return input.Where(i => i != _root).ToList();
        }
    }
}
