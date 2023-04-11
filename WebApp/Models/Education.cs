﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

[Table("tb_m_educations")]
public class Education
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Column("major", TypeName = "varchar(100)")]
    public string Major { get; set; }
    [Column("degree", TypeName = "varchar(10)")]
    public string Degree { get; set; }
    [Column("gpa", TypeName = "decimal(3,2)")]
    public double GPA { get; set; }
    [Column("university_id")]
    public int UniversityId { get; set; }

    // Cardinality
    public University? University { get; set; }
    public Profiling? Profiling { get; set; }
}
