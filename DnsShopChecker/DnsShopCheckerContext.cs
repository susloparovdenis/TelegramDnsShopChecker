using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnsShopChecker
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    class DnsShopCheckerContext : DbContext
    {
        public DbSet<ProductEntry> ProductEntries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductEntry>()
                .HasKey(p => p.Id)
                .Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
