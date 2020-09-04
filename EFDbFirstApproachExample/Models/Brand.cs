using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFDbFirstApproachExample.Models
{
    public class Brand
    {
        [Key]
        public long BrandID { get; set; }
        [Required(ErrorMessage = "Brand Name is required")]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
    }
}