﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

[Table("tb_m_accounts")]
public class Account
{
    [Key, Column("employee_nik", TypeName = "nchar(5)")]
    public string EmployeeNIK { get; set; }
    [Column("password"), MaxLength(255)]
    public string Password { get; set; }

    // Cardinality
    public Employee? Employee { get; set; }
    public ICollection<AccountRole>? AccountRoles { get; set; }
}
