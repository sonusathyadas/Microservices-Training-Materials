using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EshopWebApp.Models
{
    [Table("products")]
    public class CatalogItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Product name cannot be empty")]
        [StringLength(50, ErrorMessage ="Maximum 50 characters allowed")]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, Double.MaxValue)]
        [Required(ErrorMessage ="Price cannot be empty")]
        public double Price { get; set; }

        [Required(ErrorMessage ="Quantity cannot be empty")]
        [Range(0, Int32.MaxValue)]
        public int Quantity { get; set; }

        [Required(ErrorMessage ="Brand name cannot be empty")]
        [StringLength(50, ErrorMessage ="Maximum 50 characters allowed")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Product category cannot be empty")]
        [StringLength(50, ErrorMessage = "Maximum 50 characters allowed")]
        public string Category { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Manufacturing Date")]
        public DateTime? MfgDate { get; set; }
    }
}
