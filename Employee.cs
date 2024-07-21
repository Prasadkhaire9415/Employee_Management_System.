using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_ManageMent_System.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string Firstname { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string Lastname { get; set; }
        [DisplayName(" Date Of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("Email")]
        [Required]
        public string Email { get; set; }
        public int Salary { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return Firstname + " " + Lastname; }
        }

    }
}
