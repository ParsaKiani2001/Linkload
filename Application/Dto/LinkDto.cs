using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Dto
{
    public class LinkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AccessCode { get; set; }
        public LinkMode Mode { get; set; }
        public bool IsSecure { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
