using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Repositories;

public sealed class InMemoryEventRepository : IEventRepository
{
    private readonly List<Event> _store = new List<Event>();

    public IReadOnlyList<Event> GetByClub(Guid clubId)
    {
        return _store.Where(clubEvent => clubEvent.ClubId == clubId).ToList();
    }

    public Event? GetById(Guid id)
    {
        return _store.FirstOrDefault(clubEvent => clubEvent.Id == id);
    }

    public void Add(Event ev)
    {
        _store.Add(ev);
    }
}
