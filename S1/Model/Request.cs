﻿using System;
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

    public int? RequestTypeId { get; set; }

    public int? RequestStatusId { get; set; }

    public virtual User? Employee { get; set; }

    public virtual User? Guest { get; set; }

    public virtual RequestStatus? RequestStatus { get; set; }

    public virtual RequestType? RequestType { get; set; }
}
