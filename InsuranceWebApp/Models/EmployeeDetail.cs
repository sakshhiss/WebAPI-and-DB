using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InsuranceWebApp.Models;

public partial class EmployeeDetail
{
    public int Id { get; set; }

    public string? EmpName { get; set; }

    public string? CompanyName { get; set; }

    public int? UserId { get; set; }

    [JsonIgnore]
    public virtual UserLoginDetail? User { get; set; }
}
