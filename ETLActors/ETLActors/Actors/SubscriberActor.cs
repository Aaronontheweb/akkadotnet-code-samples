using System;
using Akka.Actor;
using Akka.Dispatch.SysMsg;
using Akka.Routing;
using ETLActors.Shared.Commands;

namespace ETLActors.Actors
{
    public class SubscriberActor : ReceiveActor
    {
        public SubscriberActor()
        {
            Ready();
        }

        private void Ready()
        {
            Receive<SubscribeToTopics>(sub =>
            {
                foreach (var type in sub.Types)
                {
                    Console.WriteLine("Subscribing {0} to messages of type {1}", sub.Subscriber, type);
                    Context.System.EventStream.Subscribe(sub.Subscriber, type);
                }

                //susbscribe to deathwatch
                Context.Watch(Sender);
            });

            Receive<UnsubscribeFromTopics>(unsub =>
            {
                foreach (var type in unsub.Types)
                {
                    Console.WriteLine("Unsubscribing {0} from messages of type {1}", unsub.Subscriber, type);
                    Context.System.EventStream.Unsubscribe(unsub.Subscriber, type);
                }
            });

            Receive<UnsusbscribeFromAll>(unsub =>
            {
                Console.WriteLine("Unsubscribing {0} from ALL messages", unsub.Subscriber);
                Context.Unwatch(unsub.Subscriber);
                Context.System.EventStream.Unsubscribe(unsub.Subscriber);
            });

            Receive<DeathWatchNotification>(deathWatch =>
            {
                Console.WriteLine("Unsubscribing {0} from ALL messages because DEATHWATCH.", deathWatch.Actor);
                Context.System.EventStream.Unsubscribe(deathWatch.Actor);
            });
        }
    }
}
