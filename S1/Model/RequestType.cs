using System;
using System.Collections.Generic;

namespace S1.Model;

public partial class RequestType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Request> Requests { get; } = new List<Request>();
}
