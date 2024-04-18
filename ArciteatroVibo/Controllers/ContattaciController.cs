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
    public class ContattaciController : Controller
    {
        private readonly ArciteatroViboValentiaContext _context;

        public ContattaciController(ArciteatroViboValentiaContext context)
        {
            _context = context;
        }

        // GET: Contattaci
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contattacis.ToListAsync());
        }

        // GET: Contattaci/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contattaci = await _context.Contattacis
                .FirstOrDefaultAsync(m => m.IdContattici == id);
            if (contattaci == null)
            {
                return NotFound();
            }

            return View(contattaci);
        }

        //public IActionResult PartialForm()
        //{  
        //    return PartialView("_PartialForm");
        //}


        // GET: Contattaci/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contattaci/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContattici,Titolo,Email,Testo")] Contattaci contattaci)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contattaci);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contattaci);
        }

        // GET: Contattaci/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contattaci = await _context.Contattacis.FindAsync(id);
            if (contattaci == null)
            {
                return NotFound();
            }
            return View(contattaci);
        }

        // POST: Contattaci/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdContattici,Titolo,Email,Testo")] Contattaci contattaci)
        {
            if (id != contattaci.IdContattici)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contattaci);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContattaciExists(contattaci.IdContattici))
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
            return View(contattaci);
        }

        // GET: Contattaci/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contattaci = await _context.Contattacis
                .FirstOrDefaultAsync(m => m.IdContattici == id);
            if (contattaci == null)
            {
                return NotFound();
            }

            return View(contattaci);
        }

        // POST: Contattaci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contattaci = await _context.Contattacis.FindAsync(id);
            if (contattaci != null)
            {
                _context.Contattacis.Remove(contattaci);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContattaciExists(int id)
        {
            return _context.Contattacis.Any(e => e.IdContattici == id);
        }
    }
}
