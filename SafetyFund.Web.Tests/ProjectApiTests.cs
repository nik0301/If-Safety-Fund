using System.Net;
using Moq;
using SafetyFund.Business;
using SafetyFund.Data.Repositories;
using SafetyFund.Data.Tests;
using Xunit;

namespace SafetyFund.Web.Tests
{
    public class ProjectApiTests : AbstractBaseTests
    {
        [Fact]
        public void When_Getting_Location_Country_It_Returns_CountryName()
        {
            // Setup
            var ip = new IPAddress(new byte[]{ 208, 80, 152, 201});

            var repoMock = new Mock<LocationRepo>(null);
            repoMock.Setup(m => m.IsCountryAllowed(It.IsAny<string>()))
                .Returns(true);

            // Act
            var isCountryAllowed = new LocationService(repoMock.Object).IsCountryAllowed(ip);

            // Assert
            Assert.True(isCountryAllowed);
            repoMock.Verify(m => m.IsCountryAllowed(It.Is<string>(c => c == "United States")));
        }
    }
}
