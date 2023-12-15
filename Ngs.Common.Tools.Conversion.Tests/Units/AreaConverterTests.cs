using Ngs.Common.Tools.Conversion.Units;

namespace Ngs.Common.Tools.Conversion.Tests.Units;

public class AreaConverterTests
{
    [Fact]
    public void SquareMetersToSquareFeetTest()
    {
        const double squareMeters = 10;
        const double expected = 107.639;

        var squareFeet = AreaConverter.SquareMetersToSquareFeet(squareMeters);
        
        Assert.Equal(expected, squareFeet);
    }
    
    [Fact]
    public void SquareFeetToSquareMetersTest()
    {
        const double squareFeet = 107.639;
        const double expected = 10;

        var squareMeters = AreaConverter.SquareFeetToSquareMeters(squareFeet);
        
        Assert.Equal(expected, squareMeters);
    }
    
    [Fact]
    public void SquareKilometersToSquareMeters()
    {
        const double squareKilometres = 10;
        const double expected = 10_000_000;

        var squareMeters = AreaConverter.SquareKilometersToSquareMeters(squareKilometres);
        
        Assert.Equal(expected, squareMeters);
    }
    
    [Fact]
    public void SquareMetersToSquareKilometers()
    {
        const double expected = 10;
        const double squareMeters = 10_000_000;

        var squareKilometres = AreaConverter.SquareMetersToSquareKilometers(squareMeters);
        
        Assert.Equal(expected, squareKilometres);
    }
    
    [Fact]
    public void AcresToSquareMeters()
    {
        const double acres = 10;
        const double expected = 40_468.599999999999;

        var squareMeters = AreaConverter.AcresToSquareMeters(acres);
        
        Assert.Equal(expected, squareMeters);
    }
    
    [Fact]
    public void SquareMetersToAcres()
    {
        const double expected = 10;
        const double squareMeters = 40_468.599999999999;

        var acres = AreaConverter.SquareMetersToAcres(squareMeters);
        
        Assert.Equal(expected, acres);
    }
    
    [Fact]
    public void HectaresToAcres()
    {
        const double hectares = 10;
        const double expected = 24.7105;

        var acres = AreaConverter.HectaresToAcres(hectares);
        
        Assert.Equal(expected, acres);
    }
    
    [Fact]
    public void AcresToHectares()
    {
        const double acres = 24.7105;
        const double expected = 10;

        var hectares = AreaConverter.AcresToHectares(acres);
        
        Assert.Equal(expected, hectares);
    }
}