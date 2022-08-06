using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBfirst_2106.Models
{
    public partial class Course
    {
        public Course()
        {
            StuEnrollCourses = new HashSet<StuEnrollCourse>();
            TauCourses = new HashSet<TauCourse>();
        }

        [Key]
        [Column("courseid")]
        [Display(Name = "Course Code")]
        public int Courseid { get; set; }
        [Column("coursename")]
        [StringLength(50)]
        [Unicode(false)]
        [Display(Name = "Course Name")]
        public string Coursename { get; set; } = null!;

        [InverseProperty("Course")]
        public virtual ICollection<StuEnrollCourse> StuEnrollCourses { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<TauCourse> TauCourses { get; set; }
    }
}
