using DatabaseBroker.Repositories.FileRepository;
using DatabaseBroker.Repositories.OrderRepositories;
using DatabaseBroker.Repositories.Products.FoodProductRepository;
using DatabaseBroker.Repositories.Products.HouseHoldProductsRepository;
using DatabaseBroker.Repositories.Products.OilProductsRepository;
using DatabaseBroker.Repositories.Products.WaterAndDrinksRepository;
using Entity.Attributes;
using Entity.Models.File;
using Entity.Models.Order;
using Entity.Models.Product;
using Entity.Models.Product.Products;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace Services.Services;

[Injectable]
public class FileService : IFileService
{
    private readonly IFileRepository _fileRepository;
    private readonly IHouseHoldProductsRepository _houseHoldProductsRepository;
    private readonly IFoodProductRepository  _foodProductRepository;
    private readonly IWaterAndDrinksRepository _waterAndDrinksRepository;
    private readonly IOilProductsRepository  _oilProductsRepository;
    private readonly IOrderRepository _orderRepository;
    //private readonly IWebHostEnvironment _webHostEnvironment;

    
    public FileService(IFileRepository fileRepository, IHouseHoldProductsRepository houseHoldProductsRepository, IFoodProductRepository foodProductRepository, IWaterAndDrinksRepository waterAndDrinksRepository, IOilProductsRepository oilProductsRepository, IOrderRepository orderRepository)
    {
        
        _fileRepository = fileRepository;
        _houseHoldProductsRepository = houseHoldProductsRepository;
        _foodProductRepository = foodProductRepository;
        _waterAndDrinksRepository = waterAndDrinksRepository;
        _oilProductsRepository = oilProductsRepository;
        _orderRepository = orderRepository;
        
        QuestPDF.Settings.License = LicenseType.Community;
    }


    public async ValueTask<FileModel> UploadFileAsync(IFormFile file)
    {
        var resultFile = await CreateAsync(file);
        await SaveFileAsync(file, resultFile.Path);
        return resultFile;
    }

