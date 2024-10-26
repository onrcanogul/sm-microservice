using System.ComponentModel.DataAnnotations.Schema;
using Shared.Base.Models;

namespace Chat.API.Models;

public class Chat : BaseEntity
{
    public Guid Id { get; set; }

    public Guid User1Id { get; set; } // İlk kullanıcı
    public Guid User2Id { get; set; } // İkinci kullanıcı
    
    public List<Message> Messages { get; set; } = new(); // Mesaj listesi
}