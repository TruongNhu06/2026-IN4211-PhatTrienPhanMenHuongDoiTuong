using EduHub.Ch02;

var logger = new ConsoleAppLogger();
var inMemoryRepository = new InMemoryClubRepository();
var repository = new LoggingClubRepository(inMemoryRepository, inMemoryRepository, logger);
var notificationFactory = new NotificationServiceFactory();
var clubService = new ClubService(repository, repository, logger, notificationFactory);

var club = clubService.CreateClub("Tech Club", "Câu lạc bộ công nghệ", "president@example.com");
clubService.AddMemberToClub(club.Id, "Nguyễn Văn A", "a@student.example.edu");

var observers = new IEventObserver[]
{
    new EmailNotifier(repository, repository, notificationFactory.Create(NotificationChannel.Email)),
    new LogNotifier(logger)
};
var eventService = new EventService(repository, logger, observers);
eventService.CreateEvent(club.Id, "Workshop SOLID", new DateTime(2026, 11, 20));

var reportService = new ReportService(repository, repository, logger);
Console.WriteLine(reportService.Generate(new ConsoleReportStrategy()));
Console.WriteLine("\nCSV report:");
Console.WriteLine(reportService.Generate(new CsvReportStrategy()));
