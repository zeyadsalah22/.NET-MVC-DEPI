using Microsoft.EntityFrameworkCore;
using webapidemo.Models;

namespace webapidemo.Data
{
	public class AppDbContext : DbContext
	{
        public AppDbContext()
        {
            
        }

		public AppDbContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<Employee> Employees { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// optionsBuilder.UseSqlServer("Server=.; initial Catalog=webapicoredb; Integrated Security=True; Trust Server Certificate = True");
		}
	}
}
