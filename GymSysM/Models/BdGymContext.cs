using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GymSysM.Models
{
    public partial class BdGymContext : DbContext
    {
        public BdGymContext()
        {
        }

        public BdGymContext(DbContextOptions<BdGymContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actividad> Actividad { get; set; }
        public virtual DbSet<CategoriaEmpleado> CategoriaEmpleado { get; set; }
        public virtual DbSet<Clase> Clase { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Matricula> Matricula { get; set; }
        public virtual DbSet<Planilla> Planilla { get; set; }
        public virtual DbSet<Sala> Sala { get; set; }
        public virtual DbSet<SesionUva> SesionUva { get; set; }
        public virtual DbSet<Subscripcion> Subscripcion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-C2F1F6H\\SQLEXPRESS;Database=BdGym;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.HasKey(e => e.IdActividad);

                entity.Property(e => e.IdActividad).HasColumnName("idActividad");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CategoriaEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaEmpleado)
                    .HasName("PK_CategoriaEmpleado_1");

                entity.Property(e => e.IdCategoriaEmpleado).HasColumnName("idCategoriaEmpleado");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50);

                entity.Property(e => e.SalarioBase)
                    .HasColumnName("salarioBase")
                    .HasColumnType("money");

                entity.Property(e => e.SalarioHora)
                    .HasColumnName("salarioHora")
                    .HasColumnType("money");
            });

            modelBuilder.Entity<Clase>(entity =>
            {
                entity.HasKey(e => e.IdClase);

                entity.Property(e => e.IdClase).HasColumnName("idClase");

                entity.Property(e => e.Capacidad).HasColumnName("capacidad");

                entity.Property(e => e.Dia)
                    .IsRequired()
                    .HasColumnName("dia")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.HoraFin).HasColumnName("horaFin");

                entity.Property(e => e.HoraInicio).HasColumnName("horaInicio");

                entity.Property(e => e.IdActividad).HasColumnName("idActividad");

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.IdSala).HasColumnName("idSala");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.Clase)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clase_Actividad");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Clase)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clase_Empleado");

                entity.HasOne(d => d.IdSalaNavigation)
                    .WithMany(p => p.Clase)
                    .HasForeignKey(d => d.IdSala)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clase_Sala");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnName("apellidos")
                    .HasMaxLength(50);

                entity.Property(e => e.CantSesionesUva).HasColumnName("cantSesionesUVA");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnName("correo")
                    .HasColumnType("xml");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnName("direccion")
                    .HasMaxLength(50);

                entity.Property(e => e.Dni)
                    .IsRequired()
                    .HasColumnName("DNI")
                    .HasMaxLength(50);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("fechaNacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.FechaRenovacion)
                    .HasColumnName("fechaRenovacion")
                    .HasColumnType("date");

                entity.Property(e => e.FechaSubscripcion)
                    .HasColumnName("fechaSubscripcion")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IdSubscripcion).HasColumnName("idSubscripcion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50);

                entity.Property(e => e.SesionesUVAdisp)
                    .HasColumnName("sesionesUVAdisp")
                    .HasColumnType("int")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasColumnName("telefono")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdSubscripcionNavigation)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.IdSubscripcion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente_Subscripcion");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado);

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnName("apellidos")
                    .HasMaxLength(50);

                entity.Property(e => e.Correo)
                    .HasColumnName("correo")
                    .HasColumnType("xml");

                entity.Property(e => e.CuentaIban)
                    .IsRequired()
                    .HasColumnName("cuentaIBAN")
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnName("direccion")
                    .HasMaxLength(50);

                entity.Property(e => e.Dni)
                    .IsRequired()
                    .HasColumnName("DNI")
                    .HasMaxLength(50);

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("fechaNacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.IdCategoriaEmpleado).HasColumnName("idCategoriaEmpleado");

                entity.Property(e => e.NSeguroSocial)
                    .IsRequired()
                    .HasColumnName("nSeguroSocial")
                    .HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50);

                entity.Property(e => e.Telefono).HasColumnName("telefono");

                entity.HasOne(d => d.IdCategoriaEmpleadoNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdCategoriaEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empleado_CategoriaEmpleado");
            });

            modelBuilder.Entity<Matricula>(entity =>
            {
                entity.HasKey(e => e.IdMatricula);

                entity.Property(e => e.IdMatricula).HasColumnName("idMatricula");

                entity.Property(e => e.FechaHora)
                    .HasColumnName("fechaHora")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IdClase).HasColumnName("idClase");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.HasOne(d => d.IdClaseNavigation)
                    .WithMany(p => p.Matricula)
                    .HasForeignKey(d => d.IdClase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Matricula_Clase");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Matricula)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Matricula_Cliente");
            });

            modelBuilder.Entity<Planilla>(entity =>
            {
                entity.HasKey(e => e.IdPlanilla);

                entity.Property(e => e.IdPlanilla).HasColumnName("idPlanilla");

                entity.Property(e => e.Ccss)
                    .HasColumnName("CCSS")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.HorasTrabajadas)
                    .HasColumnName("horasTrabajadas")
                    .HasDefaultValueSql("((8))");

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.SalarioBruto)
                    .HasColumnName("salarioBruto")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SalarioHora)
                    .HasColumnName("salarioHora")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SalarioNeto)
                    .HasColumnName("salarioNeto")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Planilla)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Planilla_Empleado");
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.IdSala);

                entity.Property(e => e.IdSala).HasColumnName("idSala");

                entity.Property(e => e.Capacidad).HasColumnName("capacidad");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SesionUva>(entity =>
            {
                entity.HasKey(e => e.IdSesionUva);

                entity.ToTable("SesionUVA");

                entity.Property(e => e.IdSesionUva).HasColumnName("idSesionUVA");

                entity.Property(e => e.Duracion).HasColumnName("duracion");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.HoraInicio).HasColumnName("horaInicio");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.Tarifa)
                    .HasColumnName("tarifa")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.SesionUva)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SesionUVA_Cliente");
            });

            modelBuilder.Entity<Subscripcion>(entity =>
            {
                entity.HasKey(e => e.IdSubscripcion);

                entity.Property(e => e.IdSubscripcion).HasColumnName("idSubscripcion");

                entity.Property(e => e.CantidadSesionesUva).HasColumnName("cantidadSesionesUVA");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(50);

                entity.Property(e => e.Meses).HasColumnName("meses");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50);

                entity.Property(e => e.Tarifa)
                    .HasColumnName("tarifa")
                    .HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
