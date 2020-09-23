using DeviceAbriDoor.Model.Authorization;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using System;

namespace DeviceAbriDoor.Cachings
{
    public class LocalStorageCaching
    {
        private static LocalStorageCaching instance = new LocalStorageCaching();
        public static LocalStorageCaching Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LocalStorageCaching();
                }
                return instance;
            }
        }

        private ICacheManager cache = CacheFactory.GetCacheManager();

        private static readonly object lob_CheckSessionExist = new object();

        private const string KeyAdminSession = "KeyAdminSession";

        public AuthenticateResultModel AdminSession
        {
            get
            {
                if (cache.Contains(KeyAdminSession))
                {
                    return (AuthenticateResultModel)cache.GetData(KeyAdminSession);
                }
                return null;
            }
            set
            {
                lock (lob_CheckSessionExist)
                {
                    if (cache.Contains(KeyAdminSession))
                    {
                        cache.Remove(KeyAdminSession);
                    }
                    if (value != null)
                    {
                        cache.Add(KeyAdminSession, value);
                    }
                }
            }
        }

        #region Support Methods

       
        #endregion

        #region ILocalStorageService object caching methods

        public string Cache(object obj, TimeSpan duration)
        {
            string alias = GenerateId();
            Cache(alias, obj, duration);
            return alias;
        }

        private string GenerateId()
        {
            return Environment.TickCount.ToString();
        }

        public void Cache(string alias, object obj, TimeSpan duration)
        {
            SlidingTime objSlidingTime = new SlidingTime(duration);
            cache.Add(alias, obj, CacheItemPriority.Normal, null, objSlidingTime);
        }

        public void CachePermanently(string alias, object obj)
        {
            cache.Add(alias, obj);
        }

        public bool Contains(string alias)
        {
            return cache.Contains(alias);
        }

        public void Remove(string alias)
        {
            if (cache.Contains(alias))
            {
                cache.Remove(alias);
            }
        }

        public object Get(string alias)
        {
            if (cache.Contains(alias))
            {
                return cache.GetData(alias);
            }
            return null;
        }

        public void Cleanup()
        {
            cache.Flush();
        }

        #endregion
    }
}
