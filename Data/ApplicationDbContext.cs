using Home_Loan_App.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;//Install this package to inherit DbContext

namespace Home_Loan_App.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                
        }
        public DbSet<Customer> Customers { get; set; }//Creates a table (Categories)using Customer members

    }
}
 