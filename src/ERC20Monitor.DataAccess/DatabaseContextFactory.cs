using Microsoft.EntityFrameworkCore;

namespace DLTech.ERC20Monitor.DataAccess
{
	public class DatabaseContextFactory : IDatabaseContextFactory<DatabaseContext>
	{
		private readonly DbContextOptions<DatabaseContext> _options;

		public DatabaseContextFactory(string connString)
		{
			var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
			optionsBuilder.UseSqlServer(connString);

			_options = optionsBuilder.Options;
		}

		public DatabaseContext Create()
		{
			return new DatabaseContext(_options);
		}
	}
}
