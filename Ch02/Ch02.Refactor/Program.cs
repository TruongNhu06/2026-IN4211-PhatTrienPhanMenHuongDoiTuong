using EduHub.Ch02.Logging;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Patterns.Observers;
using EduHub.Ch02.Patterns.Strategies;
using EduHub.Ch02.Repositories;
using EduHub.Ch02.Services;

IAppLogger logger = new ConsoleLogger();
INotificationServiceFactory notificationFactory = new NotificationServiceFactory();
INotificationService notifier = notificationFactory.Create(NotificationChannel.Email);
IClubRepository clubRepo = new LoggingClubRepository(new InMemoryClubRepository(), logger);
IMemberRepository memberRepo = new InMemoryMemberRepository();
IEventRepository eventRepo = new InMemoryEventRepository();
IClubService clubService = new ClubService(clubRepo, memberRepo, notifier, logger);
IEventService eventService = new EventService(clubRepo, memberRepo, eventRepo);
IReportService reportService = new ReportService(clubRepo, memberRepo, eventRepo);

Console.WriteLine("== 1. Tạo CLB và thêm thành viên ==");
var club = clubService.CreateClub("Tech Club", "Câu lạc bộ công nghệ", "president@gmail.com");
var memberA = clubService.AddMember(club.Id, "Nguyễn Văn A", "a@student.dthu.edu.vn");
var memberB = clubService.AddMember(club.Id, "Trần Thị B", "b@student.dthu.edu.vn");
Console.WriteLine("\n== 2. Observer: tạo sự kiện ==");
eventService.Subscribe(new LogNotifier(logger));
eventService.Subscribe(new EmailNotifier(memberRepo, notifier));
var clubEvent = eventService.CreateEvent(club.Id, "Workshop SOLID", new DateTime(2026, 11, 20));

eventService.Register(clubEvent.Id, memberA.Id);
eventService.Register(clubEvent.Id, memberB.Id);
eventService.CheckIn(clubEvent.Id, memberA.Id);
Console.WriteLine("\n== 3. Strategy: báo cáo Console ==");
IReportStrategy consoleStrategy = new ConsoleReportStrategy();
Console.Write(reportService.Generate(consoleStrategy));
Console.WriteLine("== 3. Strategy: báo cáo CSV ==");
IReportStrategy csvStrategy = new CsvReportStrategy();
Console.Write(reportService.Generate(csvStrategy));
Console.WriteLine("\n== 4. Factory: thử các kênh thông báo ==");
foreach (NotificationChannel channel in Enum.GetValues<NotificationChannel>())
{
    INotificationService channelNotifier = notificationFactory.Create(channel);

    channelNotifier.Send("a@student.dthu.edu.vn", $"Thử kênh {channel}");
}
Console.WriteLine("\n== 5. Kiểm tra validate ==");
try
{
    clubService.CreateClub("", "x", "p@gmail.com");
}
catch (ArgumentException exception)
{
    Console.WriteLine($"Lỗi đúng dự kiến: {exception.Message}");
}