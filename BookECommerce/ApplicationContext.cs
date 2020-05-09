using Microsoft.EntityFrameworkCore;

namespace BookECommerce.Models
{
    public class ApplicationContext : DbContext 
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base (options) { }

        //Tables
        public DbSet<Register> Registers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<RequestItem> RequestItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
