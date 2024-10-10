using System.Text.Json.Serialization;

namespace WebApplication1.Models;

public class Player : BaseEntity
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Shortname { get; set; }
    public string? Sex { get; set; }
    public Country? Country { get; set; }
    public string? Picture { get; set; }
    public Data? Data { get; set; }

    /// <summary>
    /// Returns the player's winning ratio for the last games.
    /// </summary>
    [JsonIgnore]
    public double? WinningRatio => Data?.Last?.Count > 0 ? ((double?)Data?.Last?.Count(w => w == 1) / Data?.Last.Count) : null;

    /// <summary>
    /// Returns the player's IMC. Formula : IMC = (weight / (size * size))
    /// How to calculate IMC : https://www.canada.ca/content/dam/ircc/migration/ircc/francais/ministere/partenariat/md/pdf/iemi_imc.pdf
    /// </summary>
    [JsonIgnore]
    public double? IMC => (double?)(Data?.Weight / 1000) / (((double?)Data?.Height / 100) * ((double?)Data?.Height / 100));

}
