﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VectorSite.DL.Models;

namespace VectorSite.DL
{
    public interface IDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }

        public DbSet<SubscriptionPrice> SubscriptionPrices { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public int SaveChanges();

        public IDbContextTransaction BeginTransaction();
    }
}
