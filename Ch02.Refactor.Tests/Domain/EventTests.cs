using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests.Domain;

public class EventTests
{
    [Fact]
    public void Register_NewMember_ReturnsTrueAndAddsMember()
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
