using FlightSearchApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FlightSearchApp.Infrastructure
{
    public class FSADbContext : DbContext
    {
        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=dbFlightSearch;TrustServerCertificate=true;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        #endregion

        #region Dbset

        public DbSet<CodeList> CodeLists { get; set; }
        public DbSet<FlightOffer> FlightOffers { get; set; }

        #endregion
    }
}
