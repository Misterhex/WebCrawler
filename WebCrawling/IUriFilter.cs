using System;
using System.Collections.Generic;

namespace WebCrawling
{
    public interface IUriFilter
    {
        List<Uri> Filter(IEnumerable<Uri> input);
    }
}
