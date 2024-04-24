using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ArciteatroVibo.Models
{
    public class ContattiController : Controller
    {
        private readonly ArciteatroViboValentiaContext _context;

        public ContattiController(ArciteatroViboValentiaContext context)
        {
            _context = context;
        }

        // GET: Contatti
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contattis.ToListAsync());
        }

        // GET: Contatti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatti = await _context.Contattis
                .FirstOrDefaultAsync(m => m.IdContatto == id);
            if (contatti == null)
            {
                return NotFound();
            }

            return View(contatti);
        }

        // GET: Contatti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contatti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContatto,Sede,Telefono1,Telefono2,Email,Pec")] Contatti contatti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contatti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contatti);
        }

        // GET: Contatti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatti = await _context.Contattis.FindAsync(id);
            if (contatti == null)
            {
                return NotFound();
            }
            return View(contatti);
        }

        // POST: Contatti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdContatto,Sede,Telefono1,Telefono2,Email,Pec")] Contatti contatti)
        {
            if (id != contatti.IdContatto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contatti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContattiExists(contatti.IdContatto))
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
            return View(contatti);
        }

        // GET: Contatti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatti = await _context.Contattis
                .FirstOrDefaultAsync(m => m.IdContatto == id);
            if (contatti == null)
            {
                return NotFound();
            }

            return View(contatti);
        }

        // POST: Contatti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contatti = await _context.Contattis.FindAsync(id);
            if (contatti != null)
            {
                _context.Contattis.Remove(contatti);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContattiExists(int id)
        {
            return _context.Contattis.Any(e => e.IdContatto == id);
        }
    }
}
