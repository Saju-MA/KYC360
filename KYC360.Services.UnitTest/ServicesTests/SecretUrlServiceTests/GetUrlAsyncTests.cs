using FluentAssertions;
using KYC360.Services.Models;
using KYC360.Services.Models.Dto;

namespace KYC360.Services.UnitTest.ServicesTests.SecretUrlServiceTests;

public class GetUrlAsyncTests : SecretUrlServiceTestBase
{
    [Fact]
    public async Task GetUrlAsync_WithValidId_ReturnsSuccess()
    {
        // Arrange
        var id = Guid.NewGuid();
        var secretUrl = new SecretUrl { Id = id, UserName = "testuser", ClickTimes = 1 };
        _context.SecretUrl.Add(secretUrl);
        await _context.SaveChangesAsync();

        var expectedResponse = new ResponseDto
        {
            Result = $"You have found the secret, {secretUrl.UserName}!",
            IsSuccess = true
        };

        // Act
        var response = await _service.GetUrlAsync(id);

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
        response.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task GetUrlAsync_WithValidId_ReturnsSuccessAndDecrementsClickTimes()
    {
        // Arrange
        var id = Guid.NewGuid();
        var secretUrl = new SecretUrl { Id = id, UserName = "testuser", ClickTimes = 1 };
        _context.SecretUrl.Add(secretUrl);
        await _context.SaveChangesAsync();

        var expectedResponse = new ResponseDto
        {
            Result = $"You have found the secret, {secretUrl.UserName}!",
            IsSuccess = true
        };

        // Act
        var response = await _service.GetUrlAsync(id);

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
        response.IsSuccess.Should().BeTrue();
        secretUrl.ClickTimes.Should().Be(0);
    }

    [Fact]
    public async Task GetUrlAsync_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var response = await _service.GetUrlAsync(id);

        // Assert
        response.Message.Should().Be("Secret URL not found.");
        response.IsSuccess.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(SecretUrlTestData))]
    public async Task GetUrlAsync_WithExpiredData_ReturnsExpired(SecretUrl secretUrl)
    {
        // Arrange
        _context.SecretUrl.Add(secretUrl);
        await _context.SaveChangesAsync();

        // Act
        var response = await _service.GetUrlAsync(secretUrl.Id);

        // Assert
        response.Message.Should().Be("Secret URL expired.");
        response.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task GetUrlAsync_ShouldHandleException_AndReturnErrorMessage()
    {
        // Arrange
        _context.Dispose();

        var id = Guid.NewGuid();

        // Act
        var result = await _service.GetUrlAsync(id);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("An error occurred while processing the request.");
    }

    public static IEnumerable<object[]> SecretUrlTestData()
    {
        yield return new object[] { new SecretUrl { Id = Guid.NewGuid(), UserName = "testuser", ClickTimes = 0, ValidTo = null } };
        yield return new object[] { new SecretUrl { Id = Guid.NewGuid(), UserName = "testuser", ClickTimes = 0, ValidTo = DateTime.UtcNow} };
        yield return new object[] { new SecretUrl { Id = Guid.NewGuid(), UserName = "testuser", ClickTimes = 0, ValidTo = DateTime.UtcNow.AddDays(1) } };
        yield return new object[] { new SecretUrl { Id = Guid.NewGuid(), UserName = "testuser", ClickTimes = 1, ValidTo = DateTime.UtcNow } };
        yield return new object[] { new SecretUrl { Id = Guid.NewGuid(), UserName = "testuser", ClickTimes = 1, ValidTo = DateTime.UtcNow.AddDays(-1) } };
    }
}
