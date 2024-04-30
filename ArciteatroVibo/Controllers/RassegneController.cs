using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArciteatroVibo.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Diagnostics;




namespace ArciteatroVibo.Controllers
{
    public class RassegneController : Controller
    {
        private readonly ArciteatroViboValentiaContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public RassegneController(ArciteatroViboValentiaContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
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
        public async Task<IActionResult> Create([Bind("IdRassegna,Titolo,Locandina,Testo,Edizione,Data,Luogo,Regia,Interpreti,Extra,LocandinaUp")] Rassegne rassegne)
        {
            ModelState.Remove("Locandina");
            if (ModelState.IsValid)
            {
                if (rassegne.LocandinaUp != null && rassegne.LocandinaUp.Length > 0)
                {
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "immagini/Locandine", rassegne.LocandinaUp.FileName);

                    using (var Filestream = new FileStream(path, FileMode.Create))
                    {
                        await rassegne.LocandinaUp.CopyToAsync(Filestream);
                    }
                    rassegne.Locandina = "/immagini/Locandine/" + rassegne.LocandinaUp.FileName;
                }
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
        public async Task<IActionResult> Edit(int id, [Bind("IdRassegna,Titolo,Locandina,Testo,Edizione,Data,Luogo,Regia,Interpreti,Extra,LocandinaUp")] Rassegne rassegne)
        {
            if (id != rassegne.IdRassegna)
            {
                return NotFound();
            }
            ModelState.Remove("Locandina");
            if (ModelState.IsValid)
            {
                if (rassegne.LocandinaUp != null && rassegne.LocandinaUp.Length > 0)
                {
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "immagini/Locandine", rassegne.LocandinaUp.FileName);

                    using (var Filestream = new FileStream(path, FileMode.Create))
                    {
                        await rassegne.LocandinaUp.CopyToAsync(Filestream);
                    }
                    rassegne.Locandina = "/immagini/Locandine/" + rassegne.LocandinaUp.FileName;
                }
                _context.Update(rassegne);
                await _context.SaveChangesAsync();
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
