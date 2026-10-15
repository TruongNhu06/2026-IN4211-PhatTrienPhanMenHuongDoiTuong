using EduHub.Ch02.Patterns.Strategies;
using EduHub.Ch02.Repositories;

namespace EduHub.Ch02.Services;

public class ReportService : IReportService
{
    private readonly IClubRepository _clubs;
    private readonly IMemberRepository _members;
    private readonly IEventRepository _events;

    public ReportService(
        IClubRepository clubs,
        IMemberRepository members,
        IEventRepository events)
    {
        _clubs = clubs;
        _members = members;
        _events = events;
    }

    public string Generate(IReportStrategy strategy)
    {
        var stats = new List<ClubStat>();
        foreach (var club in _clubs.GetAll())
        {
            var members = _members.GetByClub(club.Id);
            var events = _events.GetByClub(club.Id);
            int registered = events.Sum(clubEvent => clubEvent.RegisteredMemberIds.Count);
            int checkedIn = events.Sum(clubEvent => clubEvent.CheckedInMemberIds.Count);
            stats.Add(new ClubStat(club.Name, members.Count, events.Count, registered, checkedIn));
        }
        return strategy.Render(stats);
    }
}