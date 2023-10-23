namespace CodeCommApi.Response
{
    public class DefaultResponse<T>
    {
        public bool Status { get; set; }=false;
        public string ResponseCode { get; set; }="99";
        public string ResponseMessage { get; set; }="Execution Failed";
        public T Data { get; set; }
    }
}
