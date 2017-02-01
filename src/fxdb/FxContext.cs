using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fxdb.Models;
using Microsoft.EntityFrameworkCore;

namespace fxdb
{
    public class FxContext : DbContext
    {
        public FxContext(DbContextOptions<FxContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<EffectItem>()
                        .HasIndex(e => e.Id)
                        .IsUnique();
            modelBuilder.Entity<EffectItem>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();*/
        }
        public DbSet<EffectItem> EffectItems { get; set; }
    }
}
