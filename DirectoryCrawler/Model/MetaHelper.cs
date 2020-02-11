using System;
using System.Collections.Generic;
using DotNet.Globbing;

namespace DirectoryCrawler.Model
{
    public static class MetaHelper
    {
        private static IDictionary<string, Glob> globs = new Dictionary<string, Glob>();
        public static bool IsMatching(this string pattern, string name)
        {
            if (!globs.TryGetValue(pattern, out var glob))
            {
                glob = Glob.Parse(pattern);
                globs[pattern] = glob;
            }
            return glob.IsMatch(name);
        }
        public static IDictionary<string, T> Merge<T>(this IDictionary<string, T> properties,
            IDictionary<string, T> next, Action<T, T>? merge = null)
        {
            var dictionary = new Dictionary<string, T>(properties);
            foreach (var (key, value) in next)
            {
                if (merge != null && dictionary.TryGetValue(key, out var existing) && existing != null && value != null)
                {
                    merge(existing, value);
                }
                else
                {
                    dictionary[key] = value;
                }
            }
            return dictionary;
        }
    }
}