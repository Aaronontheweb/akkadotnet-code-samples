using System;

namespace ETLActors.Shared.State
{
    public enum ProductTypes { Laptop, Desktop, Phone, Watch, Tablet }

    public class Product : IIdentifiable
    {
        public Product(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        
    }

    public class Laptop : Product {
        public Laptop(Guid id) : base(id) {}
        public Laptop() : base(Guid.NewGuid()) { }
    }

    public class Desktop : Product
    {
        public Desktop(Guid id) : base(id) { }
        public Desktop() : base(Guid.NewGuid()) { }
    }

    public class Phone : Product
    {
        public Phone(Guid id) : base(id) { }
        public Phone() : base(Guid.NewGuid()) { }
    }

    public class Watch : Product
    {
        public Watch(Guid id) : base(id) { }
        public Watch() : base(Guid.NewGuid()) { }
    }

    public class Tablet : Product
    {
        public Tablet(Guid id) : base(id) { }
        public Tablet() : base(Guid.NewGuid()) { }
    }
}