using Microsoft.Extensions.Logging;
using SharedModels;
using System.Text.Json;

namespace RecruitmentApiClient;

public class RecruitmentApiClient : IRecruitmentApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RecruitmentApiClient> _logger;

    public RecruitmentApiClient(HttpClient httpClient, ILogger<RecruitmentApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }  
    public async Task<List<User>> GetUserDataAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/test");

            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var userData = await JsonSerializer.DeserializeAsync<List<User>>(stream);
                    if(userData is not null)
                    return userData;
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
