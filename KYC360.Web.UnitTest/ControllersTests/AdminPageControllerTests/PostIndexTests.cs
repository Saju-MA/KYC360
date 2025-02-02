using KYC360.Web.ViewModels.AdminPageViewModels;
using KYC360.Web.Models;
using KYC360.Web.Services;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace KYC360.Web.UnitTest.ControllersTests.AdminPageControllerTests;

public class PostIndexTests : AdminPageControllerTestBase
{
    [Fact]
    public async Task PostIndex_InvalidModelState_ReturnsViewWithModelErrors()
    {
        // Arrange
        var model = new AdminPageViewModel { UserName = "", ClickTimes = 0 };
        _controller.ModelState.AddModelError("UserName", "The UserName field is required.");

        // Act
        var result = await _controller.Index(model);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        viewResult.ViewName.Should().Be("Index");
        viewResult.Model.Should().Be(model);
        _controller.ModelState.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task PstIndex_SuccessfulCreation_ReturnsViewWithGeneratedUrl()
    {
        // Arrange
        var model = new AdminPageViewModel { UserName = "testuser"};
        var id = Guid.NewGuid();
        var response = new ResponseDto { Result = id, IsSuccess = true };
        _service.Setup(s => s.CreateUrlAsync(It.IsAny<SecretUrlDto>())).ReturnsAsync(response);
        _httpRequest.Setup(x => x.Scheme).Returns("https");
        _httpRequest.Setup(x => x.Host).Returns(new HostString("localhost"));

        // Act
        var result = await _controller.Index(model);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var returnedModel = Assert.IsType<AdminPageViewModel>(viewResult.Model);
        returnedModel.GeneratedUrl.Should().Be($"https://localhost/supersecret?id={id}");
        returnedModel.CanCopy.Should().BeTrue();
        returnedModel.ClickTimes.Should().Be(1);
        returnedModel.Minutes.Should().BeNull();
    }

    [Fact]
    public async Task PostIndex_FailedCreation_ReturnsViewWithMessage()
    {
        // Arrange
        var model = new AdminPageViewModel { UserName = "testuser"};
        var errorMessage = "Bad Request";
        var response = new ResponseDto { IsSuccess = false, Message = errorMessage };
        _service.Setup(s => s.CreateUrlAsync(It.IsAny<SecretUrlDto>())).ReturnsAsync(response);

        // Act
        var result = await _controller.Index(model);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var returnedModel = Assert.IsType<AdminPageViewModel>(viewResult.Model);
        returnedModel.GeneratedUrl.Should().Be(errorMessage);
        returnedModel.CanCopy.Should().BeFalse();
    }       

    [Fact]
    public async Task PstIndex_MinutesIsZero_ValidToIsNull()
    {
        // Arrange
        var model = new AdminPageViewModel { UserName = "testuser", Minutes = 0 };
        var id = Guid.NewGuid();
        var response = new ResponseDto { Result = id, IsSuccess = true };
        _service.Setup(x => x.CreateUrlAsync(It.Is<SecretUrlDto>(dto => dto.ValidTo == null)))
            .ReturnsAsync(response);
        _httpRequest.Setup(x => x.Scheme).Returns("https");
        _httpRequest.Setup(x => x.Host).Returns(new HostString("localhost"));

        // Act
        var result = await _controller.Index(model);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var returnedModel = Assert.IsType<AdminPageViewModel>(viewResult.Model);
        returnedModel.GeneratedUrl.Should().Be($"https://localhost/supersecret?id={id}");
        returnedModel.CanCopy.Should().BeTrue();
        returnedModel.Minutes.Should().BeNull();
    }

    [Fact]
    public async Task PostIndex_ExceptionThrown_ReturnsViewWithError()
    {
        // Arrange
        var model = new AdminPageViewModel { UserName = "testuser", ClickTimes = 10, Minutes = 30 };
        _service.Setup(x => x.CreateUrlAsync(It.IsAny<SecretUrlDto>()))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Index(model);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var returnedModel = Assert.IsType<AdminPageViewModel>(viewResult.Model);
        _controller.ModelState.ContainsKey("").Should().BeTrue();
        returnedModel.Should().Be(model);
    }
}
