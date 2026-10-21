using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public sealed class ReportService(IClubRepository repository, IMemberRepository members, IAppLogger logger)
{
    public string Generate(IReportStrategy strategy)
    {
        ArgumentNullException.ThrowIfNull(strategy);
        var rows = repository.GetAll()
            .Select(club => new ClubReportRow(club.Name, members.CountMembers(club.Id)))
            .ToArray();
        var report = strategy.Generate(rows);
        logger.Log("Club report generated");
        return report;
    }
}

public sealed class ConsoleReportStrategy : IReportStrategy
{
    public string Generate(IReadOnlyList<ClubReportRow> rows) => string.Join(
        Environment.NewLine,
        rows.Select(row => $"Club: {row.ClubName} | Members: {row.MemberCount}"));
}

public sealed class CsvReportStrategy : IReportStrategy
{
    public string Generate(IReadOnlyList<ClubReportRow> rows)
    {
        var lines = new List<string> { "ClubName,MemberCount" };
        lines.AddRange(rows.Select(row => $"\"{row.ClubName.Replace("\"", "\"\"")}\",{row.MemberCount}"));
        return string.Join(Environment.NewLine, lines);
    }
}
