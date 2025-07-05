using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;

namespace Domain.Entity.Users
{
    public class UserPicture :BaseEntity<int>
    {
       public byte[] File {  get; set; }
       public string Link { get; set; }
       public virtual ICollection<User> Users { get; set; }
    }
}
