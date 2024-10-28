using System.ComponentModel.DataAnnotations;

namespace Comment.API.Models;

public class CommentOutbox
{
    [Key]
    public Guid IdempotentToken { get; set; }
    public DateTime OccuredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public string Type { get; set; } = null!;
    public string Payload { get; set; } = null!;
}