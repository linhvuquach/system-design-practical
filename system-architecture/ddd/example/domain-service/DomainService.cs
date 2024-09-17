// Break down OrderProcessingService
// 1. Multiple domain concepts: 
// The service interacts with multiple domain concepts: Order, OrderItem, Customer, and Inventory. 
// This demonstrates how Domain Services can coordinate operations that span multiple aggregates or entities.



using System;
using System.Collections.Generic;
using System.Linq;

namespace DDD.Domain
{
    // Domain Entities and Value Objects
    public class Order
    {
        public Guid Id { get; private set; }
        public Customer Customer { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public decimal TotalAmount { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order(Customer customer, List<OrderItem> items)
        {
            Id = Guid.NewGuid();
            Customer = customer;
            Items = items;
            Status = OrderStatus.Pending;
        }

        public void SetTotalAmount(decimal amount)
        {
            TotalAmount = amount;
        }

        public void SetStatus(OrderStatus status)
        {
            Status = status;
        }
    }

    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public CustomerType Type { get; private set; }

        public Customer(string name, CustomerType type)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
        }
    }

    public class OrderItem
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public OrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }

    public class Inventory
    {
        public Guid ProductId { get; private set; }
        public int AvailableQuantity { get; private set; }

        public Inventory(Guid productId, int availableQuantity)
        {
            ProductId = productId;
            AvailableQuantity = availableQuantity;
        }

        public bool HasSufficientStock(int requiredQuantity)
        {
            return AvailableQuantity >= requiredQuantity;
        }

        public void ReduceStock(int quantity)
        {
            if (quantity > AvailableQuantity)
                throw new InvalidOperationException("Insufficient stock");

            AvailableQuantity -= quantity;
        }
    }

    public enum CustomerType
    {
        Regular,
        Premium
    }

    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Shipped,
        Cancelled
    }

    // Repository interfaces
    public interface IInventoryRepository
    {
        Inventory GetByProductId(Guid productId);
        void Update(Inventory inventory);
    }

    // Domain Service
    public class OrderProcessingService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public OrderProcessingService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public bool ProcessOrder(Order order)
        {
            // Check inventory and update stock
            foreach (var item in order.Items)
            {
                var inventory = _inventoryRepository.GetByProductId(item.ProductId);
                if (!inventory.HasSufficientStock(item.Quantity))
                {
                    order.SetStatus(OrderStatus.Cancelled);
                    return false;
                }
                inventory.ReduceStock(item.Quantity);
                _inventoryRepository.Update(inventory);
            }

            // Calculate total amount
            decimal total = order.Items.Sum(item => item.Quantity * item.UnitPrice);

            // Apply discount based on customer type
            if (order.Customer.Type == CustomerType.Premium)
            {
                total *= 0.9m; // 10% discount for premium customers
            }

            order.SetTotalAmount(total);
            order.SetStatus(OrderStatus.Confirmed);

            return true;
        }
    }

    // Usage example
    public class OrderManager
    {
        private readonly OrderProcessingService _orderProcessingService;

        public OrderManager(OrderProcessingService orderProcessingService)
        {
            _orderProcessingService = orderProcessingService;
        }

        public void PlaceOrder(Order order)
        {
            bool success = _orderProcessingService.ProcessOrder(order);
            if (success)
            {
                Console.WriteLine($"Order {order.Id} has been successfully processed.");
            }
            else
            {
                Console.WriteLine($"Order {order.Id} could not be processed due to insufficient inventory.");
            }
        }
    }
}