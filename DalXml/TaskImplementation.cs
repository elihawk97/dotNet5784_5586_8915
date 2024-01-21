using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
namespace DalXml
{
    internal class TaskImplementation : ITask
    {
        readonly string s_tasks_xml = "tasks";

        public int Create(DO.Task entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DO.Task? Read(int id)
        {
            throw new NotImplementedException();
        }

        public DO.Task? Read(Func<DO.Task, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Update(DO.Task item)
        {
            throw new NotImplementedException();
        }
    }
}
