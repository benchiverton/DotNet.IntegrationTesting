using IntegrationTesting.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTesting.Data.EntityFramework;

public class BusinessEventDbContext : DbContext
{
    public DbSet<BusinessEvent> BusinessEvents { get; set; }

    public BusinessEventDbContext(DbContextOptions<BusinessEventDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BusinessEvent>()
            .HasKey(be => be.EventId);
        modelBuilder.Entity<BusinessEvent>()
            .HasIndex(be => be.BusinessEntityId);
        modelBuilder.Entity<BusinessEvent>()
            .Property(be => be.EventType)
            .IsRequired();
        modelBuilder.Entity<BusinessEvent>()
            .Property(be => be.EventDetails)
            .IsRequired();
        modelBuilder.Entity<BusinessEvent>()
            .Property(be => be.CreatedUtc)
            .IsRequired();
    }
}
