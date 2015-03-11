using System;

namespace ETLActors
{
    public enum ProductTypes { Laptop, Desktop, Phone, Watch, Tablet }

    public class ProductType : IIdentifiable
    {
        public ProductType(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        
    }

    public class Laptop : ProductType {
        public Laptop(Guid id) : base(id) {}
        public Laptop() : base(Guid.NewGuid()) { }
    }

    public class Desktop : ProductType
    {
        public Desktop(Guid id) : base(id) { }
        public Desktop() : base(Guid.NewGuid()) { }
    }

    public class Phone : ProductType
    {
        public Phone(Guid id) : base(id) { }
        public Phone() : base(Guid.NewGuid()) { }
    }

    public class Watch : ProductType
    {
        public Watch(Guid id) : base(id) { }
        public Watch() : base(Guid.NewGuid()) { }
    }

    public class Tablet : ProductType
    {
        public Tablet(Guid id) : base(id) { }
        public Tablet() : base(Guid.NewGuid()) { }
    }
}