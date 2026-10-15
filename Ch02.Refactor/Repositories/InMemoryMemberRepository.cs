using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Repositories;

public sealed class InMemoryMemberRepository : IMemberRepository
{
    private readonly List<Member> _store = new List<Member>();

    public IReadOnlyList<Member> GetByClub(Guid clubId)
    {
        return _store.Where(member => member.ClubId == clubId).ToList();
    }

    public Member? GetById(Guid id)
    {
        return _store.FirstOrDefault(member => member.Id == id);
    }

    public void Add(Member member)
    {
        _store.Add(member);
    }
}
