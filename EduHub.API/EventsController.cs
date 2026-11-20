using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers
{
    [ApiController]
    public class EventsController : ControllerBase
    {
        // 1. Lấy danh sách sự kiện của CLB
        [HttpGet("/api/clubs/{clubId}/events")]
        public IActionResult GetClubEvents(int clubId)
        {
            var sampleEvents = new[] { new { Id = 1, Title = "Sự kiện mẫu", ClubId = clubId } };
            return Ok(sampleEvents); // Trả về 200 OK
        }

        // 2. Xem một sự kiện theo Id (Dùng cái này để test cả 200 và 404)
        [HttpGet("/api/events/{id}")]
        public IActionResult GetEventById(int id)
        {
            // Giả sử chỉ có sự kiện ID = 1 tồn tại, nhập số khác (như 999) sẽ báo lỗi 404
            if (id != 1)
            {
                return NotFound(new { Message = "Không tìm thấy sự kiện" }); // Trả về 404
            }
            return Ok(new { Id = 1, Title = "Sự kiện mẫu" }); // Trả về 200 OK
        }

        // 3. Tạo sự kiện mới cho CLB
        [HttpPost("/api/clubs/{clubId}/events")]
        public IActionResult CreateEvent(int clubId, [FromBody] EventDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest("Tiêu đề không được để trống"); // Trả về 400
            }
            int newId = 1; // Đã khai báo biến newId để hết lỗi
            return Created($"/api/events/{newId}", request); // Trả về 201 Created
        }

        // 4. Xóa một sự kiện
        [HttpDelete("/api/events/{id}")]
        public IActionResult DeleteEvent(int id)
        {
            if (id != 1)
            {
                return NotFound(); // Trả về 404
            }
            return NoContent(); // Trả về 204 No Content
        }
    }

    public class EventDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}