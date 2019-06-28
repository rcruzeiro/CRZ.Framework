using System;

namespace CRZ.Framework.Cache
{
    public interface ICacheService : IDisposable
    {
        TimeSpan ExpirationTime { get; }

        bool Exists(string keyName);

        T GetValue<T>(string keyName)
            where T : class;

        void SetValue<T>(string keyName, T value, TimeSpan? timeToExpire = null)
            where T : class;

        void RemoveKey(string keyName);

        void Flush();
    }
}
