namespace EduHub.Ch02.Logging;

public class FileLogger : IAppLogger
{
    private readonly string _path;

    public FileLogger(string path)
    {
        _path = path;
    }

    public void Info(string message)
    {
        string logEntry = $"[{DateTime.Now:s}] {message}{Environment.NewLine}";
        File.AppendAllText(_path, logEntry);
    }
}