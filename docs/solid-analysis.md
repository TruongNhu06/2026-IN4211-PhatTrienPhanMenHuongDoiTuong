# SOLID analysis of the original `ClubManager`

The line references refer to the original `starter/Ch02.Refactor/ClubManager.cs` before the refactor.

| Original location | Principle | Violation and consequence |
| --- | --- | --- |
| `ClubManager` fields and methods, lines 28–116 | SRP | One class owns club and member storage, business rules, events, notification delivery, file logging, and report formatting. A change to any responsibility makes this class a change point and increases regression risk. |
| `CreateClub`, lines 35–48 | SRP and DIP | The use case validates input, creates and stores the club, and writes directly to a file. Storage and filesystem logging cannot be replaced independently or isolated in tests. |
| `CreateClub`, lines 50–63; `AddMemberToClub`, lines 81–83 | OCP | Notification behavior is hard-coded with domain-specific conditionals and console calls. Adding or changing a channel requires editing business methods. |
| `AddMemberToClub`, lines 67–83 | SRP | Membership lookup, capacity rules, persistence, logging, and two notification channels are combined. A change to notification or logging can disturb membership behavior. |
| `CreateEvent`, lines 88–98 | SRP and OCP | Event creation also loops through members and sends notifications directly. Each new reaction to an event requires changing the creation method. |
| `PrintClubReport`, lines 103–112 | SRP, OCP, and DIP | The method selects console output, calculates report data, and writes a file. Supporting CSV or a different logging implementation requires modifying this method. |
| `GetAllClubs`, line 116 | Encapsulation (design issue) | Returning the mutable backing list lets callers alter internal state without business rules. The refactored repository returns an `IReadOnlyList<Club>` snapshot. |

## Refactored responsibilities

- `ClubService` handles club and membership use cases through injected repository, logger, and notification interfaces.
- `IClubRepository` and `IMemberRepository` keep club and membership storage contracts focused. `LoggingClubRepository` decorates both contracts.
- `EventService` publishes created events to `IEventObserver` implementations.
- `ReportService` delegates formatting to `IReportStrategy` implementations for console and CSV output.
- `NotificationServiceFactory` creates notification services by `NotificationChannel`.
