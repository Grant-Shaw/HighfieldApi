using Microsoft.AspNetCore.Mvc;
using RecruitmentApiClient;

namespace HighfieldApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IRecruitmentApiClient _recruitmentApiClient;
    private readonly IUserDataProcessor _userDataProcessor;

    public UsersController(IRecruitmentApiClient recruitmentApiService, IUserDataProcessor userDataProcessor)
    {
        _recruitmentApiClient = recruitmentApiService;
        _userDataProcessor = userDataProcessor;
    }

    [HttpGet("UserData")]
    public async Task<IActionResult> GetRawUserDataAsync()
    {
        try
        {
            // Fetch user data from the recruitment API
            var userData = await _recruitmentApiClient.GetUserDataAsync();

            if (userData == null || userData.Count == 0)
            {
                return NotFound("No user data found.");
            }

            return Ok(userData);
        }
        catch (Exception ex)
        {
            // Handle exceptions and log errors
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpGet("ProcessedData")]
    public async Task<IActionResult> GetProcessedDataAsync()
    {
        try
        {
            // Fetch user data from the recruitment API
            var userData = await _recruitmentApiClient.GetUserDataAsync();

            if (userData == null || userData.Count == 0)
            {
                return NotFound("No user data found.");
            }

            // Calculate color frequency
            var colorFrequencyData = _userCalculationService.CalculateColorFrequency(userData);

            // Calculate age plus twenty
            var agePlusTwentyData = _userCalculationService.CalculateAgePlusTwenty(userData);

            // Create a response model or DTO to hold the calculated data
            var response = new
            {
                ColorFrequency = colorFrequencyData,
                AgePlusTwenty = agePlusTwentyData
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            // Handle exceptions and log errors
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}


