using KYC360.Web.ViewModels.AdminPageViewModels;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace KYC360.Web.UnitTest.ControllersTests.AdminPageControllerTests;

public class GetIndexTests : AdminPageControllerTestBase
{
    [Fact]
    public void GetIndex_WhenCalled_ReturnsViewWithEmptyModel()
    {
        // Arrange

        // Act
        var result = _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<AdminPageViewModel>(viewResult.Model);
        model.Should().NotBeNull();
    }
}
