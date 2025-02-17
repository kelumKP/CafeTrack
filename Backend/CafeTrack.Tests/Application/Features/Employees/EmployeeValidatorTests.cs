using FluentValidation.Results;
using CafeTrack.Application.DTOs;  // Update to use EmployeeDto
using CafeTrack.Application.Validators;

namespace CafeTrack.Tests.Application.Features.Employees
{
    public class EmployeeDtoValidatorTests
    {
        private readonly EmployeeDtoValidator _validator;  // Use the DTO validator

        public EmployeeDtoValidatorTests()
        {
            _validator = new EmployeeDtoValidator();  // Create an instance of the EmployeeDtoValidator
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var employeeDto = new EmployeeDto { EmailAddress = "invalid-email" };

            // Manual validation
            ValidationResult result = _validator.Validate(employeeDto);

            // Assert validation error for EmailAddress
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == "EmailAddress" && error.ErrorMessage == "Invalid email address format.");
        }

        [Fact]
        public void Should_Have_Error_When_PhoneNumber_Is_Invalid()
        {
            var employeeDto = new EmployeeDto { PhoneNumber = "12345" }; // Invalid phone number

            // Manual validation
            ValidationResult result = _validator.Validate(employeeDto);

            // Assert validation error for PhoneNumber
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == "PhoneNumber" && error.ErrorMessage == "Phone number must start with 8 or 9 and contain 8 digits.");
        }

        [Fact]
        public void Should_Have_Error_When_Gender_Is_Invalid()
        {
            var employeeDto = new EmployeeDto { Gender = "Other" }; // Invalid gender

            // Manual validation
            ValidationResult result = _validator.Validate(employeeDto);

            // Assert validation error for Gender
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == "Gender" && error.ErrorMessage == "Gender must be either 'Male' or 'Female'.");
        }
    }
}
