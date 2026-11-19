using Insurance.Api.Models;

namespace Insurance.Api.Data;

public static class InsuranceProductCatalog
{
    public static IReadOnlyList<InsuranceProduct> Products { get; } = Array.AsReadOnly(new[]
    {
        new InsuranceProduct(
            "bil", "Bilforsikring", "På vejen",
            "Kør afsted med ro i maven – uanset om turen er kort eller lang.",
            "Vælg en dækning, der passer til din bil og din hverdag. Få hjælp, hvis uheldet er ude, og kom godt videre på vejen.",
            "car", "coral", "Populær"),
        new InsuranceProduct(
            "hus", "Husforsikring", "Dit hjem",
            "Beskyt hjemmets rammer, så du kan føle dig hjemme – også når noget går galt.",
            "Dit hjem er fuld af minder og planer. En husforsikring kan hjælpe, hvis for eksempel storm eller vandskade rammer.",
            "home", "blue", null),
        new InsuranceProduct(
            "indbo", "Indboforsikring", "Dine ting",
            "Dækning til dine ejendele og en hjælpende hånd, hvis de bliver stjålet eller går i stykker.",
            "Fra sofaen i stuen til cyklen på farten: indboforsikringen hjælper med at passe på de ting, du holder af.",
            "shield", "green", null),
        new InsuranceProduct(
            "ulykke", "Ulykkesforsikring", "Dig og dine",
            "Ekstra tryghed, hvis en ulykke vender op og ned på hverdagen.",
            "En ulykkesforsikring kan give økonomisk støtte ved varige følger efter en ulykke – både i fritiden og i hverdagen.",
            "heart", "amber", null),
        new InsuranceProduct(
            "rejse", "Rejseforsikring", "Ud i verden",
            "Nyd turen, og få hjælp, hvis ferien ikke går helt efter planen.",
            "Uanset om du rejser langt væk eller tager på en forlænget weekend, kan du få hjælp, når du har brug for den.",
            "plane", "violet", null),
        new InsuranceProduct(
            "hund", "Hundeforsikring", "Hele familien",
            "Pas godt på din firbenede ven – fra skovtur til dyrlægebesøg.",
            "Hunden er en del af familien. Med en hundeforsikring kan du få hjælp til uforudsete dyrlægeregninger.",
            "dog", "slate", null),
    });
}