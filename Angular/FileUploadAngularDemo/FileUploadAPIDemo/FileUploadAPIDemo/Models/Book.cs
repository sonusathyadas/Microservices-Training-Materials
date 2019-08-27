using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadAPIDemo.Models
{
    public class Book
    {
        [Required]
        [JsonProperty(PropertyName ="bookName")]
        public string BookName { get; set; }

        [Required]
        [JsonProperty(PropertyName ="author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName ="bookFile")]
        public IFormFile BookFile { get; set; }
    }
}
