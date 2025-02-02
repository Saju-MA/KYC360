using Moq;
using KYC360.Web.Services;
using KYC360.Web.Controllers;

namespace KYC360.Web.UnitTest.ControllersTests.SecretUrlControllerTests;

public class SecretUrlControllerTestBase
{
    protected readonly Mock<ISecretUrlService> _service;
    protected readonly SecretUrlController _controller;

    public SecretUrlControllerTestBase()
    {
        _service = new Mock<ISecretUrlService>();
        _controller = new SecretUrlController(_service.Object);
    }
}
