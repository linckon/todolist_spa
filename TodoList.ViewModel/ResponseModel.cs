using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.ViewModel
{
    public class ResponseModel
    {
        public ResponseModel(object data = null, bool isSuccess = true, string message = "Success", Exception exception = null)
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
           Exception = exception;
        }

        public object Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
