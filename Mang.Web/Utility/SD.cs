﻿namespace Mang.Web.Utility
{
    public class SD
    {
        public static string CouponApiBase { get; set; }
        public static string AuthApiBase { get; set; }
        public static string ProductApiBase { get; set; }
        public static string ShoppingCartApiBase { get; set; }


        public const string RoleADMIN = "Admin";
        public const string RoleCustomer= "Customer";
        public const string TokenCookie = "JWTToken";

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}
