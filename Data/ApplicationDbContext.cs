using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly DbContextOptions _context;
        public ApplicationDbContext(DbContextOptions context) : base(context)
        {
            _context = context;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Group>()
                .HasOne(g => g.Parent)
                .WithMany(g => g.SubGroups)
                .HasForeignKey(g => g.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<server.Models.Group> Groups { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}