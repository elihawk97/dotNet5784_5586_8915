using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BlApi
{
    public interface ITools
    {
        void SetProjectEndDate(DateTime? endDate);
        void SetProjectStartDate(DateTime? startDate);
        DateTime getProjectStartDate();
        DateTime getProjectEndDate();
    }
}
