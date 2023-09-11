using Microsoft.AspNetCore.Mvc;
using RecruitmentApiClient;
using SharedModels;
using UserDataProcessor;

namespace HighfieldApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IRecruitmentApiClient _recruitmentApiClient;
    private readonly IUserDataProcessor _userDataProcessor;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IRecruitmentApiClient recruitmentApiService, IUserDataProcessor userDataProcessor, ILogger<UsersController> logger)
    {
        _recruitmentApiClient = recruitmentApiService;
        _userDataProcessor = userDataProcessor;
        _logger = logger;
    }

    [HttpGet("UserData")]
    public async Task<IActionResult> GetUserDataAsync()
    {
        try
        {
            // Fetch user data from the recruitment API
            var userData = await _recruitmentApiClient.GetUserDataAsync();

            if (userData == null || !userData.Any())
            {
                return NotFound("No user data found.");
            }

            // Calculate color frequency
            var colorFrequencyData = _userDataProcessor.GetColourFrequencyData(userData);

            // Calculate age plus twenty
            var agePlusTwentyData = _userDataProcessor.GetAgePlusTwentyData(userData);

            // Create a response model or DTO to hold the calculated data
            var response = new ResponseDTO
            {
                Users = userData,
                TopColours = colorFrequencyData,
                Ages = agePlusTwentyData   
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            // Handle exceptions
            _logger.LogError("An Exception occurred {Type}, Message: {message}", ex.GetType(), ex.Message);
            return StatusCode(500, "Internal server error: " + ex.Message);

        }
    }
}


