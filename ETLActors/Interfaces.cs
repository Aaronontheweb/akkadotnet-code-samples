using System;

namespace ETLActors
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