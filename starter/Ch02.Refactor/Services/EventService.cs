using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public sealed class EventService(
    IClubRepository clubs,
    IAppLogger logger,
    IEnumerable<IEventObserver> observers)
{
    public Event CreateEvent(Guid clubId, string title, DateTime date)
    {
        var club = clubs.GetById(clubId)
            ?? throw new InvalidOperationException("Club not found");

        var createdEvent = new Event { Id = Guid.NewGuid(), ClubId = clubId, Title = title, Date = date };
        logger.Log($"Event created: {createdEvent.Title} for {club.Name}");
        foreach (var observer in observers)
            observer.OnEventCreated(createdEvent);
        return createdEvent;
    }
}

public sealed class EmailNotifier(
    IClubRepository clubs,
    IMemberRepository members,
    INotificationService notificationService) : IEventObserver
{
    public void OnEventCreated(Event createdEvent)
    {
        var club = clubs.GetById(createdEvent.ClubId);
        if (club is null)
            return;

        foreach (var member in members.GetMembers(createdEvent.ClubId))
            notificationService.Notify(member.Email, $"New event '{createdEvent.Title}' at {club.Name} on {createdEvent.Date:g}.");
    }
}

public sealed class LogNotifier(IAppLogger logger) : IEventObserver
{
    public void OnEventCreated(Event createdEvent) =>
        logger.Log($"Observer received event-created notification: {createdEvent.Title}");
}
