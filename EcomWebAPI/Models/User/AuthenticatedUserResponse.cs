﻿namespace EcomWebAPI.Models.User
{
    public class AuthenticatedUserResponse
    {
        public  string AccessToken { get; set; }
        public  string RefreshToken { get; set; }
    }
}
