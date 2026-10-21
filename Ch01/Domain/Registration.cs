namespace EduHub.Ch01.Domain;

public enum RegistrationStatus
{
    Pending,
    Confirmed,
    Cancelled
}

public class Registration
{
    private static int nextId = 1;

    public int Id { get; private set; }
    public int MemberId { get; private set; }
    public int EventId { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public RegistrationStatus Status { get; private set; }
    public CheckIn? CheckIn { get; private set; }

    private Registration(int memberId, int eventId)
    {
        Id = nextId++;
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = DateTime.Now;
        Status = RegistrationStatus.Pending;
    }

    public static Registration Create(int memberId, int eventId)
    {
        if (memberId <= 0)
            throw new ArgumentException("Mã thành viên không hợp lệ.");

        if (eventId <= 0)
            throw new ArgumentException("Mã sự kiện không hợp lệ.");

        return new Registration(memberId, eventId);
    }

    public void Confirm()
    {
        if (Status == RegistrationStatus.Cancelled)
            throw new InvalidOperationException("Đăng ký đã hủy.");

        Status = RegistrationStatus.Confirmed;
    }

    public void Cancel()
    {
        if (CheckIn != null)
            throw new InvalidOperationException("Không thể hủy sau khi điểm danh.");

        Status = RegistrationStatus.Cancelled;
    }

    public void RecordAttendance(DateTime arrivalTime)
    {
        if (Status != RegistrationStatus.Confirmed)
            throw new InvalidOperationException(
                "Cần xác nhận đăng ký trước khi điểm danh.");

        if (CheckIn != null)
            throw new InvalidOperationException("Đã điểm danh.");

        CheckIn = global::EduHub.Ch01.Domain.CheckIn.Create(Id, arrivalTime);
    }
}
