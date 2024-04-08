using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using ArciteatroVibo.Models;
using Microsoft.AspNetCore.StaticFiles;

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
        public async Task<IActionResult> Create([Bind("IdHome1,Foto2,Foto3,Upload,Video,updateimm, updateimmDue, updateimmTre,UploadUp,PdfPath")] Home1 home1)
        { 
            ModelState.Remove("Foto1"); // quelli che sono obbligatori vanno rimossi soprattutto se ci sono dei campi hidden
           
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

                if (home1.updateimmDue != null && home1.updateimmDue.Length > 0)
                {
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "immagini", home1.updateimmDue.FileName);

                    using (var Filestream = new FileStream(path, FileMode.Create))
                    {
                        await home1.updateimmDue.CopyToAsync(Filestream);
                    }

                    home1.Foto2 = "/immagini/" + home1.updateimmDue.FileName;
                }

                if (home1.updateimmTre != null && home1.updateimmTre.Length > 0)
                {
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "immagini", home1.updateimmTre.FileName);

                    using (var Filestream = new FileStream(path, FileMode.Create))
                    {
                        await home1.updateimmTre.CopyToAsync(Filestream);
                    }

                    home1.Foto3 = "/immagini/" + home1.updateimmTre.FileName;
                }
                if (home1.Upload != null && home1.Upload.Length > 0)
                {
                    var provider = new FileExtensionContentTypeProvider();

                    // Add new mappings
                    provider.Mappings[".pdf"] = "application/pdf";

                    // Verifica del tipo MIME per assicurarsi che sia un file PDF
                    string contentType;
                    if (provider.TryGetContentType(home1.UploadUp.FileName, out contentType) && contentType == "application/pdf")
                    {
                        // Salva il file PDF nel percorso desiderato
                        var pdfPath = Path.Combine(_hostingEnvironment.WebRootPath, "immagini", home1.UploadUp.FileName);

                        using (var fileStream = new FileStream(pdfPath, FileMode.Create))
                        {
                            await home1.UploadUp.CopyToAsync(fileStream);
                        }

                        // Assegna il percorso del file PDF al modello
                        home1.PdfPath = "/pdf/" + home1.UploadUp.FileName;
                    }
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


        private MemoryStream DownloadSinghFile(IFormFile file)
        {
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            ms.Position = 0;
            return ms;
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
