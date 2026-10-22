using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests.Domain;

public class ClubTests
{
    [Fact]
    public void Create_WithValidName_ReturnsClubWithGeneratedId()
    {
        const string name = "Tech Club";

        var club = Club.Create(name, "Technology community");

        Assert.NotEqual(Guid.Empty, club.Id);
        Assert.Equal(name, club.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidName_ThrowsArgumentException(string name)
    {
        var act = () => Club.Create(name, "Description");

        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Create_WithNameLongerThanMaximum_ThrowsArgumentException()
    {
        var name = new string('a', Club.MaxNameLength + 1);

        var act = () => Club.Create(name, "Description");

        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData(49, true)]
    [InlineData(50, false)]
    public void HasRoom_AtCapacityBoundary_ReturnsExpectedResult(int currentMemberCount, bool expected)
    {
        var club = Club.Create("Tech Club", "Technology community");

        var hasRoom = club.HasRoom(currentMemberCount);

        Assert.Equal(expected, hasRoom);
    }
}
