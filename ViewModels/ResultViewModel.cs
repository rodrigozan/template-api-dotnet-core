namespace api.ViewModels
{
    public class ResultViewModel<T>
    {
        public ResultViewModel(string message, T data)
        {
            Message = message;
            Data = data;
        }

        public ResultViewModel(T data)
        {
            Data = data;

        }

        public string Message { get; set; }
        public T Data { get; private set; }

    }
}