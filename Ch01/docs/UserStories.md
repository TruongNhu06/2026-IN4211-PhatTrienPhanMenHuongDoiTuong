# Câu lạc bộ Hiến máu — User Stories

## US-01 · Sinh viên đăng ký lịch hiến máu

- Khi sự kiện còn chỗ, sinh viên tạo đăng ký; hệ thống lưu đăng ký ở trạng thái `Pending`.
- Khi sự kiện đã đủ sức chứa, hệ thống từ chối thêm đăng ký.

## US-02 · Ban CLB quản lý thành viên

- Ban CLB xem trạng thái hoạt động của thành viên.
- Khi cần sửa thông tin, ban CLB cập nhật họ tên, email và số điện thoại; hệ thống kiểm tra dữ liệu trước khi lưu.

## US-03 · Ban CLB xác nhận hoặc hủy đăng ký

- Ban CLB xác nhận đăng ký đang chờ; hệ thống chuyển trạng thái sang `Confirmed`.
- Ban CLB hủy đăng ký; hệ thống chuyển trạng thái sang `Cancelled`.
- Không thể hủy đăng ký sau khi đã điểm danh.

## US-04 · Ban CLB ghi nhận điểm danh

- Khi đăng ký đã được xác nhận và sinh viên đến sự kiện, ban CLB ghi nhận điểm danh; hệ thống lưu thời gian đến.
- Không thể điểm danh nếu đăng ký chưa được xác nhận hoặc đã điểm danh trước đó.

## US-05 · Ban CLB ghi nhận kết quả hiến máu

- Ban CLB ghi nhận nhóm máu, tình trạng đủ điều kiện và kết quả hiến máu.
- Hệ thống yêu cầu có nhóm máu và tình trạng điều kiện đã được kiểm tra trước khi lưu kết quả.

## US-06 · Ban CLB quản lý sự kiện hiến máu

- Ban CLB tạo sự kiện với tên, ngày, địa điểm và sức chứa hợp lệ.
- Hệ thống không cho tạo sự kiện với dữ liệu bắt buộc bị thiếu hoặc sức chứa không hợp lệ.

## US-07 · Ban CLB xem thống kê đăng ký

- Ban CLB xem tổng số đăng ký của sự kiện.
- Nếu chưa có đăng ký, hệ thống hiển thị số lượng bằng 0.
