using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_API.Models;

namespace Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly PersonalContext _context;

        public RegistrationsController(PersonalContext context)
        {
            _context = context;
        }

        // GET: api/Registrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
        {
          if (_context.Registrations == null)
          {
              return NotFound();
          }
            return await _context.Registrations.ToListAsync();
        }

        // GET: api/Registrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Registration>> GetRegistration(int id)
        {
          if (_context.Registrations == null)
          {
              return NotFound();
          }
            var registration = await _context.Registrations.FindAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            return registration;
        }

        // PUT: api/Registrations/5
        // To protect from overposting attacks,
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistration(int id, Registration registration)
        {
            if (id != registration.Id)
            {
                return BadRequest();
            }

            _context.Entry(registration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }




        // POST: api/Registrations
        [HttpPost]
        public async Task<ActionResult<Registration>> PostRegistration(Registration registration)
        {
            if (registration == null)
            {
                return BadRequest();
            }

            // Validate first name
            if (string.IsNullOrEmpty(registration.FName))
            {
                return BadRequest("First name must not be empty since its a required field.");
            }

            if(registration.FName.Length > AppConstants.FirstName)
            {
                return BadRequest("First name should be 10 characters long.");
            }           

            // Validate last name
            if (string.IsNullOrEmpty(registration.LName))
            {
                return BadRequest("Last name must not be empty since its a required field.");
            }


            if (registration.LName.Length > AppConstants.LastName)
            {
                return BadRequest("Last name should be 10 characters long.");

            }

            // Validate email format
            if (string.IsNullOrEmpty(registration.Email) || !IsValidEmail(registration.Email) || !registration.Email.EndsWith("@gmail.com"))
            {
                return BadRequest("Invalid email format. Email should be in the format 'example@gmail.com'.");
            }

            // Validate phone number
            if (string.IsNullOrEmpty(registration.PhoneNo.ToString()))
            {
                return BadRequest("Phone number must not be empty since its a required field.");
            }


            if (registration.PhoneNo.ToString().Length != AppConstants.PhoneNumber)
            {
                return BadRequest("Phone number should be 10 digits long.");

            }

            //validate country
            if(string.IsNullOrEmpty(registration.Country))
            {
                return BadRequest("Country must not be empty since its a required field.");

            }

            

            try
            {
                _context.Registrations.Add(registration);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRegistration), new { id = registration.Id }, registration);
            }
            catch (Exception ex)
            {
                // Handle any specific exception or log the error
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the registration.");
            }
        }

        // validate email format
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }



        // DELETE: api/Registrations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            if (_context.Registrations == null)
            {
                return NotFound();
            }
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistrationExists(int id)
        {
            return (_context.Registrations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
