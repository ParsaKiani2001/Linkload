using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Common.Interface
{
    public interface IResponceBase
    {
        public ResponceMessage Status {  get; set; }
        public string Message { get; set; }
    }
}
