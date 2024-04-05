using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using ArciteatroVibo.Models;

namespace ArciteatroVibo.Controllers
{
    public class Home1Controller : Controller
    {
        private readonly ArciteatroViboValentiaContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Home1Controller(ArciteatroViboValentiaContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Home1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Home1s.ToListAsync());
        }

        // GET: Home1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home1 = await _context.Home1s
                .FirstOrDefaultAsync(m => m.IdHome1 == id);
            if (home1 == null)
            {
                return NotFound();
            }

            return View(home1);
        }

        // GET: Home1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHome1,Foto1,Foto2,Foto3,Upload,Video")] Home1 home1)
        { 
           
            if (ModelState.IsValid)
            {
                if (home1.updateimm != null && home1.updateimm.Length > 0)
                {
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "immagini", home1.updateimm.FileName);

                    using (var Filestream = new FileStream(path, FileMode.Create))
                    {
                        await home1.updateimm.CopyToAsync(Filestream);
                    }

                    home1.Foto1 = "/immagini/" + home1.updateimm.FileName;
                }
                else
                {
                    home1.Foto1 = "/img/alla.jpeg";
                }


                _context.Add(home1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(home1);
        }

        // GET: Home1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home1 = await _context.Home1s.FindAsync(id);
            if (home1 == null)
            {
                return NotFound();
            }
            return View(home1);
        }

        // POST: Home1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHome1,Foto1,Foto2,Foto3,Upload,Video")] Home1 home1)
        {
            if (id != home1.IdHome1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(home1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Home1Exists(home1.IdHome1))
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
            return View(home1);
        }

        // GET: Home1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home1 = await _context.Home1s
                .FirstOrDefaultAsync(m => m.IdHome1 == id);
            if (home1 == null)
            {
                return NotFound();
            }

            return View(home1);
        }

        // POST: Home1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var home1 = await _context.Home1s.FindAsync(id);
            if (home1 != null)
            {
                _context.Home1s.Remove(home1);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Home1Exists(int id)
        {
            return _context.Home1s.Any(e => e.IdHome1 == id);
        }
    }
}
