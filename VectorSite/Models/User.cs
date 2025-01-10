﻿namespace VectorSite.Models
{
    public class User
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public List<Subscription>? Subscriptions { get; set; } = null;

        public List<Payment>? Payment { get; set; } = null;

        public string Password { get; set; } = string.Empty;
    }
}
