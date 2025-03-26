using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ContactsManager.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Country> countries { get; set; }


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().ToTable("countries");
            modelBuilder.Entity<Person>().ToTable("Persons");

            //Fluent API
            modelBuilder.Entity<Person>().Property(temp => temp.TIN)
              .HasColumnName("TaxIdentificationNumber")
              .HasColumnType("varchar(8)")
              .HasDefaultValue("ABC12345");

            modelBuilder.Entity<Person>().ToTable(b => b
                .HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber]) = 8"));

        }

    }
}
