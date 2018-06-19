using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullStackTest.Models;

namespace FullStackTest.DAL
{
    //Implement repository pattern
    public interface IRepository<T>:IDisposable
    {
        IEnumerable<T> GetList();
        void Insert(T obj);
        void Delete(int ID);
        void Update(T obj);
        T Find(int ID);
        bool Save();
    }
}
