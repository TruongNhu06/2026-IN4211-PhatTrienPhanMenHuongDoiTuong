using EduHub.Ch02.Domain;
using EduHub.Ch02.Patterns.Observers;

namespace EduHub.Ch02.Services;

public interface IClubService
{
    Club CreateClub(string name, string description, string presidentEmail);
    Member AddMember(Guid clubId, string memberName, string email);
}