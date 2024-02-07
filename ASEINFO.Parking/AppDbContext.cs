using ASEINFO.Parking.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

namespace ASEINFO.Parking
{
    public class AppDbContext : DbContext
    {
        public DbSet<TipoVehiculo> TiposVehiculos { get; set; }

        public DbSet<Vehiculo> Vehiculos { get; set; }

        public DbSet<Estancia> Estancias { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TipoVehiculo>().HasData(
                [
                    new TipoVehiculo(){TipoVehiculoId = 1, Descripcion="NO RESIDENTES", Precio=(decimal)0.50},
                    new TipoVehiculo(){TipoVehiculoId = 2, Descripcion="RESIDENTES", Precio=(decimal)0.05},
                    new TipoVehiculo(){TipoVehiculoId = 3, Descripcion="VEHICULOS OFICIALES", Precio=null}
                ]);

            modelBuilder.Entity<Vehiculo>()
                .HasIndex(e => e.Placa)
                .IsUnique();
            

            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }
    }
}
