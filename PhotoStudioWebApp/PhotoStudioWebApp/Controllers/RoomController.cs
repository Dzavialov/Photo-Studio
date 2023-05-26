using Microsoft.AspNetCore.Mvc;
using PhotoStudio.Domain;
using PhotoStudio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using PhotoStudio.Core;
using PhotoStudio.Application.DTOs;

namespace PhotoStudioWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        private readonly AuthorizationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<RoomDto> _validator;
        private readonly IConfiguration _configuration;


        public RoomController(AuthorizationDbContext dbContext, IMapper mapper, IValidator<RoomDto> validator, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = validator;
            _configuration = configuration;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [Route("create-room")]
        [HttpPost]
        public async Task<IActionResult> CreateRoomAsync([FromForm]RoomDto room, [FromForm]List<IFormFile> images)
        {
            var validationResult = _validator.Validate(room);
            if (!validationResult.IsValid)
            {
                return BadRequest();
            }

            var mappedRoom = _mapper.Map<RoomDto, Room>(room);

            await _dbContext.Rooms.AddAsync(mappedRoom);
            await _dbContext.SaveChangesAsync();
            foreach (var item in images)
            {
                if (item.FileName == null || item.FileName.Length == 0)
                {
                    return Content("Files not selected");
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);

                string filePath = Path.Combine(_configuration["FileStoragePath"], "Rooms", fileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    await item.CopyToAsync(fs);
                    fs.Close();
                }

                var roomImage = new RoomImageDto
                {
                    RoomId = mappedRoom.Id,
                    ImageName = fileName,
                    ImagePath = filePath
                };

                var mappedImages = _mapper.Map<RoomImageDto, RoomImage>(roomImage);
                await _dbContext.RoomImages.AddAsync(mappedImages);
                await _dbContext.SaveChangesAsync();
            }
            return Ok(mappedRoom);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetRoomByIdAsync(Guid id)
        {
            try
            {
                Room room = await _dbContext.Rooms.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);
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
            IList<Room> rooms = await _dbContext.Rooms.Include(x => x.Images).ToListAsync();

            if (rooms.Count == 0)
            {
                return NotFound();
            }
            
            return Ok(rooms);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [Route("edit-room/{id}")]
        [HttpPut]
        public async Task<IActionResult> EditRoomAsync([FromForm] RoomDto roomToUpdate, [FromForm] List<IFormFile> images)
        {
            var validationResult = _validator.Validate(roomToUpdate);
            if (!validationResult.IsValid)
            {
                return BadRequest();
            }

            var room = await _dbContext.Rooms.Include(r => r.Images).FirstOrDefaultAsync(r => r.Id == roomToUpdate.Id);

            if (room == null)
            {
                return NotFound();
            }

            var mappedRoom = _mapper.Map<RoomDto, Room>(roomToUpdate);
            room.Name = mappedRoom.Name;
            room.Description = mappedRoom.Description;
            room.AdditionalInformation= mappedRoom.AdditionalInformation;

            if (room.Images != null)
            {
                // Delete existing images
                foreach (var image in room.Images)
                {
                    _dbContext.RoomImages.Remove(image);
                    string filePath = Path.Combine(_configuration["FileStoragePath"], "Rooms", image.ImageName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
            }

            // Add new images
            foreach (var image in images)
            {
                if (image.FileName == null || image.FileName.Length == 0)
                {
                    return Content("Files not selected");
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                string filePath = Path.Combine(_configuration["FileStoragePath"], "Rooms", fileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fs);
                    fs.Close();
                }

                var roomImage = new RoomImageDto
                {
                    RoomId = room.Id,
                    ImageName = fileName,
                    ImagePath = filePath
                };

                var mappedImages = _mapper.Map<RoomImageDto, RoomImage>(roomImage);
                await _dbContext.RoomImages.AddAsync(mappedImages);
            }

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRoomAsync(Guid id)
        {
            var roomToDelete = await _dbContext.Rooms.Include(r => r.Images).FirstOrDefaultAsync(r => r.Id == id);

            if (roomToDelete == null)
            {
                return NotFound();
            }

            if (roomToDelete.Images != null)
            {
                // Delete existing images
                foreach (var image in roomToDelete.Images)
                {
                    _dbContext.RoomImages.Remove(image);
                    string filePath = Path.Combine(_configuration["FileStoragePath"], "Rooms", image.ImageName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
            }
            _dbContext.Rooms.Remove(roomToDelete);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
