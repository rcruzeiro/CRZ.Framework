using System;

namespace CRZ.Framework.Domain
{
    public interface IValueObject
    {
        int Id { get; }

        DateTimeOffset CreatedAt { get; }
    }

    public interface IValueObject<T>
        where T : struct
    {
        T Id { get; }

        DateTimeOffset CreatedAt { get; }
    }
}
