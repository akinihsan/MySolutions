namespace Farm.API.Model.Base
{
    public class ApiResult<T>
    {
        public ApiResult(bool isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public ApiResult(bool isSuccess, string message, T data, string errorCode="", string errorMessage = "")
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;

            this.Errors.Add(new ErrorItem(errorCode, errorMessage));
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<ErrorItem> Errors { get; set; } = new List<ErrorItem>();

        public class ErrorItem
        {
            public ErrorItem(string code, string message)
            {
                Code = code;
                Message = message;
            }

            public string Code { get; set; }
            public string Message { get; set; }
        }
    }
}
