namespace DBfirst_2106.Models
{
    public class VM1
    {
        public List<StuEnrollCourse> Courseids { get; set; }
        public List<TauCourse> Profs { get; set; }

        public TauCourse profname { get; set; }

        public StuEnrollCourse stucourseid { get; set; }
    }
}
