using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using StackExchange.Redis;

namespace CRZ.Framework.Cache.Redis
{
    public class RedisCacheService : ICacheService
    {
        readonly ConnectionMultiplexer _redis;
        readonly IServer _server;
        readonly IDatabase _cache;

        public TimeSpan ExpirationTime { get; private set; }

        public RedisCacheService(ICacheConfiguration cacheConfiguration)
        {
            if (cacheConfiguration == null) throw new ArgumentNullException(nameof(cacheConfiguration));

            var options = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                AllowAdmin = true
            };

            options.EndPoints.Add(cacheConfiguration.Endpoint);

            _redis = ConnectionMultiplexer.Connect(options);
            _server = _redis.GetServer(cacheConfiguration.Endpoint);
            _cache = _redis.GetDatabase(cacheConfiguration.Database);

            ExpirationTime = cacheConfiguration.ExpirationTime;
        }

        public bool Exists(string keyName)
        {
            return _cache.KeyExists(keyName);
        }

        public T GetValue<T>(string keyName)
            where T : class
        {
            var cacheResult = _cache.StringGet(keyName);

            return JsonConvert.DeserializeObject<T>(cacheResult);
        }

        public void SetValue<T>(string keyName, T value, TimeSpan? timeToExpire = null)
            where T : class
        {
            if (timeToExpire.HasValue) ExpirationTime = timeToExpire.Value;

            var stringValue = JsonConvert.SerializeObject(value);

            _cache.StringSet(keyName, stringValue, ExpirationTime);
        }

        public void RemoveKey(string keyName)
        {
            _cache.KeyDelete(keyName);
        }

        public void Flush()
        {
            var database = _cache.Database;
            _server.FlushDatabase(database);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_redis != null)
                    _redis.Dispose();
            }
        }

        public static Task<T> GetOrSetAsync<T>(ICacheConfiguration cacheConfiguration, string key, Func<Task<T>> func, int retryCount = 1)
            where T : class
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    return Policy.Handle<RedisException>().Retry(retryCount).Execute(() =>
                    {
                        using var redis = new RedisCacheService(cacheConfiguration);
                        if (redis.Exists(key)) return redis.GetValue<T>(key);

                        var task = func();
                        task.Wait();
                        T result = task.Result;

                        redis.SetValue(key, result);

                        return result;
                    });
                }
                catch (RedisException)
                {
                    return null;
                }
            });
        }
    }
}
