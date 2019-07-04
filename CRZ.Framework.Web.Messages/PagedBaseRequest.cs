namespace CRZ.Framework.Web.Messages
{
    public abstract class PagedBaseRequest : BaseRequest
    {
        public int Page { get; set; }

        public int PageSize { get; set; } = 10;
    }
}
