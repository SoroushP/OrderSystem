using System;
using System.Threading.Tasks;

namespace Domain.Service
{
  public interface ICacheService
  {
    Task<T> GetAsync<T>(string key);

    Task SetAsync<T>(
        string key,
        T value,
        TimeSpan expiration);

    Task RemoveAsync(string key);
  }
}
