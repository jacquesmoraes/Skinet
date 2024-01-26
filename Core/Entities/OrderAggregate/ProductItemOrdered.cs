using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public int Id { get; set; }
        public ProductItemOrdered(int productItem, string productName, string pictureUrl)
        {
            ProductItem = productItem;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductItem { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}
