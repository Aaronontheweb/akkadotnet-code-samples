﻿using Akka.Actor;
using Akka.Cluster.Sharding;
using Akka.Event;
using Akka.Persistence;
using SqlSharding.Shared;
using SqlSharding.Shared.Commands;
using SqlSharding.Shared.Events;
using SqlSharding.Shared.Queries;

namespace SqlSharding.Host.Actors;

/// <summary>
/// Manages the state for a given product.
/// </summary>
public sealed class ProductTotalsActor : ReceivePersistentActor
{
    public static Props GetProps(string persistenceId)
    {
        return Props.Create(() => new ProductTotalsActor(persistenceId));
    }
    
    /// <summary>
    /// Used to help differentiate what type of entity this is inside Akka.Persistence's database
    /// </summary>
    public const string TotalsEntityNameConstant = "totals";
    private readonly ILoggingAdapter _log = Context.GetLogger();
    public override string PersistenceId { get; }
    // <ProductStateUsage>
    public ProductState State { get; set; }

    public ProductTotalsActor(string persistenceId)
    {
        PersistenceId = $"{TotalsEntityNameConstant}-" + persistenceId;
        State = new ProductState();
        
        Recover<SnapshotOffer>(offer =>
        {
            if (offer.Snapshot is ProductState state)
            {
                State = state;
            }
        });
        
        // rest of message processing
        // </ProductStateUsage>

        Recover<IProductEvent>(productEvent =>
        {
            State = State.ProcessEvent(productEvent);
        });

        Command<IProductCommand>(cmd =>
        {
            var response = State.ProcessCommand(cmd);
            var sentResponse = false;

            var sender = Sender;

            if (response.ResponseEvents.Any())
            {
                PersistAll(response.ResponseEvents, productEvent =>
                {
                    _log.Info("Processed: {0}", productEvent);
                
                    if (productEvent is ProductInventoryWarningEvent warning)
                    {
                        _log.Warning(warning.ToString());
                    }
                    State = State.ProcessEvent(productEvent);

                    if (!sentResponse) // otherwise we'll generate a response-per-event
                    {
                        sentResponse = true;
                        sender.Tell(response);
                    }
                
                    if(LastSequenceNr % 10 == 0)
                        SaveSnapshot(State);
                });
            }
            else
            {
                Sender.Tell(response);
            }
        });


        Command<SaveSnapshotSuccess>(success =>
        {
            
        });

        Command<FetchProduct>(fetch =>
        {
            Sender.Tell(new FetchResult(State));

            if (State.IsEmpty)
            {
                // we don't exist, so don't let `remember-entities` keep us alive
                Context.Parent.Tell(new Passivate(PoisonPill.Instance));
            }
        });
    }
}