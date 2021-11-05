using System.Collections.Generic;

namespace DirectoryCrawler.Model
{
    public class PropertiesSet : Dictionary<string, object>
    {
        public PropertiesSet(PropertiesSet baseMeta, params IDictionary<string, object>[] metas) : this((IDictionary<string, object>)baseMeta, metas)
        {
        }
        public PropertiesSet(IDictionary<string, object> baseMeta, params IDictionary<string, object>[] metas)
        {
            foreach (var item in baseMeta)
            {
                this[item.Key] = item.Value;
            }
            foreach (var meta in metas)
            {
                foreach (var item in meta)
                {
                    this[item.Key] = item.Value;
                }
            }
        }
        public PropertiesSet Merge(params IDictionary<string, object>[] metas)
        {
            return new PropertiesSet(this, metas);
        }
    }
}