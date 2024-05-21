using assessment_platform_developer.Common.Enums;
using assessment_platform_developer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Reflection;
using System.Web.UI.WebControls;

namespace assessment_platform_developer.Tests
{
	[TestClass]
	public class CustomersValidationTests
    {
        private Mock<ICustomerService> customerServiceMock;
        private Customers customersPage;

        [TestInitialize]
        public void Setup()
        {
            customerServiceMock = new Mock<ICustomerService>();
            customersPage = new Customers(customerServiceMock.Object);

            // Simulate initialization of controls
            var countryDropDownList = new DropDownList();
            countryDropDownList.Items.Add(new ListItem("Canada", ((int)Countries.Canada).ToString()));
            countryDropDownList.Items.Add(new ListItem("United States", ((int)Countries.UnitedStates).ToString()));

            // Using reflection to set private fields
            var countryField = typeof(Customers).GetField("CountryDropDownList", BindingFlags.NonPublic | BindingFlags.Instance);
            countryField.SetValue(customersPage, countryDropDownList);
        }


        [TestMethod]
        [DataRow("K1A 0B1", true, DisplayName = "Valid Canadian ZIP Code")]
        [DataRow("12345", false, DisplayName = "Invalid Canadian ZIP Code")]
        public void ZipCodeValidate_CanadianZipCodeValidation(string zipCode, bool expectedIsValid)
        {
            // Arrange
            var validator = new CustomValidator();
            var args = new ServerValidateEventArgs(zipCode, true);
            customersPage.TestCountryDropDownList.SelectedValue = ((int)Countries.Canada).ToString();

            // Act
            customersPage.ZipCodeValidate(validator, args);

            // Assert
            Assert.AreEqual(expectedIsValid, args.IsValid);
        }

        [TestMethod]
        [DataRow("12345", true, DisplayName = "Valid US ZIP Code without extension")]
        [DataRow("12345-6789", true, DisplayName = "Valid US ZIP Code with extension")]
        [DataRow("ABCDE", false, DisplayName = "Invalid US ZIP Code")]
        public void ZipCodeValidate_USZipCodeValidation(string zipCode, bool expectedIsValid)
        {
            // Arrange
            var validator = new CustomValidator();
            var args = new ServerValidateEventArgs(zipCode, true);

            customersPage.TestCountryDropDownList.SelectedValue = ((int)Countries.UnitedStates).ToString();

            // Act
            customersPage.ZipCodeValidate(validator, args);

            // Assert
            Assert.AreEqual(expectedIsValid, args.IsValid);
        }

        [TestMethod]
        [DataRow("abc@gmail.com", true, DisplayName = "Valid email address")]
        [DataRow("abc.com@", false, DisplayName = "Invalid email address")]
        public void EmailValidate_CustomerEmailValidation(string email, bool expectedIsValid)
        {
            // Arrange
            var validator = new CustomValidator();
            var args = new ServerValidateEventArgs(email, true);

            // Act
            customersPage.EmailValidate(validator, args);

            // Assert
            Assert.AreEqual(expectedIsValid, args.IsValid);
        }

        [TestMethod]
        [DataRow("123-456-7890", true, DisplayName = "Valid US phone number")]
        [DataRow("123.456.7890", true, DisplayName = "Valid US phone number with dots")]
        [DataRow("123 456 7890", true, DisplayName = "valid US phone number with spaces")]
        [DataRow("(123) 456-7890", true, DisplayName = "Valid US phone number with parentheses")]
        [DataRow("+1 (123) 456-7890", true, DisplayName = "Valid US phone number with country code")]
        [DataRow("1234567890", true, DisplayName = "valid US phone number without formatting")]
        [DataRow("123-45-67890", false, DisplayName = "Invalid US phone number with incorrect format")]
        public void IsValidPhoneNumber_Test(string phoneNumber, bool expectedIsValid)
        {
            // Arrange
            var validator = new CustomValidator();
            var args = new ServerValidateEventArgs(phoneNumber, true);

            // Act
            customersPage.PhoneNumberValidator(validator, args);

            // Assert
            Assert.AreEqual(expectedIsValid, args.IsValid);
        }
    }
}