    // private async Task SaveFileAsync(IFormFile file, string filePath)
    // {
    //     using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
    //     {
    //         await file.CopyToAsync(stream);
    //     }
    // }
    public async Task SaveFileAsync(IFormFile file, string filePath)
    {
        // Fayl yo'lining katalog qismini olish
        var directory = Path.GetDirectoryName(filePath);

        // Agar katalog mavjud bo'lmasa, uni yaratish
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

    // public async Task SaveFileAsync(byte[] bytes,string filePath)
    // {
    //     using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
    //     {
    //         stream.Write(bytes);
    //     }
    // }

    public async Task SaveFileAsync(byte[] bytes, string filePath)
    {
        await File.WriteAllBytesAsync(filePath, bytes);
    }

    public async ValueTask<string> MakeFilePath(string filePath)
    {
        var webRootFolder =  "wwwroot/uploads";
        var path = Path.Combine(Directory.GetCurrentDirectory(), webRootFolder, filePath);
        return path;
    }

    private async Task<FileModel> CreateAsync(IFormFile file)
    {
        var id = Guid.NewGuid();
        string fileName = id.ToString() + DetectionFileType(file.FileName);
        var resultFile = new FileModel()
        {
            Id = id,
            FileName = fileName,
            ContentType = file.ContentType ?? string.Empty,
            Path = await MakeFilePath(fileName)
        };
        resultFile = await _fileRepository.AddAsync(resultFile);
        return resultFile;
    }

    public string DetectionFileType(string fileName)
    {
        string[] names = fileName.Split('.');
        return ("." + names[names.Length - 1]);
    }


    public async ValueTask<FileModel> DeleteAsync(Guid id)
    {
        var file = await _fileRepository.GetByIdAsync(id);
        if (file is null)
            throw new Exception("File not found");
        await RemoveFileWebRootFolderAsync(file.Path);
        var result = await _fileRepository.RemoveAsync(file);
        return result;
    }

    private async Task RemoveFileWebRootFolderAsync(string path)
    {
        if (System.IO.File.Exists(path))
        {
            await Task.Run(() => System.IO.File.Delete(path));
        }
    }


    public async ValueTask<FileModel> UpdateFileAsync(Guid id, IFormFile file)
    {
        var updateFile = await GetByIdAsync(id);
        updateFile = await UpdateAsync(updateFile, file);
        await RemoveFileWebRootFolderAsync(updateFile.Path);
        await SaveFileAsync(file, updateFile.Path);
        return updateFile;
    }

    private async ValueTask<FileModel> UpdateAsync(FileModel file,
        IFormFile updateFile)
    {
        file.FileName = updateFile.FileName;
        file.ContentType = updateFile.ContentType;
        file = await _fileRepository.UpdateAsync(file);
        return file;
    }

    // public async ValueTask<(FileStream, FileModel)> GetByFileIdAsync(long id)
    // {
    //     var file = await GetByIdAsync(id);
    //     var fileStream = new FileStream(file.Path, FileMode.Open, FileAccess.Read);
    //
    //     return (fileStream, file);
    // }

    public async ValueTask<FileModel> GetByIdAsync(Guid id)
    {
        var file = await _fileRepository.GetByIdAsync(id);
        if (file is null)
            throw new Exception("File not  found");
        return file;
    }

    public async Task<Stream> SendFileAsync(Guid id)
    {
        var file = await _fileRepository.GetByIdAsync(id);
        if (file == null || !System.IO.File.Exists(file.Path))
            throw new FileNotFoundException("Fayl topilmadi.");

        var stream = new FileStream(file.Path, FileMode.Open, FileAccess.Read);
        return stream;
    }


// public async Task<Stream> GetProductCheck(long orderId)
// {
//     var order = await _orderRepository.GetByIdAsync(orderId);
//     if (order == null)
//         throw new Exception("Buyurtma topilmadi");
//
//     var productsData = await Task.WhenAll(order.ProductsIds.Select(async p =>
//     {
//         var prod = await GetProductsDates(p.ProductType, p.ProductId);
//         return new
//         {
//             Name = prod.Name.uz,
//             Quantity = p.Quantity,
//             Price = prod.Price,
//             Total = prod.Price * p.Quantity
//         };
//     }));
//
//     var stream = new MemoryStream();
//
//     var document = Document.Create(container =>
//     {
//         container.Page(page =>
//         {
//             page.Size(PageSizes.A4);
//             page.Margin(20);
//             page.PageColor(Colors.White);
//             page.DefaultTextStyle(x => x.FontSize(12));
//
//             // Header
//             page.Header()
//                 .Text($"Buyurtma ID: {order.Id} | Mijoz: {order.Client.FullName}")
//                 .SemiBold()
//                 .FontSize(14);
//
//             // Table
//             page.Content()
//                 .Table(table =>
//                 {
//                     table.ColumnsDefinition(columns =>
//                     {
//                         columns.ConstantColumn(30); // N
//                         columns.RelativeColumn(3);  // Name
//                         columns.RelativeColumn();    // Quantity
//                         columns.RelativeColumn();    // Price
//                         columns.RelativeColumn();    // Total
//                     });
//
//                     // Table header
//                     table.Header(header =>
//                     {
//                         header.Cell().Background(Colors.Grey.Lighten2).Text("N").SemiBold();
//                         header.Cell().Background(Colors.Grey.Lighten2).Text("MAHSULOT NOMI").SemiBold();
//                         header.Cell().Background(Colors.Grey.Lighten2).Text("SONI").SemiBold();
//                         header.Cell().Background(Colors.Grey.Lighten2).Text("NARXI").SemiBold();
//                         header.Cell().Background(Colors.Grey.Lighten2).Text("JAMI").SemiBold();
//                     });
//
//                     // Table rows
//                     int n = 1;
//                     foreach (var item in productsData)
//                     {
//                         table.Cell().Text(n.ToString());
//                         table.Cell().Text(item.Name);
//                         table.Cell().Text(item.Quantity.ToString());
//                         table.Cell().Text(item.Price.ToString("N0"));
//                         table.Cell().Text(item.Total.ToString("N0"));
//                         n++;
//                     }
//                 });
//
//             // Footer
//             page.Footer()
//                 .AlignRight()
//                 .Text($"Umumiy summa: {productsData.Sum(x => x.Total):N0} so‘m")
//                 .SemiBold();
//         });
//     });
//
//     document.GeneratePdf(stream);
//     stream.Position = 0;
//     return stream;
// }


public async Task<Stream> GetProductCheck(long orderId)
{
    QuestPDF.Settings.License = LicenseType.Community;

    var order = await _orderRepository.GetByIdAsync(orderId);
    if (order == null)
        throw new Exception("Buyurtma topilmadi");

    var products = new List<(string Name, int Qty, decimal Price)>();

    foreach (var item in order.ProductsIds)
    {
        var product = await GetProductsDates(item.ProductType, item.ProductId);
        if (product != null)
        {
            products.Add((
                product.Name?.uz ?? "Noma'lum",
                item.QuantityBox,
                product.Price
            ));
        }
    }

    var stream = new MemoryStream();

    var document = Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(25);
            page.DefaultTextStyle(x => x.FontSize(11));

            // ===== HEADER =====
            page.Header().Column(col =>
            {
                col.Item().AlignRight()
                    .Text($"Sana: {DateTime.Now:dd.MM.yyyy}");

                col.Item()
                    .Padding(10)         
                    .AlignCenter()
                    .Text("Xarid cheki")
                    .FontSize(16)
                    .Bold();




                col.Item().Text($"Kimga: {order.Client?.ClientFullName ?? "Mijoz"}");
                col.Item().Text("Kimdan: ____________________________");
            });

            // ===== TABLE =====
            page.Content().PaddingTop(15).Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(30);  // №
                    columns.RelativeColumn(5);   // Nomi
                    columns.RelativeColumn(2);   // Miqdori
                    columns.RelativeColumn(2);   // Narxi
                    columns.RelativeColumn(2);   // Summa
                });

