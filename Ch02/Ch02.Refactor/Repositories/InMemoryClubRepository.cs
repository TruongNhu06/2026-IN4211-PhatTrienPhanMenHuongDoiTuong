using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Repositories;

public sealed class InMemoryClubRepository : IClubRepository
{
    private readonly List<Club> _store = new List<Club>();

    public IReadOnlyList<Club> GetAll()
    {
        return _store.AsReadOnly();
    }

    public Club? GetById(Guid id)
    {
        return _store.FirstOrDefault(club => club.Id == id);
    }

    public void Add(Club club)
    {
        _store.Add(club);
    }
}
