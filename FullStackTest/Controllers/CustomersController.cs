using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FullStackTest.DAL;
using FullStackTest.Models;
using PagedList;
using System.Net.Mail;

namespace FullStackTest.Controllers
{
    public class CustomersController : Controller
    {
        private IRepository<Customer> customerRepo;
        private bool isSuccess = false;

        public CustomersController()
        {
            this.customerRepo = new CustomerRepository(new CustomerContext());
        }

        public CustomersController(IRepository<Customer> custRepo)
        {
            this.customerRepo = custRepo;
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Email")] Customer customer)
        {
            try
            {
                MailAddress validateEmail = new MailAddress(customer.Email);
            }
            catch
            {
                return Json("Invalid email!", JsonRequestBehavior.AllowGet);
            }

            customerRepo.Insert(customer);
            isSuccess = customerRepo.Save();

            if (isSuccess == true)
            {
                return Json("Create success!", JsonRequestBehavior.AllowGet);
            }

            return Json("Create failed!", JsonRequestBehavior.AllowGet);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Email")] Customer customer)
        {
            customerRepo.Update(customer);
            isSuccess = customerRepo.Save();

            if (isSuccess == true)
            {
                return Json("Update success!", JsonRequestBehavior.AllowGet);
            }

            return Json("Update failed!", JsonRequestBehavior.AllowGet);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed([Bind(Include = "CustomerID")] Customer customer)
        {

            customerRepo.Delete(customer.CustomerID);
            isSuccess = customerRepo.Save();

            if (isSuccess == true)
            {
                return Json("Delete success!", JsonRequestBehavior.AllowGet);
            }

            return Json("Delete failed!", JsonRequestBehavior.AllowGet);
        }

        #region GET Methods
        // GET: Customers
        public ActionResult Index(int? page)
        {
            int pageSize = 50;
            int pageNumber = (page ?? 1);

            //TODO: provide a more efficient way of grabbing data
            var customers = from c in customerRepo.GetList() select c;

            return View(customers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            Customer customer = customerRepo.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            Customer customer = customerRepo.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            Customer customer = customerRepo.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return Json("Delete success!", JsonRequestBehavior.AllowGet);
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                customerRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
