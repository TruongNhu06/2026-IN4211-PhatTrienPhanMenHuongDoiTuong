using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests;

public class DomainTests
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
    public void Create_WithInvalidName_ThrowsArgumentException(string name)
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
    public void HasRoom_AtCapacityBoundary_ReturnsExpectedResult(int currentMemberCount, bool expected)
    {
        // Arrange
        var club = Club.Create("Tech Club", "Technology community");

        // Act
        var hasRoom = club.HasRoom(currentMemberCount);

        // Assert
        Assert.Equal(expected, hasRoom);
    }

    [Fact]
    public void Register_NewMember_ReturnsTrue()
    {
        // Arrange
        var clubEvent = CreateEvent();
        var memberId = Guid.NewGuid();

        // Act
        var registered = clubEvent.Register(memberId);

        // Assert
        Assert.True(registered);
        Assert.Contains(memberId, clubEvent.RegisteredMemberIds);
    }

    [Fact]
    public void Register_SameMemberTwice_ReturnsFalse()
    {
        // Arrange
        var clubEvent = CreateEvent();
        var memberId = Guid.NewGuid();
        clubEvent.Register(memberId);

        // Act
        var registeredAgain = clubEvent.Register(memberId);

        // Assert
        Assert.False(registeredAgain);
    }

    [Fact]
    public void CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        // Arrange
        var clubEvent = CreateEvent();
        var memberId = Guid.NewGuid();

        // Act
        var act = () => clubEvent.CheckIn(memberId);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void CheckIn_RegisteredMember_RecordsCheckIn()
    {
        // Arrange
        var clubEvent = CreateEvent();
        var memberId = Guid.NewGuid();
        clubEvent.Register(memberId);

        // Act
        clubEvent.CheckIn(memberId);

        // Assert
        Assert.Contains(memberId, clubEvent.CheckedInMemberIds);
    }

    private static Event CreateEvent() => new()
    {
        Id = Guid.NewGuid(),
        ClubId = Guid.NewGuid(),
        Title = "Workshop",
        Date = new DateTime(2026, 11, 20)
    };
}
