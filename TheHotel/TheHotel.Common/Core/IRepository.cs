using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Common.Core
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        List<T> GetAll();
        int Add(T entity);
        int Update(T entity);
        //void Delete(int id);
    }
}
