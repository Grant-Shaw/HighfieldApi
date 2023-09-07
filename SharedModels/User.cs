using System.Text.Json.Serialization;

namespace SharedModels;
public class User
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName ("lastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("dob")]
    public string? Dob { get; set; }

    [JsonPropertyName("favouriteColour")]
    public string? FavouriteColour { get; set; }

}
