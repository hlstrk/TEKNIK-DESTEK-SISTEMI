using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Cache
{
    public class DefaultCacheProvider : CacheProvider
    {
        ObjectCache _cache;
        CacheItemPolicy _policy;

        public DefaultCacheProvider()
        {

            Trace.WriteLine("Cache Başlatıldı!");
            _cache = MemoryCache.Default;

            _policy = new CacheItemPolicy
            {
                Priority = CacheItemPriority.NotRemovable,
                //AbsoluteExpiration = DateTime.Now.AddHours(1),
                //RemovedCallback = new CacheEntryRemovedCallback(RemovedCallback)
                RemovedCallback = RemovedCallback
            };
        }

        private void RemovedCallback(CacheEntryRemovedArguments arguments)
        {
            Trace.WriteLine("------Cache Yıkıldı!------");
            Trace.WriteLine("Key:" + arguments.CacheItem.Key);
            Trace.WriteLine("Value:" + arguments.CacheItem.Value);
            Trace.WriteLine("RemovedReason" + arguments.RemovedReason);
        }

        public override object Get(string key)
        {
            object item;
            try
            {
                item = _cache.Get(key);

                if (item == null)
                {
                    Trace.WriteLine("Cache Getirilemedi.");
                    throw new Exception("Cache Getirilemedi.");
                }
            }
            catch (Exception error)
            {
                item = null;
                Trace.WriteLine("Cache Get() Hata Oluştu." + error.Message);
                throw new Exception("Cache Get() Sırasında Hata Oluştu.", error);
            }

            return item;
        }

        public override bool IsExist(string key)
        {
            return _cache.Any(x => x.Key == key);
        }

        public override void Remove(string key)
        {
            if (IsExist(key))
                _cache.Remove(key);
        }

        public override void Set(string key, object value)
        {
            _cache.Set(key, value, _policy);
        }
    }
}