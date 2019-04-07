using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DLTech.ERC20Monitor.DataAccess;
using DLTech.ERC20Monitor.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLTech.ERC20Monitor.BusinessLogic
{
	public class MonitorService
	{
		private readonly IDatabaseContextFactory<DatabaseContext> _dbContextFactory;
		private byte[] _tipHash;
		private long _tipHeight;
		private List<byte[]> _history;

		public MonitorService(IDatabaseContextFactory<DatabaseContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
		}

		public async Task InitializeAsync(long initialHeight, byte historyDepth)
		{
			List<Block> lastBlocks = null;
			await _dbContextFactory.ExecuteAsync(async (DatabaseContext context) =>
			{
				lastBlocks = await context.Blocks
				.Where(x => x.Status == BlockStatus.Valid)
				.OrderByDescending(x => x.Height)
				.Take(historyDepth)
				.ToListAsync();
			});

			var tipBlock = lastBlocks.FirstOrDefault();
			if (tipBlock == null)
			{
				// TODO: no blocks in database we need to initialize from blockchain
			}

			_tipHeight = tipBlock.Height;
			_tipHash = tipBlock.Id;
			_history = lastBlocks.Select(x => x.Id).Reverse().ToList();
		}

		public async Task<CheckResult> CheckAsync(CancellationToken cancellationToken)
		{
			var result = new CheckResult();

			return result;
		}
	}
}
