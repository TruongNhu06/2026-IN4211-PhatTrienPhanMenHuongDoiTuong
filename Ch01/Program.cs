using EduHub.Ch01.Domain;

try
{
    Club club = Club.Create(
        "Câu lạc bộ Hiến máu",
        "Hiến máu tình nguyện");

    Member member1 = Member.Create(
        "Nguyễn Văn A",
        "an@gmail.com",
        "0901234567",
        "SV001");

    Member member2 = Member.Create(
        "Trần Thị B",
        "binh@gmail.com",
        "0901234568",
        "SV002");

    club.AddMember(member1);
    club.AddMember(member2);

    Event donationEvent = club.CreateDonationEvent(
        "Ngày hội hiến máu",
        DateTime.Today.AddDays(1),
        "Đại học Đồng Tháp",
        50);

    Registration registration = Registration.Create(
        member1.Id,
        donationEvent.Id);

    donationEvent.AddRegistration(registration);

    registration.Confirm();
    registration.RecordAttendance(DateTime.Now);

    CheckIn checkIn = registration.CheckIn!;

    checkIn.RecordDonationResult(
        BloodType.O,
        EligibilityStatus.Eligible,
        DonationResult.Successful);

    Console.WriteLine("=== QUẢN LÝ CÂU LẠC BỘ HIẾN MÁU ===");
    Console.WriteLine($"Câu lạc bộ: {club.Name}");
    Console.WriteLine($"Số thành viên: {club.Members.Count}");
    Console.WriteLine($"Sự kiện: {donationEvent.Title}");
    Console.WriteLine($"Số lượt đăng ký: {donationEvent.CountRegistrations()}");

    Console.WriteLine("\n=== DANH SÁCH THÀNH VIÊN ===");

    foreach (Member member in club.Members)
    {
        Console.WriteLine(
            $"{member.StudentCode} - {member.FullName} - {member.CheckStatus()}");
    }

    Console.WriteLine("\n=== BÁO CÁO ĐIỂM DANH ===");
    Console.WriteLine($"Mã thành viên: {registration.MemberId}");
    Console.WriteLine($"Mã sự kiện: {registration.EventId}");
    Console.WriteLine($"Trạng thái đăng ký: {registration.Status}");
    Console.WriteLine($"Thời gian đến: {checkIn.ArrivalTime}");
    Console.WriteLine(checkIn.GetDonationInfo());

    Console.WriteLine(
        $"Tổng số đã điểm danh: {donationEvent.Registrations.Count(r => r.CheckIn != null)}");
}
catch (Exception ex)
{
    Console.WriteLine($"Lỗi: {ex.Message}");
}