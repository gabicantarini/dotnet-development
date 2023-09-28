using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlsCompras.Data;
using AlsCompras.Models.AreaVehicle;

namespace AlsCompras.Areas.BackOffice.Controllers
{
    [Area("BackOffice")]
    public class VehicleEquipamentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleEquipamentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BackOffice/VehicleEquipaments
        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleEquipament.ToListAsync());
        }

        // GET: BackOffice/VehicleEquipaments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleEquipament = await _context.VehicleEquipament
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleEquipament == null)
            {
                return NotFound();
            }

            return View(vehicleEquipament);
        }

        // GET: BackOffice/VehicleEquipaments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/VehicleEquipaments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] VehicleEquipament vehicleEquipament)
        {
            if (ModelState.IsValid)
            {
                vehicleEquipament.Id = Guid.NewGuid();
                _context.Add(vehicleEquipament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleEquipament);
        }

        // GET: BackOffice/VehicleEquipaments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleEquipament = await _context.VehicleEquipament.FindAsync(id);
            if (vehicleEquipament == null)
            {
                return NotFound();
            }
            return View(vehicleEquipament);
        }

        // POST: BackOffice/VehicleEquipaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] VehicleEquipament vehicleEquipament)
        {
            if (id != vehicleEquipament.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleEquipament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleEquipamentExists(vehicleEquipament.Id))
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
            return View(vehicleEquipament);
        }

        // GET: BackOffice/VehicleEquipaments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleEquipament = await _context.VehicleEquipament
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleEquipament == null)
            {
                return NotFound();
            }

            return View(vehicleEquipament);
        }

        // POST: BackOffice/VehicleEquipaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vehicleEquipament = await _context.VehicleEquipament.FindAsync(id);
            _context.VehicleEquipament.Remove(vehicleEquipament);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleEquipamentExists(Guid id)
        {
            return _context.VehicleEquipament.Any(e => e.Id == id);
        }
    }
}
