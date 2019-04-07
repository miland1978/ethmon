using System;
using System.ComponentModel.DataAnnotations;

namespace DLTech.ERC20Monitor.DataAccess.Entities
{
	public class Block
	{
		[Required]
		[StringLength(32)]
		public byte[] Id { get; set; }

		[Required]
		public long Height { get; set; }

		[Required]
		public BlockStatus Status { get; set; }

		[Required]
		public DateTimeOffset BlockTime { get; set; }

		[Required]
		[StringLength(20)]
		public byte[] Miner { get; set; }

		[Required]
		public DateTimeOffset CreatedAt { get; set; }

		[Required]
		public DateTimeOffset LastUpdatedAt { get; set; }
	}
}
