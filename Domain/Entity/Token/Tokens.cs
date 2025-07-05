using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;
using Domain.Entity.Users;

namespace Domain.Entity.Token
{
    public class Tokens:BaseEntity<Guid>
    {
        public string Token { get; set; }
        public string Jwtid { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime ExpireRefreshToken {  get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public bool isUsed {  get; set; }


    }
}
