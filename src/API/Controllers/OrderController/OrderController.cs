using API.Common;
using API.Controllers.OrderController.Dtos;
using DatabaseBroker.Repositories.ClientRepository;
using DatabaseBroker.Repositories.OrderRepositories;
using DatabaseBroker.Repositories.Products.FoodProductRepository;
using DatabaseBroker.Repositories.Products.HouseHoldProductsRepository;
using DatabaseBroker.Repositories.Products.OilProductsRepository;
using DatabaseBroker.Repositories.Products.WaterAndDrinksRepository;
using DatabaseBroker.Repositories.WarehouseDatesRepositories;
using Entity.Enums;
using Entity.Models.Order;
using Entity.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OrderController;
[ApiController]
[Route("[controller]/[action]")]
public class OrderController : ControllerBase
{
    private IOrderRepository OrderRepository { get; set; }
    private IOilProductsRepository OilProductsRepository { get; set; }
    private IFoodProductRepository FoodProducts { get; set; } 
    private IHouseHoldProductsRepository HouseholdProducts { get; set; }
    private IWaterAndDrinksRepository WaterAndDrinks { get; set; }
    private IClientRepository ClientRepository { get; set; }
    private IWarehouseDatesRepository WarehouseDatesRepository { get; set; }
    public OrderController(IOrderRepository orderRepository, IOilProductsRepository oilProductsRepository, 
        IFoodProductRepository foodProducts, IHouseHoldProductsRepository householdProducts, 
        IWaterAndDrinksRepository waterAndDrinks, IClientRepository clientRepository, IWarehouseDatesRepository warehouseDatesRepository)
    {
        OrderRepository = orderRepository;
        OilProductsRepository = oilProductsRepository;
        FoodProducts = foodProducts;
        HouseholdProducts = householdProducts;
        WaterAndDrinks = waterAndDrinks;
        ClientRepository = clientRepository;
        WarehouseDatesRepository = warehouseDatesRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( OrderCreationDto dto)
    {
        var entity = new Order
        {
            ProductsIds = dto.ProductsIds,
            TotalPrice = dto.TotalPrice,
            OrderStatus = dto.OrderStatus,
            DeliveryDate = dto.DeliveryDate,
            CustomerId = dto.CustomerId,
            Client = null
        };

        var resEntity=await OrderRepository.AddAsync(entity);
        await this.ChangeWarehouseDates(entity.ProductsIds);

        var resDto = new OrderGetDto
        {
            Id = resEntity.Id,
            ProductsIds = resEntity.ProductsIds,
            TotalPrice = resEntity.TotalPrice,
            OrderStatus = resEntity.OrderStatus,
            DeliveryDate = resEntity.DeliveryDate,
            CustomerId = resEntity.CustomerId,
            User = resEntity.Client
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( OrderUpdateDto dto)
    {
        var res =  await OrderRepository.GetByIdAsync(dto.Id);
        res.ProductsIds = dto.ProductsIds;
        res.TotalPrice = dto.TotalPrice;
        res.OrderStatus = dto.OrderStatus;
        res.DeliveryDate = dto.DeliveryDate;
        res.CustomerId = dto.CustomerId;
        
        await OrderRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await OrderRepository.GetByIdAsync(id);
        await OrderRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var resEntity =  await OrderRepository.GetByIdAsync(id);
        var dto = new OrderGetDto()
        { 
            Id = resEntity.Id,
            ProductsIds = resEntity.ProductsIds,
            TotalPrice = resEntity.TotalPrice,
            OrderStatus = resEntity.OrderStatus,
            DeliveryDate = resEntity.DeliveryDate,
            CustomerId = resEntity.CustomerId,
            User = resEntity.Client
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   OrderRepository.GetAllAsQueryable().ToList();
        List<OrderGetDto> dtos = new List<OrderGetDto>();
        foreach (Order model in res)
        {
            dtos.Add(new OrderGetDto()
            {
                Id = model.Id,
                ProductsIds = model.ProductsIds,
                TotalPrice = model.TotalPrice,
                OrderStatus = model.OrderStatus,
                DeliveryDate = model.DeliveryDate,
                CustomerId = model.CustomerId,
                User = model.Client
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllUserOrdersAsync(long userId)
    {
        var models =   OrderRepository.GetAllAsQueryable().
            Where(item=>item.CustomerId==userId).ToList();
        if (models == null)
            throw new NullReferenceException("Order not found OrderController");
        
        
        List<OrderGetDto> dtos = new List<OrderGetDto>();
        foreach (Order model in models)
        {
            dtos.Add(new OrderGetDto() 
            { 
                Id = model.Id, 
                ProductsIds = model.ProductsIds, 
                TotalPrice = model.TotalPrice, 
                OrderStatus = model.OrderStatus, 
                DeliveryDate = model.DeliveryDate, 
                CustomerId = model.CustomerId, 
                User = model.Client 
            });
          

        }

        return new ResponseModelBase(dtos);
    }   
 
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAcceptedOrders(
        DateTime? date,
        DateTime? startDate,
        DateTime? endDate)
    {
        var query = OrderRepository.GetAllAsQueryable()
            .Where(o => o.OrderStatus == OrderStatus.Accepted);

        // Bitta sana bo‘yicha filter
        if (date.HasValue)
        {
            var d = date.Value.Date;
            query = query.Where(o => o.DeliveryDate.Date == d);
        }

        // Sana oralig‘i bo‘yicha filter
        if (startDate.HasValue && endDate.HasValue)
        {
            var start = startDate.Value.Date;
            var end = endDate.Value.Date.AddDays(1).AddSeconds(-1);

            query = query.Where(o => o.DeliveryDate >= start && o.DeliveryDate <= end);
        }

        var models = query.ToList();

        var dtos = models.Select(o => new OrderGetDto
        {
            Id = o.Id,
            ProductsIds = o.ProductsIds,
            TotalPrice = o.TotalPrice,
            OrderStatus = o.OrderStatus,
            DeliveryDate = o.DeliveryDate,
            CustomerId = o.CustomerId,
            User = o.Client
        }).ToList();

        return new ResponseModelBase(dtos);
    }
 
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllShippedOrders(
        DateTime? date,
        DateTime? startDate,
        DateTime? endDate)
    {
        var query = OrderRepository.GetAllAsQueryable()
            .Where(o => o.OrderStatus == OrderStatus.Shipped);

        if (date.HasValue)
        {
            var d = date.Value.Date;
            query = query.Where(o => o.DeliveryDate.Date == d);
        }

        if (startDate.HasValue && endDate.HasValue)
        {
            var start = startDate.Value.Date;
            var end = endDate.Value.Date.AddDays(1).AddSeconds(-1);

            query = query.Where(o => o.DeliveryDate >= start && o.DeliveryDate <= end);
        }

        var models = query.ToList();

        var dtos = models.Select(o => new OrderGetDto
        {
            Id = o.Id,
            ProductsIds = o.ProductsIds,
            TotalPrice = o.TotalPrice,
            OrderStatus = o.OrderStatus,
            DeliveryDate = o.DeliveryDate,
            CustomerId = o.CustomerId,
            User = o.Client
        }).ToList();

        return new ResponseModelBase(dtos);
    }

    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllDeliveredOrders(
        DateTime? date,
        DateTime? startDate,
        DateTime? endDate)
    {
        var query = OrderRepository.GetAllAsQueryable()
            .Where(o => o.OrderStatus == OrderStatus.Delivered);

        if (date.HasValue)
        {
            var d = date.Value.Date;
            query = query.Where(o => o.DeliveryDate.Date == d);
        }

        if (startDate.HasValue && endDate.HasValue)
        {
            var start = startDate.Value.Date;
            var end = endDate.Value.Date.AddDays(1).AddSeconds(-1);

            query = query.Where(o => o.DeliveryDate >= start && o.DeliveryDate <= end);
        }

        var models = query.ToList();

        var dtos = models.Select(o => new OrderGetDto
        {
            Id = o.Id,
            ProductsIds = o.ProductsIds,
            TotalPrice = o.TotalPrice,
            OrderStatus = o.OrderStatus,
            DeliveryDate = o.DeliveryDate,
            CustomerId = o.CustomerId,
            User = o.Client
        }).ToList();

        return new ResponseModelBase(dtos);
    }

   
    [HttpGet]
    public async Task<ResponseModelBase> GetAllPaidOrders(
        DateTime? date,
        DateTime? startDate,
        DateTime? endDate)
    {
        var query = OrderRepository.GetAllAsQueryable()
            .Where(o => o.OrderStatus == OrderStatus.Paid);

        if (date.HasValue)
        {
            var d = date.Value.Date;
            query = query.Where(o => o.DeliveryDate.Date == d);
        }

        if (startDate.HasValue && endDate.HasValue)
        {
            var start = startDate.Value.Date;
            var end = endDate.Value.Date.AddDays(1).AddSeconds(-1);

            query = query.Where(o => o.DeliveryDate >= start && o.DeliveryDate <= end);
        }

        var models = query.ToList();

        var dtos = models.Select(o => new OrderGetDto
        {
            Id = o.Id,
            ProductsIds = o.ProductsIds,
            TotalPrice = o.TotalPrice,
            OrderStatus = o.OrderStatus,
            DeliveryDate = o.DeliveryDate,
            CustomerId = o.CustomerId,
            User = o.Client
        }).ToList();

        return new ResponseModelBase(dtos);
    }

    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllUnPaidOrders(
        DateTime? date,
        DateTime? startDate,
        DateTime? endDate)
    {
        var query = OrderRepository.GetAllAsQueryable()
            .Where(o => o.OrderStatus == OrderStatus.Unpaid);

        if (date.HasValue)
        {
            var d = date.Value.Date;
            query = query.Where(o => o.DeliveryDate.Date == d);
        }

        if (startDate.HasValue && endDate.HasValue)
        {
            var start = startDate.Value.Date;
            var end = endDate.Value.Date.AddDays(1).AddSeconds(-1);

            query = query.Where(o => o.DeliveryDate >= start && o.DeliveryDate <= end);
        }

        var models = query.ToList();

        var dtos = models.Select(o => new OrderGetDto
        {
            Id = o.Id,
            ProductsIds = o.ProductsIds,
            TotalPrice = o.TotalPrice,
            OrderStatus = o.OrderStatus,
            DeliveryDate = o.DeliveryDate,
            CustomerId = o.CustomerId,
            User = o.Client
        }).ToList();

        return new ResponseModelBase(dtos);
    }

    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetOrderDetails(long orderId)
    {
        var model =   OrderRepository.GetAllAsQueryable().
            FirstOrDefault(item=>item.Id==orderId);
        if (model == null)
            throw new NullReferenceException("Order not found OrderController");
        var client = await ClientRepository.GetByIdAsync(model.CustomerId);

        
        List<OrderDetailsProducts> products = new List<OrderDetailsProducts>();
        foreach (ProductItem item in model.ProductsIds)
        {
            Product product = new Product();

            switch (item.ProductType)
            {
                case "OilProduct" :
                   product=await OilProductsRepository.GetByIdAsync(item.ProductId);
                    break;               
                case "HouseHoldProduct" :
                   product=await HouseholdProducts.GetByIdAsync(item.ProductId);
                    break;                
                case "FoodProduct" :
                   product=await FoodProducts.GetByIdAsync(item.ProductId);
                    break;                
                case "WaterAndDrinksProduct" :
                   product=await WaterAndDrinks.GetByIdAsync(item.ProductId);
                    break;
                default:
                product=null;
                    break;
            }
            
            products.Add(new OrderDetailsProducts
            {
                ProductId = item.ProductId,
                ProductName = product.Name.uz,
                Price = product.Price,
                CountBox = item.QuantityBox,
                CountPiece = item.QuantityPiece,
            });
            product = null;
        }

        var res = new OrderDetailsGetDto
        {
            OrderId = model.Id,
            Status = model.OrderStatus,
            TotalPrice = model.TotalPrice,
            OrderedDate = model.DeliveryDate,
            OrderDetailsProducts = products,
            CustomerName = client.ClientFullName,
            CustomerPhoneNumber = client.PhoneNumber
        };


        return new ResponseModelBase(res);
    }


    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateShippedOrder(long  orderId)
    {
        var order= await OrderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new NullReferenceException("Order not found OrderController");
        
        order.OrderStatus = OrderStatus.Delivered;
        var res=await OrderRepository.UpdateAsync(order);
        return new ResponseModelBase(res);
    }

    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAcceptedOrder(long orderId)
    {
        var order= await OrderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new NullReferenceException("Order not found OrderController");
        
        order.OrderStatus = OrderStatus.Shipped;
        order.DeliveryDate = DateTime.Now;
        var res=await OrderRepository.UpdateAsync(order);
        return new ResponseModelBase(res);
    }


    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateOrderStatus(long orderId,int status)
    {
        var order= await OrderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new NullReferenceException("Order not found OrderController");
        switch (status)
        {
            case 0:
                order.OrderStatus = OrderStatus.Paid;
                break;
            case 1:
                order.OrderStatus = OrderStatus.Unpaid;
                break;
            case 2:
                order.OrderStatus = OrderStatus.Shipped;
                break;
            case 3:
                order.OrderStatus = OrderStatus.Delivered;
                break;
            case 4:
                order.OrderStatus = OrderStatus.Accepted;
                break;
        }
        var res=await OrderRepository.UpdateAsync(order);
        return new ResponseModelBase(res);
    }


    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> GetProductNames(GetProductDatas dto)
    {
        List<string> dtos = new List<string>();
        foreach (ProductItem item in dto.productsItemIds)
        {
            var data=await GetData(item.ProductType,item.ProductId);
            dtos.Add(data.Name.uz);
        }
        
        return new ResponseModelBase(dtos);
    }

    private async Task<Product> GetData(string type, long id)
    {
        switch (type)
        {
            case "FoodProduct":
                return await FoodProducts.GetByIdAsync(id);
            case "HouseHoldProduct": 
                return await HouseholdProducts.GetByIdAsync(id);
            case "WaterAndDrinksProduct": 
                return await WaterAndDrinks.GetByIdAsync(id);
            case "OilProduct":
                return await OilProductsRepository.GetByIdAsync(id);
            default:
                return null;
        }
    }


    private async Task<bool> RemoveProductFromWarehouse(long warehouseId, int count)
    {
       var warehouse=await WarehouseDatesRepository.GetByIdAsync(warehouseId);
       if (warehouse.QuantityBoxes>count)
       {
           warehouse.QuantityBoxes -= count;
           await WarehouseDatesRepository.UpdateAsync(warehouse);
           return true;
       }
       return false;
    }
    
    private async Task ChangeWarehouseDates(List<ProductItem> products)
    {
        
        foreach (var product in products)
        {
            switch (product.ProductType)
            {
                case "FoodProduct":
                    var f=await FoodProducts.GetByIdAsync(product.ProductId);
                    await RemoveProductFromWarehouse(f.WarehouseDatesId, product.QuantityBox);
                    break;
                case "HouseHoldProduct":
                    var h=await HouseholdProducts.GetByIdAsync(product.ProductId);
                    await RemoveProductFromWarehouse(h.WarehouseDatesId, product.QuantityBox);
                    break;
                case "WaterAndDrinksProduct":
                    var w=await WaterAndDrinks.GetByIdAsync(product.ProductId);
                    await RemoveProductFromWarehouse(w.WarehouseDatesId, product.QuantityBox);
                    break;
                case "OilProduct":
                    var o=await OilProductsRepository.GetByIdAsync(product.ProductId);
                    await RemoveProductFromWarehouse(o.WarehouseDatesId, product.QuantityBox);
                    break;
            }  
        }
        

    }

}