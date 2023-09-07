using SharedModels;

namespace RecruitmentApiClient;

public interface IRecruitmentApiClient
{
    Task<IEnumerable<User>> GetUserDataAsync();
}
