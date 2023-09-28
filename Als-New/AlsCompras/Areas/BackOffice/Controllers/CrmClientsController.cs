using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlsCompras.Data;
using AlsCompras.Models.AreaVehicle;
using AlsCompras.Models.AreaCrm;

namespace AlsCompras.Areas.BackOffice.Controllers
{
    [Area("BackOffice")]
    public class CrmClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CrmClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BackOffice/CrmClients
        public async Task<IActionResult> Index()
        {
            return View(await _context.CrmClient.ToListAsync());
        }

        // GET: BackOffice/CrmClients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crmClient = await _context.CrmClient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crmClient == null)
            {
                return NotFound();
            }

            return View(crmClient);
        }

        // GET: BackOffice/CrmClients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/CrmClients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CrmClient crmClient)
        {
            if (ModelState.IsValid)
            {
                crmClient.Id = Guid.NewGuid();
                _context.Add(crmClient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crmClient);
        }

        // GET: BackOffice/CrmClients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crmClient = await _context.CrmClient.FindAsync(id);
            if (crmClient == null)
            {
                return NotFound();
            }
            return View(crmClient);
        }

        // POST: BackOffice/CrmClients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] CrmClient crmClient)
        {
            if (id != crmClient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crmClient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrmClientExists(crmClient.Id))
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
            return View(crmClient);
        }

        // GET: BackOffice/CrmClients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crmClient = await _context.CrmClient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crmClient == null)
            {
                return NotFound();
            }

            return View(crmClient);
        }

        // POST: BackOffice/CrmClients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var crmClient = await _context.CrmClient.FindAsync(id);
            _context.CrmClient.Remove(crmClient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrmClientExists(Guid id)
        {
            return _context.CrmClient.Any(e => e.Id == id);
        }
    }
}
