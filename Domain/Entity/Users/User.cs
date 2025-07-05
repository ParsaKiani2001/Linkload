using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;
using Domain.Entity.Token;

namespace Domain.Entity.Users
{
    public class User:BaseEntity<Guid>
    {
        public User() {
            this.IsActive = false;
        }
        public string Name {  get; set; }
        public string Family { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int? ProfilePicId { get; set; }
        [ForeignKey("ProfilePicId")]
        public virtual UserPicture? ProfilePic { get; set; }
        public virtual ICollection<Tokens> Tokens { get; set; }
        public virtual ICollection<Otp> Otps { get; set; }
        
    }
}
