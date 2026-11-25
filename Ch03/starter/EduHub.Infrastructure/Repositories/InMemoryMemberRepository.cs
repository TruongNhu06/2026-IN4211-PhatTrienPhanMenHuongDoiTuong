using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Infrastructure.Repositories;

/// <summary>Repository in-memory cho Member (dùng khi chưa có database).</summary>
public class InMemoryMemberRepository : IMemberRepository
{
    // static: giữ dữ liệu qua các request dù repository đăng ký Scoped
    private static readonly List<Member> _members = new();
    private static readonly object _lock = new();

    public Task<IEnumerable<Member>> GetAllAsync(CancellationToken ct = default)
    {
        lock (_lock)
            return Task.FromResult<IEnumerable<Member>>(_members.ToList());
    }

    public Task<Member?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        lock (_lock)
            return Task.FromResult(_members.FirstOrDefault(m => m.Id == id));
    }

    public Task<Member?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        lock (_lock)
            return Task.FromResult(_members.FirstOrDefault(
                m => string.Equals(m.Email, email, StringComparison.OrdinalIgnoreCase)));
    }

    public Task AddAsync(Member member, CancellationToken ct = default)
    {
        lock (_lock)
            _members.Add(member);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Member member, CancellationToken ct = default)
    {
        lock (_lock)
        {
            var index = _members.FindIndex(m => m.Id == member.Id);
            if (index < 0)
                throw new KeyNotFoundException($"Không tìm thấy member {member.Id}");
            _members[index] = member;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        lock (_lock)
        {
            var member = _members.FirstOrDefault(m => m.Id == id)
                ?? throw new KeyNotFoundException($"Không tìm thấy member {id}");
            _members.Remove(member);
        }
        return Task.CompletedTask;
    }
}
