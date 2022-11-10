using TMS.Core.Abstracts;

namespace TMS.Models.Entites.Base;

public class BaseEntity : IEntity
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
}

