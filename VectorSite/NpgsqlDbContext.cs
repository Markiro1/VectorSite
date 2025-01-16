﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VectorSite.DL;
using VectorSite.DL.Models;

namespace VectorSite
{
    public class NpgsqlDbContext : IdentityDbContext<User>, IDbContext
    {
        public NpgsqlDbContext(DbContextOptions<NpgsqlDbContext> options) : base(options)
        {
        }

        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }

        public DbSet<SubscriptionPrice> SubscriptionPrices { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Payment> Payments { get; set; }

        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
        }*/
    }
}
