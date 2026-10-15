using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public interface IClubRepository
{
    void Add(Club club);
    Club? GetById(Guid id);
    IReadOnlyList<Club> GetAll();
}

public interface IMemberRepository
{
    int CountMembers(Guid clubId);
    void AddMember(Member member);
    IReadOnlyList<Member> GetMembers(Guid clubId);
}

public interface IAppLogger
{
    void Log(string message);
}

public enum NotificationChannel
{
    Email,
    Slack
}

public interface INotificationService
{
    void Notify(string recipient, string message);
}

public interface INotificationServiceFactory
{
    INotificationService Create(NotificationChannel channel);
}

public interface IEventObserver
{
    void OnEventCreated(Event createdEvent);
}

public sealed record ClubReportRow(string ClubName, int MemberCount);

public interface IReportStrategy
{
    string Generate(IReadOnlyList<ClubReportRow> rows);
}
