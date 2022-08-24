using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Cache
{
    public abstract class CacheProvider
    {
        public static int CacheDuration = 60;
        public static CacheProvider Instance;
        public abstract void Set(string key, object value);
        public abstract object Get(string key);
        public abstract void Remove(string key);
        public abstract bool IsExist(string key);
    }
}
