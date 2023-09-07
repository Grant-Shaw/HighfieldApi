using RecruitmentApiClient.Abstractions;

namespace RecruitmentApiClient;

public interface IRecruitmentApiClient
{
    Task<List<UserData>> GetUserDataAsync();
}
