namespace EduHub.Ch01.Domain;

public class Event
{
    private static int nextId = 1;

    private List<Registration> registrations = new();

    public int Id { get; private set; }
    public int ClubId { get; private set; }
    public string Title { get; private set; }
    public DateTime Date { get; private set; }
    public string Location { get; private set; }
    public int Capacity { get; private set; }

    public IReadOnlyList<Registration> Registrations => registrations;

    private Event(
        int clubId,
        string title,
        DateTime date,
        string location,
        int capacity)
    {
        Id = nextId++;
        ClubId = clubId;
        Title = title;
        Date = date;
        Location = location;
        Capacity = capacity;
    }

    public static Event Create(
        int clubId,
        string title,
        DateTime date,
        string location,
        int capacity)
    {
        if (clubId <= 0)
            throw new ArgumentException("Mã câu lạc bộ không hợp lệ.");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tên sự kiện không được để trống.");

        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Địa điểm không được để trống.");

        if (capacity <= 0)
            throw new ArgumentException("Sức chứa phải lớn hơn 0.");

        return new Event(clubId, title, date, location, capacity);
    }

    public bool CheckCapacity()
    {
        return registrations.Count < Capacity;
    }

    public int CountRegistrations()
    {
        return registrations.Count;
    }

    public void AddRegistration(Registration registration)
    {
        if (registration == null)
            throw new ArgumentNullException(nameof(registration));

        if (!CheckCapacity())
            throw new InvalidOperationException("Sự kiện đã đủ số lượng.");

        if (registrations.Any(r => r.Id == registration.Id))
            throw new InvalidOperationException("Đăng ký đã được thêm.");

        registrations.Add(registration);
    }
}