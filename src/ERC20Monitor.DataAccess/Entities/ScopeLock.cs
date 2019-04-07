using System;
using System.ComponentModel.DataAnnotations;

namespace DLTech.ERC20Monitor.DataAccess.Entities
{
	public class ScopeLock
	{
		[Required]
		[StringLength(16)]
		public string Id { get; set; }

		[Required]
		public DateTimeOffset LastLockedAt { get; set; }
	}
}
