using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasketService.DTOs;
using BasketService.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IVoucherService _voucherService;
        private readonly IMapper _mapper;

        public BasketController(IProductService productService, IMapper mapper,
            IVoucherService voucherService)
        {
            _productService = productService;
            _mapper = mapper;
            _voucherService = voucherService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBasket(CreateBasketDTO createBasket)
        {
            var basketItems = new List<BasketItemDTO>();
            var products = await _productService.GetProductsAsync(
                        createBasket.Products.Select(p => p.ProductId));

            foreach (var product in products)
            {
                basketItems.Add(new BasketItemDTO
                {
                    Product = _mapper.Map<ProductDTO>(product),
                    Quantity = createBasket.Products.Single(p => p.ProductId == product.Id).Quantity
                });
            }

            var basket = new BasketDTO();
            foreach (var basketItem in basketItems)
            {
                basket.BasketTotal += basketItem.Product.Price * basketItem.Quantity;
                basket.BasketItems.Add(basketItem);
            }

            if (!string.IsNullOrEmpty(createBasket.OfferVoucherCode))
            {
                var offerVoucher = await _voucherService.GetOfferVoucherAsync(createBasket.OfferVoucherCode);
                if (offerVoucher == null)
                    return NotFound($"{nameof(offerVoucher)} not found");

                basket.Message = _voucherService.CheckVoucherIsValidAsync(basket, offerVoucher);
                if (string.IsNullOrEmpty(basket.Message))
                    basket.BasketDiscountTotal = basket.BasketTotal - offerVoucher.Amount;
            }

            if (createBasket.GiftVoucherCodes.Any())
                basket.BasketDiscountTotal =
                    await _voucherService.DeductGiftVouchers(
                        createBasket.GiftVoucherCodes,
                        string.IsNullOrEmpty(basket.Message) 
                            && string.IsNullOrEmpty(createBasket.OfferVoucherCode) ? 
                                basket.BasketTotal : basket.BasketDiscountTotal);

            return Ok(basket);
        }
    }
}
