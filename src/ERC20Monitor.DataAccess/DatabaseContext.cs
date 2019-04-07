using System;
using DLTech.ERC20Monitor.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLTech.ERC20Monitor.DataAccess
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions)
			: base(dbContextOptions)
		{
		}

		public DbSet<ScopeLock> ScopeLocks { get; set; }

		public DbSet<Block> Blocks { get; set; }

		public DbSet<Transaction> Transactions { get; set; }

		public DbSet<Confirmation> Confirmations { get; set; }

		public DbSet<Transfer> Transfers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			SetupSchema(modelBuilder);

			SeedData(modelBuilder);

			modelBuilder.AdjustCase();
		}

		private void SetupSchema(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Block>()
				.HasIndex(x => new { x.Status, x.Height });

			modelBuilder.Entity<Confirmation>()
				.HasKey(x => new { x.TransactionId, x.BlockId });

			modelBuilder.Entity<Transaction>()
				.HasIndex(x => x.From);

			modelBuilder.Entity<Transaction>()
				.HasIndex(x => x.To);

			modelBuilder.Entity<Transfer>()
				.HasKey(x => new { x.TransactionId, x.Position });

			modelBuilder.Entity<Transfer>()
				.HasIndex(x => x.From);

			modelBuilder.Entity<Transfer>()
				.HasIndex(x => x.To);
		}

		private void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ScopeLock>()
				.HasData(
					new ScopeLock { Id = "blocks", LastLockedAt = new DateTimeOffset(new DateTime(2019, 4, 7, 12, 0, 0, DateTimeKind.Utc)) }
				);
		}
	}
}
