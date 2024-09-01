using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.DTOs
{
    public interface ICommand
    {
        public void Validate();
    }
}
