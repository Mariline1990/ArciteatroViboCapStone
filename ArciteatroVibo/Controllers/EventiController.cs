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

namespace ArciteatroVibo.Controllers
{
    public class EventiController : Controller
    {
        private readonly ArciteatroViboValentiaContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EventiController(ArciteatroViboValentiaContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }


        // GET: Eventi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eventis.ToListAsync());
        }

        // GET: Eventi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventi = await _context.Eventis
                .FirstOrDefaultAsync(m => m.IdEvento == id);
            if (eventi == null)
            {
                return NotFound();
            }

            return View(eventi);
        }

        // GET: Eventi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEvento,Titolo,Sottotitolo,Data,Luogo,InCorso,Testo,Locandina,LocandinaUp")] Eventi eventi)
        {
            string oggiFormattato = DateTime.Now.ToString("MM/dd/yyyy");

            // Non è necessario creare un nuovo oggetto DateTime se eventi.Data è già un DateTime valido
            DateTime theDay = new DateTime(eventi.Data?.Year ?? 1, eventi.Data?.Month ?? 1, eventi.Data?.Day ?? 1);


            // Confronto delle date
            int compareValue = theDay.Date.CompareTo(DateTime.Today);

            if (compareValue == 0)
            {
                eventi.InCorso = true;
            }
            else if (compareValue < 0)
            {
                eventi.InCorso = false;
            }
            else
            {
                eventi.InCorso = true;
            }

            if (ModelState.IsValid)
            {
                if (eventi.LocandinaUp != null && eventi.LocandinaUp.Length > 0)
                {
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "immagini/Locandine", eventi.LocandinaUp.FileName);

                    using (var Filestream = new FileStream(path, FileMode.Create))
                    {
                        await eventi.LocandinaUp.CopyToAsync(Filestream);
                    }

                    eventi.Locandina = "/immagini/Locandine/" + eventi.LocandinaUp.FileName;
                }
               
                    _context.Add(eventi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(eventi);
        }

        // GET: Eventi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventi = await _context.Eventis.FindAsync(id);
            if (eventi == null)
            {
                return NotFound();
            }
            return View(eventi);
        }

        // POST: Eventi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEvento,Titolo,Sottotitolo,Data,Luogo,InCorso,Testo,Locandina")] Eventi eventi)
        {
            if (id != eventi.IdEvento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventiExists(eventi.IdEvento))
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
            return View(eventi);
        }

        // GET: Eventi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventi = await _context.Eventis
                .FirstOrDefaultAsync(m => m.IdEvento == id);
            if (eventi == null)
            {
                return NotFound();
            }

            return View(eventi);
        }

        // POST: Eventi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventi = await _context.Eventis.FindAsync(id);
            if (eventi != null)
            {
                _context.Eventis.Remove(eventi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventiExists(int id)
        {
            return _context.Eventis.Any(e => e.IdEvento == id);
        }
    }
}
