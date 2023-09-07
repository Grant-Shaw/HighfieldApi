using SharedModels;

namespace RecruitmentApiClient;

public interface IRecruitmentApiClient
{
    Task <List<User>> GetUserDataAsync();
}
