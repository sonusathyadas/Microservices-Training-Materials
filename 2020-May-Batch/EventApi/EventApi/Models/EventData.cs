using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventApi.Models
{
    [Table("events")]
    public class EventData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required,StringLength(250)]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("description")]
        public string Description { get; set; }

        [DataType(DataType.DateTime), Required]
        [Column("startdate")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime), Required]
        [Column("enddate")]
        public DateTime EndDate { get; set; }

        [Required, StringLength(50)]
        [Column("location")]
        public string Location { get; set; }

        [Required, DataType(DataType.Url), StringLength(250)]
        [Column("reg_url")]
        public string RegistrationUrl { get; set; }

        [Required, StringLength(100)]
        [Column("organizer")]
        public string Organizer { get; set; }

        [Required, DataType(DataType.DateTime)]
        [Column("last_reg_date")]
        public DateTime LastDate { get; set; }
    }
}
