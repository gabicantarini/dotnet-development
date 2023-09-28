using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlsCompras.Data;
using AlsCompras.Models.AreaVehicle;

namespace AlsCompras.Areas.BackOffice.Views
{
    [Area("BackOffice")]
    public class VehicleColorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleColorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BackOffice/VehicleColors
        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleColor.ToListAsync());
        }

        // GET: BackOffice/VehicleColors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleColor = await _context.VehicleColor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleColor == null)
            {
                return NotFound();
            }

            return View(vehicleColor);
        }

        // GET: BackOffice/VehicleColors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/VehicleColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] VehicleColor vehicleColor)
        {
            if (ModelState.IsValid)
            {
                vehicleColor.Id = Guid.NewGuid();
                _context.Add(vehicleColor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleColor);
        }

        // GET: BackOffice/VehicleColors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleColor = await _context.VehicleColor.FindAsync(id);
            if (vehicleColor == null)
            {
                return NotFound();
            }
            return View(vehicleColor);
        }

        // POST: BackOffice/VehicleColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] VehicleColor vehicleColor)
        {
            if (id != vehicleColor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleColor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleColorExists(vehicleColor.Id))
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
            return View(vehicleColor);
        }

        // GET: BackOffice/VehicleColors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleColor = await _context.VehicleColor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleColor == null)
            {
                return NotFound();
            }

            return View(vehicleColor);
        }

        // POST: BackOffice/VehicleColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vehicleColor = await _context.VehicleColor.FindAsync(id);
            _context.VehicleColor.Remove(vehicleColor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleColorExists(Guid id)
        {
            return _context.VehicleColor.Any(e => e.Id == id);
        }
    }
}
