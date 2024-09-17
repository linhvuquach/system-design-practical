## Anemic Domain Model

In an Anemic Domain Model, entities primarily contain only getters and setters, without encapsulating the business logic.

```c#
public class Order
{
    public Guid OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsPaid { get; set; }

    // Constructor
    // public Order(Guid orderId, DateTime orderDate, decimal totalAmount)
    // {
    //     OrderId = orderId;
    //     OrderDate = orderDate;
    //     TotalAmount = totalAmount;
    //     IsPaid = false;
    // }
}
```

## Rich Domain Model

A Rich Domain Model encapsulates both data and behavior within the domain entity. The entity is responsible for enforcing its own invariants and business rules.

```c#
public class Order
{
    public Guid OrderId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsPaid { get; private set; }

    // Constructor
    public Order(Guid orderId, DateTime orderDate, decimal totalAmount)
    {
        OrderId = orderId;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
        IsPaid = false;
    }

    // Business logic encapsulated inside the domain model
    public void MarkAsPaid()
    {
        if (IsPaid)
            throw new InvalidOperationException("Order is already paid.");

        IsPaid = true;
    }

    public void ApplyDiscount(decimal discount)
    {
        if (discount < 0 || discount > 100)
            throw new ArgumentException("Invalid discount.");

        TotalAmount = TotalAmount * ((100 - discount) / 100);
    }
}
```
