using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Green_Collector.Models;
using Green_Collector.ViewModels;
using DataLibrary;
using static DataLibrary.BusinessLogic.CustomersProcessor;
using Microsoft.AspNet.Identity;
using Green_Collector.Models.ViewModels;
using Microsoft.Owin.BuilderProperties;
using static Green_Collector.Controllers.LocalisationController;
using System.Security.Cryptography.X509Certificates;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace Green_Collector.Controllers
{
    public class OrdersController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Waste()
        {
            ViewBag.Message = "Add Waste Types";
            return View();
        }

        public ActionResult Edit(int? Id)
        {
            var data = LoadLocations();
            EditStatus a= new EditStatus();
            foreach (var row in data)
                if (row.OrderId == Id )
                {
                    a.Id = row.OrderId;
                    a.Status = row.Collected;
                }
                    
            return View(a);
        }

        [HttpPost]
        public ActionResult Edit(EditStatus model)
        {
            if (ModelState.IsValid)
            {
                int updateStatus = UpdateStatus(model.Id, model.Status);
                return RedirectToAction("IstoricHartie");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Waste(WasteModel model)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = CreateWaste(model.TypeName);
                return RedirectToAction("Waste");
            }
            return View();
        }

        [Authorize]
        public ActionResult NewOrder()
        {
            ViewBag.Message = "Add a new order";
            var item = LoadWaste();
            OrderWasteViewModel type = new OrderWasteViewModel();
            type.WasteTypes = item.Select(x => new CheckBoxItem()
            {
                Id = x.Id,
                Title = x.TypeName,
                IsChecked = false,
                Quantity = 0

            }).ToList();
            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewOrder(OrderWasteViewModel OWVM, OrderModel order,  LocationCoordinates COO )
        {
            var data1 = LoadCustomers();
            var idClient = 0;
            foreach (var row1 in data1)
            {
                if(row1.EmailAddress == User.Identity.GetUserName())
                {
                    idClient = row1.Id;
                }
            }
            order.Town = "Baia Mare";
                order.Number = OWVM.Number;
                order.Address = OWVM.Address;
                order.PhoneNumber = OWVM.PhoneNumber;
                order.Date = DateTime.Now;
                order.CustomerId = idClient;
            
                

            CreateComenzi(order.Town, OWVM.Address, OWVM.Number, OWVM.PhoneNumber, DateTime.Now, order.CustomerId);

            var customer = LoadCustomers();
            var id = 0;
            var q = 0;
            var data = LoadComenzi();
            var orderId=0;
            var town = "";
            var street = "";
            var number = "";
            var address = ""; 
            
            foreach (var row in data)
            {
                orderId = row.Id;
                town = row.Town;
                street = row.Address;
                number = row.Number.ToString();
                address = town + " Strada " + street + " Numarul " + number;
            }

                foreach (var item in OWVM.WasteTypes)
                {
                    if (item.IsChecked == true)
                    {


                       
                        COO.ClientName = User.Identity.GetUserName();
                        Coordinate coordinate = new Coordinate();
                        coordinate = GetCoordinates(address);
                        COO.OrderId = orderId;
                        COO.TypeId = item.Id;
                        COO.Quantity =item.Quantity;
                        COO.Address = address;
                        COO.Longitude = ((decimal)coordinate.Longitude).ToString();
                        COO.Latitude = ((decimal)coordinate.Latitude).ToString();
                        COO.Date =order.Date;
                        CreateLocations(COO.OrderId, COO.TypeId, COO.Quantity, COO.Address, COO.Longitude, COO.Latitude, COO.Date, COO.ClientName);
                        foreach(var row in customer)
                    {
                        if (COO.ClientName == row.EmailAddress)
                        {
                            id = row.Id;
                            q = q+row.QuantityColected;
                        }
                    }
                    UpdateQuantityColected(id, q, COO.Quantity);

                    }
                }
                
           
            return RedirectToAction("Confirmare", "Orders");
        }

        
        public static string FindType(int TypeId)
        {
            var tip = "";
            if (TypeId == 1)
                tip = "Hartie";
            else if (TypeId == 2)
                tip = "Metal";
            else if (TypeId == 1002)
                tip = "Plastic";
            else if (TypeId == 1003)
                tip = "Sticla";
            return tip;
        }
        
        public List<ViewComenzi> ViewOrders( int TypeId)
        {
            
            var dataComenzi = LoadComenzi();
            var status = LoadLocations();
            List<ViewComenzi> comenzi = new List<ViewComenzi>();
            foreach (var row in status)
            {
               if (row.TypeId == TypeId)
               {
                   foreach (var row1 in dataComenzi)
                   {
                       if (row.OrderId == row1.Id)
                       {
      
                                comenzi.Add(new ViewComenzi
                                {
                                    IdComanda = row1.Id,
                                    UserName = row.ClientName,
                                    WasteType = FindType(TypeId),
                                    Quantity = row.Quantity,
                                    Town = row1.Town,
                                    Address = row1.Address,
                                    Number = row1.Number,
                                    PhoneNumber = row1.PhoneNumber,
                                    Date = row1.Date,
                                    Status = row.Collected,
                                    IdType=row.TypeId

                                });
                            
                       }
                   }
               }

            }
        return (comenzi);                          
        }

        [Authorize(Roles = "PaperCollector")]
        public ActionResult IstoricHartie()
        {
            ViewBag.Message = "IstoricHartie";
            var TypeId = 1;
            
            return View(ViewOrders(TypeId));
        }

        [Authorize(Roles = "MetalCollector")]
        public ActionResult IstoricMetal()
        {
            ViewBag.Message = "IstoricMetal";
            var TypeId = 2;

            return View(ViewOrders(TypeId));
        }

        [Authorize(Roles = "GlassCollector")]
        public ActionResult IstoricSticla()
        {
            ViewBag.Message = "IstoricSticla";
            var TypeId = 1003;

            return View(ViewOrders(TypeId));
        }

        [Authorize(Roles = "PlasticCollector")]
        public ActionResult IstoricPlastic()
        {
            ViewBag.Message = "IstoricPlastic";
            var TypeId = 1002;

            return View(ViewOrders(TypeId));
        }

        public ActionResult Confirmare()
        {
            return View();
        }

    }
}

