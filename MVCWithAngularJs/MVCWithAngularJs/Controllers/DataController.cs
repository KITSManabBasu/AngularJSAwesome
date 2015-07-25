using MVCWithAngularJs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MVCWithAngularJs.Controllers
{
    public class DataController : Controller
    {
        //
        // GET: /Data/
        //For fetch Last Contact
        public JsonResult GetLastContact()
        {
            Contact c = null;
            //here MyDatabaseEntities our DBContext
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                c = dc.Contacts.OrderByDescending(a => a.ContactID).Take(1).FirstOrDefault();
            }
            return new JsonResult { Data = c, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult UserLogin(LoginData d)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var user = dc.Users.Where(a => a.Username.Equals(d.Username) && a.Password.Equals(d.Password)).FirstOrDefault();
                return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetEmployeeList()
        {
            List<Employee> Employee = new List<Employee>();
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                Employee = dc.Employees.OrderBy(a => a.FirstName).ToList();
            }

            return new JsonResult { Data = Employee, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // Fetch Country
        public JsonResult GetCountries()
        {
            List<Country> allCountry = new List<Country>();
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                allCountry = dc.Countries.OrderBy(a => a.CountryName).ToList();
            }
            return new JsonResult { Data = allCountry, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // Fetch State by Country ID
        public JsonResult GetStates(int countryID)
        {
            List<State> allState = new List<State>();
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                allState = dc.States.Where(a => a.CountryID.Equals(countryID)).OrderBy(a => a.StateName).ToList();
            }
            return new JsonResult { Data = allState, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Save Simple Registration Form
        [HttpPost]
        public JsonResult Register(User u)
        {
            string message = "";
            //Here we will save data to the database
            if (ModelState.IsValid)
            {
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    //check username available
                    var user = dc.Users.Where(a => a.Username.Equals(u.Username)).FirstOrDefault();
                    if (user == null)
                    {
                        //Save here
                        dc.Users.Add(u);
                        dc.SaveChanges();
                        message = "Success";
                    }
                    else
                    {
                        message = "Username not available!";
                    }
                }
            }
            else
            {
                message = "Failed!";
            }
            return new JsonResult { Data = message };
        }

        [HttpGet]
        public JsonResult CustomerOrders()
        {
            List<CustomerOrders> CO = new List<CustomerOrders>();
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var cust = dc.Customers.OrderBy(a => a.ContactName).ToList();
                foreach (var i in cust)
                {
                    var orders = dc.Orders.Where(a => a.CustomerID.Equals(i.CustomerID)).OrderBy(a => a.OrderDate).ToList();
                    CO.Add(new CustomerOrders
                    {
                        Customer = i,
                        Orders = orders
                    });
                }
            }
            return new JsonResult { Data = CO, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult SaveFiles(string description)
        {
            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;
            bool flag = false;
            if (Request.Files != null)
            {
                var file = Request.Files[0];
                actualFileName = file.FileName;
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                int size = file.ContentLength;

                try
                {
                    file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), fileName));

                    UploadedFile f = new UploadedFile
                    {
                        FileName = actualFileName,
                        FilePath = fileName,
                        Description = description,
                        FileSize = size
                    };
                    using (MyDatabaseEntities dc = new MyDatabaseEntities())
                    {
                        dc.UploadedFiles.Add(f);
                        dc.SaveChanges();
                        Message = "File uploaded successfully";
                        flag = true;
                    }
                }
                catch (Exception)
                {
                    Message = "File upload failed! Please try again";
                }

            }
            return new JsonResult { Data = new { Message = Message, Status = flag } };
        }
    }
}
