namespace RabbitHelper.ResponseModels
{
    public class BaseResponse<T>
    {
        public T Body { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
