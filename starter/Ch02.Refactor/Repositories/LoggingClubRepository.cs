using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

/// <summary>Repository decorator that logs calls before delegating to the wrapped repository.</summary>
public sealed class LoggingClubRepository(
    IClubRepository inner,
    IMemberRepository members,
    IAppLogger logger) : IClubRepository, IMemberRepository
{
    public void Add(Club club)
    {
        logger.Log($"Adding club {club.Id}");
        inner.Add(club);
    }

    public Club? GetById(Guid id)
    {
        logger.Log($"Getting club {id}");
        return inner.GetById(id);
    }

    public IReadOnlyList<Club> GetAll()
    {
        logger.Log("Getting all clubs");
        return inner.GetAll();
    }

    public int CountMembers(Guid clubId)
    {
        logger.Log($"Counting members for club {clubId}");
        return members.CountMembers(clubId);
    }

    public void AddMember(Member member)
    {
        logger.Log($"Adding member {member.Id} to club {member.ClubId}");
        members.AddMember(member);
    }

    public IReadOnlyList<Member> GetMembers(Guid clubId)
    {
        logger.Log($"Getting members for club {clubId}");
        return members.GetMembers(clubId);
    }
}
