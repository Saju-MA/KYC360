using Moq;
using AutoMapper;
using KYC360.Services.Data;
using KYC360.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace KYC360.Services.UnitTest.ServicesTests.SecretUrlServiceTests;

public class SecretUrlServiceTestBase
{
    protected readonly AppDbContext _context;
    protected readonly Mock<IMapper> _mapper;
    protected readonly SecretUrlService _service;

    public SecretUrlServiceTestBase()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);
        _mapper = new Mock<IMapper>();
        _service = new SecretUrlService(_context, _mapper.Object);
    }
}
