using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SharedModels;
using System.Text.Json;

namespace RecruitmentApiClient;

public class RecruitmentApiClient : IRecruitmentApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RecruitmentApiClient> _logger;
    private readonly IMemoryCache _cache;

    private const int CacheExpiryMinutes = 5;

    public RecruitmentApiClient(HttpClient httpClient, ILogger<RecruitmentApiClient> logger, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _logger = logger;
        _cache = cache;
    }  
    public async Task<List<User>> GetUserDataAsync()
    {

        if (_cache.TryGetValue("UserDataCacheKey", out List<User> cachedUserData))
        {
            return cachedUserData; // Return cached data if available
        }

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/test");

            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var userData = await JsonSerializer.DeserializeAsync<List<User>>(stream);
                    if (userData is not null)
                    {
                        _cache.Set("UserDataCacheKey", userData, TimeSpan.FromMinutes(CacheExpiryMinutes));
                        return userData;
                    }
                    return new List<User>();
                }
            }

            throw new HttpRequestException($"Failed to fetch user data from the API. Status code: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            _logger.LogError("An exception occured while retreiving data from {URL}, Exception: {Exception}", _httpClient.BaseAddress, ex.Message);
            throw;
        }
    }
}
