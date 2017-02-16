using System;
using System.ComponentModel.DataAnnotations;
using Ease.Extract.Web.Classes;

namespace Ease.Extract.Web.Models
{
    public class UserModel
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}