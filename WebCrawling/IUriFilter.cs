using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace MisterHex.WebCrawling
{
    internal interface IUriFilter
    {
        List<Uri> Filter(IEnumerable<Uri> input);
    }
}
