using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArciteatroVibo.Models;
using Microsoft.AspNetCore.Hosting;


namespace ArciteatroVibo.Controllers
{
    public class LaboratorioController : Controller
    {
        private readonly ArciteatroViboValentiaContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public LaboratorioController(ArciteatroViboValentiaContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Laboratorio
        public async Task<IActionResult> Index()
        {
         
           

            return View(await _context.Laboratorios.ToListAsync());
        }

        // GET: Laboratorio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
              

                return NotFound();
            }

            int postiPrenotati = _context.Richiestes.Where(r => r.FkLaboratorio == id).Count();
            int PostiRimanenti = _context.Laboratorios.Where(l => l.IdLaboratorio == id).Select(l => l.PostiLiberi).FirstOrDefault() - postiPrenotati;
            ViewBag.postiRimanenti = PostiRimanenti;
            TempData["postiRimanenti"] = PostiRimanenti;

            var laboratorio = await _context.Laboratorios
                .FirstOrDefaultAsync(m => m.IdLaboratorio == id);
            if (laboratorio == null)
            {
                return NotFound();
            }

            return View(laboratorio);
        }

        // GET: Laboratorio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Laboratorio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLaboratorio,Immagine,Titolo,Testo,PostiLiberi,Testo2,EMail,Telefono,DataInizio,DataFine,ImmagineUp")] Laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                if(laboratorio.ImmagineUp != null)
                {
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "immagini", laboratorio.ImmagineUp.FileName);

                    using (var Filestream = new FileStream(path, FileMode.Create))
                    {
                        await laboratorio.ImmagineUp.CopyToAsync(Filestream);
                    }

                    laboratorio.Immagine = "/immagini/" + laboratorio.ImmagineUp.FileName;
                }
                _context.Add(laboratorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(laboratorio);
        }

        // GET: Laboratorio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laboratorio = await _context.Laboratorios.FindAsync(id);
            if (laboratorio == null)
            {
                return NotFound();
            }
            return View(laboratorio);
        }

        // POST: Laboratorio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLaboratorio,Immagine,Titolo,Testo,PostiLiberi,Testo2,EMail,Telefono,DataInizio,DataFine")] Laboratorio laboratorio)
        {
            if (id != laboratorio.IdLaboratorio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laboratorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaboratorioExists(laboratorio.IdLaboratorio))
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
            return View(laboratorio);
        }

        // GET: Laboratorio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laboratorio = await _context.Laboratorios
                .FirstOrDefaultAsync(m => m.IdLaboratorio == id);
            if (laboratorio == null)
            {
                return NotFound();
            }

            return View(laboratorio);
        }

        // POST: Laboratorio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laboratorio = await _context.Laboratorios.FindAsync(id);
            if (laboratorio != null)
            {
                _context.Laboratorios.Remove(laboratorio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaboratorioExists(int id)
        {
            return _context.Laboratorios.Any(e => e.IdLaboratorio == id);
        }
    }
}
