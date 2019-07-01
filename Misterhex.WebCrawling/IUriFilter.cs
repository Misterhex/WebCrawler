using System;
using System.Collections.Generic;

namespace MisterHex.WebCrawling
{
    public interface IUriFilter
    {
        List<Uri> Filter(IEnumerable<Uri> input);
    }
}
