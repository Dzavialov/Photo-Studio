using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.Domain;
using PhotoStudio.Domain.Entities;

namespace PhotoStudioWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentController : Controller
    {
        private readonly AuthorizationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<EquipmentItemDto> _validator;

        public EquipmentController(AuthorizationDbContext dbContext, IMapper mapper, IValidator<EquipmentItemDto> validator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = validator;
        }

        [Route("create-item")]
        [HttpPost]
        public async Task<IActionResult> CreateItemAsync(EquipmentItemDto item)
        {
            var validationResult = _validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return BadRequest();
            }
            var mappedItem = _mapper.Map<EquipmentItemDto, EquipmentItem>(item);
            await _dbContext.EquipmentItems.AddAsync(mappedItem);
            await _dbContext.SaveChangesAsync();
            return Created($"/get-item/{mappedItem.Id}", mappedItem);
        }

        [Route("get-item/{id}")]
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
