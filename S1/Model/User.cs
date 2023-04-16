using System;
using System.Collections.Generic;

namespace S1.Model;

public partial class User
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Mail { get; set; }

    public string? PhoneNumber { get; set; }

    public int? RoleId { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? ThirdName { get; set; }

    public string? Company { get; set; }

    public string? Note { get; set; }

    public int? PassportSeries { get; set; }

    public int? PassportNumber { get; set; }

    public byte[]? Photo { get; set; }

    public string? BirthDate { get; set; }

    public int? DepartmentId { get; set; }

    public string? EmployeeCode { get; set; }

    public bool? IsBlacklisted { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Request> RequestEmployees { get; } = new List<Request>();

    public virtual ICollection<Request> RequestGuests { get; } = new List<Request>();

    public virtual Role? Role { get; set; }
}
