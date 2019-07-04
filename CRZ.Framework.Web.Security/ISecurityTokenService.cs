namespace CRZ.Framework.Web.Security
{
    public interface ISecurityTokenService<T>
        where T : class
    {
        string CreateToken(T data);
    }
}
