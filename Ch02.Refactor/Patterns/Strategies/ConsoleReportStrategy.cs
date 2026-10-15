using System.Text;

namespace EduHub.Ch02.Patterns.Strategies;

public class ConsoleReportStrategy : IReportStrategy
{
    public string Name
    {
        get
        {
            return "Console";
        }
    }

    public string Render(IReadOnlyList<ClubStat> stats)
    {
        StringBuilder builder = new StringBuilder();

        foreach (ClubStat stat in stats)
        {
            builder.AppendLine($"Club: {stat.ClubName} | Members: {stat.Members} | Events: {stat.Events} | " + $"Registered: {stat.Registered} | Checked-in: {stat.CheckedIn}");
        }

        return builder.ToString();
    }
}