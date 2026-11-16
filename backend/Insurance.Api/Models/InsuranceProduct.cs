namespace Insurance.Api.Models;

public sealed record InsuranceProduct(
    string Id,
    string Title,
    string Category,
    string Description,
    string Details,
    string Icon,
    string Theme,
    string? Badge);
