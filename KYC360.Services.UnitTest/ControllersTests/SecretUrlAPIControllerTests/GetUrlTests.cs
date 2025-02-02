using Moq;
using FluentAssertions;
using KYC360.Services.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace KYC360.Services.UnitTest.ControllersTests.SecretUrlAPIControllerTests;

public class GetUrlTests : SecretUrlAPIControllerTestBase
{
    [Fact]
    public async Task GetUrl_WhenUrlExists_ShouldReturnOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        var response = new ResponseDto { IsSuccess = true, Result = "Secret URL Found" };
        _service.Setup(s => s.GetUrlAsync(id)).ReturnsAsync(response);

        // Act
        var result = await _controller.GetUrl(id);

        // Assert
        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().Be(response);
    }

    [Fact]
    public async Task GetUrl_WhenUrlDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var response = new ResponseDto { IsSuccess = false, Message = "Secret URL not found" };
        _service.Setup(s => s.GetUrlAsync(id)).ReturnsAsync(response);

        // Act
        var result = await _controller.GetUrl(id);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>()
            .Which.Value.Should().Be(response);
    }
}
