

namespace Capstone_BLL.BusinessObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Used for mapping values across Users objects
    /// </summary>
    public class UsersBO
    {
        private int userID;
        private int userRole;
        private string username;
        private string password;
        private string email;

        public string Email {
            get { return email; }
            set { email = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public int UserRole
        {
            get { return userRole; }
            set { userRole = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
