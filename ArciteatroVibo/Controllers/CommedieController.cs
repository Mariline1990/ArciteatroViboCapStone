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
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace ArciteatroVibo.Controllers
{
    public class CommedieController : Controller
    {
        private readonly ArciteatroViboValentiaContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CommedieController(ArciteatroViboValentiaContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Commedie
        public async Task<IActionResult> Index()
        {
            return View(await _context.Commedies.ToListAsync());
        }

        // GET: Commedie/Details/5
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

        // GET: Commedie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Commedie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCommedia,Titolo,Autore,Trama,Locandina,Regia,Interpreti,Extra,Foto1,Foto2,Foto3,LocandinaUp,Foto1Up,Foto2Up,Foto3Up")] Commedie commedie)
        {
            if (ModelState.IsValid)
            {
                if (commedie.LocandinaUp != null && commedie.LocandinaUp.Length > 0)
                {
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "immagini/Locandine", commedie.LocandinaUp.FileName);

                    using (var Filestream = new FileStream(path, FileMode.Create))
                    {
                        await commedie.LocandinaUp.CopyToAsync(Filestream);
                    }
                    commedie.Locandina = "/immagini/Locandine/" + commedie.LocandinaUp.FileName;
                }

                if (commedie.Foto1Up != null && commedie.Foto1Up.Length > 0)
                {
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "immagini/Commedie", commedie.Foto1Up.FileName);

                    using (var Filestream = new FileStream(path, FileMode.Create))
                    {
                        await commedie.Foto1Up.CopyToAsync(Filestream);
                    }
                    commedie.Foto1 = "/immagini/Commedie/" + commedie.Foto1Up.FileName;

                }

                if (commedie.Foto2Up != null && commedie.Foto2Up.Length > 0)
                {
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "immagini/Commedie", commedie.Foto2Up.FileName);

                    using (var Filestream = new FileStream(path, FileMode.Create))
                    {
                        await commedie.Foto2Up.CopyToAsync(Filestream);
                    }
                    commedie.Foto2 = "/immagini/Commedie/" + commedie.Foto2Up.FileName;
                }

                if (commedie.Foto3Up != null && commedie.Foto3Up.Length > 0)
                {
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "immagini/Commedie", commedie.Foto3Up.FileName);

                    using (var Filestream = new FileStream(path, FileMode.Create))
                    {
                        await commedie.Foto3Up.CopyToAsync(Filestream);
                    }
                    commedie.Foto3 = "/immagini/Commedie/" + commedie.Foto3Up.FileName;
                }

                _context.Add(commedie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commedie);
        }

        // GET: Commedie/Edit/5
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

        // POST: Commedie/Edit/5
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

        // GET: Commedie/Delete/5
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

        // POST: Commedie/Delete/5
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
