using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WarehouseReport.Api.Authentication;

namespace WarehouseReport.Api.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<CargoModel> Cargo { get; set; }
        public DbSet<ClientModel> Client { get; set; }
        public DbSet<ContainerModel> Container { get; set; }
        public DbSet<PackageModel> Package { get; set; }
        public DbSet<TransporterModel> Transporter { get; set; }

        public DbSet<RegisterModel> RegisterModels { get; set; }

        //Updated dbSet object
        // public DbSet<ToDoItemModel> ToDoItems { get; set; }
        //
        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     builder.Entity<ToDoItemModel>(entity =>
        //     {
        //         entity.Property(e => e.ItemName)
        //             .IsRequired()
        //             .HasMaxLength(100);
        //
        //         entity.Property(e => e.ItemDescription)
        //             .IsRequired()
        //             .HasMaxLength(100);
        //
        //         entity.Property(e => e.ItemStatus)
        //             .IsRequired()
        //             .HasMaxLength(1);
        //     });
        //
        //     base.OnModelCreating(builder);
        // }
    }
}