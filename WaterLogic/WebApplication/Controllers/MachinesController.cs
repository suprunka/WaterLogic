using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.MachinesReference;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class MachinesController : Controller
    {
        private IMachinesService proxy = new MachinesServiceClient("machinesHttpEndpoint");
        // GET: Machines
        public ActionResult Index()
        {
            IList<MachineOfferModel> models = new List<MachineOfferModel>();
            var list = proxy.GetAllAsync().Result;
            int type = 3;
            foreach (var el in list)
            {
                switch (el.MachineType)
                {
                    case MachineType.Small:
                        type = 1;
                        break;
                    case MachineType.Medium:
                        type = 2;
                        break;
                }

                models.Add(new MachineOfferModel()
                {
                    Id = el.Id,
                    Name = el.Name,
                    Price = el.Price,
                    Type = type,

                });
            }
            return View("Index", models);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(AddMachineModel model)
        {
            MachineType mType = MachineType.Big;
            switch (model.Type)
            {
                case 1:
                    mType = MachineType.Small;
                    break;
                case 2:
                    mType = MachineType.Medium;
                    break;
            }


           var result = proxy.CreateAsync(new Model.Machine() { Name = model.Name, Description = model.Description, Price = model.Price, Quantity = model.Quantity, MachineType = mType });
            if (result.Result)
            {
                return View("Add", new AddMachineModel() { Succed = true });

            }
            return View("Add", new AddMachineModel() { Succed = false });
        }
        public ActionResult Details(int id)
        {
            var machine = proxy.GetAsync(id).Result;
            int type = 3;

            switch (machine.MachineType)
            {
                case MachineType.Small:
                    type = 1;
                    break;
                case MachineType.Medium:
                    type = 2;
                    break;
            }
            return View("Details", new AddMachineModel() {Id= machine.Id, Name = machine.Name, Description = machine.Description, Price = machine.Price, Quantity = machine.Quantity, Type = type });
        }
    }
}