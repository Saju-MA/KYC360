using Moq;
using KYC360.Web.Services;
using KYC360.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KYC360.Web.UnitTest.ControllersTests.AdminPageControllerTests;

public class AdminPageControllerTestBase
{
    protected readonly Mock<ISecretUrlService> _service;
    protected readonly AdminPageController _controller;
    protected readonly Mock<HttpRequest> _httpRequest;

    public AdminPageControllerTestBase()
    {
        _service = new Mock<ISecretUrlService>();
        _httpRequest = new Mock<HttpRequest>();
        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(x => x.Request).Returns(_httpRequest.Object);

        _controller = new AdminPageController(_service.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            }
        };
    }
}
