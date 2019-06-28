using System;

namespace CRZ.Framework.Domain
{
    public interface IEntity
    {
        int Id { get; }

        DateTimeOffset CreatedAt { get; }

        DateTimeOffset? UpdatedAt { get; }
    }
}
