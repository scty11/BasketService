using System;
using BasketService.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace BasketService.Data
{
    public class BasketDbContext : DbContext
    {
        public BasketDbContext(DbContextOptions<BasketDbContext> options)
            : base(options)
        {}

        public DbSet<GiftVoucherEntity> GiftVouchers { get; set; }
        public DbSet<OfferVoucherEntity> OfferVouchers { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductTypeEntity> ProductTypes { get; set; }
    }
}
