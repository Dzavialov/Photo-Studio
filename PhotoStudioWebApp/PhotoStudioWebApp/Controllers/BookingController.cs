using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using PhotoStudio.Application.DTOs;
using PhotoStudio.Core;
using PhotoStudio.Domain;
using PhotoStudio.Domain.Entities;
using System.Security.Claims;

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

        [Authorize(Roles = UserRoles.User)]
        [Route("create-booking")]
        [HttpPost]
        public async Task<IActionResult> CreateBookingAsync([FromForm]BookingDto booking)
        {
            IList<Booking> bookingList = await _dbContext.Bookings.ToListAsync();
            foreach(var x in bookingList)
            {
                var overlap = HasOverlap(x.BookFrom, x.BookTo, booking.BookFrom, booking.BookTo);
                if(overlap && x.RoomId == booking.RoomId)
                {
                    return BadRequest();
                }
            }
            
            var validationResult = _validator.Validate(booking);
            if (!validationResult.IsValid)
            {
                return BadRequest();
            }

            
            var mappedBooking = _mapper.Map<BookingDto, Booking>(booking);
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;    

            mappedBooking.UserId = Guid.Parse(currentUserId).ToString();
            await _dbContext.Bookings.AddAsync(mappedBooking);
            await _dbContext.SaveChangesAsync();

            return Ok(mappedBooking);
        }

        [Authorize]
        [Route("{id}")]
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

        [Authorize(Roles = UserRoles.User)]
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

        [Authorize]
        [Route("edit-booking")]
        [HttpPut]
        public async Task<IActionResult> EditBookingAsync(BookingDto bookingToUpdate)
        {
            var validationResult = _validator.Validate(bookingToUpdate);
            if (!validationResult.IsValid)
            {
                return BadRequest();
            }
            var mappedBooking = _mapper.Map<BookingDto, Booking>(bookingToUpdate);
            _dbContext.Bookings.Update(mappedBooking);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [Authorize]
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
        public static bool HasOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return start1 < end2 && end1 > start2;
        }
    }
}
