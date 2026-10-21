using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Repositories;

namespace EduHub.Ch02.Services;

public class ClubService : IClubService
{
    private readonly IClubRepository _clubs;
    private readonly IMemberRepository _members;
    private readonly INotificationService _notifier;
    private readonly IAppLogger _logger;

    public ClubService(
        IClubRepository clubs, IMemberRepository members, INotificationService notifier, IAppLogger logger)
    {
        _clubs = clubs;
        _members = members;
        _notifier = notifier;
        _logger = logger;
    }

    public Club CreateClub(string name, string description, string presidentEmail)
    {
        var club = Club.Create(name, description);
        _clubs.Add(club);
        _logger.Info($"Đã tạo câu lạc bộ: {name}");
        _notifier.Send(presidentEmail, $"Câu lạc bộ '{name}' đã được tạo");
        return club;
    }

    public Member AddMember(Guid clubId, string memberName, string email)
    {
        var club = _clubs.GetById(clubId) ?? throw new InvalidOperationException("Không tìm thấy câu lạc bộ");

        if (!club.HasRoom(_members.GetByClub(clubId).Count))
        {
            throw new InvalidOperationException($"Câu lạc bộ đã đầy (max {Club.MaxMembers} thành viên)");
        }

        var member = new Member
        {
            Id = Guid.NewGuid(),
            Name = memberName,
            Email = email,
            ClubId = clubId
        };
        _members.Add(member);
        _logger.Info($"hành viên {memberName} đã tham gia câu lạc bộ {club.Name}");
        _notifier.Send(email, $"Chào mừng bạn đến với câu lạc bộ  {club.Name}!");

        return member;
    }
}