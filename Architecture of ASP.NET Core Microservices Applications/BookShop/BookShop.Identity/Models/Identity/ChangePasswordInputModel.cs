﻿namespace BookShop.Identity.Models.Identity
{
    public class ChangePasswordInputModel
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}