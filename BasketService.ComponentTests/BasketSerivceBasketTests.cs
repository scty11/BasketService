using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BasketService.ComponentTests.Configuration;
using BasketService.DTOs;
using BasketService.EntityModels;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace BasketService.ComponentTests
{
    [Collection("SQL server test collection")]
    public class BasketSerivceBasketTests : SqlLiteTestBase, IClassFixture<SqlLiteTestFactory>
    {
        private readonly SqlLiteTestFactory _sqlLiteTestFactory;

        public BasketSerivceBasketTests(SqlLiteTestFactory sqlLiteTestFactory)
        {
            _sqlLiteTestFactory = sqlLiteTestFactory;
        }

        [Fact(DisplayName =
            "Basket 1: Given a gift voucher when creating a basket then the voucher is applied")]
        public async Task Post_GiftVoucher_AppliesGiftVoucher()
        {
            var productOneId = Guid.NewGuid();
            var productTwoId = Guid.NewGuid();
            var giftVoucherCode = "YYY";

            var products = new List<ProductEntity>
            {
                new ProductEntity
                {
                    Id = productOneId,
                    Description = "Hat",
                    Price = 10.50M,
                    ProductType = new ProductTypeEntity
                    {
                        Description = "Headwear"
                    }
                },
                new ProductEntity
                {
                    Id = productTwoId,
                    Description = "Jumper",
                    Price = 54.65M,
                    ProductType = new ProductTypeEntity
                    {
                        Description = "Top"
                    }
                }
            };

            var giftVoucher = new GiftVoucherEntity
            {
                Amount = 5.00M,
                Code = giftVoucherCode
            };

            using (var context = GivenBasketContext())
            {
                await context.Products.AddRangeAsync(products);
                await context.GiftVouchers.AddAsync(giftVoucher);
                await context.SaveChangesAsync();
            }

            var createBasket = new CreateBasketDTO
            {
                Products = new List<CreatBasketItemDTO>
                {
                    new CreatBasketItemDTO
                    {
                        ProductId = productOneId,
                        Quantity = 1
                    },
                    new CreatBasketItemDTO
                    {
                        ProductId = productTwoId,
                        Quantity = 1
                    }
                },
                GiftVoucherCodes = new List<string>
                {
                    giftVoucherCode
                }
            };

            var response = await _sqlLiteTestFactory.CreateClient()
                .PostAsJsonAsync($"/api/v1/basket", createBasket);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BasketDTO>(content);

            result.BasketDiscountTotal.Should().Be(products.Select(p => p.Price).Sum() - giftVoucher.Amount);
            result.BasketTotal.Should().Be(result.BasketDiscountTotal + giftVoucher.Amount);
        }

        [Fact(DisplayName =
            "Basket 2: Given an offer voucher when no valid product then a warning message is returned")]
        public async Task Post_OfferVoucherNoValidProduct_returnsWarningMessage()
        {
            var productOneId = Guid.NewGuid();
            var productTwoId = Guid.NewGuid();
            var offerVoucherCode = "AAA";

            var products = new List<ProductEntity>
            {
                new ProductEntity
                {
                    Id = productOneId,
                    Description = "Hat",
                    Price = 25.00M,
                    ProductType = new ProductTypeEntity
                    {
                        Description = "Headwear"
                    }
                },
                new ProductEntity
                {
                    Id = productTwoId,
                    Description = "Jumper",
                    Price = 26.00M,
                    ProductType = new ProductTypeEntity
                    {
                        Description = "Top"
                    }
                }
            };

            var offerVoucher = new OfferVoucherEntity
            {
                Amount = 5.00M,
                Code = offerVoucherCode,
                ThresHold = 50.00M,
                ProductType = new ProductTypeEntity
                {
                    Description = "Head Gear"
                }
            };

            using (var context = GivenBasketContext())
            {
                await context.Products.AddRangeAsync(products);
                await context.OfferVouchers.AddAsync(offerVoucher);
                await context.SaveChangesAsync();
            }

            var createBasket = new CreateBasketDTO
            {
                Products = new List<CreatBasketItemDTO>
                {
                    new CreatBasketItemDTO
                    {
                        ProductId = productOneId,
                        Quantity = 1
                    },
                    new CreatBasketItemDTO
                    {
                        ProductId = productTwoId,
                        Quantity = 1
                    }
                },
                OfferVoucherCode = offerVoucherCode
            };

            var response = await _sqlLiteTestFactory.CreateClient()
                .PostAsJsonAsync($"/api/v1/basket", createBasket);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BasketDTO>(content);

            result.BasketDiscountTotal.Should().Be(0);
            result.Message.Should().Be($"There are no products in your basket applicable to voucher {offerVoucher.Code}.");
        }

        [Fact(DisplayName =
            "Basket 3: Given an offer voucher when valid threshold then the voucher is applied")]
        public async Task Post_OfferVoucher_VoucherIsApplied()
        {
            var productOneId = Guid.NewGuid();
            var productTwoId = Guid.NewGuid();
            var productThreeId = Guid.NewGuid();
            var productTypeCode = Guid.NewGuid();
            var offerVoucherCode = "XXX";

            var products = new List<ProductEntity>
            {
                new ProductEntity
                {
                    Id = productOneId,
                    Description = "Hat",
                    Price = 25.00M,
                    ProductType = new ProductTypeEntity
                    {
                        Description = "Headwear"
                    }
                },
                new ProductEntity
                {
                    Id = productTwoId,
                    Description = "Jumper",
                    Price = 26.00M,
                    ProductType = new ProductTypeEntity
                    {
                        Description = "Top"
                    }
                },
                new ProductEntity
                {
                    Id = productThreeId,
                    Description = "Head Light",
                    Price = 3.50M,
                    ProductType = new ProductTypeEntity
                    {
                        Id = productTypeCode,
                        Description = "Head Gear"
                    }
                }
            };

            var offerVoucher = new OfferVoucherEntity
            {
                Amount = 5.00M,
                Code = offerVoucherCode,
                ThresHold = 50.00M,
                ProductType = products[2].ProductType
            };

            using (var context = GivenBasketContext())
            {
                await context.Products.AddRangeAsync(products);
                await context.OfferVouchers.AddAsync(offerVoucher);
                await context.SaveChangesAsync();
            }

            var createBasket = new CreateBasketDTO
            {
                Products = new List<CreatBasketItemDTO>
                {
                    new CreatBasketItemDTO
                    {
                        ProductId = productOneId,
                        Quantity = 1
                    },
                    new CreatBasketItemDTO
                    {
                        ProductId = productTwoId,
                        Quantity = 1
                    },
                    new CreatBasketItemDTO
                    {
                        ProductId = productThreeId,
                        Quantity = 1
                    }
                },
                OfferVoucherCode = offerVoucherCode
            };

            var response = await _sqlLiteTestFactory.CreateClient()
                .PostAsJsonAsync($"/api/v1/basket", createBasket);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BasketDTO>(content);

            result.BasketDiscountTotal.Should().Be(products.Select(p => p.Price).Sum() - offerVoucher.Amount);
            result.BasketTotal.Should().Be(result.BasketDiscountTotal + offerVoucher.Amount);
        }

        [Fact(DisplayName =
            "Basket 4: Given an offer voucher and gift voucher when valid threshold then the voucher is applied")]
        public async Task Post_OfferVoucherAndGiftVoucher_VoucherIsApplied()
        {
            var productOneId = Guid.NewGuid();
            var productTwoId = Guid.NewGuid();
            var productTypeCode = Guid.NewGuid();
            var offerVoucherCode = "BBB";
            var giftVoucherCode = "CCC";

            var products = new List<ProductEntity>
            {
                new ProductEntity
                {
                    Id = productOneId,
                    Description = "Hat",
                    Price = 25.00M,
                    ProductType = new ProductTypeEntity
                    {
                        Description = "Headwear"
                    }
                },
                new ProductEntity
                {
                    Id = productTwoId,
                    Description = "Jumper",
                    Price = 26.00M,
                    ProductType = new ProductTypeEntity
                    {
                        Description = "Top"
                    }
                }
            };

            var offerVoucher = new OfferVoucherEntity
            {
                Amount = 5.00M,
                Code = offerVoucherCode,
                ThresHold = 50.00M,
            };

            var giftVoucher = new GiftVoucherEntity
            {
                Amount = 5.00M,
                Code = giftVoucherCode
            };

            using (var context = GivenBasketContext())
            {
                await context.Products.AddRangeAsync(products);
                await context.OfferVouchers.AddAsync(offerVoucher);
                await context.GiftVouchers.AddAsync(giftVoucher);
                await context.SaveChangesAsync();
            }

            var createBasket = new CreateBasketDTO
            {
                Products = new List<CreatBasketItemDTO>
                {
                    new CreatBasketItemDTO
                    {
                        ProductId = productOneId,
                        Quantity = 1
                    },
                    new CreatBasketItemDTO
                    {
                        ProductId = productTwoId,
                        Quantity = 1
                    }
                },
                OfferVoucherCode = offerVoucherCode,
                GiftVoucherCodes = new List<string>
                {
                    giftVoucherCode
                }
            };

            var response = await _sqlLiteTestFactory.CreateClient()
                .PostAsJsonAsync($"/api/v1/basket", createBasket);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BasketDTO>(content);

            result.BasketDiscountTotal.Should()
                .Be(products.Select(p => p.Price).Sum() - offerVoucher.Amount - giftVoucher.Amount);
            result.BasketTotal.Should().Be(result.BasketDiscountTotal + offerVoucher.Amount + giftVoucher.Amount);
        }

        [Fact(DisplayName =
            "Basket 5: Given an offer voucher when the threshold is not met then return a warning message")]
        public async Task Post_ThresholdNotMet_ReturnWarningMessage()
        {
            var productOneId = Guid.NewGuid();
            var productTwoId = Guid.NewGuid();
            var offerVoucherCode = "DDD";

            var products = new List<ProductEntity>
            {
                new ProductEntity
                {
                    Id = productOneId,
                    Description = "Hat",
                    Price = 25.00M,
                    ProductType = new ProductTypeEntity
                    {
                        Description = "Headwear"
                    }
                },
                new ProductEntity
                {
                    Id = productTwoId,
                    Description = "Voucher",
                    Price = 30.00M,
                    ProductType = new ProductTypeEntity
                    {
                        Description = "Gift Voucher"
                    }
                }
            };

            var offerVoucher = new OfferVoucherEntity
            {
                Amount = 5.00M,
                Code = offerVoucherCode,
                ThresHold = 50.00M,
            };

            using (var context = GivenBasketContext())
            {
                await context.Products.AddRangeAsync(products);
                await context.OfferVouchers.AddAsync(offerVoucher);
                await context.SaveChangesAsync();
            }

            var createBasket = new CreateBasketDTO
            {
                Products = new List<CreatBasketItemDTO>
                {
                    new CreatBasketItemDTO
                    {
                        ProductId = productOneId,
                        Quantity = 1
                    },
                    new CreatBasketItemDTO
                    {
                        ProductId = productTwoId,
                        Quantity = 1
                    }
                },
                OfferVoucherCode = offerVoucherCode
            };

            var response = await _sqlLiteTestFactory.CreateClient()
                .PostAsJsonAsync($"/api/v1/basket", createBasket);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BasketDTO>(content);

            result.BasketDiscountTotal.Should().Be(0);
            result.Message.Should().Be($"You have not reached the spend threshold for voucher {offerVoucherCode}. " +
            	$"Spend another £{offerVoucher.ThresHold - 25.00M + 0.01M} " +
            	$"receive £{offerVoucher.Amount} discount from your basket total.");
        }
    }
}
