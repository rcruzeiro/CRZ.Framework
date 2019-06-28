namespace CRZ.Framework.Cloud.AWS
{
    public interface IAWSConfiguration
    {
        string AccountName { get; }

        string SecretKey { get; }

        string AccessKey { get; }

        string DefaultRegion { get; }
    }
}
