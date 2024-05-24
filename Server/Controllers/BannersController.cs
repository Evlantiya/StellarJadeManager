using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StellarJadeManager.Server;
using StellarJadeManager.Shared;

namespace StellarJadeManager.Server.Controllers
{
    public class BannersController : ControllerBase
    {
        private readonly PostgresContext _context;

        public BannersController(PostgresContext context)
        {
            _context = context;
        }

        // GET: Banners
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.Banners.Include(b => b.Patch);
            return Ok(await postgresContext.ToListAsync());
        }

        // GET: Banners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Banners == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners
                .Include(b => b.Patch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }

            return Ok(banner);
        }

        //// GET: Banners/Create
        //public IActionResult Create()
        //{
        //    ViewData["PatchId"] = new SelectList(_context.Patches, "Id", "Id");
        //    return Ok();
        //}

        // POST: Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Banner banner)
        {
            _context.Add(banner);
            await _context.SaveChangesAsync();
            return Ok(banner);
        }

        // GET: Banners/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Banners == null)
        //    {
        //        return NotFound();
        //    }

        //    var banner = await _context.Banners.FindAsync(id);
        //    if (banner == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["PatchId"] = new SelectList(_context.Patches, "Id", "Id", banner.PatchId);
        //    return View(banner);
        //}

        // POST: Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,PatchId,TypeId,StartDate,EndDate")] Banner banner)
        //{
        //    if (id != banner.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(banner);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BannerExists(banner.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["PatchId"] = new SelectList(_context.Patches, "Id", "Id", banner.PatchId);
        //    return View(banner);
        //}

        // GET: Banners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Banners == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners
                .Include(b => b.Patch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }

            return Ok(banner);
        }

        // POST: Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Banners == null)
            {
                return Problem("Entity set 'PostgresContext.Banners'  is null.");
            }
            var banner = await _context.Banners.FindAsync(id);
            if (banner != null)
            {
                _context.Banners.Remove(banner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerExists(int id)
        {
          return (_context.Banners?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
