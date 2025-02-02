using Moq;
using AutoMapper;
using FluentAssertions;
using KYC360.Services.Models;
using KYC360.Services.Models.Dto;

namespace KYC360.Services.UnitTest.ServicesTests.SecretUrlServiceTests;

public class CreateUrlAsyncTests : SecretUrlServiceTestBase
{
    private readonly SecretUrlDto secretUrlDto = new() { UserName = "testuser" };

    [Fact]
    public async Task CreateUrlAsync_WhenValidDataProvided_ShouldReturnSuccess()
    {
        // Arrange
        var secretUrl = new SecretUrl { Id = Guid.NewGuid(), UserName = "testuser"};

        _mapper.Setup(m => m.Map<SecretUrl>(It.IsAny<SecretUrlDto>())).Returns(secretUrl);

        // Act
        var response = await _service.CreateUrlAsync(secretUrlDto);

        // Assert
        response.IsSuccess.Should().BeTrue();
        response.Result.Should().Be(secretUrl.Id);
    }

    [Fact]
    public async Task CreateUrlAsync_WhenMappingFails_ShouldReturnError()
    {
        // Arrange
        _mapper.Setup(m => m.Map<SecretUrl>(It.IsAny<SecretUrlDto>())).Throws(new AutoMapperMappingException());

        // Act
        var response = await _service.CreateUrlAsync(secretUrlDto);

        // Assert
        response.IsSuccess.Should().BeFalse();
        response.Message.Should().Be("Invalid data provided. Please check your input.");
    }

    [Fact]
    public async Task CreateUrlAsync_WhenAnyException_ShouldReturnError()
    {
        // Arrange
        _context.Dispose();

        // Act
        var response = await _service.CreateUrlAsync(secretUrlDto);

        // Assert
        response.IsSuccess.Should().BeFalse();
        response.Message.Should().Be("An error occurred. Please try again later.");
    }
}
