using AlsCompras.Controllers;
using AlsCompras.Models;
using AlsCompras.Models.AreaCrm;
using AlsCompras.Models.AreaVehicle;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlsCompras.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #region Vehicle
        public DbSet<VehicleEquipament> VehicleEquipament { get; set; }
        public DbSet<VehicleColor> VehicleColor { get; set; }
        public DbSet<VehicleValuation> VehicleValuation { get; set; }

        #endregion

        #region CRM
        public DbSet<CrmClient> CrmClient { get; set; }

        #endregion
        //public DbSet<VehiclePhotoModel> VehiclePhotoModel { get; set; }
    }
}
