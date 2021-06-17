using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.Models.Shop
{
    public class Purchase //訂單
    {
        public Purchase()
        {
            Orders = new HashSet<Order>();
        }

        
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        //public DateTime UpdateDate { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; }

        [Required]
        public string ReceiveName { get; set; }
        [Required]
        public string ReceivePhoneNumber { get; set; }
        [Required]
        public string ReceiveAddress { get; set; }

        public string Note { get; set; }



        public virtual Customer User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
