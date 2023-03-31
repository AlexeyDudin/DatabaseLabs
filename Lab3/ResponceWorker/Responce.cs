namespace Lab3.ResponceWorker
{
    public class Responce
    {
        public object Content { get; }
        public ResponceCode Code { get; }

        public Responce(object content, ResponceCode code)
        {
            Content = content;
            Code = code;
        }
    }
}
