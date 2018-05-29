using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Product_CRUDOperation.Models
{
    public class ProductViewModel
    {

        public int ID { get; set; }
        [Required(ErrorMessage = "This name field is required")]
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This price field is required")]
        [Range(0.05,100.00)]
        public decimal Price { get; set; }




    }
}