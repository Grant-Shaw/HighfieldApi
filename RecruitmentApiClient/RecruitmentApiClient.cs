using RecruitmentApiClient.Abstractions;

namespace RecruitmentApiClient;

public class RecruitmentApiClient : IRecruitmentApiClient
{
    public Task<List<UserData>> GetUserDataAsync()
    {
        throw new NotImplementedException();
    }
}