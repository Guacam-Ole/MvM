using System;

namespace MvM.Uploader.Backend.Web.Models
{
    public class ActionResponse<T>
    {
        public enum ErrorCodes
        {
            WrongCode,
            WrongId,
            Unknown,
            Success,
            Exception
        }

        public T Value { get; set; }
        public Exception Exception { get; set; }
        public bool Success { get; set; }
        public ErrorCodes ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

     

        public  ActionResponse(T returnvalue)
        {
            Success = true;
            Value = returnvalue;
            ErrorCode = ErrorCodes.Success;
        }

        public ActionResponse(ErrorCodes code, string message = null)
        {
            Success = false;
            ErrorCode = code;
            ErrorMessage = message;
        }

        public ActionResponse(Exception exception, string message = null)
        {
            Success = false;
            Exception = exception;
            ErrorMessage = message ?? exception.Message;
            ErrorCode =  ErrorCodes.Exception;
        }
    }
}
