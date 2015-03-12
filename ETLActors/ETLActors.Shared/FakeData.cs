using System;
using System.Collections.Generic;
using System.Linq;
using ETLActors.Shared.Commands;
using ETLActors.Shared.State;
using Faker.Generators;
using Faker.Helpers;

namespace ETLActors.Shared
{
    public static class FakeData
    {
        public readonly static IList<ProductTypes> AllProductTypes = Enum.GetValues(typeof(ProductTypes)).Cast<ProductTypes>().ToList();
        public readonly static IList<MessageTypes> AllMessageTypes = Enum.GetValues(typeof(MessageTypes)).Cast<MessageTypes>().ToList();

        public readonly static IList<string> Streets = new List<string>() {"Granville", "Barrington", "Barry"};
        public readonly static IList<string> Cities = new List<string>() { "Los Angeles", "Santa Monica", "San Diego", "San Francisco" };
        public readonly static IList<string> States = new List<string>() { "CA", "VA", "NY", "VT" };
        public readonly static IList<string> Counties = new List<string>() { "US", "RU", "CA" };
        public readonly static IList<string> ZipCodes = new List<string>(){ "90064", "90210", "90401", "92131" };

        public static Address MakeAddress()
        {
            return new Address(
                Names.FullName(),
                Streets.GetRandom(),
                Cities.GetRandom(),
                States.GetRandom(),
                ZipCodes.GetRandom(),
                Counties.GetRandom());
        }

        public static Product MakeProduct()
        {
            var chosenProduct = AllProductTypes.GetRandom();
            switch (chosenProduct)
            {
                case ProductTypes.Desktop: return new Desktop();
                case ProductTypes.Laptop: return new Laptop();
                case ProductTypes.Phone: return new Phone();
                case ProductTypes.Watch: return new Watch();
                case ProductTypes.Tablet: return new Tablet();
                default: return new Desktop();
            }
        }

        public static BaseMessage MakeMessage()
        {
            var chosenMsg = AllMessageTypes.GetRandom();
            switch (chosenMsg)
            {
                case MessageTypes.CreateOrder: return new CreateOrder(MakeOrder());
                case MessageTypes.CancelOrder: return new CancelOrder(MakeOrder());
                default: return new CreateOrder(MakeOrder());
            }
        }

        public static Order MakeOrder()
        {
            var order = new Order(MakeAddress(), MakeProduct());
            order.Payment = MakePayment(order.Id);
            return order;
        }

        public static Payment MakePayment(Guid orderId)
        {
            var payment = new Payment(Convert.ToDecimal(Math.Max(Numbers.Double(0, 2.99D), 0)), orderId);
            return payment;
        }
    }
}