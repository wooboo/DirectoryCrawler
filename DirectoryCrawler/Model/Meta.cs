using System;
using System.Collections.Generic;
using System.IO;
using DirectoryCrawler.Services;
using Newtonsoft.Json;

namespace DirectoryCrawler.Model
{
    public class Meta
    {
        public IDictionary<string, Meta> Directories { get; set; } = new Dictionary<string, Meta>();
        public IDictionary<string, Meta> Bases { get; set; } = new Dictionary<string, Meta>();
        public IDictionary<string, IDictionary<string, object>> Files { get; set; } = new Dictionary<string, IDictionary<string, object>>();
        public IDictionary<string, string> Virtuals { get; set; } = new Dictionary<string, string>();
        public bool? Terminate { get; set; }
        public bool? Hidden { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        public Meta Merge(Meta meta, DirectoryEx directory)
        {
            var merged = new Meta()
            {
                Terminate = meta.Terminate ?? this.Terminate,
                Hidden = meta.Hidden ?? this.Hidden,
                Virtuals = meta.Virtuals,
                Properties = this.Properties.Merge(meta.Properties),
            };

            foreach (var (key, value) in this.Directories)
            {
                if (directory.Name.Equals(key)
                    || "*".Equals(key)
                    || "**".Equals(key))
                {
                    merged.Properties = merged.Properties.Merge(value.Properties);
                    merged.Terminate = value.Terminate ?? merged.Terminate;
                    merged.Hidden = value.Hidden ?? merged.Hidden;
                    merged.Virtuals = merged.Virtuals.Merge(value.Virtuals);
                }

                var keyParts = key.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                if ("**".Equals(key))
                {
                    merged.Directories[key] = value;
                }
                else if (keyParts.Length > 1 && directory.Name.Equals(keyParts[0]))
                {
                    var newKey = string.Join(Path.AltDirectorySeparatorChar, keyParts[1..]);
                    merged.Directories[newKey] = value;
                }
            }

            merged.Directories = merged.Directories.Merge(meta.Directories);

            return merged;
        }
    }

    public static class MetaHelper
    {
        public static IDictionary<string, T> Merge<T>(this IDictionary<string, T> properties,
            IDictionary<string, T> next)
        {
            var dictionary = new Dictionary<string, T>(properties);
            foreach (var (key, value) in next)
            {
                dictionary[key] = value;
            }
            return dictionary;
        }
    }
}