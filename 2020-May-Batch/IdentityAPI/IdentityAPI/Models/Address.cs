using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Models
{
    [Table("addresses")]
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(250)]
        public string AddressLine1 { get; set; }

        [Required, StringLength(250)]
        public string AddressLine2 { get; set; }

        [Required, StringLength(100)]
        public string Landmark { get; set; }

        [Required, StringLength(50)]
        public string State { get; set; }

        [Required, StringLength(50)]
        public string Country { get; set; }

        [Required, StringLength(6)]
        public string Pincode { get; set; }
        
        public virtual UserInfo User { get; set; }
    }
}
