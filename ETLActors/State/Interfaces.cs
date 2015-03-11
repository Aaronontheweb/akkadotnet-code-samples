using System;

namespace ETLActors.State
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