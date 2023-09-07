namespace SharedModels;

public class ResponseDTO
{
    public User[]? Users { get; set; }
    public UserAgePlusTwentyDTO[]? Ages { get; set; }
    public UserColorFrequencyDTO[]? TopColours { get; set; }
}
