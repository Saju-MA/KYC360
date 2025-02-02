using KYC360.Web.ViewModels.SecretUrlViewModels;
using FluentAssertions;

namespace KYC360.Web.UnitTest.ViewModelsTests;

public class SupersecretViewModelTests
{
    [Fact]
    public void Constructor_WithResult_SetsTextToResult()
    {
        // Arrange
        string expectedText = "Test";

        // Act
        var viewModel = new SupersecretViewModel(expectedText);

        // Assert
        viewModel.Text.Should().Be(expectedText);
    }

    [Fact]
    public void Constructor_WithNullResult_SetsTextToDefaultMessage()
    {
        // Arrange
        string expectedText = "There are no secrets here.";

        // Act
        var viewModel = new SupersecretViewModel(null);

        // Assert
        viewModel.Text.Should().Be(expectedText);
    }


    [Fact]
    public void Constructor_WithEmptyResult_SetsTextToDefaultMessage()
    {
        // Arrange
        string expectedText = "There are no secrets here.";

        // Act
        var viewModel = new SupersecretViewModel(String.Empty);

        // Assert
        viewModel.Text.Should().Be(expectedText);
    }
}
