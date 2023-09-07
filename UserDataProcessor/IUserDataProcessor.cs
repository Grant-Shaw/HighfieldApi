using SharedModels;

namespace UserDataProcessor;

public interface IUserDataProcessor
{
    IEnumerable<UserColorFrequencyDTO> GetColourFrequencyData(List<User> userData);
    IEnumerable<UserAgePlusTwentyDTO> GetAgePlusTwentyData(List<User> userData);
}
