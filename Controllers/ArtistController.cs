using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecordsMvcEf.Data;
using RecordsMvcEf.Models;

namespace RecordsMvcEf.Controllers
{
    public class ArtistController : Controller
    {
        private readonly AlbumContext _context;

        public ArtistController(AlbumContext context)
        {
            _context = context;
        }

        // GET: Artist
        public async Task<IActionResult> Index(string searchString)
        {
            var artists = from a in _context.Artists
                            select a;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                artists = artists.Where(s => s.ArtistName.ToLower().Contains(searchString.ToLower()));
            }

            return View(await artists.ToListAsync());
        }

        // GET: Artist/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .FirstOrDefaultAsync(m => m.ArtistId == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // GET: Artist/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artist/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtistId,ArtistName")] Artist artist)
        {
            // Make sure artist does not already exist
            var existingArtist = _context.Artists
            .Where(m => m.ArtistName.ToLower() == artist.ArtistName.ToLower()).ToList();

            if (existingArtist.Count != 0) 
            {
                ViewBag.Msg = "Artist already exists in database";
                return View();
            }
            
            if (ModelState.IsValid)
            {
                _context.Add(artist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artist/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artist/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtistId,ArtistName")] Artist artist)
        {
            if (id != artist.ArtistId)
            {
                return NotFound();
            }

            // Make sure artist does not already exist
            var existingArtist = _context.Artists
            .Where(m => m.ArtistName == artist.ArtistName).ToList();

            if (existingArtist.Count != 0) 
            {
                ViewBag.Msg = "Artist already exists in database";
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.ArtistId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artist/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .FirstOrDefaultAsync(m => m.ArtistId == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.ArtistId == id);
        }
    }
}
