using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;

namespace Domain.Entity.Users
{
    public class Otp:BaseEntity<Guid>
    {
        public string code {  get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool IsUsed { get; set; }
    }
}
