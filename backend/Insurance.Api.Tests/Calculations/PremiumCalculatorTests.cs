using Insurance.Api.Calculations;
using Xunit;

namespace Insurance.Api.Tests.Calculations;

public sealed class PremiumCalculatorTests
{
    public static TheoryData<int, int, decimal> AdjustmentCases => new()
    {
        { 24, 4, 120m },
        { 24, 5, 108m },
        { 25, 4, 100m },
        { 25, 5, 90m },
        { 40, 0, 100m },
        { 40, 10, 90m },
    };

    public static TheoryData<decimal, decimal> RoundingCases => new()
    {
        { 12.059m, 1.00m },
        { 12.06m, 1.01m },
        { 12.061m, 1.01m },
        { 12.18m, 1.02m },
        { 12.30m, 1.03m },
        { 0.01m, 0.00m },
        { 0.06m, 0.01m },
        { 1234.56m, 102.88m },
        { 1200000000000000000000000m, 100000000000000000000000m },
    };

    public static TheoryData<decimal> NegativePremiumCases => new()
    {
        -0.0000000000000000000000000001m,
        -0.01m,
        -1200m,
        decimal.MinValue,
    };

    [Theory]
    [MemberData(nameof(AdjustmentCases))]
    public void CalculateMonthlyPremium_AdjustmentEligibility_ReturnsExpectedPremium(
        int age, int claimFreeYears, decimal expectedPremium)
    {
        // Arrange
        const decimal annualPremium = 1200m;

        // Act
        var result = PremiumCalculator.CalculateMonthlyPremium(annualPremium, age, claimFreeYears);

        // Assert
        Assert.Equal(expectedPremium, result);
    }

    [Theory]
    [InlineData(24, 0)]
    [InlineData(24, 5)]
    [InlineData(25, 0)]
    [InlineData(25, 5)]
    public void CalculateMonthlyPremium_ZeroAnnualPremium_ReturnsZero(int age, int claimFreeYears)
    {
        // Arrange
        const decimal annualPremium = 0m;

        // Act
        var result = PremiumCalculator.CalculateMonthlyPremium(annualPremium, age, claimFreeYears);

        // Assert
        Assert.Equal(0m, result);
    }

    [Theory]
    [InlineData(0, 0, 120)]
    [InlineData(1, 1, 120)]
    [InlineData(5, 5, 108)]
    [InlineData(25, 25, 90)]
    [InlineData(120, 0, 100)]
    [InlineData(120, 120, 90)]
    public void CalculateMonthlyPremium_ValidAgeAndClaimFreeBoundaries_ReturnsExpectedPremium(
        int age, int claimFreeYears, int expectedPremium)
    {
        // Arrange
        const decimal annualPremium = 1200m;

        // Act
        var result = PremiumCalculator.CalculateMonthlyPremium(annualPremium, age, claimFreeYears);

        // Assert
        Assert.Equal((decimal)expectedPremium, result);
    }

    [Theory]
    [MemberData(nameof(RoundingCases))]
    public void CalculateMonthlyPremium_FractionalMonthlyPremium_RoundsToTwoDecimalsAwayFromZero(
        decimal annualPremium, decimal expectedPremium)
    {
        // Arrange
        const int age = 25;
        const int claimFreeYears = 0;

        // Act
        var result = PremiumCalculator.CalculateMonthlyPremium(annualPremium, age, claimFreeYears);

        // Assert
        Assert.Equal(expectedPremium, result);
    }

    [Fact]
    public void CalculateMonthlyPremium_BothAdjustmentsAndFractionalPremium_RoundsOnlyFinalMonthlyAmount()
    {
        // Arrange
        const decimal annualPremium = 100.055m;
        const int age = 24;
        const int claimFreeYears = 5;

        // Act
        var result = PremiumCalculator.CalculateMonthlyPremium(annualPremium, age, claimFreeYears);

        // Assert
        Assert.Equal(9.00m, result);
    }

    [Theory]
    [MemberData(nameof(NegativePremiumCases))]
    public void CalculateMonthlyPremium_NegativeAnnualPremium_ThrowsForAnnualPremium(decimal annualPremium)
    {
        // Arrange
        const int age = 25;
        const int claimFreeYears = 0;

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            PremiumCalculator.CalculateMonthlyPremium(annualPremium, age, claimFreeYears));

        // Assert
        Assert.Equal("annualPremium", exception.ParamName);
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(121)]
    [InlineData(int.MaxValue)]
    public void CalculateMonthlyPremium_InvalidAge_ThrowsForAge(int age)
    {
        // Arrange
        const decimal annualPremium = 1200m;
        const int claimFreeYears = 0;

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            PremiumCalculator.CalculateMonthlyPremium(annualPremium, age, claimFreeYears));

        // Assert
        Assert.Equal("age", exception.ParamName);
    }

    [Theory]
    [InlineData(25, int.MinValue)]
    [InlineData(25, -1)]
    [InlineData(0, 1)]
    [InlineData(25, 26)]
    [InlineData(120, 121)]
    [InlineData(25, int.MaxValue)]
    public void CalculateMonthlyPremium_InvalidClaimFreeYears_ThrowsForClaimFreeYears(int age, int claimFreeYears)
    {
        // Arrange
        const decimal annualPremium = 1200m;

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            PremiumCalculator.CalculateMonthlyPremium(annualPremium, age, claimFreeYears));

        // Assert
        Assert.Equal("claimFreeYears", exception.ParamName);
    }
}