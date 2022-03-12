using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
	[Table("students", Schema = "models")]
	public class Student
	{
		[Column("id")]
		public short Id { get; set; }

		[Column("last_name")]
		public string LastName { get; set; }

		[Column("first_name")]
		public string FirstName { get; set; }

		[Column("middle_name")]
		public string MiddleName { get; set; }

		[Column("date_of_birth")]
		public DateTime DateOfBirth { get; set; }
	}
}
