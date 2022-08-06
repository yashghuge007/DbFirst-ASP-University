using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBfirst_2106.Models
{
    [Table("Student1")]
    public partial class Student1
    {
        public Student1()
        {
            StuEnrollCourses = new HashSet<StuEnrollCourse>();
        }

        [Key]
        [Column("s_id")]
        public int SId { get; set; }
        [Column("s_name")]
        [StringLength(50)]
        [Unicode(false)]
        [Display(Name = "Name")]
        public string SName { get; set; } = null!;
        [Column("s_homecity")]
        [StringLength(50)]
        [Unicode(false)]
        [Display(Name = "Home Town")]
        public string SHomecity { get; set; } = null!;
        [Column("s_dept")]
        [StringLength(50)]
        [Unicode(false)]
        [Display(Name = "Department")]
        public string SDept { get; set; } = null!;

        [InverseProperty("SIdNavigation")]
        public virtual ICollection<StuEnrollCourse> StuEnrollCourses { get; set; }
    }
}
