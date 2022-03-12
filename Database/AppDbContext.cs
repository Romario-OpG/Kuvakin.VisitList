using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Database
{
	public class AppDbContext : DbContext
	{
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<Student> Students { get; set; }

		public AppDbContext()
		{
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("DefaultConnection");
			optionsBuilder.UseNpgsql(connectionString);
		}
	}
}
