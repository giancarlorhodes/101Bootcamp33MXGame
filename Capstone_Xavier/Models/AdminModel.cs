

namespace Capstone_Xavier.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    //For use in the admin pages. Used to hold both the admins info and possible users basic info
    public class AdminModel
    {
        public List<UserModel> users { get; set; }
        public List<MonsterModel> monsters { get; set; }

       
    }
}