using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        IMemoryCache _memoryCache;
       HashSet<string> _keys = new();
        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key,value,TimeSpan.FromMinutes(duration));
            _keys.Add(key);
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public List<string> GetAllKeys()
        {
            return _keys.ToList();
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key,out _); //out _ Bişey döndürmemek için
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
            _keys.Remove(key);
        }

        //public void RemoveByPattern(string pattern)
        //{
        //    var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        //    Console.WriteLine(typeof(MemoryCache).GetProperties());
        //    var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
        //    List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

        //    foreach (var cacheItem in cacheEntriesCollection)
        //    {
        //        ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
        //        cacheCollectionValues.Add(cacheItemValue);
        //    }

        //    var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //    var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

        //    foreach (var key in keysToRemove)
        //    {
        //        _memoryCache.Remove(key);
        //    }

        //}
        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            var keysToRemove = _keys.Where(k => regex.IsMatch(k)).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
                _keys.Remove(key);
            }
        }

        
    }
}
