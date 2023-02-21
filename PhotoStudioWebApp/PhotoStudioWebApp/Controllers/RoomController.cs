using Microsoft.AspNetCore.Mvc;
using PhotoStudio.Domain;
using PhotoStudio.Domain.Entities;
using System.Diagnostics;
using System.Net;
using Microsoft.EntityFrameworkCore;


namespace PhotoStudioWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        private readonly AuthorizationDbContext _dbContext;

        public RoomController(AuthorizationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("create-room")]
        [HttpPost]
        public async Task<IActionResult> CreateRoomAsync(Room room)
        {
            try
            {
                await _dbContext.Rooms.AddAsync(room);
                await _dbContext.SaveChangesAsync();
                
                return Created($"/get-room/{room.Id}", room);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("get-room/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetRoomByIdAsync(Guid id)
        {
            try
            {
                Room room = await _dbContext.Rooms.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(room);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("get-rooms")]
        [HttpGet]
        public async Task<IActionResult> GetAllRoomsAsync()
        {
            IList<Room> rooms = await _dbContext.Rooms.ToListAsync();

            if (rooms.Count == 0)
            {
                return NotFound();
            }
            
            return Ok(rooms);
        }

        [Route("edit-room")]
        [HttpPut]
        public async Task<IActionResult> EditRoomAsync(Room roomToUpdate)
        {
            _dbContext.Rooms.Update(roomToUpdate);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRoomAsync(Guid id)
        {
            var roomToDelete = await _dbContext.Rooms.FindAsync(id);
            if (roomToDelete == null)
            {
                return NotFound();
            }
            _dbContext.Rooms.Remove(roomToDelete);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
