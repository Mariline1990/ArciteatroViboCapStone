using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArciteatroVibo.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace ArciteatroVibo.Controllers
{
    public class LoginController : Controller
    {
        private readonly ArciteatroViboValentiaContext _context;

        public LoginController(ArciteatroViboValentiaContext context)
        {
            _context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utentis.ToListAsync());
        }

        // GET: Login/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utenti = await _context.Utentis
                .FirstOrDefaultAsync(m => m.IdUtente == id);
            if (utenti == null)
            {
                return NotFound();
            }

            return View(utenti);
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home1");
        }


        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Utenti utenti)
        {

          
            if (!string.IsNullOrEmpty(utenti.Email) && !string.IsNullOrEmpty(utenti.Password))
            {
                var user = _context.Utentis.FirstOrDefault(u => u.Email == utenti.Email);


                if (user != null && VerifyPasswordHash(utenti.Password, user.Password))
                {
                    string roleName = user.Ruolo ? "Admin" : "User";


                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email), // nome
                new Claim(ClaimTypes.Role, roleName), // Usa il nome del ruolo qui
                new Claim(ClaimTypes.NameIdentifier, user.IdUtente.ToString()), // id
            };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync( CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),authProperties);
                    return RedirectToAction("Index", "Home1");
                }

            }
             ViewBag.Error = "Email o Password errati";
            return View("Create");


        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utenti = await _context.Utentis.FindAsync(id);
            if (utenti == null)
            {
                return NotFound();
            }
            return View(utenti);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUtente,Newsletter,Nome,Cognome,Email,Password,Ruolo")] Utenti utenti)
        {
            if (id != utenti.IdUtente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utenti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtentiExists(utenti.IdUtente))
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
            return View(utenti);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utenti = await _context.Utentis
                .FirstOrDefaultAsync(m => m.IdUtente == id);
            if (utenti == null)
            {
                return NotFound();
            }

            return View(utenti);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utenti = await _context.Utentis.FindAsync(id);
            if (utenti != null)
            {
                _context.Utentis.Remove(utenti);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtentiExists(int id)
        {
            return _context.Utentis.Any(e => e.IdUtente == id);
        }


        private bool VerifyPasswordHash(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
