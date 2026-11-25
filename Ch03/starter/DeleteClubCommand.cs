using EduHub.Domain.Interfaces;

namespace EduHub.Application.Clubs;

public record DeleteClubCommand(Guid Id);

public class DeleteClubCommandHandler
{
    private readonly IClubRepository _repository;

    public DeleteClubCommandHandler(IClubRepository repository)
    {
        _repository = repository;
    }

    /// <summary>Xóa club; repository throw KeyNotFoundException nếu không có.</summary>
    public async Task HandleAsync(DeleteClubCommand command, CancellationToken ct = default)
    {
        // TODO 5: Gọi repository để xóa theo Id; để KeyNotFoundException truyền lên controller.
        await _repository.DeleteAsync(command.Id, ct);
    }
}
