using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.Models.Shop
{
    public class ChangePassWord
    {
        public int id { get; set; }

        
         public string OriginalPassword { get; set; }

        public string CheckOriginalPassword { get; set; }
        
        public string NewPassword { get; set; }

        
        public string ConfirmedPassword { get; set; }
        public string Email { get; set; }

        public int UserId { get; set; }

    }
}
