using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDISAngular.Abstract
{
    interface IRepository<T>
    {
        IQueryable<T> Get();
        T FindById(string id);
        void Remove(T item);
        void Remove(string id);
        void Save();
    }
}
