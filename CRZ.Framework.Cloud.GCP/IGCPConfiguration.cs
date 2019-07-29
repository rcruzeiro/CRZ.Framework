namespace CRZ.Framework.Cloud.GCP
{
    public interface IGCPConfiguration
    {
        string ProjectId { get; }

        string ComputeZone { get; }
    }
}
