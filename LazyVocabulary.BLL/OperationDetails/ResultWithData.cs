namespace LazyVocabulary.BLL.OperationDetails
{
    public class ResultWithData<T> : Result
    {
        public T ResultData { get; set; }
    }
}
