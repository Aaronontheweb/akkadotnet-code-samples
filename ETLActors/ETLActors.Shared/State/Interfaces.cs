using System;

namespace ETLActors.Shared.State
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }

    public interface IDataEvent : IIdentifiable
    {
        long Timestamp { get; }
    }
}