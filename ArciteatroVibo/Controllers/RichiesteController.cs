using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArciteatroVibo.Models;
using System.Security.Claims;

namespace ArciteatroVibo.Controllers
{
    public class RichiesteController : Controller
    {
        private readonly ArciteatroViboValentiaContext _context;
     


        public RichiesteController(ArciteatroViboValentiaContext context )
        {
            _context = context;
            
        }


            


        // GET: Richieste
        public async Task<IActionResult> Index()
        {

            var arciteatroViboValentiaContext = _context.Richiestes.Include(r => r.FkLaboratorioNavigation).Include(r => r.FkUtenteNavigation);
            return View(await arciteatroViboValentiaContext.ToListAsync());
        }

        // GET: Richieste/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var richieste = await _context.Richiestes
                .Include(r => r.FkLaboratorioNavigation)
                .Include(r => r.FkUtenteNavigation)
                .FirstOrDefaultAsync(m => m.IdRichiesta == id);
            if (richieste == null)
            {
                return NotFound();
            }

            return View(richieste);
        }

       // creo artificiosamente il mio id PartialForm prendendo l'id del laboratorio
        public IActionResult PartialForm(int?id)
        {
          
                ViewBag.Laboratorio = id;

            TempData["Laboratorio"] = id;

            return PartialView("_PartialForm");
        }


        // GET: Richieste/Create
        public IActionResult Create()
        {      
            return View();
        }

        // POST: Richieste/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Create([Bind("IdRichiesta,Nome,Cognome,CorpoRichiesta,DataNascita,FkUtente,FkLaboratorio")] Richieste richieste)
        {
            ViewBag.Laboratorio = TempData["Laboratorio"];

            ModelState.Remove("FkUtenteNavigation");
            ModelState.Remove("FkLaboratorioNavigation");

           var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           var utente = _context.Utentis.FirstOrDefault(u => u.IdUtente == int.Parse(userId));   
           ViewBag.Utente = utente.IdUtente; // Accedi all'ID dell'utente

   

            if (ModelState.IsValid)
            {


                _context.Add(richieste);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(richieste);
        }

        // GET: Richieste/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var richieste = await _context.Richiestes.FindAsync(id);
            if (richieste == null)
            {
                return NotFound();
            }
          
            return View(richieste);
        }

        // POST: Richieste/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRichiesta,Nome,Cognome,CorpoRichiesta,FkUtente,FkLaboratorio")] Richieste richieste)
        {
            if (id != richieste.IdRichiesta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(richieste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RichiesteExists(richieste.IdRichiesta))
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
            ViewData["FkLaboratorio"] = new SelectList(_context.Laboratorios, "IdLaboratorio", "IdLaboratorio", richieste.FkLaboratorio);
            ViewData["FkUtente"] = new SelectList(_context.Utentis, "IdUtente", "IdUtente", richieste.FkUtente);
            return View(richieste);
        }

        // GET: Richieste/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var richieste = await _context.Richiestes
                .Include(r => r.FkLaboratorioNavigation)
                .Include(r => r.FkUtenteNavigation)
                .FirstOrDefaultAsync(m => m.IdRichiesta == id);
            if (richieste == null)
            {
                return NotFound();
            }

            return View(richieste);
        }

        // POST: Richieste/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var richieste = await _context.Richiestes.FindAsync(id);
            if (richieste != null)
            {
                _context.Richiestes.Remove(richieste);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RichiesteExists(int id)
        {
            return _context.Richiestes.Any(e => e.IdRichiesta == id);
        }
    }
}
