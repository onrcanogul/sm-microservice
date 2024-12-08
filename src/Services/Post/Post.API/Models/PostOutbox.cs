using System.ComponentModel.DataAnnotations;

namespace Post.API.Models;

public class PostOutbox 
{
    [Key]
    public Guid IdempotentToken { get; set; }
    public DateTime OccuredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public string Type { get; set; } = null!;
    public string Payload { get; set; } = null!;
}