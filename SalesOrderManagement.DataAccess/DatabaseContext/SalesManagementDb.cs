using Microsoft.EntityFrameworkCore;
using SalesManagementApp.DataAccess.Entities;
using System.Collections.Generic;

namespace SalesManagementApp.DataAccess.DatabaseContext
{
    public class SalesManagementDb : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Window> Windows { get; set; }
        public DbSet<SubElement> SubElements { get; set; }
        public SalesManagementDb(DbContextOptions<SalesManagementDb> options) : base(options)
        {

        }

        public SalesManagementDb()
        {

        }
    }
}
