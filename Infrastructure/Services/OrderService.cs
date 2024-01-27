using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<DeliveryMethod> _deliveryRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IBasketRepository _basketRepo;

        public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<DeliveryMethod> deliveryRepo,
            IGenericRepository<Product> productRepo, IBasketRepository basketRepo)
        {
            _orderRepository = orderRepository;
            _deliveryRepo = deliveryRepo;
            _productRepo = productRepo;
            _basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId,
            Address address)
        {
            //get basket from repo
            var basket = await _basketRepo.GetBasketAsync(basketId);
            
            //get items from product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items) 
            {
                var prodItems = await _productRepo.GetByIdAsync(item.Id);
                var itemsOrdered = new ProductItemOrdered(prodItems.Id, prodItems.Name, prodItems.PictureUrl);
                var orderItem = new OrderItem(itemsOrdered, prodItems.Price, item.Quantity);
                items.Add(orderItem);
            }

            //get delivery method from repo
            var deliveryMethod = await _deliveryRepo.GetByIdAsync(deliveryMethodId);
            
            //calculate subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);
            
            //create order
            var order =new Order(items, buyerEmail, address,deliveryMethod, subtotal);

            //TODO: save to database

            //return order
            return order;
            

        }

        public Task<IReadOnlyList<Order>> CreateOrderForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
