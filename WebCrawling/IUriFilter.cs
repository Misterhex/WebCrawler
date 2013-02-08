using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace MisterHex.WebCrawling
{
    public interface IUriFilter
    {
        List<Uri> Filter(IEnumerable<Uri> input);
    }
}
