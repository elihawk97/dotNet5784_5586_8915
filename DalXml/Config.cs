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


    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId", false); }
    internal static int NextEngineerId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextEngineerId", false); }
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId", false); }

    private static DateTime? _projectStartDate = null;
    internal static DateTime? ProjectStartDate
    {
        get
        {
            if (_projectStartDate == null)
            {
                _projectStartDate = XMLTools.GetProjectStartDate();
            }
            return _projectStartDate;
        }
        set
        {
            _projectStartDate = value;
            XMLTools.SetProjectDates(value, "StartDate"); // Update the XML upon setting the start date.
        }
    }

    private static DateTime? _projectEndDate = null;
    internal static DateTime? ProjectEndDate
    {
        get
        {
            if (_projectEndDate == null)
            {
                _projectEndDate = XMLTools.GetProjectEndDate();
            }
            return _projectEndDate;
        }
        set
        {
            _projectEndDate = value;
            XMLTools.SetProjectDates(value, "EndDate"); // Update the XML upon setting the end date.
        }
    }

}
