﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interface;
using Domain.Enums;

namespace Application.Models
{
    public class ResponceBaseModel : IResponceBase
    {
        public ResponceMessage Status { get; set; }
        public string Message { get; set; }

    }

    public class ResponceBaseModel<T> : IResponceBase
    {
        public ResponceMessage Status { get; set; }
        public string Message { get; set; }

        public T Data { get; set; }
    }
}
