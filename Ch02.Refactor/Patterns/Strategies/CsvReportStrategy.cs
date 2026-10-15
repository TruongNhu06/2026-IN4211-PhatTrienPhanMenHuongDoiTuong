using System.Text;

namespace EduHub.Ch02.Patterns.Strategies;

public class CsvReportStrategy : IReportStrategy
{
    public string Name
    {
        get
        {
            return "CSV";
        }
    }

    public string Render(IReadOnlyList<ClubStat> stats)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Club,Members,Events,Registered,CheckedIn");

        foreach (ClubStat stat in stats)
        {
            string clubName = stat.ClubName.Replace("\"", "\"\"");
            builder.AppendLine($"\"{clubName}\",{stat.Members},{stat.Events},{stat.Registered},{stat.CheckedIn}");
        }

        return builder.ToString();
    }
}