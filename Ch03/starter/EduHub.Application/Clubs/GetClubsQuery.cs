using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Application.Clubs;

public record GetClubsQuery;

public class GetClubsQueryHandler
{
    private readonly IClubRepository _repository;

    public GetClubsQueryHandler(IClubRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Club>> HandleAsync(GetClubsQuery query, CancellationToken ct = default)
    {
        // TODO 2: Gọi repository để lấy danh sách CLB và trả kết quả.
        return await _repository.GetAllAsync(ct);
    }
}
