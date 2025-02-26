using Microsoft.EntityFrameworkCore;

namespace Examen1_LeonardoMadrigal.Models
{
    public class Examen1Context : DbContext
    {
        public Examen1Context(DbContextOptions<Examen1Context> options) : base(options)
        {
        }
        // Tablas o la entidades de la base de datos
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Ruta> Rutas { get; set; }
        public DbSet<Boleto> Boletos { get; set; }

        // Sobre escribir el evento para modificar la creacion de la instancia y sus propiedades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llamar al metodo de la clase base
            modelBuilder.Entity<Vehiculo>(Vehiculo =>
            {
                // Vehiculo.HasKey(e => new {e.Id, e.Nombre});
                Vehiculo.HasKey(e => e.Id);
                Vehiculo.Property(n => n.Placa).IsRequired().HasMaxLength(100).IsUnicode(true);
                Vehiculo.Property(f => f.Modelo).IsRequired().HasColumnName("Modelo");
            });

            // Se configura la tabla de Usuario
            modelBuilder.Entity<Usuario>(Usuario =>
            {
                Usuario.HasKey(e => e.Id);
                Usuario.Property(n => n.Nombre).HasMaxLength(250).IsRequired();
                Usuario.Property(c => c.Correo).IsRequired().HasMaxLength(300).IsUnicode(true);
                Usuario.Property(c => c.Password).HasMaxLength(150);
            });

            // Se configura la tabla de Ruta
            modelBuilder.Entity<Ruta>(Ruta =>
            {
                Ruta.HasKey(e => e.Id);
            });

            // Se configura la tabla de Boleto
            modelBuilder.Entity<Boleto>(Boleto =>
            {
                Boleto.HasKey(e => e.Id);
            });


            // Yo tengo muchas rutas con un usuario que de identifica por el UsuarioId
            modelBuilder.Entity<Ruta>().HasOne(x => x.Usuario).WithMany(e => e.Rutas).HasForeignKey(r => r.UsuarioId);

            // Yo tengo muchos vehiculos con un usuario que se identifica por el UsuarioId
            modelBuilder.Entity<Vehiculo>().HasOne(x => x.Usuario).WithMany(e => e.Vehiculos).HasForeignKey(r => r.UsuarioId);

            // Yo tengo muchos boletos con una ruta que se identifica por el RutaId
            modelBuilder.Entity<Boleto>().HasOne(x => x.Ruta).WithMany(e => e.Boletos).HasForeignKey(r => r.RutaId);

            // Yo tengo muchos boletos con un usuario que se identifica por el UsuarioId
            modelBuilder.Entity<Boleto>().HasOne(x => x.Usuario).WithMany(e => e.Boletos).HasForeignKey(r => r.UsuarioId).OnDelete(DeleteBehavior.NoAction);

            // Yo tengo muchos boletos con un vehiculo que se identifica por el VehiculoId
            modelBuilder.Entity<Boleto>().HasOne(x => x.Vehiculo).WithMany(e => e.Boletos).HasForeignKey(r => r.VehiculoId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
