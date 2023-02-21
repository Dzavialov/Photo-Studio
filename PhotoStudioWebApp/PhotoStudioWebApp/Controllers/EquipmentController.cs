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

        public EquipmentController(AuthorizationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("create-item")]
        [HttpPost]
        public async Task<IActionResult> CreateItemAsync(EquipmentItem item)
        {
            try
            {
                await _dbContext.EquipmentItems.AddAsync(item);
                await _dbContext.SaveChangesAsync();

                return Created($"/get-item/{item.Id}", item);
            }
            catch
            {
                return BadRequest();
            }
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
        public async Task<IActionResult> EditItemAsync(EquipmentItem itemToUpdate)
        {
            _dbContext.EquipmentItems.Update(itemToUpdate);
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
