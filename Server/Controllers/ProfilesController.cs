using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StellarJadeManager.Server;
using StellarJadeManager.Shared;

namespace StellarJadeManager.Server.Controllers
{
    [Route("api/profile")]
    [Authorize]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly PostgresContext _context;

        public ProfilesController(PostgresContext context)
        {
            _context = context;
        }

        // GET: api/Profiles
        [HttpGet("list")]
        
        public async Task<ActionResult<List<Profile>>> GetProfiles()
        {
            var id = Convert.ToInt32(User.Claims.First(claim => claim.Type == "Id").Value);
            if (_context.Profiles == null)
            {
                return NotFound();
            }
            var profiles = await _context.Profiles.Where(profile => profile.UserId == id).ToListAsync();
            return profiles;
        }

        // GET: api/profiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetProfile(int id)
        {
            if (_context.Profiles == null)
            {
                return NotFound();
            }
            var profile = await _context.Profiles.FindAsync(id);

            if (profile == null)
            {
                return NotFound();
            }

            return profile;
        }

        // PUT: api/profile/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile([FromQuery] string id,[FromBody] Profile profile)
        {
            if (id != profile.Id.ToString())
            {
                return BadRequest();
            }
            _context.Entry(profile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
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

        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profile>> PostProfile(Profile profile)
        {
            if (_context.Profiles == null)
            {
                return Problem("Entity set 'ApplicationSupabaseContext.Profiles'  is null.");
            }
            _context.Profiles.Add(profile);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (true)//
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProfile", new { id = profile.Uid }, profile);
        }

        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            if (_context.Profiles == null)
            {
                return NotFound();
            }
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileExists(string id)
        {
            return (_context.Profiles?.Any(e => e.Uid == id)).GetValueOrDefault();
        }
    }
}
