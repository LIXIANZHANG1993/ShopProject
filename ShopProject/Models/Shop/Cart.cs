using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.Models.Shop
{
    public class Cart
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public long Model { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }

        public string ImgUrl { get; set; }

        public string UserId { set; get; }

        

    }
}
