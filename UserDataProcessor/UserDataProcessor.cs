using SharedModels;

namespace UserDataProcessor;

public class UserDataProcessor : IUserDataProcessor
{
    public IEnumerable<UserAgePlusTwentyDTO> GetAgePlusTwentyData(List<User> userData)
    {
        List<UserAgePlusTwentyDTO> agePlusTwentyData = new();

        foreach (var user in userData)
        {
            if (!string.IsNullOrEmpty(user.Dob))
            {
                if (DateTime.TryParse(user.Dob, out DateTime dob))
                {
                    var age = CalculateAge(dob);
                    var agePlusTwenty = CalculateAgePlusTwenty(age);

                    agePlusTwentyData.Add(new UserAgePlusTwentyDTO
                    {
                        UserId = user.id,
                        OriginalAge = age,
                        AgePlusTwenty = agePlusTwenty
                    });
                }
                else
                {
                    throw new InvalidDataException($"The user {user.id} date of birth is not in the correct format");
                }
            }
            else
            {
                throw new InvalidDataException($"The user {user.id} does not have a date of birth.");
            }
        }

        return agePlusTwentyData;
    }
    public IEnumerable<UserColorFrequencyDTO> GetColourFrequencyData(List<User> userData)
    {
        if (userData == null || !userData.Any())
        {
            throw new ArgumentNullException(nameof(userData), "User data is null or empty.");
        }

        Dictionary<string, int> colorFrequencies = new();

        foreach (var user in userData)
        {
            if (!string.IsNullOrWhiteSpace(user.favouriteColour))
            {
                string color = user.favouriteColour.Trim().ToLower();

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