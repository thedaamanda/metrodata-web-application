using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

[Table("tb_m_educations")]
public class Education
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Column("major"), MaxLength(100)]
    public string Major { get; set; }
    [Column("degree"), MaxLength(10)]
    public string Degree { get; set; }
    [Column("gpa"), MaxLength(5)]
    public string GPA { get; set; }
    [Column("university_id")]
    public int UniversityId { get; set; }

    // Cardinality
    public University? University { get; set; }
    public Profiling? Profiling { get; set; }
}
