using FluentAssertions;
using KYC360.Web.ViewModels.AdminPageViewModels;
using System.ComponentModel.DataAnnotations;

namespace KYC360.Web.UnitTest.ViewModelsTests;

public class AdminPageViewModelTests
{
    [Fact]
    public void UserName_Required_IsValid()
    {
        // Arrange
        var viewModel = new AdminPageViewModel { UserName = "testuser" };

        // Act
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), results, true);

        // Assert
        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [Fact]
    public void UserName_TooLong_IsInvalid()
    {
        // Arrange
        var viewModel = new AdminPageViewModel { UserName = new string('a', 51) };

        // Act
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), results, true);

        // Assert
        isValid.Should().BeFalse();
        results.Should().HaveCount(1);
        results[0].ErrorMessage.Should().Be("User Name cannot exceed 50 characters.");
    }

    [Fact]
    public void UserName_InvalidCharacters_IsInvalid()
    {
        // Arrange
        var viewModel = new AdminPageViewModel { UserName = "test user!" };

        // Act
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), results, true);

        // Assert
        isValid.Should().BeFalse();
        results.Should().HaveCount(1);
        results[0].ErrorMessage.Should().Be("User Name can only contain letters and numbers.");
    }

    [Fact]
    public void ClickTimes_Required_IsValid()
    {
        // Arrange
        var viewModel = new AdminPageViewModel { UserName = "testuser", ClickTimes = 1 };

        // Act
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), results, true);

        // Assert
        isValid.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [Fact]
    public void ClickTimes_Zero_IsInvalid()
    {
        // Arrange
        var viewModel = new AdminPageViewModel { UserName = "testuser", ClickTimes = 0 };

        // Act
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), results, true);

        // Assert
        isValid.Should().BeFalse();
        results.Should().HaveCount(1);
        results[0].ErrorMessage.Should().Be("Click Times must be greater than 0.");
    }

    [Fact]
    public void Minutes_Optional_IsValid()
    {
        // Arrange
        var viewModel1 = new AdminPageViewModel { UserName = "testuser", Minutes = 30 };
        var viewModel2 = new AdminPageViewModel { UserName = "testuser", Minutes = null };

        // Act
        var results1 = new List<ValidationResult>();
        var isValid1 = Validator.TryValidateObject(viewModel1, new ValidationContext(viewModel1), results1, true);

        var results2 = new List<ValidationResult>();
        var isValid2 = Validator.TryValidateObject(viewModel2, new ValidationContext(viewModel2), results2, true);

        // Assert
        isValid1.Should().BeTrue();
        results1.Should().BeEmpty();

        isValid2.Should().BeTrue();
        results2.Should().BeEmpty();
    }
}
