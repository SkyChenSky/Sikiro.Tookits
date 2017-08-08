using System;
using System.Runtime.Caching;
using Framework.Common.Extension;
using Framework.Common.Interface;

namespace Framework.Common.FCache
{
    #region 本地缓存
    /// <summary>
    /// 本地缓存
    /// </summary>
    public class LocalCache : ICache
    {
        private static readonly ObjectCache Cache;

        static LocalCache()
        {
            Cache = MemoryCache.Default;
        }

        private static DateTimeOffset GetTimeOffset(int seconds)
        {
            return seconds <= 0 ? DateTimeOffset.MaxValue : DateTimeOffset.Now.AddSeconds(seconds);
        }

        public bool Add(string key, string value, int seconds = 0)
        {
            return !value.IsNullOrEmpty() && Cache.Add(key, value, GetTimeOffset(seconds));
        }

        public bool Add<T>(string key, T value, int seconds = 0) where T : class, new()
        {
            return Add(key, value.ToJson(), seconds);
        }

        public bool Set(string key, string value, int seconds = 0)
        {
            if (value.IsNullOrEmpty())
                return false;

            Cache.Set(key, value, GetTimeOffset(seconds));
            return true;
        }

        public bool Set<T>(string key, T value, int seconds = 0) where T : class, new()
        {
            if (value.IsNull())
                return false;

            Set(key, value.ToJson(), seconds);
            return true;
        }

        public string Get(string key)
        {
            return Get<string>(key);
        }

        public T Get<T>(string key) where T : class
        {
            return Cache.Get(key) as T;
        }

        public string GetOrAdd(string key, Func<string> aquire, int seconds = 0)
        {
            var data = Get(key);
            if (!data.IsNullOrEmpty())
                return data;

            data = aquire();
            if (data.IsNullOrEmpty())
                return null;

            Cache.Add(key, data, GetTimeOffset(seconds));
            return data;
        }

        public T GetOrAdd<T>(string key, Func<T> aquire, int seconds = 0) where T : class, new()
        {
            var data = Cache.Get(key) as T;
            if (data.IsNotNull())
                return data;

            data = aquire();
            if (data.IsNull())
                return null;

            Cache.Add(key, data, GetTimeOffset(seconds));
            return data;
        }

        public string GetOrSet(string key, Func<string> aquire, int seconds = 0)
        {
            var data = Get(key);
            if (!data.IsNullOrEmpty())
                return data;

            data = aquire();
            if (data.IsNullOrEmpty())
                return null;

            Set(key, data, seconds);
            return data;
        }

        public T GetOrSet<T>(string key, Func<T> aquire, int seconds = 0) where T : class, new()
        {
            var data = Get<T>(key);
            if (data.IsNotNull())
                return data;

            data = aquire();
            if (data.IsNull())
                return null;

            Set(key, data, seconds);
            return data;
        }

        public bool Contains(string key)
        {
            return Cache.Contains(key);
        }

        public bool Remove(string key)
        {
            return Cache.Remove(key) != null;
        }
    }
    #endregion
}
