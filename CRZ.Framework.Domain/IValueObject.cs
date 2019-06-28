using System;

namespace CRZ.Framework.Domain
{
    public interface IValueObject
    {
        int Id { get; }

        DateTimeOffset CreatedAt { get; }
    }
}
