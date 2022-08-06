using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBfirst_2106.Models
{
    [Table("Professor")]
    public partial class Professor
    {
        public Professor()
        {
            TauCourses = new HashSet<TauCourse>();
        }

        [Key]
        [Column("p_id")]
        [Display(Name = "Profesor Id")]
        public int PId { get; set; }
        [Column("p_name")]
        [StringLength(50)]
        [Unicode(false)]
        [Display(Name = "Name")]
        public string PName { get; set; } = null!;
        [Column("p_dept")]
        [StringLength(50)]
        [Unicode(false)]
        [Display(Name = "Department")]
        public string PDept { get; set; } = null!;

        [InverseProperty("PIdNavigation")]
        public virtual ICollection<TauCourse> TauCourses { get; set; }
    }
}
