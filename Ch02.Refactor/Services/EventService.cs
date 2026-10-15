using EduHub.Ch02.Domain;
using EduHub.Ch02.Patterns.Observers;
using EduHub.Ch02.Repositories;

namespace EduHub.Ch02.Services;

public class EventService : IEventService
{
    private readonly IClubRepository _clubs;
    private readonly IMemberRepository _members;
    private readonly IEventRepository _events;
    private readonly List<IEventObserver> _observers = new List<IEventObserver>();

    public EventService(
        IClubRepository clubs,
        IMemberRepository members,
        IEventRepository events)
    {
        _clubs = clubs;
        _members = members;
        _events = events;
    }

    public void Subscribe(IEventObserver observer)
    {
        _observers.Add(observer);
    }

    public void Unsubscribe(IEventObserver observer)
    {
        _observers.Remove(observer);
    }

    public Event CreateEvent(Guid clubId, string title, DateTime date)
    {
        var club = _clubs.GetById(clubId)
            ?? throw new InvalidOperationException("Không tìm thấy câu lạc bộ");

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Tên sự kiện không được để trống", nameof(title));
        }

        var clubEvent = new Event
        {
            Id = Guid.NewGuid(),
            ClubId = clubId,
            Title = title,
            Date = date
        };

        _events.Add(clubEvent);

        foreach (var observer in _observers.ToList())
        {
            observer.OnEventCreated(clubEvent, club);
        }

        return clubEvent;
    }

    public void Register(Guid eventId, Guid memberId)
    {
        var clubEvent = GetEvent(eventId);
        var member = _members.GetById(memberId) ?? throw new InvalidOperationException("Không tìm thấy thành viên");

        if (member.ClubId != clubEvent.ClubId)
        {
            throw new InvalidOperationException("Thành viên không thuộc câu lạc bộ tổ chức sự kiện này");
        }

        clubEvent.Register(memberId);
    }

    public void CheckIn(Guid eventId, Guid memberId)
    {
        var clubEvent = GetEvent(eventId);
        clubEvent.CheckIn(memberId);
    }

    private Event GetEvent(Guid id)
    {
        return _events.GetById(id) ?? throw new InvalidOperationException("Không tìm thấy sự kiện");
    }
}