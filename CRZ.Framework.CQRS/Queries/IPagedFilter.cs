namespace CRZ.Framework.CQRS.Queries
{
    public interface IPagedFilter : IFilter
    {
        int Page { get; set; }

        int PageSize { get; set; }
    }
}
