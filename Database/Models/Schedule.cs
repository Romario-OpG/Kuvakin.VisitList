using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
	[Table("shedules", Schema = "models")]
	public class Schedule
	{
		[Column("id")]
		public long Id { get; set; }

		[Column("student_id")]
		public short StudentId { get; set; }

		[Column("date_of_lesson")]
		public DateTime DateOfLesson { get; set; }

		public Student Student { get; set; }
	}
}
