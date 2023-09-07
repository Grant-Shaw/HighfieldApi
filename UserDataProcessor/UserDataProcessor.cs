using Microsoft.Extensions.Logging;
using SharedModels;

namespace UserDataProcessor;

public class UserDataProcessor : IUserDataProcessor
{
    private readonly ILogger<UserDataProcessor> _logger;

    public UserDataProcessor(ILogger<UserDataProcessor> logger) 
    {
        _logger = logger;
    }

    public IEnumerable<UserAgePlusTwentyDTO> GetAgePlusTwentyData(List<User> userData)
    {
        List<UserAgePlusTwentyDTO> agePlusTwentyData = new();

        foreach (var user in userData)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Dob))
                {
                    if (DateTime.TryParse(user.Dob, out DateTime dob))
                    {
                        var age = CalculateAge(dob);
                        var agePlusTwenty = CalculateAgePlusTwenty(age);

                        agePlusTwentyData.Add(new UserAgePlusTwentyDTO
                        {
                            UserId = user.Id,
                            OriginalAge = age,
                            AgePlusTwenty = agePlusTwenty
                        });
                    }
                    else
                    {
                        throw new InvalidDataException("The users date of birth is not in the correct format.");
                    }
                }
                else
                {
                    throw new InvalidDataException("The Users date of birth is null or empty");
                }
            }
            catch (Exception ex)
            {
                // Log an error message for any other exceptions
                _logger.LogError($"An error occurred for user {user.Id}: {ex.Message}");
                throw;
            }
        }

        return agePlusTwentyData;
    }

    public IEnumerable<UserColorFrequencyDTO> GetColourFrequencyData(List<User> userData)
    {

        Dictionary<string, int> colorFrequencies = new();

        foreach (var user in userData)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(user.FavouriteColour))
                {
                    string color = user.FavouriteColour.Trim().ToLower();

                    if (colorFrequencies.ContainsKey(color))
                    {
                        colorFrequencies[color]++;
                    }
                    else
                    {
                        colorFrequencies[color] = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred for user {user.Id}: {ex.Message}");
                throw;
            }
        }

        List<UserColorFrequencyDTO> colorFrequencyData = colorFrequencies
            .Select(kv => new UserColorFrequencyDTO
            {
                Colour = kv.Key,
                Count = kv.Value
            })
            .OrderByDescending(c => c.Count)
            .ThenBy(c => c.Colour)
            .ToList();

        return colorFrequencyData;
    }

    private int CalculateAge(DateTime dateOfBirth)
    {
        DateTime currentDate = DateTime.Now;
        int age = currentDate.Year - dateOfBirth.Year;

        // Check if birthday has occurred this year
        if (currentDate.Month < dateOfBirth.Month || (currentDate.Month == dateOfBirth.Month && currentDate.Day < dateOfBirth.Day))
        {
            age--;
        }
        return age;
    }

    private int CalculateAgePlusTwenty(int age)
    {
        return age + 20;
    }
}