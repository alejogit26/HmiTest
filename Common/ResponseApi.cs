namespace Api.Common
{
    public struct ResponseApi
    {
        public int Code { get; }

        public string Message { get; }

        public string InnerMessage { get; }

        public ResponseApi(int code, string message, string detail)
        {
            Code = code;
            Message = message;
            InnerMessage = detail;
        }
    }
}
