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

        //[Authorize(Roles = UserRoles.Admin)]
        //[Route("add-images")]
        //[HttpPut]
        //public async Task<IActionResult> UploadRoomImagesAsync([FromForm]Guid roomId, List<IFormFile> images)
        //{
        //    var room = await _dbContext.Rooms.FindAsync(roomId);
        //    if(room == null)
        //    {
        //        return NotFound();
        //    }

        //    foreach(var item in images)
        //    {
        //        if(item.FileName == null || item.FileName.Length == 0)
        //        {
        //            return Content("Files not selected");
        //        }

        //        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);

        //        string filePath = Path.Combine(_configuration["FileStoragePath"], "Rooms", fileName);
        //        using(FileStream fs = new FileStream(filePath, FileMode.Create))
        //        {
        //            await item.CopyToAsync(fs);
        //            fs.Close();
        //        }

        //        var roomImage = new RoomImageDto
        //        {
        //            RoomId = room.Id,
        //            ImageName = fileName,
        //            ImagePath = filePath
        //        };

        //        var mappedImages = _mapper.Map<RoomImageDto, RoomImage>(roomImage);
        //        await _dbContext.RoomImages.AddAsync(mappedImages);
        //        await _dbContext.SaveChangesAsync();
        //    }
        //    return Ok();
        //}

        [Route("{id}")]
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

        [Authorize(Roles = UserRoles.Admin)]
        [Route("edit-room")]
        [HttpPut]
        public async Task<IActionResult> EditRoomAsync(RoomDto roomToUpdate)
        {
            var validationResult = _validator.Validate(roomToUpdate);
            if (!validationResult.IsValid)
            {
                return BadRequest();
            }
            var mappedRoom = _mapper.Map<RoomDto, Room>(roomToUpdate);
            _dbContext.Rooms.Update(mappedRoom);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = UserRoles.Admin)]
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
