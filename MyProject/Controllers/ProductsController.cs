using FYP.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Web;
using MyProject.Models;
using System.Net.Http;
using System.Web.UI.WebControls;
using IqbalElectronics.DB.Models;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FYP.Controllers
{
    public class ProductsController : Controller
    {

       static  Account account = new Account(
      "nomancloudinary",
      "265779579282175",
      "mIrPL8wcDU1crT3Dk4eC37usiT4");

        Cloudinary cloudinary = new Cloudinary(account);
        FileUpload uploader { get; set; }
        private readonly HttpClient httpClient;
        
        public ProductsController()
        {
            this.httpClient = new HttpClient();
            uploader = new FileUpload();
        }


        public async Task<List<Product>> getAllProducts()
        {
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var response= await this.httpClient.GetAsync("https://localhost:7232/api/Products");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<Product>>(json,options);
            return products;
        }



        private static int edit = -1;
        List<CategoryModel> categories = new List<CategoryModel>
            {
                new CategoryModel
                {
                    CategoryId=1,
                    Description="Mobile Phones"
                },
                new CategoryModel
                {
                    CategoryId=2,
                    Description="Laptops"
                },
                new CategoryModel
                {
                    CategoryId=3,
                    Description="Apliances"
                }

            };
        List<Product> products = new List<Product>
        {
            new Product
            {
                Id="1",
                Title="Samsung S10",
                Description="abcdefghijklmnopqrstuvwxyzc cnnceknc ncoqndkwn knwkdnk xkxjqmdxkdinx",
                Image="https://res.cloudinary.com/nomancloudinary/image/upload/v1637396398/sample.jpg",
                Price=100,
                CategoryId="1234567890",
                Percentage=15,
                Daily=true,
                Monthly=false,
                CreatedBy="2345678943678283 y3u9",
                Category=new Category
                {
                    Id="73720 9723 283710",
                    Description="Mobile Phones"
                },
                OrderDetails=new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        Id="1",
                        ProductId="1",
                        OrderId=1,
                        ProductQuantity=1,
                        TotalAmount=100,
                        AmountDue=11,
                        DueDate=DateTime.Now,
                        MinimumInstallment=10,
                    },
                    new OrderDetail
                    {
                        Id="1",
                        ProductId="1",
                        OrderId=1,
                        ProductQuantity=1,
                        TotalAmount=100,
                        AmountDue=11,
                        DueDate=DateTime.Now,
                        MinimumInstallment=10,
                    }
                },
                DealerPurchases=new List<DealerPurchase>
                {
                    new DealerPurchase
                    {
                        Id=1,
                        ProductId="1",
                        PersonId="1",
                        Quantity=10,
                        TotalBill=50000,
                        PaymentMethodId="123456789"
                    }      , new DealerPurchase
                    {
                        Id=1,
                        ProductId="1",
                        PersonId="1",
                        Quantity=10,
                        TotalBill=50000,
                        PaymentMethodId="123456789"
                    }, new DealerPurchase
                    {
                        Id=1,
                        ProductId="1",
                        PersonId="1",
                        Quantity=10,
                        TotalBill=50000,
                        PaymentMethodId="123456789",CreatedOn=DateTime.Now
                    }
                }

            }
        };
        public async Task<ActionResult> AllProducts()
        {
            //List<Product> pr = await getAllProducts();
            return View(products);
        }
        public ActionResult ProductDetails()
        {
            if (Request["viewDetails"] != null)
            {
                if(products[0].Daily)
                {
                    ViewBag.Type = "Daily";
                }
                else
                {
                    ViewBag.Type = "Monthly";
                }
                return View("ProductDetailsView", products[0]);
            }
            else
            {
                return View("AllProducts","Products");
            }
        }

        public ActionResult deleteOrEdit()
        {
            int delete = Request["delete"] == null ? -1 : Convert.ToInt32(Request["delete"]);
            int edit = Request["edit"] == null ? -1 : Convert.ToInt32(Request["edit"]);
            if (edit != -1)
            {
                Product p = products[0];   
                return RedirectToAction("EditProduct",p);
            }
            else
            {
                //TODO delete product
                return RedirectToAction("AllProducts", "Products");

            }
        }
        public ActionResult EditProduct(Product p)
        {
            @ViewBag.NavAction = "Products";
            edit = 1;
            ViewBag.EditProductCategory = products[0].Category.Description;
            ViewBag.EditProductPaymentType = p.Daily ? "Daily" : "Monthly";
            ViewBag.Categories = categories;
            return View(p);
        }
       
        


        public ActionResult AddProduct()
        {
            @ViewBag.NavAction = "Products";
            edit = -1;
            return View(categories);
        }
        [HttpPost]
        [ActionName("AddProduct")]
        public ActionResult AddProduct(string code,string title, string description, string price, string category, string percentage, string type)
        {
            String image;
            HttpPostedFileBase file = Request.Files["image"];
            if (file != null && file.ContentLength > 0)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.InputStream)
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                image = uploadResult.Uri.ToString();
            }
            else
            {
                image = "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png";
            }
            ProductModel p = new ProductModel { 
                ProductId= Convert.ToInt16(code), 
                Title =title,
                Description= description, 
                Image = image, 
                Price = Convert.ToInt32(price),
                CategoryId= 1, 
                Percentage= Convert.ToInt32(percentage),
                Daily= true,
                Monthly= false, 
                CreatedBy= 2 };
            if(edit==1)
            {
                //EDIT
                return RedirectToAction("AddProduct", "Products");
            }
            else
            {
                //ADD
            }
            return RedirectToAction("AllProducts","Products");
        }
    }
}
