using Domain.Service;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Service.Cache
{
  public class RedisCacheService : ICacheService
  {
    private readonly IDatabase database;

    public RedisCacheService(ConnectionMultiplexer connectionMultiplexer)
    {
      database = connectionMultiplexer.GetDatabase();
    }

    public async Task<T> GetAsync<T>(string key)
    {
      var value = await database.StringGetAsync(key);

      if (!value.HasValue)
        return default(T);

      return JsonConvert.DeserializeObject<T>(value);
    }

    public async Task SetAsync<T>(
        string key,
        T value,
        TimeSpan expiration)
    {
      var json = JsonConvert.SerializeObject(value);

      await database.StringSetAsync(
          key,
          json,
          expiration);
    }

    public async Task RemoveAsync(string key)
    {
      await database.KeyDeleteAsync(key);
    }
  }
}
