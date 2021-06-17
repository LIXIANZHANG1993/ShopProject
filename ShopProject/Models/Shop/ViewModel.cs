using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.Models.Shop
{
    public class ViewModel
    {
        public IEnumerable<Cart> carts { get; set; }
        public Purchase purchases { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        //public IEnumerable<Product> product { get; set; }

        //public ProductType productType { get; set; }

        public Customer customers { get; set; }

        public ChangePassWord changePassWords { get; set; }


    }
}
