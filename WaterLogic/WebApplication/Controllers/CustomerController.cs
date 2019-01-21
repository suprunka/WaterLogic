using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.CustomerReference;
using WebApplication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace WebApplication.Controllers
{
    public class CustomerController : Controller
    {
        ICustomerService proxy = new CustomerReference.CustomerServiceClient("customerHttpEndpoint");
        // GET: Customer
        [HttpGet]
        public ActionResult UserProfile()
        {
            SetPropertiesModel model = null;
            var customer = proxy.GetCustomer(User.Identity.GetUserId());
            model = new SetPropertiesModel() { Name = customer.Name, Address = customer.Address, AspId = customer.AspId };
            return View("UserProfile",model);
        }
        //Post
        [HttpPost]

        public ActionResult UserProfile(SetPropertiesModel model)
        {
            proxy.Edit(new Model.Customer() { Name = model.Name, Address = model.Address, AspId = User.Identity.GetUserId() });

            return View("UserProfile", model);
        }

        public ActionResult Remove()
        { var x = proxy.DeleteAsync(User.Identity.GetUserId());
            if (x.Result)
            {
                return RedirectToAction("LogOff", "Account");

            }
            return View();

        }
    }
}