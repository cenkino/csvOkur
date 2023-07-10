using Microsoft.AspNetCore.Mvc;
using WebApplicationCenkY.Entities;
using System.Web;

namespace WebApplicationCenkY.Controllers
{
    public class FeedController : Controller
    {
        DatabaseContext _dbcontext;
        IWebHostEnvironment _webHostEnvironment;
        public FeedController(DatabaseContext dbcontext, IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = dbcontext;
            _webHostEnvironment = webHostEnvironment;
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    // Dosyayı okuyup veritabanına kaydetmek için bir metodu çağırabilirsiniz.
                    ReadAndSaveCSVData(path);
                    ViewBag.Message = "Dosya başarıyla yüklendi.";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Dosya yükleme hatası: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Lütfen bir dosya seçin.";
            }
            return View();
        }
        private void ReadAndSaveCSVData(string filePath)
        {

            var csvLines = System.IO.File.ReadAllLines(filePath).Skip(1); // Başlıkları atla
            foreach (var line in csvLines)
            {
                var values = line.Split(',');
                var product = new Product
                {
                    Barcode = values[0],
                    Price = decimal.Parse(values[1]),
                    Stock = int.Parse(values[2]),
                    Name = values[3],
                    ProductCode = values[4],
                    Description = values[5],
                    Brand = values[12],
                    Category = values[13],
                    Desi = values[14]
                };
                for (int i = 5; i <= 9; i++)
                {
                    if (!string.IsNullOrEmpty(values[i]))
                    {
                        product.Images.Add(new Image { FileName = values[i] });
                    }
                }
                _dbcontext.Products.Add(product);
            }
            _dbcontext.SaveChanges();

        }

        public ActionResult Read(string csvFilePath)
        {
            if (!string.IsNullOrEmpty(csvFilePath))
            {
                try
                {
                    
                    ReadAndMapCSVData(csvFilePath);
                    ViewBag.Message = "CSV dosyası başarıyla okundu ve veriler eşleştirildi.";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "CSV dosyasını okuma hatası: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Lütfen bir CSV dosyası seçin.";
            }
            return View();
        }

        private void ReadAndMapCSVData(string csvFilePath)
        {

            var csvLines = System.IO.File.ReadAllLines(csvFilePath).Skip(1); // Başlıkları atla
            foreach (var line in csvLines)
            {
                var values = line.Split(',');
                var barcode = values[0];
                var price = decimal.Parse(values[1]);
                var stock = int.Parse(values[2]);
                var name = values[3];
                var productCode = values[4];
                var product = _dbcontext.Products.FirstOrDefault(p => p.Barcode == barcode);
                if (product != null)
                {
                    product.Price = price;
                    product.Stock = stock;
                    product.Name = name;
                    product.ProductCode = productCode;
                }
            }
            _dbcontext.SaveChanges();

        }


    }
}
