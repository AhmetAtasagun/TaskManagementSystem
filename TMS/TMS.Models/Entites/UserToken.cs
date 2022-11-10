using TMS.Models.Entites.Base;

namespace TMS.Models.Entites
{
    public class UserToken : BaseEntity
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpireDate { get; set; }
    }
}
