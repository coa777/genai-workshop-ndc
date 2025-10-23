using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Authentication.Models;

namespace Authentication;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; } = null!;

    public DbSet<PostalAddress> PostalAddresses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("auth");

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Owner",
                NormalizedName = "OWNER"
            },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                NormalizedName = "USER"
            }
        );

        // Configure Company -> PostalAddress (one-to-many)
        builder.Entity<Company>(eb =>
        {
            eb.ToTable("Companies");
            eb.HasKey(c => c.Id);
            eb.Property(c => c.Name).IsRequired();
            eb.Property(c => c.VatNumber).IsRequired();
            eb.HasMany(c => c.Addresses).WithOne().HasForeignKey("CompanyId").IsRequired();
        });

        builder.Entity<PostalAddress>(eb =>
        {
            eb.ToTable("PostalAddresses");
            eb.HasKey(a => a.Id);
            eb.Property(a => a.StreetName).IsRequired();
            eb.Property(a => a.ZipCode).IsRequired();
            eb.Property(a => a.Number).IsRequired();
            eb.Property(a => a.Town).IsRequired();
            eb.Property(a => a.Country).IsRequired();
        });
    }
}
