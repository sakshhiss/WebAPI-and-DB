using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InsuranceWebApp.Models;

public partial class PolicyDetail
{
    public int PolicyId { get; set; }

    public string? PolicyType { get; set; }

    public string? CoverageDetails { get; set; }

    public decimal? PremiumAmount { get; set; }

    public int? UserId { get; set; }

    [JsonIgnore]
    public virtual UserLoginDetail? User { get; set; }
}
