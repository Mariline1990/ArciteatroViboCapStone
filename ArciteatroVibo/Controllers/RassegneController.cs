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
    public class RassegneController : Controller
    {
        private readonly ArciteatroViboValentiaContext _context;

        public RassegneController(ArciteatroViboValentiaContext context)
        {
            _context = context;
        }

        // GET: Rassegne
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rassegnes.ToListAsync());
        }

        // GET: Rassegne/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rassegne = await _context.Rassegnes
                .FirstOrDefaultAsync(m => m.IdRassegna == id);
            if (rassegne == null)
            {
                return NotFound();
            }

            return View(rassegne);
        }

        // GET: Rassegne/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rassegne/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRassegna,Titolo,Locandina,Testo,Edizione,Data,Luogo,Regia,Interpreti,Extra")] Rassegne rassegne)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rassegne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rassegne);
        }

        // GET: Rassegne/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rassegne = await _context.Rassegnes.FindAsync(id);
            if (rassegne == null)
            {
                return NotFound();
            }
            return View(rassegne);
        }

        // POST: Rassegne/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRassegna,Titolo,Locandina,Testo,Edizione,Data,Luogo,Regia,Interpreti,Extra")] Rassegne rassegne)
        {
            if (id != rassegne.IdRassegna)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rassegne);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RassegneExists(rassegne.IdRassegna))
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
            return View(rassegne);
        }

        // GET: Rassegne/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rassegne = await _context.Rassegnes
                .FirstOrDefaultAsync(m => m.IdRassegna == id);
            if (rassegne == null)
            {
                return NotFound();
            }

            return View(rassegne);
        }

        // POST: Rassegne/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rassegne = await _context.Rassegnes.FindAsync(id);
            if (rassegne != null)
            {
                _context.Rassegnes.Remove(rassegne);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RassegneExists(int id)
        {
            return _context.Rassegnes.Any(e => e.IdRassegna == id);
        }
    }
}
