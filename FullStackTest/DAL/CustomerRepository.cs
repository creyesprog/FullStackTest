using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FullStackTest.Models;
using System.Data.Entity;

namespace FullStackTest.DAL
{
    public class CustomerRepository : IRepository<Customer>, IDisposable
    {
        private CustomerContext context;

        public CustomerRepository(CustomerContext context)
        {
            this.context = context;
        }

        public IEnumerable<Customer> GetList()
        {
            return context.Customers.ToList();
        }

        public Customer Find(int customerID)
        {
            Customer cust = context.Customers.Find(customerID);
            return cust;
        }

        public void Update(Customer customer)
        {
            context.Entry(customer).State = EntityState.Modified;
        }

        public void Delete(int customerID)
        {
            Customer cust = context.Customers.Find(customerID);
            context.Customers.Remove(cust);
        }

        public void Insert(Customer customer)
        {
            context.Customers.Add(customer);
        }

        public bool Save()
        {
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogError(ex.ToString());
                return false;
            }
        }

        #region Disposable
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}