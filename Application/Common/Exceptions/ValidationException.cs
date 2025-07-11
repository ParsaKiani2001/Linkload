﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interface;
using Application.Models;
using Domain.Extentions;
using FluentValidation.Results;

namespace Application.Common.Exceptions
{
    public class ValidationException
    {
        public static IResponceBase Validation(IEnumerable<ValidationFailure> failures)
        {
            ResponceBaseModel Validate = new ResponceBaseModel();
            var failureGroups = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage);
            Validate.Status = Domain.Enums.ResponceMessage.NoValidParametes;
            Validate.Message = Domain.Enums.ResponceMessage.NoValidParametes.GetDescription();
            var propertyFailure = failureGroups.ToArray();
            for (int i = 0; i < propertyFailure.Length; i++)
            {
                for (int x = 0; x < propertyFailure[i].Count(); x++)
                {
                    if (i != 0 || x != 0)
                        Validate.Message += " , ";

                    Validate.Message += propertyFailure[i].ToArray()[x].ToString();
                }
            }

            Validate.Message += " )";

            return Validate;
        }
    }
}
