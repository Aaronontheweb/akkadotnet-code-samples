using System.Collections;
using System.Net;

namespace ETLActors
{
    #region Payment messages

    /// <summary>
    /// Base class for <see cref="Payment"/>-related messages.
    /// </summary>
    public class PaymentMessage
    {
        public Payment Payment { get; private set; }

        public PaymentMessage(Payment payment)
        {
            Payment = payment;
        }

    }

    /// <summary>
    /// Signal to refund a <see cref="Payment"/>.
    /// </summary>
    public class RefundPayment : PaymentMessage
    {
        public RefundPayment(Payment payment)
            : base(payment)
        {
        }
    }

    /// <summary>
    /// Signal to capture a <see cref="Payment"/>.
    /// </summary>
    public class CapturePayment : PaymentMessage
    {
        public CapturePayment(Payment payment)
            : base(payment)
        {
        }
    }
    #endregion

    #region View messages
    /// <summary>
    /// Base class for <see cref="View"/>-related messages.
    /// </summary>
    public class ViewMessage
    {
        public IPAddress IpAddress { get; private set; }
        public ProductType ProductType { get; private set; }

        public ViewMessage(IPAddress ipAddress, ProductType productType)
        {
            IpAddress = ipAddress;
            ProductType = productType;
        }
    }

    /// <summary>
    /// Signals to log a pageview for a given <see cref="ProductType" />.
    /// </summary>
    public class LogPageview : ViewMessage
    {
        public LogPageview(IPAddress ipAddress, ProductType productType) : base(ipAddress, productType)
        {
        }
    }
    #endregion

    #region Order messages
    /// <summary>
    /// Base class for <see cref="Order"/>-related messages.
    /// </summary>
    public class OrderMessage
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
