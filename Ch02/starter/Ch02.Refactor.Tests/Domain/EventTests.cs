using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests.Domain;

public class EventTests
{
    [Fact]
    public void Register_NewMember_ReturnsTrue()
    {
        var clubEvent = CreateEvent();
        var memberId = Guid.NewGuid();

        var registered = clubEvent.Register(memberId);

        Assert.True(registered);
        Assert.Contains(memberId, clubEvent.RegisteredMemberIds);
    }

    [Fact]
    public void Register_SameMemberTwice_ReturnsFalse()
    {
        var clubEvent = CreateEvent();
        var memberId = Guid.NewGuid();
        clubEvent.Register(memberId);

        var registeredAgain = clubEvent.Register(memberId);

        Assert.False(registeredAgain);
    }

    [Fact]
    public void CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        var clubEvent = CreateEvent();
        var memberId = Guid.NewGuid();

        var act = () => clubEvent.CheckIn(memberId);

        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void CheckIn_RegisteredMember_RecordsCheckIn()
    {
        var clubEvent = CreateEvent();
        var memberId = Guid.NewGuid();
        clubEvent.Register(memberId);

        clubEvent.CheckIn(memberId);

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
