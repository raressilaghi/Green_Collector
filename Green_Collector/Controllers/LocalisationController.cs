using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using GoogleMaps.LocationServices;
using Green_Collector.Models;
using Green_Collector.Models.ViewModels;
using Green_Collector.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using static DataLibrary.BusinessLogic.CustomersProcessor;
using static Green_Collector.Controllers.OrdersController;


namespace Green_Collector.Controllers
{
    public class LocalisationController : Controller
    {

        private static int ColectionDay()
        {
            int i = 0;
            DateTime today = DateTime.Today;
            if (today.DayOfWeek != DayOfWeek.Friday)
            {
                while (today.DayOfWeek != DayOfWeek.Friday)
                {
                    today = today.AddDays(1);
                    i++;
                }
                return i;
            }
            else
                return i;
        }

        [Authorize(Roles = "PaperCollector")]
        public ActionResult Hartie()
        {
            var i = ColectionDay();
            ViewBag.ListOfDropdown = locationslist(1).ToList();
            if(i==0)
                ViewBag.Message ="Azi este ziua de colectare!";
            else
            {
                ViewBag.Message = "Au mai rămas "+i+" zile până la colectare!";
            }
            
            
            return View();
        }

        [Authorize(Roles = "GlassCollector")]
        public ActionResult Sticla()
        {
            
            ViewBag.ListOfDropdown = locationslist(1003).ToList();
            var i = ColectionDay();
            if (i == 0)
                ViewBag.Message = "Azi este ziua de colectare!";
            else
            {
                ViewBag.Message = "Au mai rămas " + i + " zile până la colectare!";
            }
            return View();
        }

        [Authorize(Roles = "MetalCollector")]
        public ActionResult Metal()
        {
            
            ViewBag.ListOfDropdown = locationslist(2).ToList();
            var i = ColectionDay();
            if (i == 0)
                ViewBag.Message = "Azi este ziua de colectare!";
            else
            {
                ViewBag.Message = "Au mai rămas " + i + " zile până la colectare!";
            }
            return View();
        }

        [Authorize(Roles = "PlasticCollector")]
        public ActionResult Plastic()
        {
            ViewBag.ListOfDropdown = locationslist(1002).ToList();
            var i = ColectionDay();
            if (i == 0)
                ViewBag.Message = "Azi este ziua de colectare!";
            else
            {
                ViewBag.Message = "Au mai rămas " + i + " zile până la colectare!";
            }
            return View();
        }

        [Authorize]
        public ActionResult OrderStatus()
        {
            ViewBag.Message = ColectionDay();
            var data = LoadLocations();
            var data2 = LoadReset();
            var reset = new DateTime(0001, 1, 1);
            var dateNow = DateTime.Now;
            List<ViewStatus> info = new List<ViewStatus>();

            foreach (var row2 in data2)
            {
                reset = row2.ResetDate;
            }
            foreach(var row in data)
            {
                    if (dateNow.Date == reset.Date)
                    {
                        if (row.Date.Date >= reset.Date.AddDays(-7) && row.Date.Date != reset.Date && row.Collected == "Fals" && row.ClientName == User.Identity.GetUserName())
                        {
                            info.Add(new ViewStatus
                            {
                                Waste = FindType(row.TypeId),
                                Quantity = row.Quantity,
                                Address = row.Address,
                                Data = row.Date
                            });
                        }
                    }
                    else
                    {
                    if (row.Date.Date >= reset.Date && row.Collected == "Fals" && row.ClientName == User.Identity.GetUserName())
                    {
                        info.Add(new ViewStatus
                        {
                            Waste = FindType(row.TypeId),
                            Quantity = row.Quantity,
                            Address = row.Address,
                            Data = row.Date
                        });
                    }
                }
            }
                

            return View(info);
        }

