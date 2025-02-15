using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTOs
{
    public class ApiErrorDto
    {
        public ApiErrorDto(long timeStamp, string message, int errorCode)
        {
            TimeStamp = timeStamp;
            Message = message;
            ErrorCode = errorCode;
        }

        public ApiErrorDto(string message, int errorCode) : this(DateTimeOffset.UtcNow.ToUnixTimeSeconds(),message, errorCode)
        {
           
        }

        public long TimeStamp { get; }
        public string Message { get; }
        public int ErrorCode { get; }
    }
}
