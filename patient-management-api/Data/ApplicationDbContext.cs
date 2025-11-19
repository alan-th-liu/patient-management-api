using Microsoft.EntityFrameworkCore;
using patient_management_api.Models.Entities;

namespace patient_management_api.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Patient> Patient => Set<Patient>();
    public DbSet<PatientOrder> PatientOrder => Set<PatientOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("patient");
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.OrderId).HasColumnName("order_id");
        });

        modelBuilder.Entity<PatientOrder>(entity =>
        {
            entity.ToTable("patient_order");
            entity.Property(o => o.Message).IsRequired().HasMaxLength(2000);
        });
    }
}
