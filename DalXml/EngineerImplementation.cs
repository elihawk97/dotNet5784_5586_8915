using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
namespace DalXml;

internal class EngineerImplementation : IEngineer
{
    readonly string s_engineers_xml = "engineers";

    public int Create(Engineer entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
