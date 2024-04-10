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
    public class ProgettiController : Controller
    {
        private readonly ArciteatroViboValentiaContext _context;

        public ProgettiController(ArciteatroViboValentiaContext context)
        {
            _context = context;
        }

        // GET: Progetti
        public async Task<IActionResult> Index()
        {
            return View(await _context.Progettis.ToListAsync());
        }

        // GET: Progetti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progetti = await _context.Progettis
                .FirstOrDefaultAsync(m => m.IdProgetto == id);
            if (progetti == null)
            {
                return NotFound();
            }

            return View(progetti);
        }

        // GET: Progetti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Progetti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProgetto,Titolo,Testo,Data,Luogo,Immagine,Pdf")] Progetti progetti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(progetti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(progetti);
        }

        // GET: Progetti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progetti = await _context.Progettis.FindAsync(id);
            if (progetti == null)
            {
                return NotFound();
            }
            return View(progetti);
        }

        // POST: Progetti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProgetto,Titolo,Testo,Data,Luogo,Immagine,Pdf")] Progetti progetti)
        {
            if (id != progetti.IdProgetto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(progetti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgettiExists(progetti.IdProgetto))
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
            return View(progetti);
        }

        // GET: Progetti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progetti = await _context.Progettis
                .FirstOrDefaultAsync(m => m.IdProgetto == id);
            if (progetti == null)
            {
                return NotFound();
            }

            return View(progetti);
        }

        // POST: Progetti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var progetti = await _context.Progettis.FindAsync(id);
            if (progetti != null)
            {
                _context.Progettis.Remove(progetti);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgettiExists(int id)
        {
            return _context.Progettis.Any(e => e.IdProgetto == id);
        }
    }
}
