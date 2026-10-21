# Câu lạc bộ Hiến máu — Sơ đồ lớp

## Sơ đồ lớp

![Sơ đồ lớp hệ thống Câu lạc bộ Hiến máu](class-diagram.png)

*Hình 1. Các lớp Domain và quan hệ trong hệ thống quản lý câu lạc bộ hiến máu.*

## Các lớp Domain

- `Club`: lưu thông tin câu lạc bộ, quản lý danh sách thành viên và tạo sự kiện hiến máu.
- `Member`: lưu hồ sơ sinh viên, trạng thái hoạt động và cho phép cập nhật thông tin có kiểm tra.
- `Event`: lưu thông tin sự kiện, kiểm tra sức chứa, nhận đăng ký và đếm số đăng ký.
- `Registration`: liên kết thành viên với sự kiện, quản lý trạng thái đăng ký và ghi nhận điểm danh.
- `CheckIn`: lưu thời gian đến, nhóm máu, tình trạng đủ điều kiện và kết quả hiến máu.

## Quan hệ chính

- Một `Club` quản lý nhiều `Member` và nhiều `Event`.
- Một `Event` nhận nhiều `Registration` trong giới hạn sức chứa.
- `Registration` lưu `MemberId` và `EventId` để liên kết thành viên với sự kiện.
- Một `Registration` có thể chưa có `CheckIn`; sau khi điểm danh, đăng ký có một thông tin `CheckIn`.

## Trạng thái được dùng

- `MemberStatus`: `Active`, `Inactive`.
- `RegistrationStatus`: `Pending`, `Confirmed`, `Cancelled`.
- `EligibilityStatus`: `NotChecked`, `Eligible`, `NotEligible`.
- `DonationResult`: `NotRecorded`, `Successful`, `Unsuccessful`.
- `BloodType`: `Unknown`, `A`, `B`, `AB`, `O`.
