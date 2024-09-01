using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.DTOs
{
    public class BaseResult
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public BaseResult()
        {
        }
        public BaseResult(int status, string? message)
        {
            Status = status;
            Message = message;
        }

    }
    public class Result<T> : BaseResult
    {

        public T? Data { get; set; }


        public Result()
        {
            
        }
        public Result(int status, string? message) : base(status, message)
        {

        }

        public Result(int status, string? message, T? data) : base(status, message)
        {
            Data = data;
        }


    }
}
