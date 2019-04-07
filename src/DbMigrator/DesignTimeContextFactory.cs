using DLTech.ERC20Monitor.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DLTech.DbMigrator
{
	public class DesignTimeContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
	{
		public DatabaseContext CreateDbContext(string[] args)
		{
			IConfigurationRoot config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: true)
				.AddUserSecrets<Startup>()
				.AddEnvironmentVariables()
				.Build();

			string connString = config.GetConnectionString("Default");

			var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
			optionsBuilder.UseSqlServer(connString);

			var context = new DatabaseContext(optionsBuilder.Options);
			context.Database.SetCommandTimeout(System.TimeSpan.FromMinutes(10));
			return context;
		}
	}
}
