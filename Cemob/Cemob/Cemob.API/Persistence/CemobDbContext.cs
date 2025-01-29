using Cemob.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cemob.API.Persistence
{
    public class CemobDbContext : DbContext
    {

        public CemobDbContext(DbContextOptions<CemobDbContext> options) 
            : base(options) 
        { 

        }

        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DoctorMedicalSpeciality> DoctorMedicalSpeciality { get; set; }
        public DbSet<MedicalSpeciality> medicalSpecialities { get; set; }
        public DbSet<ServiceComment> ServicesComment { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Attendant> Attendant { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<MedicalSpeciality>(entity =>
                {
                    entity.HasKey(medicalspeciality => medicalspeciality.Id); //-> definir a chave da speciality que será o Id
                });

            builder
                .Entity<DoctorMedicalSpeciality>(entity =>
                {
                    entity.HasKey(doctorspeciality => doctorspeciality.Id);

                    entity.HasOne(doctor => doctor.MedicalSpeciality)
                                .WithMany(doctor => doctor.DoctorMedicalSpeciality)
                                .HasForeignKey(speciality => speciality.IdMedicalSpeciality)
                                .OnDelete(DeleteBehavior.Restrict);
                });

            builder
                .Entity<Service>(entity =>
                {
                    entity.HasKey(service => service.Id);

                    entity.HasOne(service => service.Patient)
                           .WithMany(patient => patient.PatientServices)
                           .HasForeignKey(service => service.IdPatient)
                           .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(service => service.Doctor)
                           .WithMany(doctor => doctor.DoctorServices) 
                           .HasForeignKey(service => service.IdDoctor)
                           .OnDelete(DeleteBehavior.Restrict);

                });

            builder
                .Entity<ServiceComment>(entity =>
                {
                    entity.HasKey(service => service.Id);
                    entity.HasOne(service => service.Service)
                            .WithMany(service => service.Comments)
                            .HasForeignKey(service => service.IdService)
                            .OnDelete(DeleteBehavior.Restrict);

                });

            builder.Entity<Patient>(entity =>
            {
                entity.HasKey(patient => patient.Id);   

                entity.HasMany(patient => patient.PatientServices)
                      .WithOne(patientservice => patientservice.Patient)
                      .HasForeignKey(patientservice => patientservice.IdPatient)
                      .OnDelete(DeleteBehavior.Restrict);

                //entity.HasMany(patient => patient.Doctor)
                //      .WithOne(patientservice => patientservice.DoctorService) //-> fazer ele receber só um serviço por vez
                //      .HasForeignKey(patientservice => patientservice.IdService)
                //      .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<Doctor>(entity =>
            {
                entity.HasKey(doctor => doctor.Id); 
            });

            builder.Entity<User>(entity =>
            {
                entity.HasKey(user => user.Id);
            });

            builder.Entity<Attendant>(entity =>
            {
                entity.HasKey(attendant => attendant.Id);

            });


            base.OnModelCreating(builder);
        }


    }
}
