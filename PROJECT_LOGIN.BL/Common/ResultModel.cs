namespace MVC_test.BL.Common
{
    public class ResultModel<T>
    {
        public bool isSuccess { get; set; }

        public string Message { get; set; }

        public T value { get; set; }

    }
}