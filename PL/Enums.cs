
using System.Collections;
namespace PL; 

public class ExperienceLevelCollection : IEnumerable
{
    static readonly IEnumerable<BO.Enums.ExperienceLevel> exp_enums =
    (Enum.GetValues(typeof(BO.Enums.ExperienceLevel)) as IEnumerable<BO.Enums.ExperienceLevel>)!;
    public IEnumerator GetEnumerator() => exp_enums.GetEnumerator();

}