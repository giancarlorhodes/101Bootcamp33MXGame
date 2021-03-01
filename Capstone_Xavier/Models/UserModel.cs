

namespace Capstone_Xavier.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class UserModel:BaseModel
    {
        public int userID { get; set; }
        public int roleID { get; set; }
        public bool changeRole { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string email { get; set; }
        public List<RoleModel> roles { get; set; }


        public List<CharacterModel> characters { get; set; }
        
    }
}