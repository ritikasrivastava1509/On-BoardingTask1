using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Product_CRUDOperation.Models
{
    public class CustomerViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This name field is required")]
        [StringLength(10, MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This name field is required")]
        [StringLength(50, MinimumLength = 3)]
        public string Address { get; set; }
    }
}