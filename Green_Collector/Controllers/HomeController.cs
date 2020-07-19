using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Green_Collector.Models;
using DataLibrary;
using static DataLibrary.BusinessLogic.CustomersProcessor;

namespace Green_Collector.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult About()
        {
            

            return View();
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Authorize(Roles ="Admin")]
        public ActionResult ViewCustomers()
        {
            ViewBag.Message = "Customers List";
            var data = LoadCustomers();
            List<Customer> customers = new List<Customer>();
           

            foreach(var row in data)
            {
                customers.Add(new Customer
                {
                    Id = row.Id,
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    EmailAddress = row.EmailAddress,
                    PhoneNumber = row.PhoneNumber,
                    Address = row.Address,
                    UniqueCodeAccess = row.UniqueCodeAccess,
                    QuantityColected =row.QuantityColected

                });
            }
            return View(customers);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Customers( )
        {
            ViewBag.Message = "Customer Sign Up";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]     
        public ActionResult Customers(Customer model)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = CreateCustomers(
                    model.FirstName,
                    model.LastName,
                    model.EmailAddress,
                    model.PhoneNumber,
                    model.Address,
                    RandomString(8));
                return RedirectToAction("Confirmare", "Home");
            }
            return View();
        }

        public ActionResult Confirmare()
        {
            return View();
        }

        public ActionResult RegisterError()
        {
            return View();
        }

        public ActionResult Detali(int? Id)
        {
            var data = LoadCustomers();
            Customer a = new Customer();
            foreach (var row in data)
                if (row.Id == Id)
                {
                    a.FirstName = row.FirstName;
                    a.LastName = row.LastName;
                    a.EmailAddress = row.EmailAddress;
                    a.PhoneNumber = row.PhoneNumber;
                    a.Address = row.Address;
                    a.UniqueCodeAccess = row.UniqueCodeAccess;
                }

            return View(a);
        }
    }
}