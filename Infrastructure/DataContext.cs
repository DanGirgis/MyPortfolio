using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().Property(p => p.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<ProtflioItem>().Property(p => p.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Owner>().HasData(
                new Owner()
                {
                    Id = Guid.NewGuid(),
                    Avatar= "Dan Girgis.jpg",
                    FullName="Dan Girgis",
                    Profil="Microsoft MVP / .Net Consultant"
                });
        }
        public DbSet<Owner> owners { get; set; }
        public DbSet<ProtflioItem> protflioItems { get; set; }
    }
}