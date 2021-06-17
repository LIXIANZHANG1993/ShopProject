using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.Models.Shop
{
    public class Customer
    {
        public Customer()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Address { get; set; }
        
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        
        public bool Admin { get; set; }

        public string Gender { get; set; }



        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