        public static List<GeoLoc> GetLocations(DateTime resetDate, DateTime dateNow)
        {
            var data = LoadLocations();
            List<GeoLoc> locations = new List<GeoLoc>();
            foreach (var row in data)
            {
                if (dateNow.Date == resetDate.Date)
                {
                    if (row.Date.Date >= resetDate.Date.AddDays(-7) && row.Date.Date != resetDate.Date)
                        if (row.Collected == "Fals")
                            locations.Add(new GeoLoc
                            {
                                OrderId = row.OrderId,
                                TypeId = row.TypeId,
                                Quantity = row.Quantity,
                                Address = row.Address,
                                Longitude = Convert.ToDouble(row.Longitude),
                                Latitude = Convert.ToDouble(row.Latitude),
                                Date = row.Date,
                                Collected = row.Collected

                            });
                }
                else if (row.Date.Date >= resetDate.Date)
                    if (row.Collected == "Fals")
                        locations.Add(new GeoLoc
                        {
                            OrderId = row.OrderId,
                            TypeId = row.TypeId,
                            Quantity = row.Quantity,
                            Address = row.Address,
                            Longitude = Convert.ToDouble(row.Longitude),
                            Latitude = Convert.ToDouble(row.Latitude),
                            Date = row.Date,
                            Collected = row.Collected

                        });


            }
            return locations;
        }

        public ActionResult GetAllLocation()
        {
            ViewBag.Message = "Your application description page.";
            var dateNow = DateTime.Now;
            var data2 = LoadReset();
            var resetDate = new DateTime(2020, 1, 1);
            var zero = new DateTime(0001, 1, 1);
            
            foreach (var row2 in data2)
            {
                if(row2.ResetDate != zero.Date)
                resetDate = row2.ResetDate;
            }
            if ( dateNow >= resetDate.Date.AddDays(7))
            {
                
                    while (dateNow.DayOfWeek != DayOfWeek.Friday)
                    {
                        dateNow = dateNow.AddDays(-1);
                    }
                    
                    CreateReset(dateNow.Date);

                return Json(GetLocations(resetDate, dateNow), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(GetLocations(resetDate, dateNow), JsonRequestBehavior.AllowGet);
            }
        }

        public List<LocationList> locationslist(int type)
        {
            var data = LoadLocations();
            List<LocationList> locations = new List<LocationList>();
            var dateNow = DateTime.Now;
            var data2 = LoadReset();
            var resetDate = new DateTime(2020, 1, 1);
            var zero = new DateTime(0001, 1, 1);
            foreach (var row2 in data2)
            {
                if (row2.ResetDate != zero.Date)
                    resetDate = row2.ResetDate;
            }
            
            
                foreach (var row in data)
                {
                    if (dateNow.Date == resetDate.Date)
                    {
                        if (row.Date.Date >= resetDate.Date.AddDays(-7) && row.Date.Date != resetDate.Date && row.TypeId == type)
                            if (row.Collected == "Fals")
                                locations.Add(new LocationList
                                {
                                    Address = row.Address,
                                });
                    }
                    else if (row.Date.Date >= resetDate.Date && row.TypeId == type)
                        if (row.Collected == "Fals")
                            locations.Add(new LocationList
                            {
                                Address = row.Address,
                            });
                }
                return locations;
            
            
            
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        

        public static Coordinate GetCoordinates(string loc)
        {
            using (var clt = new WebClient())
            {
                string uri = "https://maps.googleapis.com/maps/api/geocode/json?address=" + loc + "&key=AIzaSyBm90sjnsiX7hNaEeA5nH-CicO5Jo-yUCt6JM";

                string geocodeInfo = clt.DownloadString(uri);
                JavaScriptSerializer oJS = new JavaScriptSerializer();
                GoogleGeoCodeResponse latlongdata = oJS.Deserialize<GoogleGeoCodeResponse>(geocodeInfo);

                return new Coordinate(Convert.ToDouble(latlongdata.results[0].geometry.location.lat), Convert.ToDouble(latlongdata.results[0].geometry.location.lng));
            }
        }

        public struct Coordinate
        {
            private double lat;
            private double lng;

            public Coordinate(double latitude, double longitude)
            {
                lat = latitude;
                lng = longitude;

            }

            public double Latitude { get { return lat; } set { lat = value; } }
            public double Longitude { get { return lng; } set { lng = value; } }

        }


    }
}
