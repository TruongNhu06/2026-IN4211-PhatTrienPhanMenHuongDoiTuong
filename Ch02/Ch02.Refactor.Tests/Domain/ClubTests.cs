using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests.Domain;

public class ClubTests
{
    [Fact]
    public void Create_WithValidName_ReturnsClubWithGeneratedId()
    {
        // Arrange
        const string name = "Tech Club";

        // Act
        var club = Club.Create(name, "Technology community");

        // Assert
        Assert.NotEqual(Guid.Empty, club.Id);
        Assert.Equal(name, club.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithBlankName_ThrowsArgumentException(string name)
    {
        // Arrange

        // Act
        var act = () => Club.Create(name, "Description");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Create_WithNameLongerThanMaximum_ThrowsArgumentException()
    {
        // Arrange
        var name = new string('a', Club.MaxNameLength + 1);

        // Act
        var act = () => Club.Create(name, "Description");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData(49, true)]
    [InlineData(50, false)]
    public void HasRoom_AtCapacityBoundary_ReturnsExpectedResult(int count, bool expected)
    {
        // Arrange
        var club = Club.Create("Tech Club", "Technology community");

        // Act
        var hasRoom = club.HasRoom(count);

        // Assert
        Assert.Equal(expected, hasRoom);
    }
}
