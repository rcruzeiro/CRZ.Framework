using System;

namespace CRZ.Framework.Cache
{
    public interface ICacheConfiguration
    {
        string Endpoint { get; }

        int Database { get; }

        TimeSpan ExpirationTime { get; }
    }
}
