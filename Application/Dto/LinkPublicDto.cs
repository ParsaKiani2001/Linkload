using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class LinkPublicDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AccessCode { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
