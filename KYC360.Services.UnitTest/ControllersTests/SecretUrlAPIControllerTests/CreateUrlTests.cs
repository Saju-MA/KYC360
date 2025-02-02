using Moq;
using FluentAssertions;
using KYC360.Services.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace KYC360.Services.UnitTest.ControllersTests.SecretUrlAPIControllerTests;

public class CreateUrlTests : SecretUrlAPIControllerTestBase
{
    private readonly SecretUrlDto secretUrlDto = new() { UserName = "testuser" };

    [Fact]
    public async Task CreateUrl_WhenSuccessful_ShouldReturnCreated()
    {
        // Arrange
        var response = new ResponseDto { IsSuccess = true, Result = Guid.NewGuid() };
        _service.Setup(s => s.CreateUrlAsync(secretUrlDto)).ReturnsAsync(response);

        // Act
        var result = await _controller.CreateUrl(secretUrlDto);

        // Assert
        var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.ActionName.Should().Be(nameof(_controller.GetUrl));
        if (createdResult.RouteValues != null)
        {
            createdResult.RouteValues.Should().ContainKey("id");
            createdResult.RouteValues["id"].Should().Be(response.Result);
        }
        createdResult.Value.Should().Be(response);
    }

    [Fact]
    public async Task CreateUrlt_WhenModelStateIsInvalid_ShouldReturnBadReques()
    {
        // Arrange
        _controller.ModelState.AddModelError("UserName", "Required");

        // Act
        var result = await _controller.CreateUrl(secretUrlDto);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().BeAssignableTo<SerializableError>();
    }

    [Fact]
    public async Task CreateUrl_WhenCreationFails_ShouldReturnInternalServerError()
    {
        // Arrange
        var response = new ResponseDto { IsSuccess = false, Message = "An error occurred" };
        _service.Setup(s => s.CreateUrlAsync(secretUrlDto)).ReturnsAsync(response);

        // Act
        var result = await _controller.CreateUrl(secretUrlDto);

        // Assert
        var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
        objectResult.StatusCode.Should().Be(500);
        objectResult.Value.Should().Be(response);
    }
}
