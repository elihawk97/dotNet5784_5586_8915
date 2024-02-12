using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;


internal class Config
{
    static string s_data_config_xml = "data-config";


    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static int NextEngineerId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextEngineerId"); }
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }

    internal static DateTime ProjectStartDate { get; set; } = DateTime.Now.AddDays(-(365 + 365));
    internal static DateTime ProjectEndDate { get; set; } = DateTime.Now.AddDays(365 + 365);


    internal static void SetProjectDates(DateTime startDate, DateTime endDate)
    {
        ProjectStartDate = startDate;
        ProjectEndDate = endDate;

        XMLTools.SetProjectDates(ProjectStartDate, "StartDate");
        XMLTools.SetProjectDates(ProjectEndDate, "EndDate");
    }
}
