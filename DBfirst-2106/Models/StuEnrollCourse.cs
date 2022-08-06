using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBfirst_2106.Models

{
    public partial class StuEnrollCourse
    {



        [Column("s_id")]
        [Display(Name = "Roll Number")]
        public int? SId { get; set; }
        [Column("courseid")]
        [Display(Name = "Course Code")]
        public int? Courseid { get; set; }
        [Column("grade", TypeName = "decimal(18, 2)")]
        [Display(Name = "CGPA")]
        [ForeignKey("SId")]
        [InverseProperty("StuEnrollCourses")]
        public decimal? Grade { get; set; }
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Courseid")]
        [InverseProperty("StuEnrollCourses")]
        public virtual Course? Course { get; set; }
        [ForeignKey("SId")]
        [InverseProperty("StuEnrollCourses")]
        public virtual Student1? SIdNavigation { get; set; }

    }
}
