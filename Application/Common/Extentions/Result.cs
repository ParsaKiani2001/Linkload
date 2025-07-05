using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interface;
using Application.Models;
using Domain.Enums;
using Domain.Extentions;

namespace Application.Common.Extentions
{
    public abstract class Result
    {
        public async Task<IResponceBase> FalseOk(ResponceMessage message)
        {
            return new ResponceBaseModel() { Status = message, Message = message.GetDescription() };
        }
        public async Task<IResponceBase> FalseException(ResponceMessage message)
        {
            throw new Exception(message.GetDescription());
        }
        public async Task<IResponceBase> Ok(ResponceMessage message = 0)
        {
            if (message == ResponceMessage.Ok)
                return new ResponceBaseModel() { Message = ResponceMessage.Ok.GetDescription(), Status = message };
            return new ResponceBaseModel() { Status = message, Message = message.GetDescription() };

        }
        public async Task<IResponceBase> CustomOk(string message, ResponceMessage status)
        {
            return new ResponceBaseModel() { Status = status, Message = message };
        }
        public async Task<IResponceBase> CustomOk<T>(ResponceBaseModel<T> model)
        {
            return model;
        }
        public async Task<IResponceBase> UnHandledException(string message)
        {
            return new ResponceBaseModel() { Status = ResponceMessage.ServerError, Message = message };
        }
        public async Task<ResponceBaseModel<T>> TrueOk<T>(T data, ResponceMessage message = 0)
        {
            if (message == ResponceMessage.Ok)
                return new ResponceBaseModel<T>() { Data = data, Message = ResponceMessage.Ok.GetDescription(), Status = message };

            return new ResponceBaseModel<T>() { Data = data, Message = message.GetDescription(), Status = message };
        }
    }
}
