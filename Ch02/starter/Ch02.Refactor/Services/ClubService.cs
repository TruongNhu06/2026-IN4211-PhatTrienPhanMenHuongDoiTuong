using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public sealed class ClubService(
    IClubRepository repository,
    IMemberRepository members,
    IAppLogger logger,
    INotificationServiceFactory notificationFactory)
{
    public Club CreateClub(string name, string description, string presidentEmail)
    {
        var club = Club.Create(name, description);
        repository.Add(club);
        logger.Log($"Club created: {club.Name}");
        notificationFactory.Create(NotificationChannel.Email)
            .Notify(presidentEmail, $"Club '{club.Name}' was created.");
        return club;
    }

    public Member AddMemberToClub(Guid clubId, string memberName, string email)
    {
        var club = repository.GetById(clubId)
            ?? throw new InvalidOperationException("Club not found");

        if (!club.HasRoom(members.CountMembers(clubId)))
            throw new InvalidOperationException($"Club is full (max {Club.MaxMembers} members)");

        var member = new Member { Id = Guid.NewGuid(), Name = memberName, Email = email, ClubId = clubId };
        members.AddMember(member);
        logger.Log($"Member {member.Name} joined {club.Name}");
        notificationFactory.Create(NotificationChannel.Email).Notify(email, $"Welcome to {club.Name}!");
        notificationFactory.Create(NotificationChannel.Slack).Notify(club.Name, $"{member.Name} joined the club.");
        return member;
    }
}
