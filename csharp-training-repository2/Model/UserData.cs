﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class UserData
    {
        private string username;
        private string password;

        public UserData(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public string Username { get { return username; } set { username = value; } }  
        public string Password { get { return password; } set { password = value; } }
    }
}
