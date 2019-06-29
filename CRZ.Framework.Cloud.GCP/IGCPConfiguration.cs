namespace CRZ.Framework.Cloud.GCP
{
    public interface IGCPConfiguration
    {
        string ProductId { get; }

        string ComputeZone { get; }
    }
}
