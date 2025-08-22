using KayraExport.Application.Abstractions.Cache;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace KayraExport.Infrastructure.Services.RedisCache
{
    public class RedisCacheService : ICacheService
    {
        readonly ConnectionMultiplexer _connectionMultiplexer;
        readonly IDatabase _database;
        readonly RedisCacheSettings _redisCacheSettings;

        public RedisCacheService(IOptions<RedisCacheSettings> options)
        {
            _redisCacheSettings = options.Value;

            ConfigurationOptions opt = ConfigurationOptions.Parse(_redisCacheSettings.ConnectionString);
            _connectionMultiplexer = ConnectionMultiplexer.Connect(opt);
            _database = _connectionMultiplexer.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            RedisValue value = await _database.StringGetAsync(key);

            if (value.HasValue)
                return JsonConvert.DeserializeObject<T>(value!);

            return default;
        }


        public async Task SetAsync<T>(string key, T value, DateTime? expirationTime = null)
        {
            TimeSpan? timeUnitExpiration = null;

            if (expirationTime.HasValue)
            {
                timeUnitExpiration = expirationTime.Value - DateTime.Now;
            }

            await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), timeUnitExpiration);
        }



        public async Task RemoveAsync(string key)
            => await _database.KeyDeleteAsync(key);

         
        public async Task RemoveRangeAsync(params string[] keys)
        {
            RedisKey[] redisKeys = keys.Select(k => (RedisKey)k).ToArray();
            
            await _database.KeyDeleteAsync(redisKeys);
        }


    }
}
