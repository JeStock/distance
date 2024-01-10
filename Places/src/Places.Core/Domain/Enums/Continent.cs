namespace Places.Core.Domain.Enums;

public enum Continent
{
    Africa = 1,
    Antarctica = 2,
    Asia = 3,
    Europe = 4,
    NorthAmerica = 5,
    Oceania = 6,
    SouthAmerica = 7
}

public static class ContentExtensions
{
    public static Continent? MapToDomain(this Contracts.Models.Continent? continent) =>
        continent switch
        {
            Contracts.Models.Continent.Africa => Continent.Africa,
            Contracts.Models.Continent.Antarctica => Continent.Antarctica,
            Contracts.Models.Continent.Asia => Continent.Asia,
            Contracts.Models.Continent.Europe => Continent.Europe,
            Contracts.Models.Continent.NorthAmerica => Continent.NorthAmerica,
            Contracts.Models.Continent.Oceania => Continent.Oceania,
            Contracts.Models.Continent.SouthAmerica => Continent.SouthAmerica,
            _ => (Continent?)default
        };

    public static Contracts.Models.Continent ToDto(this Continent type) =>
        type switch
        {
            Continent.Africa => Contracts.Models.Continent.Africa,
            Continent.Antarctica => Contracts.Models.Continent.Antarctica,
            Continent.Asia => Contracts.Models.Continent.Asia,
            Continent.Europe => Contracts.Models.Continent.Europe,
            Continent.NorthAmerica => Contracts.Models.Continent.NorthAmerica,
            Continent.Oceania => Contracts.Models.Continent.Oceania,
            Continent.SouthAmerica => Contracts.Models.Continent.SouthAmerica,
        };
}