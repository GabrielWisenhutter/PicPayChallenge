using PicPay.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        public Task<BaseResult> Handle(T command);
    }
}
