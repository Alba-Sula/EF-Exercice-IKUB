using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFDbFirstApproachExample.Models
{
    public class Category
    {
        [Key]
        public long CategoryID { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        [Display(Name = "CAtegory Name")]
        public string CategoryName { get; set; }
    }
}