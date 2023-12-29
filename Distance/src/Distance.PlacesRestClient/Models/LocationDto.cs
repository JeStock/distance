namespace Distance.PlacesClient.Models;

public class LocationDto
{
    public double Lon { get; set; }
    public double Lat { get; set; }

    public override string ToString() => $"{{ Lon: {Lon}, Lat: {Lat} }}";
}