using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.Application.DTOs;
using PhotoStudio.Core;
using PhotoStudio.Domain;
using PhotoStudio.Domain.Entities;
using System.Data;

namespace PhotoStudioWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentController : Controller
    {
        private readonly AuthorizationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<EquipmentItemDto> _validator;
        private readonly IConfiguration _configuration;

        public EquipmentController(AuthorizationDbContext dbContext, IMapper mapper, IValidator<EquipmentItemDto> validator, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = validator;
            _configuration = configuration;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [Route("create-item")]
        [HttpPost]
        public async Task<IActionResult> CreateItemAsync([FromForm]EquipmentItemDto item, [FromForm] IFormFile image)
        {
            var validationResult = _validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return BadRequest();
            }
            

            if (image.FileName == null || image.FileName.Length == 0)
            {
                return Content("Files not selected");
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

            string filePath = Path.Combine(_configuration["FileStoragePath"], "EquipmentItems", fileName);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fs);
                fs.Close();
            }

            item.ImageName = fileName;
            item.ImagePath = filePath;

            var mappedItem = _mapper.Map<EquipmentItemDto, EquipmentItem>(item);
            await _dbContext.EquipmentItems.AddAsync(mappedItem);
            await _dbContext.SaveChangesAsync();

            return Ok(mappedItem);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetItemByIdAsync(Guid id)
        {
            try
            {
                EquipmentItem item = await _dbContext.EquipmentItems.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(item);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("get-items")]
        [HttpGet]
        public async Task<IActionResult> GetAllItemsAsync()
        {
            IList<EquipmentItem> items = await _dbContext.EquipmentItems.ToListAsync();

            if (items.Count == 0)
            {
                return NotFound();
            }

            return Ok(items);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [Route("edit-item")]
        [HttpPut]
        public async Task<IActionResult> EditItemAsync(EquipmentItemDto itemToUpdate)
        {
            var validationResult = _validator.Validate(itemToUpdate);
            if (!validationResult.IsValid)
            {
                return BadRequest();
            }
            var mappedItem = _mapper.Map<EquipmentItemDto, EquipmentItem>(itemToUpdate);
            _dbContext.EquipmentItems.Update(mappedItem);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteItemAsync(Guid id)
        {
            var itemToDelete = await _dbContext.EquipmentItems.FindAsync(id);
            if (itemToDelete == null)
            {
                return NotFound();
            }
            _dbContext.EquipmentItems.Remove(itemToDelete);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
