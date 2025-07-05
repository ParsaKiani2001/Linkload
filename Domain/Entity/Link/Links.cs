using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;
using Domain.Entity.Users;
using Domain.Enums;

namespace Domain.Entity.Link
{
    public class Links:BaseEntity<Guid>
    {
        public string AccessCode { get; set; }
        public string Text { get; set; }
        public string? password { get; set; }
        public DateTime? ExpireTime { get; set; }
        public LinkMode Mode { get; set; } = LinkMode.privated;
        public bool IsSecurted { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UsreId")]
        public virtual User User { get; set; }
    }
}
