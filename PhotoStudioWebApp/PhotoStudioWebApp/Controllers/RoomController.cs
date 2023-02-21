using Microsoft.AspNetCore.Mvc;
using PhotoStudio.Domain;
using PhotoStudio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation;

namespace PhotoStudioWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        private readonly AuthorizationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<RoomDto> _validator;

        public RoomController(AuthorizationDbContext dbContext, IMapper mapper, IValidator<RoomDto> validator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = validator;
        }

        [Route("create-room")]
        [HttpPost]
        public async Task<IActionResult> CreateRoomAsync(RoomDto room)
        {
            var validationResult = _validator.Validate(room);
            if (validationResult.IsValid)
            {
                var mappedRoom = _mapper.Map<RoomDto, Room>(room);
                await _dbContext.Rooms.AddAsync(mappedRoom);
                await _dbContext.SaveChangesAsync();

                return Created($"/get-room/{mappedRoom.Id}", mappedRoom);
            }
            
            else return BadRequest();
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
        public async Task<IActionResult> EditRoomAsync(RoomDto roomToUpdate)
        {
            var validationResult = _validator.Validate(roomToUpdate);
            if (validationResult.IsValid)
            {
                var mappedRoom = _mapper.Map<RoomDto, Room>(roomToUpdate);
                _dbContext.Rooms.Update(mappedRoom);
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            else return BadRequest();
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
