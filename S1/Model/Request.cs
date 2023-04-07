using System;
using System.Collections.Generic;

namespace S1.Model;

public partial class Request
{
    public int Id { get; set; }

    public string? DateStart { get; set; }

    public string? DateEnd { get; set; }

    public string? Purpose { get; set; }

    public int? GuestId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual User? Employee { get; set; }

    public virtual User? Guest { get; set; }
}
