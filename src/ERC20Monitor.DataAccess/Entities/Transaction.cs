using System;
using System.ComponentModel.DataAnnotations;

namespace DLTech.ERC20Monitor.DataAccess.Entities
{
	public class Transaction
	{
		[Required]
		[StringLength(32)]
		public byte[] Id { get; set; }

		[Required]
		[StringLength(20)]
		public byte[] From { get; set; }

		[Required]
		[StringLength(20)]
		public byte[] To { get; set; }

		[Required]
		public UInt64 Value { get; set; }
	}
}
