using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class of functions to use in the Bl 
/// </summary>
namespace BL.BlApi
{
    public interface ITools
    {
        void SetProjectEndDate(DateTime? endDate);
        void SetProjectStartDate(DateTime? startDate);
        DateTime? getProjectStartDate();
        DateTime? getProjectEndDate();

        // Refactored to use properties for ProjectStage
        BO.Enums.ProjectStages CurrentProjectStage { get; set; }

        // Refactored to use properties for CurrentUser
        BO.Enums.User CurrentUser { get; set; }
    }
}
