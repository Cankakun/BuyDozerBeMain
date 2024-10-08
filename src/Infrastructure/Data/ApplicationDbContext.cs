﻿using System.Reflection;
using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Domain.Entities;
using BuyDozerBeMain.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuyDozerBeMain.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<UserEntity>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // public DbSet<TodoList> TodoLists => Set<TodoList>();

    // public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<HeavyUnit> HeavyUnits => Set<HeavyUnit>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<DetailRent> DetailRents => Set<DetailRent>();
    public DbSet<DetailBuy> DetailBuys => Set<DetailBuy>();
    public DbSet<PriceListRent> PriceListRents => Set<PriceListRent>();
    public DbSet<UserEntity> UserEntitys => Set<UserEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
