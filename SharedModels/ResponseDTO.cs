namespace SharedModels;

public class ResponseDTO
{
    public IEnumerable<User>? Users { get; set; }
    public IEnumerable<UserAgePlusTwentyDTO>? Ages { get; set; }
    public IEnumerable<UserColorFrequencyDTO>? TopColours { get; set; }
}