                // Header
                table.Header(header =>
                {
                    header.Cell().Border(1).Padding(5).AlignCenter().Text("№").Bold();
                    header.Cell().Border(1).Padding(5).Text("Mahsulot nomi").Bold();
                    header.Cell().Border(1).Padding(5).AlignCenter().Text("Miqdori").Bold();
                    header.Cell().Border(1).Padding(5).AlignRight().Text("Narxi").Bold();
                    header.Cell().Border(1).Padding(5).AlignRight().Text("Summa").Bold();
                });

                decimal total = 0;
                int index = 1;

                foreach (var p in products)
                {
                    var sum = p.Qty * p.Price;
                    total += sum;

                    table.Cell().Border(1).Padding(5).AlignCenter().Text(index++.ToString());
                    table.Cell().Border(1).Padding(5).Text(p.Name);
                    table.Cell().Border(1).Padding(5).AlignCenter().Text(p.Qty.ToString());
                    table.Cell().Border(1).Padding(5).AlignRight().Text($"{p.Price:N0}");
                    table.Cell().Border(1).Padding(5).AlignRight().Text($"{sum:N0}");
                }

                // TOTAL
                table.Cell().ColumnSpan(4).Border(1).Padding(5)
                    .AlignRight().Text("Jami:").Bold();

                table.Cell().Border(1).Padding(5)
                    .AlignRight().Text($"{total:N0} so'm").Bold();
            });

            // ===== FOOTER =====
            page.Footer().PaddingTop(30).Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("Topshirdi: _______________________");
                    col.Item().Text("Imzo: _____________");
                });

                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("Qabul qildi: ______________________");
                    col.Item().Text("Imzo: _____________");
                });
            });
        });
    });

    document.GeneratePdf(stream);
    stream.Position = 0;
    return stream;
}

// public async Task<Stream> GetProductCheck(long orderId)
// {
//     // Litsenziya sozlash
//     QuestPDF.Settings.License = LicenseType.Community;
//
//     var order = await _orderRepository.GetByIdAsync(orderId);
//     if (order == null)
//         throw new Exception("Buyurtma topilmadi");
//
//     // Mahsulotlar ro'yxatini tayyorlash
//     var productsWithData = new List<(string Name, int Quantity, decimal Price)>();
//     foreach (var item in order.ProductsIds)
//     {
//         var productData = await GetProductsDates(item.ProductType, item.ProductId);
//         if (productData != null)
//         {
//             productsWithData.Add((
//                 productData.Name?.uz ?? "Noma'lum",
//                 item.QuantityBox,
//                 productData.Price
//             ));
//         }
//     }
//
//     var clientName = order.Client?.ClientFullName ?? "Mijoz";
//
//     var stream = new MemoryStream();
//
//     var document = Document.Create(container =>
//     {
//         container.Page(page =>
//         {
//             page.Margin(15);
//             page.Size(PageSizes.A6); 
//             page.PageColor(Colors.White);
//             page.DefaultTextStyle(x => x.FontSize(10));
//
//             // Header
//             page.Header().Text($"Buyurtma ID: {order.Id} - {clientName}")
//                 .FontSize(12).Bold();
//
//             page.Content().PaddingVertical(10).Table(table =>
//             {
//                 table.ColumnsDefinition(columns =>
//                 {
//                     columns.RelativeColumn(4); // Mahsulot nomi uzunroq bo‘lsin
//                     columns.RelativeColumn(1); // Soni
//                     columns.RelativeColumn(2); // Narxi
//                 });
//
//                 // Jadval header
//                 table.Header(header =>
//                 {
//                     header.Cell().Text("Mahsulot nomi").Bold();
//                     header.Cell().Text("Soni").Bold();
//                     header.Cell().Text("Narxi").Bold();
//                 });
//
//                 // Jadval satrlari
//                 foreach (var p in productsWithData)
//                 {
//                     table.Cell().Text(p.Name);
//                     table.Cell().AlignCenter().Text(p.Quantity.ToString());
//                     table.Cell().AlignRight().Text(p.Price.ToString("N0") + " so‘m");
//                 }
//             });
//
//             // Footer
//             page.Footer().AlignCenter()
//                 .Text($"Umumiy summa: {order.TotalPrice:N0} so‘m")
//                 .FontSize(12).SemiBold();
//         });
//     });
//
//     document.GeneratePdf(stream);
//     stream.Position = 0;
//     return stream;
// }



public async Task<Product> GetProductsDates(string productType,long productId)
    {
        Product product = new Product();

        switch (productType)
        {
            case "OilProduct" :
                product= await _oilProductsRepository.GetByIdAsync(productId);
                break;               
            case "HouseHoldProduct" :
                product= await _houseHoldProductsRepository.GetByIdAsync(productId);
                break;                
            case "FoodProduct" :
                product= await _foodProductRepository.GetByIdAsync(productId);
                break;                
            case "WaterAndDrinksProduct" :
                product=await _waterAndDrinksRepository.GetByIdAsync(productId);
                break;
            default:
                product=null;
                break;
        }

        return product;
    }

}