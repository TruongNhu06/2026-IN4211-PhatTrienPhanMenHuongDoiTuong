using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public sealed class InMemoryClubRepository : IClubRepository, IMemberRepository
{
    private readonly Dictionary<Guid, Club> _clubs = new();
    private readonly List<Member> _members = new();

    public void Add(Club club) => _clubs.Add(club.Id, club);
    public Club? GetById(Guid id) => _clubs.GetValueOrDefault(id);
    public IReadOnlyList<Club> GetAll() => _clubs.Values.ToArray();
    public int CountMembers(Guid clubId) => _members.Count(member => member.ClubId == clubId);
    public void AddMember(Member member) => _members.Add(member);
    public IReadOnlyList<Member> GetMembers(Guid clubId) => _members.Where(member => member.ClubId == clubId).ToArray();
}
