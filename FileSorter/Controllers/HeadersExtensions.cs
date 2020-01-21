using Microsoft.AspNetCore.Http;
using System;

namespace FileSorter.Controllers
{
    public static class HeadersExtensions
    {
        public static bool TryGetIfModifiedSince(this IHeaderDictionary headers, out DateTimeOffset date)
        {
            if (headers.TryGetValue("If-Modified-Since", out var lastModifiedSince)
                && DateTimeOffset.TryParse(lastModifiedSince[0], out date))
            {
                return true;
            }
            date = default;
            return false;
        }
    }

}
