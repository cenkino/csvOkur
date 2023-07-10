

using AppConsoleCY.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppConsoleCY
{
    internal class Program
    {

        static void Main(string[] args)

        {


            
            var timer = new Timer(ProcessFeed, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
            Console.WriteLine("Uygulama çalışıyor. Çıkmak için Enter tuşuna basın.");
            Console.ReadLine();
        }
        static void ProcessFeed(object state)
        {
            try
            {
                string feedFilePath = @"C:/Users/cenk/Desktop/xml-to-csv.csv"; 
                
                ReadAndSaveCSVData(feedFilePath);
                Console.WriteLine("CSV feed dosyası başarıyla işlendi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("CSV feed dosyasını işleme hatası: " + ex.Message);
            }
        }
        static void ReadAndSaveCSVData(string filePath)
        {

            DatabaseContext dbContext = new DatabaseContext();

            var csvLines = File.ReadAllLines(filePath).Skip(1); 
            foreach (var line in csvLines)
            {
                var values = line.Split(',');
                var barcode = values[0];
                var price = decimal.Parse(values[1]);
                var stock = int.Parse(values[2]);
                var name = values[3];
                var productCode = values[4];
                var product = dbContext.Products.FirstOrDefault(p => p.Barcode == barcode);
                if (product != null)
                {
                    
                    product.Price = price;
                    product.Stock = stock;
                }
                else
                {
                    
                    var newProduct = new Product
                    {
                        Barcode = barcode,
                        Price = price,
                        Stock = stock,
                        Name = name,
                        Product_code = productCode,
                        Description = values[10],
                        Brand = values[11],
                        Category = values[12],
                        Desi = values[14]
                    };
                    for (int i = 5; i <= 9; i++)
                    {
                        if (!string.IsNullOrEmpty(values[i]))
                        {
                            newProduct.Images.Add(new Image { FileName = values[i] });
                        }
                    }
                    dbContext.Products.Add(newProduct);
                }
            }
            dbContext.SaveChanges();
        }
    }
}