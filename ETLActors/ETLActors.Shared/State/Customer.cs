using System;
using System.Collections.Generic;

namespace ETLActors.Shared.State
{
    public class Customer : IIdentifiable {
        public Customer(Guid id)
        {
            Id = id;
        }

        public Customer() : this(Guid.NewGuid())
        {
        }

        public Guid Id { get; private set; }

        // should this setter be public? use other methods to change list?
        public IList<Order> Orders { get; set; }
    }
}