using System.Linq;

namespace DirectoryCrawler.Model
{
    public class DirectoryPropertiesSet : PropertiesSet
    {
        public DirectoryPropertiesSet(DirectoryPropertiesSet baseMeta, params Meta[] metas) : base(baseMeta, metas.Select(o => o.Properties).ToArray())
        {
            this.Terminate = baseMeta.Terminate;
            this.Hidden = baseMeta.Hidden;
            foreach (var meta in metas)
            {
                this.Terminate = meta.Terminate ?? this.Terminate;
                this.Hidden = meta.Hidden ?? this.Hidden;
            }
        }
        public DirectoryPropertiesSet(Meta baseMeta, params Meta[] metas) : base(baseMeta.Properties, metas.Select(o => o.Properties).ToArray())
        {
            this.Terminate = baseMeta.Terminate ?? this.Terminate;
            foreach (var meta in metas)
            {
                this.Terminate = meta.Terminate ?? this.Terminate;
                this.Hidden = meta.Hidden ?? this.Hidden;
            }
        }

        public DirectoryPropertiesSet Merge(params Meta[] metas)
        {
            return new DirectoryPropertiesSet(this, metas);
        }
        public bool Terminate { get; }
        public bool Hidden { get; }
    }
}