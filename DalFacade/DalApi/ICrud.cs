using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace DalApi; 

    public interface ICrud<T> where T : class
    {
        public int Create(T entity); 
        public T? Read(int id);
        T? Read(Func<T, bool> filter); // stage 2

        public void Update(T item); 
        public void Activate(int id);
        public void Delete(int id);
        IEnumerable<T?> ReadAll(Func<T, bool>? filter = null);
        public void Reset();
}


