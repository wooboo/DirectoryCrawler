using System.Collections.Generic;

namespace DirectoryCrawler.Model
{
    public class FilePropertiesSet : PropertiesSet
    {
        public FilePropertiesSet(IDictionary<string, object> baseMeta, params IDictionary<string, object>[] metas) : base(baseMeta, metas)
        {
        }
    }
}