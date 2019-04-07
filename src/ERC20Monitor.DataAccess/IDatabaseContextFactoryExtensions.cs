using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DLTech.ERC20Monitor.DataAccess
{
	public static class IDatabaseContextFactoryExtensions
	{
		public static async Task ExecuteAsync<T>(
			this IDatabaseContextFactory<T> contextFactory,
			Func<IDatabaseContextFactory<T>, Task> operation)
			where T : DbContext
		{
			using (var context = contextFactory.Create())
			{
				IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
				await strategy.ExecuteAsync(async () =>
				{
					await operation(contextFactory);
				});
			}
		}

		public static async Task ExecuteAsync<T>(
			this IDatabaseContextFactory<T> contextFactory,
			Func<T, Task> operation)
			where T : DbContext
		{
			using (var strategyContext = contextFactory.Create())
			{
				IExecutionStrategy strategy = strategyContext.Database.CreateExecutionStrategy();
				await strategy.ExecuteAsync(async () =>
				{
					using (var context = contextFactory.Create())
					{
						await operation(context);
					}
				});
			}
		}
	}
}
