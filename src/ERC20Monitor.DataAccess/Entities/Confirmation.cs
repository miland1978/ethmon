using System.ComponentModel.DataAnnotations;

namespace DLTech.ERC20Monitor.DataAccess.Entities
{
	public class Confirmation
	{
		[Required]
		[StringLength(32)]
		public byte[] TransactionId { get; set; }

		public Transaction Transaction { get; set; }

		[Required]
		[StringLength(32)]
		public byte[] BlockId { get; set; }

		public Block Block { get; set; }
	}
}
