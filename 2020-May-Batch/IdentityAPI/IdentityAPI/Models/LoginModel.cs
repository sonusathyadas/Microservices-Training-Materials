using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Models
{
    public class LoginModel
    {
        [Required, StringLength(100), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, StringLength(25), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
