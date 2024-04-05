using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArciteatroVibo.Models;

namespace ArciteatroVibo.Controllers
{
    public class CommediesController : Controller
    {
        private readonly ArciteatroViboValentiaContext _context;

        public CommediesController(ArciteatroViboValentiaContext context)
        {
            _context = context;
        }

        // GET: Commedies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Commedies.ToListAsync());
        }

        // GET: Commedies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commedie = await _context.Commedies
                .FirstOrDefaultAsync(m => m.IdCommedia == id);
            if (commedie == null)
            {
                return NotFound();
            }

            return View(commedie);
        }

        // GET: Commedies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Commedies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCommedia,Titolo,Autore,Trama,Locandina,Regia,Interpreti,Extra,Foto1,Foto2,Foto3")] Commedie commedie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commedie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commedie);
        }

        // GET: Commedies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commedie = await _context.Commedies.FindAsync(id);
            if (commedie == null)
            {
                return NotFound();
            }
            return View(commedie);
        }

        // POST: Commedies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCommedia,Titolo,Autore,Trama,Locandina,Regia,Interpreti,Extra,Foto1,Foto2,Foto3")] Commedie commedie)
        {
            if (id != commedie.IdCommedia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commedie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommedieExists(commedie.IdCommedia))
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
            return View(commedie);
        }

        // GET: Commedies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commedie = await _context.Commedies
                .FirstOrDefaultAsync(m => m.IdCommedia == id);
            if (commedie == null)
            {
                return NotFound();
            }

            return View(commedie);
        }

        // POST: Commedies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commedie = await _context.Commedies.FindAsync(id);
            if (commedie != null)
            {
                _context.Commedies.Remove(commedie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommedieExists(int id)
        {
            return _context.Commedies.Any(e => e.IdCommedia == id);
        }
    }
}
