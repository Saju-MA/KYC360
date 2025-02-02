using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using KYC360.Web.Models;
using KYC360.Web.ViewModels.SecretUrlViewModels;

namespace KYC360.Web.UnitTest.ControllersTests.SecretUrlControllerTests;

public class GetIndexTests : SecretUrlControllerTestBase
{
    private readonly string failText = "There are no secrets here.";

    [Fact]
    public async Task Index_ValidId_ReturnsViewWithValue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var text = "Test";
        var response = new ResponseDto { IsSuccess = true, Result = text };
        _service.Setup(s => s.GetUrlAsync(id)).ReturnsAsync(response);

        // Act
        var result = await _controller.Index(id);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<SupersecretViewModel>(viewResult.Model);
        model.Text.Should().Be(text);
    }

    [Fact]
    public async Task Index_InvalidId_ReturnsViewWithNullValue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var response = new ResponseDto { IsSuccess = false};
        _service.Setup(s => s.GetUrlAsync(id)).ReturnsAsync(response);

        // Act
        var result = await _controller.Index(id);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<SupersecretViewModel>(viewResult.Model);
        model.Text.Should().Be(failText);
    }

    [Fact]
    public async Task Index_ServiceThrowsException_ReturnsViewWithNullValue()
    {
        // Arrange
        var id = Guid.NewGuid();
        _service.Setup(s => s.GetUrlAsync(id)).ThrowsAsync(new Exception("Test Exception"));

        // Act
        var result = await _controller.Index(id);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<SupersecretViewModel>(viewResult.Model);
        model.Text.Should().Be(failText);
    }
}
