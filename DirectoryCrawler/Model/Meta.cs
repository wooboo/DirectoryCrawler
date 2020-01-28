using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DirectoryCrawler.Services;
using DotNet.Globbing;
using Newtonsoft.Json;

namespace DirectoryCrawler.Model
{
    public class Meta
    {
        public IDictionary<string, Meta> Directories { get; set; } = new Dictionary<string, Meta>();
        public IDictionary<string, Meta> Bases { get; set; } = new Dictionary<string, Meta>();
        public IDictionary<string, IDictionary<string, object>> Files { get; set; } = new Dictionary<string, IDictionary<string, object>>();
        public IDictionary<string, Meta> Virtuals { get; set; } = new Dictionary<string, Meta>();
        public bool? Terminate { get; set; }
        public bool? Hidden { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        public Meta Clone() => new Meta()
        {
            Terminate = this.Terminate,
            Hidden = this.Hidden,
            Virtuals = this.Virtuals.ToDictionary(o => o.Key, o => o.Value),
            Properties = this.Properties.ToDictionary(o => o.Key, o => o.Value),
            Directories = Directories.ToDictionary(o => o.Key, o => o.Value),
            Files = this.Files.ToDictionary(o => o.Key, o => o.Value)
        };
        public Meta? Merge(DirectoryEx directory)
        {
            Meta? merged = null;

            foreach (var (key, value) in this.Directories)
            {

                if (key.IsMatching(directory.Name))
                {
                    merged = merged ?? new Meta();
                    merged.Merge(value);
                }
                if (merged != null)
                {
                    var keyParts = key.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                    if ("**".Equals(key))
                    {
                        if (merged.Directories.TryGetValue(key, out var subDir))
                        {
                            subDir.Merge(value);
                        }
                        else
                        {
                            merged.Directories[key] = value.Clone();
                        }
                    }
                    else if (keyParts.Length > 1 && directory.Name.Equals(keyParts[0]))
                    {
                        var newKey = string.Join(Path.AltDirectorySeparatorChar, keyParts[1..]);
                        if (merged.Directories.TryGetValue(newKey, out var subDir))
                        {
                            subDir.Merge(value);
                        }
                        else
                        {
                            merged.Directories[newKey] = value.Clone();
                        }
                    }
                }
            }

            return merged;
        }
        public Meta Merge(Meta meta, DirectoryEx directory)
        {
            var merged = meta.Clone();

            foreach (var (key, value) in this.Directories)
            {
                var keyParts = key.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                if (key.IsMatching(directory.Name)
                      || keyParts[0].IsMatching(directory.Name)
                      || "**".Equals(key))
                {
                    merged.Merge(value);
                }

                if ("**".Equals(key))
                {
                    if (merged.Directories.TryGetValue(key, out var subDir))
                    {
                        subDir.Merge(value);
                    }
                    else
                    {
                        merged.Directories[key] = value.Clone();
                    }
                }
                else if (keyParts.Length > 1 && keyParts[0].IsMatching(directory.Name))
                {
                    var newKey = string.Join(Path.AltDirectorySeparatorChar, keyParts[1..]);
                    if (merged.Directories.TryGetValue(newKey, out var subDir))
                    {
                        subDir.Merge(value);
                    }
                    else
                    {
                        merged.Directories[newKey] = value.Clone();
                    }
                }
            }

            merged.Directories = merged.Directories.Merge(meta.Directories, (a, b) => a.Merge(b));

            return merged;
        }

        public void Merge(Meta value)
        {
            this.Properties = this.Properties.Merge(value.Properties);
            this.Terminate = value.Terminate ?? this.Terminate;
            this.Hidden = value.Hidden ?? this.Hidden;
            this.Virtuals = this.Virtuals.Merge(value.Virtuals, (a, b) => a.Merge(b));
            this.Files = this.Files.Merge(value.Files);
            this.Directories = this.Directories.Merge(value.Directories, (a, b) => a.Merge(b));
        }

        internal IDictionary<string, object> GetProperties()
        {
            return this.Properties;
        }

        internal IDictionary<string, object> GetFileProperties(string name)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            foreach (var (key, value) in this.Files)
            {
                if (key.IsMatching(name))
                {
                    result = result.Merge(value);
                }
            }
            return result;
        }
    }

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