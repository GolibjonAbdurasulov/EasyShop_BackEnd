using System.Net;
using API.Common;
using API.Controllers.CartControllers.Dtos;
using DatabaseBroker.Repositories.CartRepositories;
using DatabaseBroker.Repositories.Products.FoodProductRepository;
using DatabaseBroker.Repositories.Products.HouseHoldProductsRepository;
using DatabaseBroker.Repositories.Products.OilProductsRepository;
using DatabaseBroker.Repositories.Products.WaterAndDrinksRepository;
using Entity.Models.Order;
using Entity.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CartControllers;

[ApiController]
[Route("[controller]/[action]")]
public class CartController : ControllerBase
{
   private ICartRepository CartRepository { get; set; }
   private IFoodProductRepository  FoodProductRepository { get; set; }
   private IHouseHoldProductsRepository  HouseHoldProductsRepository { get; set; }
   private IOilProductsRepository   OilProductsRepository { get; set; }
   private IWaterAndDrinksRepository   WaterAndDrinksRepository { get; set; }
    public CartController(ICartRepository cartRepository, IHouseHoldProductsRepository houseHoldProductsRepository, IFoodProductRepository foodProductRepository, IOilProductsRepository oilProductsRepository, IWaterAndDrinksRepository waterAndDrinksRepository)
    {
        CartRepository = cartRepository;
        HouseHoldProductsRepository = houseHoldProductsRepository;
        FoodProductRepository = foodProductRepository;
        OilProductsRepository = oilProductsRepository;
        WaterAndDrinksRepository = waterAndDrinksRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( CartCreationDto dto)
    {
        var entity = new Cart
        {
            ProductsId = dto.ProductsId,
            CustomerId = dto.CustomerId,
        };
        var resEntity=await CartRepository.AddAsync(entity);
        
        var resDto = new CartGetDto
        {
            Id = resEntity.Id,
            ProductsId = resEntity.ProductsId,
            CustomerId = resEntity.CustomerId,
            Customer = resEntity.Customer
        };
        return new ResponseModelBase(resDto);
    }
    
    // [HttpPost]
    // [Authorize]
    // public async Task<ResponseModelBase> AddProductToCart(CartCreationDto dto)
    // {
    //     var cart= CartRepository.GetAllAsQueryable().
    //         FirstOrDefault(item=>item.CustomerId==dto.CustomerId);
    //     if (cart == null)
    //     {
    //         var entity = new Cart
    //         {
    //             ProductsId = dto.ProductsId,
    //             CustomerId = dto.CustomerId,
    //         };
    //         var resCart=await CartRepository.AddAsync(entity);
    //         return new ResponseModelBase(resCart);
    //     }
    //     
    //     cart.ProductsId.Add(dto.ProductsId);
    //
    //     var resDto = new CartGetDto
    //     {
    //         Id = cart.Id,
    //         ProductsId = cart.ProductsId,
    //         CustomerId = cart.CustomerId,
    //         Customer = cart.Customer
    //     };
    //     return new ResponseModelBase(resDto);
    // }
    // [HttpPost]
    // [Authorize]
    // public async Task<ResponseModelBase> AddProductToCart(CartCreationDto dto)
    // {
    //     var cart = CartRepository.GetAllAsQueryable()
    //         .FirstOrDefault(item => item.CustomerId == dto.CustomerId);
    //
    //     if (cart == null)
    //     {
    //         var entity = new Cart
    //         {
    //             ProductsId = dto.ProductsId,
    //             CustomerId = dto.CustomerId,
    //         };
    //         var resCart = await CartRepository.AddAsync(entity);
    //         return new ResponseModelBase(resCart);
    //     }
    //
    //     // 🧠 agar mavjud bo‘lsa — yangilarni qo‘shamiz
    //     cart.ProductsId ??= new Dictionary<string, long>();
    //
    //     foreach (var kv in dto.ProductsId)
    //     {
    //         if (!cart.ProductsId.ContainsKey(kv.Key))
    //             cart.ProductsId.Add(kv.Key, kv.Value);
    //         else
    //             cart.ProductsId[kv.Key] = kv.Value;
    //     }
    //
    //     await CartRepository.UpdateAsync(cart);
    //
    //     var resDto = new CartGetDto
    //     {
    //         Id = cart.Id,
    //         ProductsId = cart.ProductsId,
    //         CustomerId = cart.CustomerId,
    //         Customer = cart.Customer
    //     };
    //     return new ResponseModelBase(resDto);
    // }


    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> AddProductToCart(CartCreationDto dto)
    {
        // 🔍 Mavjud cartni topamiz
        var cart = CartRepository.GetAllAsQueryable()
            .FirstOrDefault(item => item.CustomerId == dto.CustomerId);

        // 🧩 Agar cart mavjud bo‘lmasa — yangisini yaratamiz
        if (cart == null)
        {
            var entity = new Cart
            {
                ProductsId = dto.ProductsId, // endi List<ProductItem>
                CustomerId = dto.CustomerId,
            };

            var resCart = await CartRepository.AddAsync(entity);
            return new ResponseModelBase(resCart);
        }

        // Agar mavjud bo‘lsa — yangilarni qo‘shamiz yoki yangilaymiz
        cart.ProductsId ??= new List<ProductItem>();

        foreach (var item in dto.ProductsId)
        {
            var product = cart.ProductsId.FirstOrDefault(p => p.ProductType == item.ProductType);

            if (product == null)
            {
                // Yangi mahsulotni qo‘shish
                cart.ProductsId.Add(item);
            }
            else
            {
                // Mavjud mahsulotni yangilash (miqdorini oshirish yoki almashtirish)
                product.Quantity = item.Quantity;
            }
        }

        await CartRepository.UpdateAsync(cart);

        var resDto = new CartGetDto
        {
            Id = cart.Id,
            ProductsId = cart.ProductsId,
            CustomerId = cart.CustomerId,
            Customer = cart.Customer
        };

        return new ResponseModelBase(resDto);
    }

    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( CartUpdateDto dto)
    {
        var res =  await CartRepository.GetByIdAsync(dto.Id);
        res.ProductsId = dto.ProductsId;
        res.CustomerId = dto.CustomerId;
        
        await CartRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }

   
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateProductItem([FromBody] ProductItem dto, long cartId)
    {
        var cart = await CartRepository.GetByIdAsync(cartId);
        if (cart == null)
            return new ResponseModelBase("Cart not found", HttpStatusCode.NotFound);

        var item = cart.ProductsId
            .FirstOrDefault(x => x.ProductType == dto.ProductType && x.ProductId == dto.ProductId);

        if (item == null)
            return new ResponseModelBase("Product not found in cart", HttpStatusCode.NotFound);

        item.Quantity = dto.Quantity;

        await CartRepository.UpdateAsync(cart);
        return new ResponseModelBase(cart, HttpStatusCode.OK);
    }

    
    // [HttpPut]
    // [Authorize]
    // public async Task<ResponseModelBase> UpdateProductItem([FromBody]ProductItem dto, long cartId)
    // {
    //     var cart = await CartRepository.GetByIdAsync(cartId);
    //     
    //      cart.ProductsId.
    //         FirstOrDefault(item=>item.ProductType == dto.ProductType && item.ProductId==dto.ProductId).Quantity=dto.Quantity;
    //      
    //     // foreach (var itemProduct in cart.ProductsId)
    //     // {
    //     //     
    //     //     switch (dto.ProductType)
    //     //     {
    //     //         case "FoodProduct":
    //     //             if (itemProduct.ProductId==dto.ProductId)
    //     //                 itemProduct.Quantity = dto.Quantity;
    //     //             break;
    //     //         case "HouseHoldProduct":
    //     //             if (itemProduct.ProductId==dto.ProductId)
    //     //                 itemProduct.Quantity = dto.Quantity;
    //     //             break;
    //     //         case "OilProduct":
    //     //             if (itemProduct.ProductId==dto.ProductId)
    //     //                 itemProduct.Quantity = dto.Quantity;
    //     //             break;
    //     //         case "WaterAndDrinksProduct":
    //     //             if (itemProduct.ProductId==dto.ProductId)
    //     //                 itemProduct.Quantity = dto.Quantity;
    //     //             break;
    //     //     }
    //     // }
    //     await CartRepository.UpdateAsync(cart);
    //     return new ResponseModelBase(cart, HttpStatusCode.OK);
    // }
    //
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteProductFromCart(DeleteProductFromCartDto dto)
    {
        var cart =await CartRepository.GetByIdAsync(dto.CartId);
        switch (dto.ProductType)
        {
            case "FoodProduct":
                cart.ProductsId.Remove(cart.ProductsId.
                    FirstOrDefault(p => p.ProductType == "FoodProduct"&& p.ProductId==dto.ProductId ));
                break;
            case "HouseHoldProduct":
                cart.ProductsId.Remove(cart.ProductsId.
                    FirstOrDefault(p => p.ProductType == "HouseHoldProduct"&& p.ProductId==dto.ProductId ));
                break;
            case "OilProduct":
                cart.ProductsId.Remove(cart.ProductsId.
                    FirstOrDefault(p => p.ProductType == "OilProduct"&& p.ProductId==dto.ProductId )); 
                break;
            case "WaterAndDrinksProduct":
                cart.ProductsId.Remove(cart.ProductsId.
                    FirstOrDefault(p => p.ProductType == "WaterAndDrinksProduct"&& p.ProductId==dto.ProductId ));

                break;
            default:
                throw new Exception("Invalid product type on CartController");
        }
        
        await CartRepository.UpdateAsync(cart);
        
        return new ResponseModelBase(cart,HttpStatusCode.OK);
    }

    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var res =  await CartRepository.GetByIdAsync(id);
        await CartRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ResponseModelBase> GetByClientIdAsync(long customerId)
    {
        var resEntity =   CartRepository.GetAllAsQueryable().
            FirstOrDefault(item=>item.CustomerId == customerId);
        if (resEntity == null)
            throw new NullReferenceException("There is no such cart on CartController");
        
        List<CartGetProductsDto> products = new List<CartGetProductsDto>();
        
        foreach (var product in resEntity.ProductsId)
        {
            switch (product.ProductType)
            {
                case "FoodProduct":
                    var item =await FoodProductRepository.GetByIdAsync(product.ProductId);
                    products.Add(new CartGetProductsDto
                    {
                        ProductId = item.Id,
                        Name = item.Name.uz,
                        Price = item.Price,
                        Quantity = product.Quantity,
                        ProductType=product.ProductType,
                        ImageId = item.ProductImageId
                    });
                    break;
                case "HouseHoldProduct":
                    var item2 = await HouseHoldProductsRepository.GetByIdAsync(product.ProductId);
                    products.Add(new CartGetProductsDto
                    {
                        ProductId = item2.Id,
                        Name = item2.Name.uz,
                        Price = item2.Price,
                        Quantity = product.Quantity,
                        ProductType=product.ProductType,
                        ImageId = item2.ProductImageId
                    });                    break;
                case "OilProduct":
                    var item3 = await OilProductsRepository.GetByIdAsync(product.ProductId);
                    products.Add(new CartGetProductsDto
                    {
                        ProductId = item3.Id,
                        Name = item3.Name.uz,
                        Price = item3.Price,
                        Quantity = product.Quantity,
                        ProductType=product.ProductType,
                        ImageId = item3.ProductImageId
                    });                    break;
                case "WaterAndDrinksProduct":
                    var item4 = await WaterAndDrinksRepository.GetByIdAsync(product.ProductId);
                    products.Add(new CartGetProductsDto
                    {
                        ProductId = item4.Id,
                        Name = item4.Name.uz,
                        Price = item4.Price,
                        Quantity = product.Quantity,
                        ProductType=product.ProductType,
                        ImageId = item4.ProductImageId
                    });                    break;
            }  
        }
        
        
        return new ResponseModelBase(products,HttpStatusCode.OK);
    }
     [HttpGet]
    [Authorize]
    public async Task<ResponseModelBase> GetProductData([FromBody] string productType ,long  productId)
    {
        var product = new Product();
        switch (productType)
        {
            case "FoodProduct":
                product =await FoodProductRepository.GetByIdAsync(productId);
                break;
            case "HouseHoldProduct":
                product = await HouseHoldProductsRepository.GetByIdAsync(productId);
                break;
            case "OilProduct":
                product = await OilProductsRepository.GetByIdAsync(productId);
                break;
            case "WaterAndDrinksProduct":
               product = await WaterAndDrinksRepository.GetByIdAsync(productId);
                
                break;
            default:
                return new ResponseModelBase("Invalid product type on CartController", HttpStatusCode.NotFound);
        }

        
        return new ResponseModelBase(new CartGetProductsDto
        {
            ProductId = product.Id,
            Name = product.Name.uz,
            Price = product.Price,
            Quantity = 0,
            ImageId = product.ProductImageId
        }, HttpStatusCode.OK);
    }

    [HttpGet]
    [Authorize]
    public async Task<ResponseModelBase> GetManyProductsDates([FromBody] List<ProductItem> items)
    {
        List<CartGetProductsDto> products = new List<CartGetProductsDto>();
        foreach (var item in items)
        {
            var product = new Product();
            switch (item.ProductType)
            {
                case "FoodProduct":
                    product =await FoodProductRepository.GetByIdAsync(item.ProductId);
                    products.Add(new CartGetProductsDto
                    {
                        ProductId = product.Id,
                        Name = product.Name.uz,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        ImageId = product.ProductImageId
                    });
                    break;
                case "HouseHoldProduct":
                    product = await HouseHoldProductsRepository.GetByIdAsync(item.ProductId);
                    products.Add(new CartGetProductsDto
                    {
                        ProductId = product.Id,
                        Name = product.Name.uz,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        ImageId = product.ProductImageId
                    });
                    break;
                case "OilProduct":
                    product = await OilProductsRepository.GetByIdAsync(item.ProductId);
                    products.Add(new CartGetProductsDto
                    {
                        ProductId = product.Id,
                        Name = product.Name.uz,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        ImageId = product.ProductImageId
                    });
                    break;
                case "WaterAndDrinksProduct":
                    product = await WaterAndDrinksRepository.GetByIdAsync(item.ProductId);
                    products.Add(new CartGetProductsDto
                    {
                        ProductId = product.Id,
                        Name = product.Name.uz,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        ImageId = product.ProductImageId
                    });
                    break;
                default:
                    return new ResponseModelBase("Invalid product type on CartController", HttpStatusCode.NotFound);
            }
        }
        return new ResponseModelBase(products, HttpStatusCode.OK);
    }

    // [HttpGet]
    // [Authorize]
    // public async Task<ResponseModelBase> GetAllProductsData([FromBody] long clientId)
    // {
    //     List<Product> products = new List<Product>();
    //     var cart=CartRepository.GetAllAsQueryable().FirstOrDefault(item=>item.CustomerId==clientId );
    //     foreach (var itemProduct in cart.ProductsId)
    //     {
    //         var product = new Product();
    //         switch (itemProduct.ProductType)
    //         {
    //             case "FoodProduct":
    //                 product =await FoodProductRepository.GetByIdAsync(itemProduct.ProductId);
    //                 break;
    //             case "HouseHoldProduct":
    //                 product = await HouseHoldProductsRepository.GetByIdAsync(itemProduct.ProductId);
    //                 break;
    //             case "OilProduct":
    //                 product = await OilProductsRepository.GetByIdAsync(itemProduct.ProductId);
    //                 break;
    //             case "WaterAndDrinksProduct":
    //                 product = await WaterAndDrinksRepository.GetByIdAsync(itemProduct.ProductId);
    //             
    //                 break;
    //         }
    //         products.Add(product);
    //     }
    //     
    //     
    //     return new ResponseModelBase(products, HttpStatusCode.OK);
    // }
    //
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await CartRepository.GetByIdAsync(id);
        var dto = new CartGetDto
        {
            Id = resEntity.Id,
            ProductsId = resEntity.ProductsId,
            CustomerId = resEntity.CustomerId,
            Customer = resEntity.Customer
        };
        return new ResponseModelBase(dto);
    }
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   CartRepository.GetAllAsQueryable().ToList();
        List<CartGetDto> dtos = new List<CartGetDto>();
        foreach (Cart model in res)
        {
            dtos.Add(new CartGetDto
            {
                Id = model.Id,
                ProductsId = model.ProductsId,
                CustomerId = model.CustomerId,
                Customer = model.Customer
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
    
    
}