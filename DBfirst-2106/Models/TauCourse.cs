using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBfirst_2106.Models
{
    public partial class TauCourse
    {
        [Column("p_id")]
        [Display(Name = "Professor ID")]
        public int? PId { get; set; }
        [Column("courseid")]
        [Display(Name = "Course Code")]
        public int? Courseid { get; set; }
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Courseid")]
        [InverseProperty("TauCourses")]
        public virtual Course? Course { get; set; }
        [ForeignKey("PId")]
        [InverseProperty("TauCourses")]
        public virtual Professor? PIdNavigation { get; set; }
    }
}
