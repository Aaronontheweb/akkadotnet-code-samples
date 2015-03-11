﻿using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Dispatch.SysMsg;
using ETLActors.Shared.Commands;

namespace ETLActors.Actors
{
    class StatsByZipCoordinatorActor : ReceiveActor
    {
        // map zipCode => actor
        private Dictionary<String, ActorRef> _workers;

        public StatsByZipCoordinatorActor()
        {
            _workers = new Dictionary<string, ActorRef>();
            MakeReceives();
        }

        private void MakeReceives()
        {
            Receive<OrderMessage>(message =>
            {
                var worker = FindOrCreateZipActor(message.Order.Address.PostalCode);
                worker.Tell(message);
            });

            // sender of this zip code needs to be added to workers dict            
            Receive<StatsByZipWorkerActor.MyZip>(zip =>
            {
               _workers[zip.Zip] = Sender;
            });
        }

        private ActorRef FindOrCreateZipActor(String zipCode)
        {
            if (_workers.ContainsKey(zipCode))
            {
                // return worker 
                return _workers[zipCode];
            }
            else if (Context.Child(zipCode) == ActorRef.Nobody)
            {
                // create worker
                var worker = Context.ActorOf(Props.Create(() => new StatsByZipWorkerActor(zipCode)), zipCode);
                _workers[zipCode] = worker;
                return worker;
            }
            else
            {
                // worker exists, but not in our dictionary (e.g. after a crash)
                var worker = Context.Child(zipCode);
                _workers[zipCode] = worker;
                return worker;
            }
        }


        protected override void PreStart()
        {
            var children = Context.GetChildren();
            foreach (var child in children)
            {
                child.Tell(new StatsByZipWorkerActor.IdentifyZip());
            }
        }

        protected override void PreRestart(Exception reason, object message)
        {
            // override restart behavior so we don't blow away workers state
            PostStop();
        }
    }
}