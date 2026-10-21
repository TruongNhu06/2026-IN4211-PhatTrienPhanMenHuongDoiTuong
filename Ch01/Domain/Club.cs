namespace EduHub.Ch01.Domain;

public class Club
{
    private static int nextId = 1;

    private List<Member> members = new();
    private List<Event> events = new();

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<Member> Members => members;
    public IReadOnlyList<Event> Events => events;

    private Club(string name, string description)
    {
        Id = nextId++;
        Name = name;
        Description = description;
        CreatedAt = DateTime.Now;
    }

    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên câu lạc bộ không được để trống.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Mô tả không được để trống.");

        return new Club(name, description);
    }

    public void AddMember(Member member)
    {
        if (member == null)
            throw new ArgumentNullException(nameof(member));

        if (members.Any(m => m.Id == member.Id))
            throw new InvalidOperationException("Thành viên đã có trong câu lạc bộ.");

        members.Add(member);
    }

    public Event CreateDonationEvent(
        string title,
        DateTime date,
        string location,
        int capacity)
    {
        Event donationEvent = Event.Create(
            Id, title, date, location, capacity);

        events.Add(donationEvent);

        return donationEvent;
    }
}