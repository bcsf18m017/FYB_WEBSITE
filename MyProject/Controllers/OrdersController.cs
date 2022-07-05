using FYP.Controllers;
using IqbalElectronics.DB.Models;
using MyProject.Api_Calls;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class OrdersController : Controller
    {
        public static List<Order> Orders;
        public static List<OrderDetail> orderDetails;
        private static Order currentOrder;
        
        // GET: Orders
        public async Task<ActionResult> AllOrders()
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }            
            ViewBag.IsOrderMessage = "no";            
            if (Orders==null)
            {
                Orders = await ApiCalls.getAllOrders();
            }
            return View(Orders);
        }
        public async Task<ActionResult> OrderDetails()
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }
            if (orderDetails==null)
            {
                orderDetails = await ApiCalls.getAllOrdersDetails();
            }
            if (Request["viewDetails"] != null)
            {
                List<OrderDetail> detail=new List<OrderDetail>();
                currentOrder = Orders.Find(o => o.Id == Convert.ToInt32(Request["viewDetails"]));
                for (int i = 0; i < orderDetails.Count; i++)
                {
                    if (orderDetails[i].OrderId == currentOrder.Id)
                    {
                        detail.Add(orderDetails[i]);
                    }
                }

                return View("OrderDetailsView", detail);
            }
            return View("AllOrders", "Orders");
        }

        public async Task<ActionResult>RefreshData()
        {

            Orders = await ApiCalls.getAllOrders();
            orderDetails = await ApiCalls.getAllOrdersDetails();
            return RedirectToAction("AllOrders", Orders);
        }
        public async Task<ActionResult>AddPayment()
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }            
            if (Request["payment"] != null)
            {
                OrderDetail orderDetail = orderDetails.Find(o => o.Id == Request["payment"]);
                Product p = orderDetail.Product;
                int price = p.Price + ((p.Price * p.Percentage) / 100);
                price = price / p.MinimumInstallments;
                if(p.Daily &&!p.Monthly)
                {
                    price = price / 30;   
                }
                OrderDetailModel orderDetailModel = new OrderDetailModel()
                {
                    OrderDetailId = orderDetail.Id,
                    ProductId=orderDetail.ProductId,
                    OrderId=orderDetail.OrderId,
                    ProductQuantity=orderDetail.ProductQuantity,
                    TotalAmount=orderDetail.TotalAmount,
                    AmountDue=orderDetail.AmountDue-price,
                    DueDate=orderDetail.DueDate,
                    MinimumInstallment=orderDetail.MinimumInstallment
                };
                await ApiCalls.updateOrderDetails(orderDetail.Id,orderDetailModel);

                Order order = Orders.Find(ord => ord.Id == orderDetailModel.OrderId);
                OrderModel orderModel = new OrderModel
                {
                    OrderId = orderDetail.OrderId,
                    PersonId = order.PersonId,
                    TotalBill = order.TotalBill,
                    BillDue = order.BillDue - price,
                    CreatedOn = order.CreatedOn
                };
                await ApiCalls.updateOrder(order.Id, orderModel);
                TransactionHistoryModel model = new TransactionHistoryModel
                {
                    TransactionHistoryId= "2f51ea22-3383-4831-85d7-4e12cc2ead2a",
                    OrderDetailId=orderDetail.Id,
                    AmountReceived=price,
                    CreatedOn=DateTime.Now,
                    PersonID=order.PersonId
                };
                await ApiCalls.addTransactionHistory(model);
                Orders = await ApiCalls.getAllOrders();
                ViewBag.IsOrderMessage = "Yes";
                ViewBag.AllOrderMessage = "Transaction Added Successfully";
                return View("AllOrders", Orders);
            }
            ViewBag.IsOrderMessage = "Yes";
                ViewBag.AllOrderMessage = "Unable to complete Transaction";

            return View("AllOrders", Orders);
        }
    }
}