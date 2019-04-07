using System;
using System.ComponentModel.DataAnnotations;

namespace DLTech.ERC20Monitor.DataAccess.Entities
{
	public class Transfer
	{
		[Required]
		[StringLength(32)]
		public byte[] TransactionId { get; set; }

		public Transaction Transaction { get; set; }

		[Required]
		public int Position { get; set; }

		[Required]
		[StringLength(20)]
		public byte[] From { get; set; }

		[Required]
		[StringLength(20)]
		public byte[] To { get; set; }

		[Required]
		public UInt64 Amount { get; set; }
	}
}
