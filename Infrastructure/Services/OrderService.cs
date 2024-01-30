using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepo;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
        {
            _unitOfWork = unitOfWork;
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
                var prodItems = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemsOrdered = new ProductItemOrdered(prodItems.Id, prodItems.Name, prodItems.PictureUrl);
                var orderItem = new OrderItem(itemsOrdered, prodItems.Price, item.Quantity);
                items.Add(orderItem);
            }

            //get delivery method from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //calculate subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            //create order
            var order = new Order(items, buyerEmail, address, deliveryMethod, subtotal);
            _unitOfWork.Repository<Order>().Add(order);

            //save to database
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;

            //delete basket
            await _basketRepo.DeleteBasketAsync(basketId);

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
