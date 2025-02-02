using Moq;
using KYC360.Services.Services;
using KYC360.Services.Controllers;

namespace KYC360.Services.UnitTest.ControllersTests.SecretUrlAPIControllerTests;

public class SecretUrlAPIControllerTestBase
{
    protected readonly Mock<ISecretUrlService> _service;
    protected readonly SecretUrlAPIController _controller;

    public SecretUrlAPIControllerTestBase()
    {
        _service = new Mock<ISecretUrlService>();
        _controller = new SecretUrlAPIController(_service.Object);
    }
}
