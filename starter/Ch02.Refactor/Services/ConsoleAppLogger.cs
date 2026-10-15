namespace EduHub.Ch02;

public sealed class ConsoleAppLogger : IAppLogger
{
    public void Log(string message) => Console.WriteLine($"[{DateTimeOffset.Now:O}] {message}");
}
