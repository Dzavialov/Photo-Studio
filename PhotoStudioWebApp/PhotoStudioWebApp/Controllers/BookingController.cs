using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.Application.DTOs;
using PhotoStudio.Application.Validation;
using PhotoStudio.Domain;
using PhotoStudio.Domain.Entities;

namespace PhotoStudioWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly AuthorizationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<BookingDto> _validator;

        public BookingController(AuthorizationDbContext dbContext, IMapper mapper, IValidator<BookingDto> validator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = validator;
        }

        [Route("create-booking")]
        [HttpPost]
        public async Task<IActionResult> CreateBookingAsync(BookingDto booking)
        {
                var validationResult = _validator.Validate(booking);
                if (validationResult.IsValid)
                {
                    var mappedBooking = _mapper.Map<BookingDto, Booking>(booking);
                    await _dbContext.Bookings.AddAsync(mappedBooking);
                    await _dbContext.SaveChangesAsync();

                    return Created($"/get-booking/{mappedBooking.Id}", mappedBooking);
                }
            
                else return BadRequest();
        }

        [Route("get-booking/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetBookingByIdAsync(Guid id)
        {
            try
            {
                Booking booking = await _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(booking);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("get-bookings")]
        [HttpGet]
        public async Task<IActionResult> GetAllBookingsAsync()
        {
            IList<Booking> booking = await _dbContext.Bookings.ToListAsync();

            if (booking.Count == 0)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [Route("edit-booking")]
        [HttpPut]
        public async Task<IActionResult> EditBookingAsync(BookingDto bookingToUpdate)
        {
            var validationResult = _validator.Validate(bookingToUpdate);
            if (validationResult.IsValid)
            {
                var mappedBooking = _mapper.Map<BookingDto, Booking>(bookingToUpdate);
                _dbContext.Bookings.Update(mappedBooking);
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            else return BadRequest();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBookingAsync(Guid id)
        {
            var bookingToDelete = await _dbContext.Bookings.FindAsync(id);
            if (bookingToDelete == null)
            {
                return NotFound();
            }
            _dbContext.Bookings.Remove(bookingToDelete);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
