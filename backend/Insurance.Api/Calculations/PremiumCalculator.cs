namespace Insurance.Api.Calculations;

public static class PremiumCalculator
{
    // Calculate the monthly premium from the annual premium.
    // If the customer is under 25, add a 20% surcharge.
    // If they have made no claims in the last 5 years, apply a 10% discount.
    // Apply the discount after the surcharge.
    // Divide by 12 and round to two decimal places.
    // Reject negative premiums and invalid ages or claim-free years.
    public static decimal CalculateMonthlyPremium(decimal annualPremium, int age, int claimFreeYears)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(annualPremium);
        ArgumentOutOfRangeException.ThrowIfNegative(age);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(age, 120);
        ArgumentOutOfRangeException.ThrowIfNegative(claimFreeYears);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(claimFreeYears, age);

        if (age < 25)
        {
            annualPremium *= 1.20m;
        }

        if (claimFreeYears >= 5)
        {
            annualPremium *= 0.90m;
        }

        return decimal.Round(annualPremium / 12m, 2, MidpointRounding.AwayFromZero);
    }
}