using Microsoft.EntityFrameworkCore;

namespace DLTech.ERC20Monitor.DataAccess
{
	public interface IDatabaseContextFactory<out T>
		where T : DbContext
	{
		T Create();
	}
}
