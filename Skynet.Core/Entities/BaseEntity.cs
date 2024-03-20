using System.ComponentModel.DataAnnotations;

namespace Skynet.Core.Entities;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
