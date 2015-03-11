using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Akka.Actor;
using ETLActors.Shared.Commands;
using ETLActors.Shared.State;
using Faker;
using Faker.Helpers;
using Address = ETLActors.Shared.State.Address;

namespace ETLActors.Actors
{
    class MessageGeneratorActor : UntypedActor
    {
        private ActorRef _publisherActor;
        private CancellationTokenSource _publishTask;
        private Fake<Payment> _fakePayment;
        private Fake<Pageview> _fakePageview;

        public MessageGeneratorActor(ActorRef publisherActor)
        {
            _publisherActor = publisherActor;
            _publishTask = new CancellationTokenSource();
        }

        protected override void OnReceive(object message)
        {
            // send fake data into the message bus
            //_publisherActor.Tell(new CapturePayment(new Payment()));
            _publisherActor.Tell(new LogPageview(new Pageview("127.0.0.1", new Laptop())));


        }

        public readonly static IList<ProductTypes> AllProductTypes = Enum.GetValues(typeof(ProductTypes)).Cast<ProductTypes>().ToList();
        public readonly static IList<String> Streets = new List<string>() {"Granville", "Barrington", "Barry"};
        public readonly static IList<String> Cities = new List<string>() { "Los Angeles", "Santa Monica", "San Diego", "San Francisco" };
        public readonly static IList<String> States = new List<string>() { "CA", "VA", "NY", "VT" };
        public readonly static IList<String> Counties = new List<string>() { "US", "RU", "CA" };





        public static Address MakeAddress()
        {
            return new Address(
                Faker.Generators.Names.FullName(),
                Streets.GetRandom(),
                Cities.GetRandom(),
                States.GetRandom(),
                Faker.Generators.Numbers.Int(10000, 99999).ToString(),
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

        public static Order MakeOrder()
        {
            var order = new Order(MakeAddress(), MakeProduct());
            order.Payment = MakePayment(order.Id);
            return order;
        }

        public static Payment MakePayment(Guid orderId)
        {
            var payment = new Payment(Faker.Generators.Numbers.Decimal(0, 5000), orderId);
            return payment;
        }

        protected override void PreStart()
        {
            _fakePayment = new Fake<Payment>()
                .SetProperty(payment => payment.Amount, () => Faker.Generators.Numbers.Decimal(0, 5000));


            Context.System.Scheduler.Schedule(TimeSpan.FromMilliseconds(30), TimeSpan.FromMilliseconds(30), Self, "message");
        }

        protected override void PostStop()
        {
            _publishTask.Cancel();
            base.PostStop();
        }
    }
}
