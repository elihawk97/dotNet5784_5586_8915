using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
namespace DalXml
{
    internal class DependencyImplementation : IDependency
    {
        readonly string s_dependencies_xml = "dependencies";

        public int Create(Dependency entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Dependency? Read(int id)
        {
            throw new NotImplementedException();
        }

        public Dependency? Read(Func<Dependency, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Update(Dependency item)
        {
            throw new NotImplementedException();
        }
    }
}
