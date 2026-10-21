namespace EduHub.Ch01.Domain;

public enum BloodType
{
    Unknown,
    A,
    B,
    AB,
    O
}

public enum EligibilityStatus
{
    NotChecked,
    Eligible,
    NotEligible
}

public enum DonationResult
{
    NotRecorded,
    Successful,
    Unsuccessful
}

public class CheckIn
{
    public int Id { get; private set; }
    public int RegistrationId { get; private set; }
    public DateTime ArrivalTime { get; private set; }
    public BloodType BloodType { get; private set; }
    public EligibilityStatus EligibilityStatus { get; private set; }
    public DonationResult DonationResult { get; private set; }

    private CheckIn(int registrationId, DateTime arrivalTime)
    {
        Id = registrationId;
        RegistrationId = registrationId;
        ArrivalTime = arrivalTime;
        BloodType = BloodType.Unknown;
        EligibilityStatus = EligibilityStatus.NotChecked;
        DonationResult = DonationResult.NotRecorded;
    }

    public static CheckIn Create(int registrationId, DateTime arrivalTime)
    {
        if (registrationId <= 0)
            throw new ArgumentException("Mã đăng ký không hợp lệ.");

        return new CheckIn(registrationId, arrivalTime);
    }

    public void RecordAttendance()
    {
        ArrivalTime = DateTime.Now;
    }

    public void RecordDonationResult(BloodType bloodType, EligibilityStatus eligibilityStatus, DonationResult donationResult)
    {
        if (bloodType == BloodType.Unknown)
            throw new ArgumentException("Cần ghi nhận nhóm máu.");

        if (eligibilityStatus == EligibilityStatus.NotChecked)
            throw new ArgumentException("Cần ghi nhận điều kiện hiến máu.");

        BloodType = bloodType;
        EligibilityStatus = eligibilityStatus;
        DonationResult = donationResult;
    }

    public string GetDonationInfo()
    {
        return $"Nhóm máu: {BloodType} | " +
               $"Điều kiện: {EligibilityStatus} | " +
               $"Kết quả: {DonationResult}";
    }
}