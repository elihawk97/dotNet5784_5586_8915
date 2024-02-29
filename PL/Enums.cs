
using System.Collections;
namespace PL;

/// <summary>
/// Represents a collection of experience levels.
/// </summary>
public class ExperienceLevelCollection : IEnumerable
{
    /// <summary>
    /// Static readonly collection of experience levels.
    /// </summary>
    static readonly IEnumerable<BO.Enums.ExperienceLevel> exp_enums =
    (Enum.GetValues(typeof(BO.Enums.ExperienceLevel)) as IEnumerable<BO.Enums.ExperienceLevel>)!;
    
    /// <summary>
    /// Returns an enumerator that iterates through the experience levels collection.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    public IEnumerator GetEnumerator() => exp_enums.GetEnumerator();

}