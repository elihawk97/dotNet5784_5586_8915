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
        public List<T> ReadAll(); 
        public void Update(T item); 
        public void Delete(int id); 
    }


