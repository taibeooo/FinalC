using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Report.Models;

namespace Report.Data
{
    public class ReportContext : DbContext
    {
        public ReportContext (DbContextOptions<ReportContext> options)
            : base(options)
        {
        }

        public DbSet<Report.Models.User> User { get; set; } = default!;

        public DbSet<Report.Models.Category>? Category { get; set; }

        public DbSet<Report.Models.Rent_Buy>? Rent_Buy { get; set; }
    }
}
