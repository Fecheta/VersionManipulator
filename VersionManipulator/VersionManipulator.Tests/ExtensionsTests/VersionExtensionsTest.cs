using VersionManipulator.Extensions;

namespace VersionManipulator.Tests.Extensions
{
    public class VersionExtensionsTests
    {
        [Fact]
        public void ParseVersion_ValidVersionString_ReturnsCorrectVersion()
        {
            // Arrange
            string versionString = "1.0.20.3";

            // Act
            var version = versionString.ParseVersion();

            // Assert
            Assert.Equal(1, version.Major);
            Assert.Equal(0, version.Minor);
            Assert.Equal(20, version.Build);
            Assert.Equal(3, version.Revision);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ParseVersion_NullOrEmptyString_ThrowsArgumentException(string versionString)
        {
            Assert.Throws<ArgumentException>(() => versionString.ParseVersion());
        }

        [Theory]
        [InlineData("1.0")]
        [InlineData("1.0.20")]
        [InlineData("1.0.20.3.5")]
        public void ParseVersion_InvalidFormat_ThrowsFormatException(string versionString)
        {
            Assert.Throws<FormatException>(() => versionString.ParseVersion());
        }

        [Theory]
        [InlineData("1.0.A.3")]
        public void ParseVersion_NonNumericOrOutOfRange_ThrowsException(string versionString)
        {
            Assert.Throws<FormatException>(() => versionString.ParseVersion());
        }

        [Fact]
        public void IncreaseBuild_IncrementsBuild()
        {
            // Arrange
            var version = new Entities.Version { Major = 1, Minor = 0, Build = 20, Revision = 3 };

            // Act
            version.IncreaseBuild();

            // Assert
            Assert.Equal(21, version.Build);
        }

        [Fact]
        public void IncreaseRevision_IncrementsRevision()
        {
            var version = new Entities.Version { Major = 1, Minor = 0, Build = 20, Revision = 3 };
            version.IncreaseRevision();
            Assert.Equal(4, version.Revision);
        }

        [Fact]
        public void GetString_ReturnsCorrectFormat()
        {
            var version = new Entities.Version { Major = 1, Minor = 2, Build = 3, Revision = 4 };
            string versionString = version.GetString();
            Assert.Equal("1.2.3.4", versionString);
        }
    }
}