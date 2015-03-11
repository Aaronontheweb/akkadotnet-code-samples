using ETLActors.Shared.State;

namespace ETLActors.Shared.Commands
{
    public class BaseMessage
    {
    }

    public enum MessageTypes { CreateOrder, CancelOrder}

    #region Order messages
    /// <summary>
    /// Base class for <see cref="Order"/>-related messages.
    /// </summary>
    public class OrderMessage : BaseMessage
    {
        public OrderMessage(Order order)
        {
            Order = order;
        }

        public Order Order { get; private set; }
    }

    /// <summary>
    /// Signal to create an <see cref="Order"/>
    /// </summary>
    public class CreateOrder : OrderMessage
    {
        public CreateOrder(Order order) : base(order)
        {
        }
    }

    /// <summary>
    /// Signal to cancel an <see cref="Order"/>
    /// </summary>
    public class CancelOrder : OrderMessage
    {
        public CancelOrder(Order order) : base(order)
        {
        }
    }

    #endregion
}
