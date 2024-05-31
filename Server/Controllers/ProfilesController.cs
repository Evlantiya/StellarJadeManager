using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly PostgresContext _db;
        private readonly IMapper _mapper;

        public ProfilesController(PostgresContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        // GET: api/Profiles
        [HttpGet("list")]
        
        public async Task<ActionResult<List<Shared.Profile>>> GetProfiles()
        {
            var id = Convert.ToInt32(User.Claims.First(claim => claim.Type == "Id").Value);
            if (_db.Profiles == null)
            {
                return NotFound();
            }
            var profiles = await _db.Profiles
                .Where(profile => profile.UserId == id)
                .Include(profile => profile.UserBannerInfos)
                .ThenInclude(bannerInfo => bannerInfo.Warps)
                .ThenInclude(Warp => Warp.Item)
                .AsNoTracking()
                .ToListAsync();
            return profiles;
        }

        // GET: api/profiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shared.Profile>> GetProfile(int id)
        {
            if (_db.Profiles == null)
            {
                return NotFound();
            }
            var profile = await _db.Profiles.FindAsync(id);

            if (profile == null)
            {
                return NotFound();
            }

            return profile;
        }

        // PUT: api/profile/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(string id,[FromBody] ProfilePutRequestDTO profile)
        {
            // if (id != profile.Id.ToString())
            // {
            //     return BadRequest();
            // }
            // _context.Entry(profile).State = EntityState.Modified;
            
            //var test = new Shared.Profile();

            var user_profile = await _db.Profiles.FirstOrDefaultAsync(prof => prof.Id.ToString() == id);
            if(user_profile == null){
                return NotFound("User with that ID not found");
            }
            try
            {
                
                _mapper.Map(profile, user_profile);
                await _db.SaveChangesAsync();
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
        public async Task<ActionResult<Shared.Profile>> PostProfile(Shared.Profile profile)
        {
            if (_db.Profiles == null)
            {
                return Problem("Entity set 'ApplicationSupabaseContext.Profiles'  is null.");
            }
            _db.Profiles.Add(profile);
            try
            {
                await _db.SaveChangesAsync();
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
            if (_db.Profiles == null)
            {
                return NotFound();
            }
            var profile = await _db.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            _db.Profiles.Remove(profile);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileExists(string id)
        {
            return (_db.Profiles?.Any(e => e.Uid == id)).GetValueOrDefault();
        }
    }
}
