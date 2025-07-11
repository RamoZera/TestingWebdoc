//-----------------------------------------------------------------------
// <copyright file="LoginResponse.cs" company="Home">
//     Author: Horus Ra
//     Copyright (c) Home. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace WebDocMobile.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public User User { get; set; }
    }

    public class User
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IdName<int, string>[] Roles { get; set; }
    }
}
