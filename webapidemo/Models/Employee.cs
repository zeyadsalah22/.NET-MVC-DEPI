using System.ComponentModel.DataAnnotations;

namespace webapidemo.Models
{
	public class Employee
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		public string Job { get; set; }
		[Required]
		[Range(0, 50000)]
		public decimal Salary { get; set; }
	}
}
