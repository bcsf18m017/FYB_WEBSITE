using IqbalElectronics.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class OrdersController : Controller
    {
        List<Order> order = new List<Order>
        {
            new Order
            {
                Id = 1,
                PersonId="1",
                TotalBill=200,
                BillDue=100,
                CreatedOn=DateTime.Now,
                Person=new Person
                {
                    Name = "Noman",
                    Address = "h#7,st#15 ramgarh main Bazar Mughallpura,Karachi",
                    Cnic = "3520126349331",
                    Image = "https://res.cloudinary.com/nomancloudinary/image/upload/v1654599479/z4eezx4pmi0i0sjaorkd.jpg",
                    Salary = 10000,
                    CreatedOn = DateTime.Now,
                    CreatorId= "1"
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
                        Product=new Product
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
                            CreatedBy="2345678943678283 y3u9"
                        }
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
                        Product=new Product
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
                            CreatedBy="2345678943678283 y3u9"
                        }
                    }
                }
                

            }
        };
        // GET: Orders
        public ActionResult AllOrders()
        {
            return View(order);
        }
        public ActionResult OrderDetails()
        {
            if (Request["viewDetails"] != null)
            {
                return View("OrderDetailsView", order.Find(p => p.Id ==Convert.ToInt32(Request["viewDetails"])));
            }
            return View("AllOrders", "Orders");
        }
    }
}