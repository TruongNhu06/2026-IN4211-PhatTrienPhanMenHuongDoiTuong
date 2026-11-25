using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Infrastructure.Repositories;

/// <summary>
/// Lưu trữ tạm trong memory — dùng cho Ch03 khi chưa có database.
/// Ch04 sẽ thay bằng EfClubRepository dùng PostgreSQL.
/// </summary>
public class InMemoryClubRepository : IClubRepository
{
    // static: repository đăng ký Scoped (mới mỗi request) nhưng dữ liệu phải sống suốt vòng đời app.
    private static readonly List<Club> _clubs = new();
    private static readonly object _lock = new();

    // TODO 1a: Lấy toàn bộ CLB, trả về bản sao danh sách và bảo vệ truy cập bằng lock.
    public Task<IEnumerable<Club>> GetAllAsync(CancellationToken ct = default)
    {
        lock (_lock)
            // Trả về bản sao để bên ngoài không sửa trực tiếp danh sách gốc
            return Task.FromResult<IEnumerable<Club>>(_clubs.ToList());
    }

    // TODO 1b: Tìm CLB theo Id; không tìm thấy thì trả null.
    public Task<Club?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        lock (_lock)
            // Không tìm thấy thì trả null, KHÔNG throw
            return Task.FromResult(_clubs.FirstOrDefault(c => c.Id == id));
    }

    // TODO 1c: Thêm CLB vào danh sách trong lock.
    public Task AddAsync(Club club, CancellationToken ct = default)
    {
        lock (_lock) _clubs.Add(club);
        return Task.CompletedTask;
    }

    // TODO 1d: Cập nhật CLB theo Id; không tìm thấy thì ném KeyNotFoundException.
    public Task UpdateAsync(Club club, CancellationToken ct = default)
    {
        lock (_lock)
        {
            var index = _clubs.FindIndex(c => c.Id == club.Id);
            if (index < 0)
                throw new KeyNotFoundException($"Không tìm thấy club {club.Id}");
            _clubs[index] = club;
        }
        return Task.CompletedTask;
    }

    // TODO 1e: Xóa CLB theo Id; không tìm thấy thì ném KeyNotFoundException.
    public Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        lock (_lock)
        {
            var club = _clubs.FirstOrDefault(c => c.Id == id)
                ?? throw new KeyNotFoundException($"Không tìm thấy club {id}");
            _clubs.Remove(club);
        }
        return Task.CompletedTask;
    }
}
