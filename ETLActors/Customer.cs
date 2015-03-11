using System;
using System.Collections.Generic;

namespace ETLActors
{
    public class Customer : IIdentifiable {
        public Customer(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        // should this setter be public? use other methods to change list?
        public IList<Order> Orders { get; set; }
    }
}