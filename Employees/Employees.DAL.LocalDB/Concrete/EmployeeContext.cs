using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Employees.DAL.LocalDB.Concrete
{
    public class EmployeeContext : DbContext, Employees.Abstract.IDALContext
    {
        public DbSet<Employees.Entities.Employee> Employees { get; set; }

        public DbSet<Employees.Entities.Department> Departments { get; set; }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}
