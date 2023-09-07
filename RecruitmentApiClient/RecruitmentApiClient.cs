using SharedModels;
using System.Text.Json;

namespace RecruitmentApiClient;

public class RecruitmentApiClient : IRecruitmentApiClient
{
    private readonly HttpClient _httpClient;

    public RecruitmentApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }  
    public async Task<IEnumerable<User>> GetUserDataAsync()
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

            throw new Exception("Failed to fetch user data from the API.");  
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
