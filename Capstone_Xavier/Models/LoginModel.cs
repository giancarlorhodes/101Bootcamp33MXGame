

namespace Capstone_Xavier.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    public class LoginModel: BaseModel
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}