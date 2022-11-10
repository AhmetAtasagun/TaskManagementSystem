using System.ComponentModel.DataAnnotations;
using TMS.Models.Entites.Base;

namespace TMS.Models.Entites;

public class User : BaseEntity
{
    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(50)]
    public string Surname { get; set; }

    [StringLength(50)]
    public string Email { get; set; }

    [StringLength(200)]
    public string Password { get; set; }

    [StringLength(200)]
    public string PasswordSalt { get; set; }
}
