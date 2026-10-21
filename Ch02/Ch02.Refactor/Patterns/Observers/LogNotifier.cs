using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;

namespace EduHub.Ch02.Patterns.Observers;

public class LogNotifier : IEventObserver
{
    private readonly IAppLogger _logger;
    public LogNotifier(IAppLogger logger)
    {
        _logger = logger;
    }
    public void OnEventCreated(Event ev, Club club)
    {
        string message = $"Event created: '{ev.Title}' ({club.Name})";
        _logger.Info(message);
    }
}