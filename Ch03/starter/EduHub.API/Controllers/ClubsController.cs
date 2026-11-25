using EduHub.Application.Clubs;
using EduHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers;

/// <summary>Quản lý câu lạc bộ. Controller chỉ nhận request, gọi handler của Application rồi trả status code.</summary>
[ApiController]
[Route("api/clubs")]
public class ClubsController : ControllerBase
{
    private readonly GetClubsQueryHandler _getClubsHandler;
    private readonly GetClubByIdQueryHandler _getClubByIdHandler;
    private readonly CreateClubCommandHandler _createClubHandler;
    private readonly DeleteClubCommandHandler _deleteClubHandler;

    public ClubsController(
        GetClubsQueryHandler getClubsHandler,
        GetClubByIdQueryHandler getClubByIdHandler,
        CreateClubCommandHandler createClubHandler,
        DeleteClubCommandHandler deleteClubHandler)
    {
        _getClubsHandler = getClubsHandler;
        _getClubByIdHandler = getClubByIdHandler;
        _createClubHandler = createClubHandler;
        _deleteClubHandler = deleteClubHandler;
    }

    /// <summary>Lấy danh sách câu lạc bộ.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Club>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        // TODO 8a: Gọi handler GetClubsQuery.
        var clubs = await _getClubsHandler.HandleAsync(new GetClubsQuery(), ct);
        // TODO 8b: Trả Ok(clubs) → 200.
        return Ok(clubs);
    }

    /// <summary>Lấy một câu lạc bộ theo Id.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Club), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        // TODO 8c: Gọi handler GetClubByIdQuery theo id.
        var club = await _getClubByIdHandler.HandleAsync(new GetClubByIdQuery(id), ct);
        // TODO 8d: Không có club → NotFound() (404); có → Ok(club) (200).
        return club is null ? NotFound() : Ok(club);
    }

    /// <summary>Tạo câu lạc bộ mới.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(Club), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateClubRequest request, CancellationToken ct)
    {
        // TODO 8e: Đổi request thành CreateClubCommand rồi gọi handler.
        try
        {
            var club = await _createClubHandler.HandleAsync(
                new CreateClubCommand(request.Name, request.Description), ct);
            // TODO 8f: Thành công → CreatedAtAction (201 + Location).
            return CreatedAtAction(nameof(GetById), new { id = club.Id }, club);
        }
        catch (ArgumentException ex)
        {
            // Dữ liệu đầu vào sai → 400 kèm lý do
            // TODO 8g: Bắt ArgumentException → BadRequest (400).
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Xoá một câu lạc bộ.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        // TODO 8h: Gọi handler xóa theo Id; thành công trả NoContent() (204).
        try
        {
            await _deleteClubHandler.HandleAsync(new DeleteClubCommand(id), ct);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            // TODO 8i: Bắt KeyNotFoundException → NotFound() (404).
            return NotFound();
        }
    }
}

/// <summary>Dữ liệu client gửi lên khi tạo CLB (DTO/mapping đầy đủ sẽ học ở Chương 4).</summary>
public record CreateClubRequest(string Name, string Description);
