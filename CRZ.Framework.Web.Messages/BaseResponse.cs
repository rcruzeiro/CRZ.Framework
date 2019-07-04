namespace CRZ.Framework.Web.Messages
{
    public abstract class BaseResponse<T>
        where T : class
    {
        public T Data { get; }
    }
}
