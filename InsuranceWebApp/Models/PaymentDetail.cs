using InsuranceWebApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

public class PaymentDetail
{
    public int PaymentId { get; set; }
    public string? CardOwnerName { get; set; }
    public string? CardNumber { get; set; }
    public string? SecurityCode { get; set; }
    public string? ValidThrough { get; set; }
    public int? UserId { get; set; } 

    [JsonIgnore]
    public virtual UserLoginDetail? User { get; set; }
}
