namespace EduHub.Ch01.Domain;

public enum MemberStatus
{
    Active,
    Inactive
}

public class Member
{
    private static int nextId = 1;

    public int Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string StudentCode { get; private set; }
    public DateTime JoinedAt { get; private set; }
    public MemberStatus Status { get; private set; }

    private Member(string fullName, string email, string phone, string studentCode)
    {
        Id = nextId++;
        FullName = fullName;
        Email = email;
        Phone = phone;
        StudentCode = studentCode;
        JoinedAt = DateTime.Now;
        Status = MemberStatus.Active;
    }

    public static Member Create(string fullName, string email, string phone, string studentCode)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Họ tên không được để trống.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email không hợp lệ.");

        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Số điện thoại không được để trống.");

        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("Mã sinh viên không được để trống.");

        return new Member(fullName, email, phone, studentCode);
    }

    public string CheckStatus()
    {
        return Status.ToString();
    }

    public void UpdateInfo(string fullName, string email, string phone)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Họ tên không được để trống.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email không hợp lệ.");

        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Số điện thoại không được để trống.");

        FullName = fullName;
        Email = email;
        Phone = phone;
    }
}