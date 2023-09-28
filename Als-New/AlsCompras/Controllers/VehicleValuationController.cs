using AlsCompras.Data;
using AlsCompras.Models.AreaVehicle;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsCompras.Controllers
{
    public class VehicleValuationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleValuationController(ApplicationDbContext context)
        {
            _context = context;
        }
        public VehicleValuation CreateVehicleValuationWithLicencePlate(string licencePlate)
        {

            VehicleValuation vehicleValuation = new()
            {
                LicencePlate = licencePlate
            };

            try
            {
                _context.VehicleValuation.Add(vehicleValuation);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                //throw;
            }


            return vehicleValuation;
        }

        public VehicleValuation GetVehicleValuationById(Guid id)
        {
            VehicleValuation vehicleValuation = new();

            try
            {
                vehicleValuation = _context.VehicleValuation
                    .Where(m => m.Id == id)
                    .FirstOrDefault();

                if (vehicleValuation != null)
                {
                    return vehicleValuation;
                }
            }
            catch (Exception ex)
            {

            }


            return vehicleValuation;
        }
    }
}
