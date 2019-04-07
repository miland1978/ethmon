using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DLTech.ERC20Monitor.DataAccess
{
	public static class DbContextExtensions
	{
		public static void AdjustCase(this ModelBuilder modelBuilder)
		{
			foreach (var entity in modelBuilder.Model.GetEntityTypes())
			{
				entity.Relational().TableName = ToUnderscoreCase(entity.Relational().TableName);

				foreach (var property in entity.GetProperties())
				{
					property.Relational().ColumnName = ToUnderscoreCase(property.Name);
				}

				foreach (var key in entity.GetKeys())
				{
					key.Relational().Name = ToUnderscoreCase(key.Relational().Name);
				}

				foreach (var key in entity.GetForeignKeys())
				{
					key.Relational().Name = ToUnderscoreCase(key.Relational().Name);
				}

				foreach (var index in entity.GetIndexes())
				{
					index.Relational().Name = ToUnderscoreCase(index.Relational().Name);
				}
			}
		}

		private static string ToUnderscoreCase(string str)
		{
			string res = string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
			res = res.Replace("p_k_", "pk_");
			res = res.Replace("f_k_", "fk_");
			res = res.Replace("i_x_", "ix_");
			res = res.Replace("_i_d", "_id");
			res = res.Replace("__", "_");
			res = res.ToLowerInvariant();
			return res;
		}
	}
}
